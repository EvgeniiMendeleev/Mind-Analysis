using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace NeuroTGAM
{
    //TODO: Переписать парсинг JSON пакетов, поступающих с нейроинтрефейса на принимающее устройство.
    public class Neurointerface
    {
#region The initialize section
        private TcpClient _thinkgearConnector;
        private Thread _brainDataReadingThread;
        private Stream _streamToThinkgearConnector;
        private Mutex _mutex;

        public delegate void BrainDataHandler(BrainInfo brainInfo);
        public event BrainDataHandler OnBrainDataReceived;
        public BrainInfo CurrentBrainData { get; private set; }
        public bool IsDataReading 
        { 
            get { return _brainDataReadingThread.IsAlive; } 
        }
#endregion
#region The functions section
        public Neurointerface()
        {
            CurrentBrainData = new BrainInfo();

            _thinkgearConnector = new TcpClient();
            _mutex = new Mutex();
            _brainDataReadingThread = new Thread(BrainDataReadingThread);
            _brainDataReadingThread.IsBackground = true;
        }

        private void BrainDataReadingThread()
        {
            try
            {
                byte[] bytesFromConnector = new byte[2048];
                while (_thinkgearConnector.Connected)
                {
                    int bytesRead = _streamToThinkgearConnector.Read(bytesFromConnector, 0, 2048);
                    if (bytesRead <= 0) continue;

                    string[] packets = Encoding.UTF8.GetString(bytesFromConnector, 0, bytesRead).Split('\r');
                    foreach (string packet in packets)
                    {
                        if (!string.IsNullOrEmpty(packet))
                        {
                            ParseJSON(packet.Trim());
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void ParseJSON(string packet)
        {
            Console.WriteLine(packet);
            dynamic jsonObject = JObject.Parse(packet);
            if (jsonObject.eegPower == null && jsonObject.eSense == null || jsonObject.status != null)
            {
                return;
            }

            _mutex.WaitOne();
            CurrentBrainData.AlphaLow = (uint)jsonObject.eegPower["lowAlpha"];
            CurrentBrainData.AlphaHigh = (uint)jsonObject.eegPower["highAlpha"];

            CurrentBrainData.BetaLow = (uint)jsonObject.eegPower["lowBeta"];
            CurrentBrainData.BetaHigh = (uint)jsonObject.eegPower["highBeta"];

            CurrentBrainData.GammaLow = (uint)jsonObject.eegPower["lowGamma"];
            CurrentBrainData.GammaHigh = (uint)jsonObject.eegPower["highGamma"];

            CurrentBrainData.Theta = (uint)jsonObject.eegPower["theta"];
            CurrentBrainData.Delta = (uint)jsonObject.eegPower["delta"];

            CurrentBrainData.Attention = (uint)jsonObject.eSense["attention"];
            CurrentBrainData.Meditation = (uint)jsonObject.eSense["meditation"];
            _mutex.ReleaseMutex();

            OnBrainDataReceived?.Invoke(CurrentBrainData.Clone() as BrainInfo);
        }

        public void Connect()
        {
            _thinkgearConnector.Connect("localhost", 13854);

            _streamToThinkgearConnector = _thinkgearConnector.GetStream();
            InitializeСonnectorStartSettings();

            _brainDataReadingThread.Start();
        }

        private void InitializeСonnectorStartSettings()
        {
            byte[] startSettingsForConnector = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": false,""format"": ""Json""}");
            _streamToThinkgearConnector.Write(startSettingsForConnector, 0, startSettingsForConnector.Length);
        }

        public void CloseConnection()
        {
            _thinkgearConnector.Close();
            _thinkgearConnector = new TcpClient();
        }
#endregion
    }
}