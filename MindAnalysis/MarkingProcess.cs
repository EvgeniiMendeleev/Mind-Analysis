using CsvHelper.Configuration;
using CsvHelper;
using MindAnalysis.GUI;
using NeuroTGAM;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MindAnalysis
{
    public partial class MarkingProcess : Form
    {
        private int _window;
        private List<BrainInfo> _brainRecords;
        private List<uint> _recordsOnChart = new List<uint>();
        private WaveChart _wave;
        private string _markingFilePath;

        public MarkingProcess(string markingFilePath, WaveChart wave, int window)
        {
            InitializeComponent();

            _markingFilePath = markingFilePath;
            _window = window;
            _wave = wave;

            LoadBrainInfoToStorage();
            LoadMarkingDataOnChart();
        }

        private void LoadBrainInfoToStorage()
        {
            string fileExtension = Path.GetExtension(_markingFilePath);
            if (fileExtension != ".csv")
            {
                throw new Exception("WavesCharts.LoadFileToCharts(): Error extension file! CSV extension is need.");
            }

            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
            using (CsvReader csvReader = new CsvReader(File.OpenText(_markingFilePath), csvConfig))
            {
                _brainRecords = csvReader.GetRecords<BrainInfo>().ToList();
            }
        }

        private void EnterState(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\x0D') return;

            StreamWriter writer = new StreamWriter($"MarkedFiles/Marked_{Path.GetFileName(_markingFilePath)}", true);
            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
            using (CsvWriter csvWriter = new CsvWriter(writer, csvConfig))
            {
                foreach (uint value in _recordsOnChart.ToArray())
                {
                    csvWriter.WriteField(value);
                }
                csvWriter.WriteField(stateTextBox.Text);
                csvWriter.NextRecord();
            }
            _recordsOnChart.Clear();

            stateTextBox.Clear();
            _markingChart.Series[0].Points.Clear();
            LoadMarkingDataOnChart();
        }

        private void LoadMarkingDataOnChart()
        {
            if (_window > _brainRecords.Count)
            {
                Close();
            }

            for (int i = 0; i < _window; i++)
            {
                var brainRecord = _brainRecords[i];
                _recordsOnChart.Add(GetBrainData(brainRecord, _wave));
                _markingChart.Series[0].Points.AddXY(brainRecord.Second.ToString(), GetBrainData(brainRecord, _wave));
            }
            _brainRecords.RemoveAt(0);
        }

        private uint GetBrainData(BrainInfo brainInfo, WaveChart wave)
        {
            switch (wave)
            {
                case WaveChart.Attention:
                    return brainInfo.Attention;
                case WaveChart.Meditation: 
                    return brainInfo.Meditation;
                default:
                    return 0;
            }
        }

        public class MarkedRow
        {
            public uint[] Data { get; set; }
            public string State { get; set; }
        }
    }
}
