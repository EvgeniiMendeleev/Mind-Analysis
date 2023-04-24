using NeuroTGAM;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System;

namespace NeuroTGAM
{
    /// <summary>
    /// Название данных, которые можно снять с нейрогарнитуры, использующую чип TGAM.
    /// </summary>
    public enum NeuroData
    {
        Meditation,
        Attention,
        Low_Alpha,
        High_Alpha,
        Low_Beta,
        High_Beta,
        Low_Gamma,
        High_Gamma,
        Theta,
        Delta
        //Blink_Strength, //ALPHA.
        //MentalEffort,   //ALPHA.
        //Familiarity     //ALPHA.
    }

    /// <summary>
    /// Класс, способный работать с нейрогарнитурой, использующий чип TGAM
    /// </summary>
    public class Neurointerface
    {
        private TcpClient _connector;                                        //TCP соединение с ThinkGear Connector.
        private Thread _readingThread;                                       //Поток, считывающий данные, получаемые от ThinkGear Connector.
        private Stream _connectorStream;
        private Mutex _mutex;                                                //Для синхронизации потоков.
        private BrainInfo _currentBrainData;                                 //Данные с нейрогарнитуры в текущую секунду.

        /// <summary>
        /// Статус подключения к ThinkGear Connector.
        /// </summary>
        public bool IsDataReading 
        { 
            get { return _connector.Connected; } 
        }

        public Neurointerface()
        {
            _mutex = new Mutex();
            _currentBrainData = new BrainInfo();
        }

        /// <summary>
        /// Установить соединение с ThinkGear Connector.
        /// </summary>
        public void Connect()
        {
            if (_connector == null) _connector = new TcpClient();

            _connector.Connect("localhost", 13854);
            _connectorStream = _connector.GetStream();

            byte[] settingsForConnector = Encoding.ASCII.GetBytes(@"{""enableRawOutput"": false,""format"": ""Json""}");
            _connectorStream.Write(settingsForConnector, 0, settingsForConnector.Length);

            if (_readingThread == null)
            {
                _readingThread = new Thread(ReadingThread);
                _readingThread.IsBackground = true;
            }
            _readingThread.Start();
        }

        /// <summary>
        /// Разорвать соединение с ThinkGear Connector.
        /// </summary>
        public void CloseConnection()
        {
            _connector.Close();
            _connector = null;
            _readingThread = null;
        }

        /// <summary>
        /// Возвращает данные о мозговой волне/состоянии в текущую секунду.
        /// </summary>
        /// <param name="brainDataTitle">Название мозговой волны/состояния.</param>
        /// <returns>Число в диапазоне, заданном протоколом NeuroSky.</returns>
        public uint GetBrainDataAbout(NeuroData nueroDataType)
        {
            uint result = 0;
            _mutex.WaitOne();
            switch (nueroDataType)
            {
                case NeuroData.Attention:
                    result = _currentBrainData.Attention;
                    break;
                case NeuroData.Meditation:
                    result = _currentBrainData.Meditation;
                    break;
            }
            _mutex.ReleaseMutex();
            return result;
        }

        /// <summary>
        /// Делегат, позволяющий работать со всеми данными нейрогарнитуры, считанные на текущий момент.
        /// </summary>
        /// <param name="brainDatas">Словарь со всеми данными нейрогарнитуры на текущую секунду.</param>
        public delegate void BrainDataHandler(BrainInfo brainInfo);
        /// <summary>
        /// Событие, позволяющее отображать все данные с нейрогарнитуры на экран.
        /// </summary>
        public event BrainDataHandler OnBrainInfoReceived;

        //Поток, считывающий информацию с нейрогарнитуры.
        private void ReadingThread()
        {
            try
            {
                byte[] bytesFromConnector = new byte[2048];
                while (_connector.Connected)
                {
                    int bytesRead = _connectorStream.Read(bytesFromConnector, 0, 2048);
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
            _currentBrainData.AlphaLow = (uint)jsonObject.eegPower["lowAlpha"];
            _currentBrainData.AlphaHigh = (uint)jsonObject.eegPower["highAlpha"];

            _currentBrainData.BetaLow = (uint)jsonObject.eegPower["lowBeta"];
            _currentBrainData.BetaHigh = (uint)jsonObject.eegPower["highBeta"];

            _currentBrainData.GammaLow = (uint)jsonObject.eegPower["lowGamma"];
            _currentBrainData.GammaHigh= (uint)jsonObject.eegPower["highGamma"];

            _currentBrainData.Theta = (uint)jsonObject.eegPower["theta"];
            _currentBrainData.Delta = (uint)jsonObject.eegPower["delta"];

            _currentBrainData.Attention = (uint)jsonObject.eSense["attention"];
            _currentBrainData.Meditation = (uint)jsonObject.eSense["meditation"];
            _mutex.ReleaseMutex();

            OnBrainInfoReceived?.Invoke(_currentBrainData.Clone() as BrainInfo);
        }
    }
}