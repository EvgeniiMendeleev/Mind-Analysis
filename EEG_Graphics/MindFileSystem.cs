using System;
using System.Collections.Generic;
using System.IO;
using NeuroTGAM;
using CsvHelper;

namespace MindFileSystem
{
    public class MindFileReader
    {
        private StreamReader _reader = null;

        public string FileName { get; private set; }

        public MindFileReader(string filePath)
        {
            Close();
            FileName = Path.GetFileNameWithoutExtension(filePath);
            _reader = new StreamReader(filePath);
        }

        public void Close()
        {
            if (_reader == null) return;
            _reader.Close();
            _reader.Dispose();
        }

        public IEnumerator<BrainInfo> GetEnumerator()
        {
            while (!_reader.EndOfStream)
            {
                string[] brainDatasAndTime = _reader.ReadLine().Split(':');
                uint time = Convert.ToUInt32(brainDatasAndTime[1]);
                string[] brainDatas = brainDatasAndTime[0].Split(',');

                foreach (string brainDataFromFile in brainDatas)
                {
                    string[] brainDataAndTitle = brainDataFromFile.Split('=');
                    Enum.TryParse(brainDataAndTitle[0], out EEG_Title brainValueTitle);
                    double brainValue = Convert.ToInt32(brainDataAndTitle[1]);
                    yield return new BrainData(time, brainValueTitle, brainValue);
                }
            }
        }
    }
}
