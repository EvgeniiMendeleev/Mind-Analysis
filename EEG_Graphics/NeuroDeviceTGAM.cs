using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace NeuroTGAM
{
    /// <summary>
    /// Название данных, которые можно снять с нейрогарнитуры, использующую чип TGAM.
    /// </summary>
    public enum BrainDataTitle 
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
        private Thread _readingThread;                                       //Поток, считывающй данные, получаемые от ThinkGear Connector.
        private Stream _connectorStream;
        private Mutex _mutex;                                                //Для синхронизации потоков.
        private Dictionary<BrainDataTitle, double> _currentBrainData;        //Данные с нейрогарнитуры в текущую секунду.

        /// <summary>
        /// Статус подключения к ThinkGear Connector.
        /// </summary>
        public bool AreDataReading { get { return _connector.Connected; } }

        public NeuroDeviceTGAM()
        {
            _mutex = new Mutex();

            BrainDataTitle[] brainDataTitles = new BrainDataTitle[] 
            { 
                BrainDataTitle.Attention, BrainDataTitle.Meditation,  BrainDataTitle.Low_Alpha, BrainDataTitle.High_Alpha, BrainDataTitle.Low_Beta, 
                BrainDataTitle.High_Beta, BrainDataTitle.Low_Gamma, BrainDataTitle.High_Gamma, BrainDataTitle.Theta, BrainDataTitle.Delta
            };
            _currentBrainData = new Dictionary<BrainDataTitle, double>();
            foreach (BrainDataTitle title in brainDataTitles)
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
        public double GetBrainDataAbout(BrainDataTitle brainDataTitle)
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
        public delegate void BrainDataHandler(Dictionary<BrainDataTitle, double> brainDatas);
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
                while (_connector.Connected)
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
            //TODO: Попробовать найти другой способ парсинга json пакетов
            dynamic jsonObject = JObject.Parse(packet);
            if (jsonObject.eegPower == null && jsonObject.eSense == null || jsonObject.status != null) return;

            _mutex.WaitOne();
            _currentBrainData[BrainDataTitle.Low_Alpha] = (double)jsonObject.eegPower["lowAlpha"];
            _currentBrainData[BrainDataTitle.High_Alpha] = (double)jsonObject.eegPower["highAlpha"];

            _currentBrainData[BrainDataTitle.Low_Beta] = (double)jsonObject.eegPower["lowBeta"];
            _currentBrainData[BrainDataTitle.High_Beta] = (double)jsonObject.eegPower["highBeta"];

            _currentBrainData[BrainDataTitle.Low_Gamma] = (double)jsonObject.eegPower["lowGamma"];
            _currentBrainData[BrainDataTitle.High_Gamma] = (double)jsonObject.eegPower["highGamma"];

            _currentBrainData[BrainDataTitle.Theta] = (double)jsonObject.eegPower["theta"];
            _currentBrainData[BrainDataTitle.Delta] = (double)jsonObject.eegPower["delta"];

            _currentBrainData[BrainDataTitle.Attention] = (double)jsonObject.eSense["attention"];
            _currentBrainData[BrainDataTitle.Meditation] = (double)jsonObject.eSense["meditation"];
            _mutex.ReleaseMutex();

            ShowBrainData?.Invoke(new Dictionary<BrainDataTitle, double>(_currentBrainData));
        }
    }
}
