using System;
using System.Collections.Generic;
using System.IO;
using NeuroTGAM;

namespace MindFileSystem
{
    //Пометка: Будет занимать память в стеке.
    public struct BrainData
    {
        public BrainDataTitle _title;
        public uint _time;
        public double _brainValue;

        public BrainData(uint time, BrainDataTitle title, double brainValue)
        {
            _time = time;
            _brainValue = brainValue;
            _title = title;
        }
    }

    public class MindFileReader
    {
        private StreamReader _reader = null;

        public MindFileReader(string filePath)
        {
            Close();
            _reader = new StreamReader(filePath);
        }

        public void Close()
        {
            if (_reader == null) return;
            _reader.Close();
            _reader.Dispose();
        }

        public IEnumerator<BrainData> GetEnumerator()
        {
            while (!_reader.EndOfStream)
            {
                string[] brainDatasAndTime = _reader.ReadLine().Split(':');
                uint time = Convert.ToUInt32(brainDatasAndTime[1]);
                string[] brainDatas = brainDatasAndTime[0].Split(',');

                foreach (string brainDataFromFile in brainDatas)
                {
                    string[] brainDataAndTitle = brainDataFromFile.Split('=');
                    Enum.TryParse(brainDataAndTitle[0], out BrainDataTitle brainValueTitle);
                    double brainValue = Convert.ToInt32(brainDataAndTitle[1]);
                    yield return new BrainData(time, brainValueTitle, brainValue);
                }
            }
        }
    }
}
