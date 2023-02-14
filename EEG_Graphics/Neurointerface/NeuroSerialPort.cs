using NeuroTGAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using System.Drawing;
using System.Globalization;

namespace EEG_Graphics
{
    public class NeuroSerialPort
    {
        /// <summary>
        /// Делегат, позволяющий работать со всеми данными нейрогарнитуры, считанные на текущий момент.
        /// </summary>
        /// <param name="currentBrainInfo">Буфер со всеми данными нейрогарнитуры на текущую секунду.</param>
        public delegate void BrainDataHandler(BrainInfo currentBrainInfo);
        /// <summary>
        /// Событие, позволяющее отображать все данные с нейрогарнитуры на экран.
        /// </summary>
        public event BrainDataHandler OnBrainInfoReceived;

        const char EXCODE = '\x55', SYNC = '\xAA', ASIC_EEG_POWER_INT = '\x83', POOR_SIGNAL = '\x02', ATTENTION = '\x04', MEDITATION = '\x05';

        /// <summary>
        /// Делегат, позволяющий работать со всеми данными нейрогарнитуры, считанные на текущий момент.
        /// </summary>
        /// <param name="currentBrainInfo">Буфер со всеми данными нейрогарнитуры на текущую секунду.</param>
        public delegate void BrainDataHandler(BrainInfo currentBrainInfo);
        /// <summary>
        /// Событие, позволяющее отображать все данные с нейрогарнитуры на экран.
        /// </summary>
        public BrainDataHandler OnBrainInfoReceived;

        private readonly Mutex _mutex;
        private SerialPort _neuroPort;
        private SerialPort _spiderPort;
        private List<byte> _spiderData;

        public bool AreDataReading { get { return _neuroPort.IsOpen; } }

        public BrainInfo CurrentBrainInfo { get; private set; }

        public NeuroSerialPort()
        {
            _mutex = new Mutex();
            _neuroPort = new SerialPort();
            _spiderPort = new SerialPort();
            CurrentBrainInfo = new BrainInfo();
            _spiderData = new List<byte>();

            _neuroPort.BaudRate = 57600;
            _neuroPort.Parity = Parity.None;
            _neuroPort.DataBits = 8;
            _neuroPort.StopBits = StopBits.One;
            _neuroPort.ReadTimeout = 12000;
        }

        public  void Connect(string neuroPort)
        {
            _neuroPort.PortName = neuroPort;
            _neuroPort.Open();

            Thread _readingThread = new Thread(ReadingThread);
            _readingThread.IsBackground = true;
            _readingThread.Start();
        }

        public void ConnectToSpider(string spiderPort)
        {
            _spiderPort.PortName = spiderPort;
            _spiderPort.Open();
        }

        public  void CloseConnection()
        {
            _neuroPort.Close();
            if(_spiderPort.IsOpen) _spiderPort.Close();
        }

        public BrainInfo GetCurrentBrainData()
        {
            _mutex.WaitOne();
            BrainInfo brainInfo = CurrentBrainInfo;
            _mutex.ReleaseMutex();
            return brainInfo;
        }

        protected void ReadingThread()
        {
            try
            {
                while (AreDataReading)
                {
                    _spiderData.Clear();
                    int pLength = ReadPayloadFromPort(out var infoFromPort);
                    if (pLength > 4)
                    {
                        ParsingPayload(infoFromPort);
                        OnBrainInfoReceived?.Invoke(CurrentBrainInfo);
                    }
                    if (_spiderPort.IsOpen) _spiderPort.Write(_spiderData.ToArray(), 0, _spiderData.Count);
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine($"[Reading thread]: {exp.Message}");
            }
        }

        private void ParsingPayload(List<int> payload)
        {
            int bytesParsed = 0;
            while (bytesParsed < payload.Count)
            {
                while (payload[bytesParsed] == EXCODE) bytesParsed++;

                int code = payload[bytesParsed++];
                int vLength = Convert.ToBoolean(code & '\x80') ? payload[bytesParsed++] : 1;

                _mutex.WaitOne();
                if (code == ASIC_EEG_POWER_INT) SaveBrainDataAboutASIG(payload, bytesParsed);
                else 
                {
                    if (code == MEDITATION) CurrentBrainInfo.Meditation = Convert.ToUInt32(payload[bytesParsed]);
                    else if (code == ATTENTION) CurrentBrainInfo.Attention = Convert.ToUInt32(payload[bytesParsed]);
                }
                _mutex.ReleaseMutex();

                bytesParsed += vLength;
            }
        }

        private void SaveBrainDataAboutASIG(in List<int> payload, int bytesParsed)
        {
            CurrentBrainInfo.Delta = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.Theta = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.AlphaLow = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.AlphaHigh = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.BetaLow = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.BetaHigh = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.GammaLow = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.GammaHigh = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
        }
        private int ReadPayloadFromPort(out List<int> infoFromNeuro)
        {
            WaitOnPortTwoSync();
            int pLength = ReadPayloadLength();
            if (pLength > SYNC)
            {
                infoFromNeuro = null;
                return 0;
            }
            infoFromNeuro = ReadPayload(pLength);
            int calculatedCheckSum = ~(infoFromNeuro.Sum() & 0xFF) & 0xFF;
            int readedCheckSum = _neuroPort.ReadByte();
            _spiderData.Add(Convert.ToByte(readedCheckSum));

            if (readedCheckSum != calculatedCheckSum)
            {
                infoFromNeuro = null;
                return 0;
            }

            return pLength;
        }

        private void WaitOnPortTwoSync()
        {
            int firstByte = _neuroPort.ReadByte();
            int secondByte = _neuroPort.ReadByte();

            while (firstByte != SYNC || secondByte != SYNC)
            {
                firstByte = secondByte;
                secondByte = _neuroPort.ReadByte();
            }

            _spiderData.Add(Convert.ToByte(firstByte));
            _spiderData.Add(Convert.ToByte(secondByte));
        }

        private int ReadPayloadLength()
        {
            int pLength = SYNC;
            do
            {
                pLength = _neuroPort.ReadByte();
            } while (pLength == SYNC);

            _spiderData.Add(Convert.ToByte(pLength));

            return pLength;
        }

        private List<int> ReadPayload(in int pLength)
        {
            List<int> readedPayload = new List<int>();
            for (int i = 0; i < pLength; i++)
            {
                int dataFromPort = _neuroPort.ReadByte();
                _spiderData.Add(Convert.ToByte(dataFromPort));
                readedPayload.Add(dataFromPort);
            }
            return readedPayload;
        }
    }
}
