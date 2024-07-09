using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NeuroTGAM
{
    public class Neurointerface
    {
        private TcpClient _client;
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

        public Neurointerface()
        {
            CurrentBrainData = new BrainInfo();
            _client = new TcpClient();
            _mutex = new Mutex();
            _brainDataReadingThread = new Thread(BrainDataReadingThread);

            _brainDataReadingThread.IsBackground = true;
        }

        private void BrainDataReadingThread()
        {
            try
            {
                byte[] bytesFromConnector = new byte[2048];
                while (_client.Connected)
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

        //TODO: Переписать парсинг JSON пакетов, поступающих с нейроинтрефейса в программу.
        private void ParseJSON(string packet)
        {
            try
            {
                Console.WriteLine(packet);
                dynamic jsonObject = JObject.Parse(packet);
                if (jsonObject.eegPower == null && jsonObject.eSense == null || jsonObject.status != null)
                {
                    return;
                }

                _mutex.WaitOne();
                CurrentBrainData.LowAlpha = (uint)jsonObject.eegPower["lowAlpha"];
                CurrentBrainData.HighAlpha = (uint)jsonObject.eegPower["highAlpha"];

                CurrentBrainData.LowBeta = (uint)jsonObject.eegPower["lowBeta"];
                CurrentBrainData.HighBeta = (uint)jsonObject.eegPower["highBeta"];

                CurrentBrainData.LowGamma = (uint)jsonObject.eegPower["lowGamma"];
                CurrentBrainData.HighGamma = (uint)jsonObject.eegPower["highGamma"];

                CurrentBrainData.Theta = (uint)jsonObject.eegPower["theta"];
                CurrentBrainData.Delta = (uint)jsonObject.eegPower["delta"];

                CurrentBrainData.Attention = (uint)jsonObject.eSense["attention"];
                CurrentBrainData.Meditation = (uint)jsonObject.eSense["meditation"];
                _mutex.ReleaseMutex();

                OnBrainDataReceived?.Invoke(CurrentBrainData.Clone() as BrainInfo);
            }
            catch (JsonReaderException)
            {
                return;
            }
        }

        public void Connect()
        {
            _client.Connect("localhost", 13854);

            _streamToThinkgearConnector = _client.GetStream();
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
            _client.Close();
            _brainDataReadingThread.Abort();
        }
    }
}