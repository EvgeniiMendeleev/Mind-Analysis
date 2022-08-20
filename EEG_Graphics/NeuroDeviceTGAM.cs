using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace NeuroTGAM
{
    /// <summary>
    /// Название данных, которые можно снять с нейрогарнитуры, использующую чип TGAM.
    /// </summary>
    public enum EEG_Title 
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
    public class NeuroDeviceTGAM
    {
        private TcpClient _connector;                                        //TCP соединение с ThinkGear Connector.
        private Thread _readingThread;                                       //Поток, считывающий данные, получаемые от ThinkGear Connector.
        private Stream _connectorStream;
        private Mutex _mutex;                                                //Для синхронизации потоков.
        private Dictionary<EEG_Title, double> _currentBrainData;        //Данные с нейрогарнитуры в текущую секунду.

        /// <summary>
        /// Статус подключения к ThinkGear Connector.
        /// </summary>
        public bool AreDataReading { get { return _connector.Connected; } }

        public NeuroDeviceTGAM()
        {
            _mutex = new Mutex();

            EEG_Title[] brainDataTitles = new EEG_Title[] 
            { 
                EEG_Title.Attention, EEG_Title.Meditation,  EEG_Title.Low_Alpha, EEG_Title.High_Alpha, EEG_Title.Low_Beta, 
                EEG_Title.High_Beta, EEG_Title.Low_Gamma, EEG_Title.High_Gamma, EEG_Title.Theta, EEG_Title.Delta
            };
            _currentBrainData = new Dictionary<EEG_Title, double>();
            foreach (EEG_Title title in brainDataTitles)
            {
                _currentBrainData.Add(title, 0);
            }
        }

        /// <summary>
        /// Установить соединение с ThinkGear Connector.
        /// </summary>
        public void ConnectToConnector()
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
        public void DisconnectFromConnector()
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
        public double GetBrainDataAbout(EEG_Title brainDataTitle)
        {
            _mutex.WaitOne();
            double brainData = _currentBrainData[brainDataTitle];
            _mutex.ReleaseMutex();
            return brainData;
        }

        /// <summary>
        /// Делегат, позволяющий работать со всеми данными нейрогарнитуры, считанные на текущий момент.
        /// </summary>
        /// <param name="brainDatas">Словарь со всеми данными нейрогарнитуры на текущую секунду.</param>
        public delegate void BrainDataHandler(Dictionary<EEG_Title, double> brainDatas);
        /// <summary>
        /// Событие, позволяющее отображать все данные с нейрогарнитуры на экран.
        /// </summary>
        public event BrainDataHandler ShowBrainData;

        //Поток, считывающий информацию с нейрогарнитуры.
        private void ReadingThread()
        {
            try
            {
                byte[] bytesFromConnector = new byte[2048];
                while (_connector != null && _connector.Connected)
                {
                    int bytesRead = _connectorStream.Read(bytesFromConnector, 0, 2048);
                    if (bytesRead <= 0) continue;

                    string[] packets = Encoding.UTF8.GetString(bytesFromConnector, 0, bytesRead).Split('\r');
                    foreach (string packet in packets) if (!string.IsNullOrEmpty(packet)) ParseJSON(packet.Trim());
                }
            }
            catch
            {
                return;
            }
        }

        private void ParseJSON(string packet)
        {
            //TODO: Желательно попробовать найти другой способ парсинга json пакетов
            dynamic jsonObject = JObject.Parse(packet);
            if (jsonObject.eegPower == null && jsonObject.eSense == null || jsonObject.status != null) return;

            _mutex.WaitOne();
            _currentBrainData[EEG_Title.Low_Alpha] = (double)jsonObject.eegPower["lowAlpha"];
            _currentBrainData[EEG_Title.High_Alpha] = (double)jsonObject.eegPower["highAlpha"];

            _currentBrainData[EEG_Title.Low_Beta] = (double)jsonObject.eegPower["lowBeta"];
            _currentBrainData[EEG_Title.High_Beta] = (double)jsonObject.eegPower["highBeta"];

            _currentBrainData[EEG_Title.Low_Gamma] = (double)jsonObject.eegPower["lowGamma"];
            _currentBrainData[EEG_Title.High_Gamma] = (double)jsonObject.eegPower["highGamma"];

            _currentBrainData[EEG_Title.Theta] = (double)jsonObject.eegPower["theta"];
            _currentBrainData[EEG_Title.Delta] = (double)jsonObject.eegPower["delta"];

            _currentBrainData[EEG_Title.Attention] = (double)jsonObject.eSense["attention"];
            _currentBrainData[EEG_Title.Meditation] = (double)jsonObject.eSense["meditation"];
            _mutex.ReleaseMutex();

            ShowBrainData?.Invoke(new Dictionary<EEG_Title, double>(_currentBrainData));
        }
    }
}
