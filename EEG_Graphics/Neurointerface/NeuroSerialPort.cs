using NeuroTGAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Thread _readingThread;
        private readonly Mutex _mutex;
        private SerialPort _neuroPort;
        private SerialPort _spiderPort;
        private List<byte> _spiderData;

        public BrainInfo CurrentBrainInfo { get; private set; }

        public NeuroSerialPort(string neuroCOM, string spiderCOM)
        {
            _readingThread = new Thread(ReadingThread);
            _readingThread.IsBackground = true;
            _mutex = new Mutex();
            _neuroPort = new SerialPort();
            _spiderPort = new SerialPort();
            _spiderData = new List<byte>();

            _neuroPort.PortName = neuroCOM;
            _neuroPort.BaudRate = 57600;
            _neuroPort.Parity = Parity.None;
            _neuroPort.DataBits = 8;
            _neuroPort.StopBits = StopBits.One;
            _neuroPort.ReadTimeout = 12000;

            _spiderPort.PortName = spiderCOM;

            CurrentBrainInfo = new BrainInfo();
        }

        public  void Connect()
        {
            if (!AreDataReading)
            {
                _neuroPort.Open();
                _readingThread.Start();
            }
        }

        public void ConnectToSpider()
        {
            if(!_spiderPort.IsOpen) _spiderPort.Open();
            Console.WriteLine("Подключился!");
        }

        public  bool AreDataReading { get { return _neuroPort.IsOpen; } }

        public  void CloseConnection()
        {
            if (AreDataReading)
            {
                _neuroPort.Close();
                _spiderPort.Close();
            }
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

                    WaitOnPortTwoSync();
                    int pLength = ReadPayloadLength();
                    if (pLength > SYNC) return;
                    var payload = ReadPayload(pLength);
                    int readedCheckSum = _neuroPort.ReadByte();
                    _spiderData.Add(Convert.ToByte(readedCheckSum));
                    if (readedCheckSum != payload.calculatedChecksum) return;

                    ParsingPayload(payload.payload);
                    _spiderPort.Write(_spiderData.ToArray(), 0, _spiderData.Count);
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
                    if (code == MEDITATION) CurrentBrainInfo.meditation = Convert.ToUInt32(payload[bytesParsed]);
                    else if (code == ATTENTION) CurrentBrainInfo.attention = Convert.ToUInt32(payload[bytesParsed]);
                }
                _mutex.ReleaseMutex();

                bytesParsed += vLength;
            }
        }

        private void SaveBrainDataAboutASIG(in List<int> payload, int bytesParsed)
        {
            CurrentBrainInfo.delta = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.theta = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.alphaLow = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.alphaHigh = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.betaLow = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.betaHigh = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.gammaLow = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
            bytesParsed += 3;
            CurrentBrainInfo.gammaHigh = Convert.ToUInt32(payload[bytesParsed] * 65536 + payload[bytesParsed + 1] * 256 + payload[bytesParsed + 2]);
        }
        private (bool isReadPayload, List<int> payload) ReadPayloadFromPort()
        {
            WaitOnPortTwoSync();
            int pLength = ReadPayloadLength();
            if (pLength > SYNC) return (false, null);
            var payload = ReadPayload(pLength);
            int readedCheckSum = _neuroPort.ReadByte();
            _spiderData.Add(Convert.ToByte(readedCheckSum));
            if (readedCheckSum != payload.calculatedChecksum) return (false, null);

            return (true, payload.payload);
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

        private (int calculatedChecksum, List<int> payload) ReadPayload(in int pLength)
        {
            var funcResult = (calculatedChecksum: 0, payload: new List<int>());

            for (int i = 0; i < pLength; i++)
            {
                int dataFromPort = _neuroPort.ReadByte();

                _spiderData.Add(Convert.ToByte(dataFromPort));

                funcResult.payload.Add(dataFromPort);
                funcResult.calculatedChecksum += dataFromPort;
            }

            funcResult.calculatedChecksum &= 0xFF;
            funcResult.calculatedChecksum = ~funcResult.calculatedChecksum & 0xFF;

            return funcResult;
        }
    }
}
