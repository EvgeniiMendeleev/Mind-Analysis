using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using NeuroTGAM;
using MindFileSystem;

namespace EEG_Graphics
{
    public partial class Form1 : Form
    {
        private delegate void ChartDisplayHandler(EEG_Title chartName, int seriesNumber, DataPoint point);

        private uint _seconds = 0;
        private NeuroDeviceTGAM _neurodevice;
        private BrainCharts _brainCharts;

        public Form1()
        {
            InitializeComponent();

            _neurodevice = new NeuroDeviceTGAM();
            _brainCharts = new BrainCharts(new Dictionary<EEG_Title, Chart>()
            {
                [EEG_Title.Low_Alpha] = graphicLowAlpha,
                [EEG_Title.High_Alpha] = graphicHighAlpha,
                [EEG_Title.Low_Beta] = graphicLowBeta,
                [EEG_Title.High_Beta] = graphicHighBeta,
                [EEG_Title.Low_Gamma] = graphicLowGamma,
                [EEG_Title.High_Gamma] = graphicHighGamma,
                [EEG_Title.Theta] = graphicTheta,
                [EEG_Title.Delta] = graphicDelta,
                [EEG_Title.Attention] = graphicAttention,
                [EEG_Title.Meditation] = graphicMeditation
            });

            //TODO: Попробовать реализовать отображение данных не при помощи вызова делегата.
            _neurodevice.ShowBrainData += DisplayDataToGraphics;
            UserControlSystem.GetSystem().Enable(btnStartRecord).Disable(btnStopRecord).Enable(groupRecordSettings);
            attentionDistributionChart.ChartAreas[0].AxisX.Maximum = 100;
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            try
            {
                _neurodevice.ConnectToConnector();
                _brainCharts.ClearSerieOnCharts(0);
            }
            finally
            {
                UserControlSystem.GetSystem().Disable(btnStartRecord).Enable(btnStopRecord).Disable(btnLoadFirstFile)
                    .Disable(groupRecordSettings).Disable(btnLoadSecondFile);
            }
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (!_neurodevice.AreDataReading)
            {
                MessageBox.Show("Соединение с ThinkGear Connector не установлено.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _neurodevice.DisconnectFromConnector();
            _seconds = 0;
            UserControlSystem.GetSystem().Disable(btnStopRecord).Enable(btnStartRecord).Enable(btnLoadFirstFile).Enable(groupRecordSettings);
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;
        private void OnChangedValueInMaxGraphPoints(object sender, EventArgs e) => numMaxChartPoints.Enabled = !numMaxChartPoints.Enabled;
        private void UploadFirstBrainDataFile(object sender, EventArgs e) => DisplayBrainCharts(1);
        private void UploadSecondBrainDataFile(object sender, EventArgs e) => DisplayBrainCharts(2);

        void DisplayBrainCharts(int seriesNumber)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Mind Files (*.mind) | *.mind";
            if (OPF.ShowDialog() != DialogResult.OK) return;

            _brainCharts.ClearSerieOnCharts(seriesNumber);
            MindFileReader mindFile = new MindFileReader(OPF.FileName);
            _brainCharts.DisplayMindFileOnCharts(mindFile, seriesNumber);

            OPF.Dispose();
            mindFile.Close();

            UserControlSystem.GetSystem().Enable(btnClearAllCharts);
        }

        private void ClearAllGraphics(object sender, EventArgs e)
        {
            _brainCharts.ClearAllSeries();
            UserControlSystem.GetSystem().Disable(btnClearAllCharts);
        }

        private void DisplayDataToGraphics(Dictionary<EEG_Title, double> currentBrainData)
        {
            var brainKeys = currentBrainData.Keys;
            foreach (EEG_Title brainKey in brainKeys)
            {
                if (numMaxChartPoints.Value < _brainCharts[brainKey].Series[0].Points.Count) _brainCharts[brainKey].Series[0].Points.RemoveAt(0);
                BeginInvoke(new ChartDisplayHandler(_brainCharts.AddPoint), new object[] { brainKey, 0, new DataPoint(_seconds, currentBrainData[brainKey]) });
            }
            _seconds++;
            if (chkSaveRecordData.Checked) using (StreamWriter writer = new StreamWriter(new FileStream(fullFilePathText.Text, FileMode.OpenOrCreate)))
                {
                    writer.WriteLine($"{EEG_Title.Attention}={currentBrainData[EEG_Title.Attention]}, {EEG_Title.Meditation}={currentBrainData[EEG_Title.Meditation]}," +
                        $"{EEG_Title.Low_Alpha}={currentBrainData[EEG_Title.Low_Alpha]}, {EEG_Title.High_Alpha}={currentBrainData[EEG_Title.High_Alpha]}," +
                        $"{EEG_Title.Low_Gamma}={currentBrainData[EEG_Title.Low_Gamma]}, {EEG_Title.High_Gamma}={currentBrainData[EEG_Title.High_Gamma]}," +
                        $"{EEG_Title.Low_Beta}={currentBrainData[EEG_Title.Low_Beta]}, {EEG_Title.High_Beta}={currentBrainData[EEG_Title.High_Beta]}," +
                        $"{EEG_Title.Theta}={currentBrainData[EEG_Title.Theta]}, {EEG_Title.Delta}={currentBrainData[EEG_Title.Delta]}:{_seconds}"
                        );
                }
        }

        private void UploadDataForDistribution(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Mind Files (*.mind) | *.mind";
            if (OPF.ShowDialog() != DialogResult.OK) return;

            attentionDistributionChart.Series[0].Points.Clear();

            MindFileReader mindFile = new MindFileReader(OPF.FileName);
            Dictionary<double, int> frequencyChartData = new Dictionary<double, int>();
            foreach (var brainData in mindFile)
            {
                if (brainData._title == EEG_Title.Attention)
                {
                    if (frequencyChartData.ContainsKey(brainData._brainValue)) ++frequencyChartData[brainData._brainValue];
                    else frequencyChartData.Add(brainData._brainValue, 1);
                }
            }

            foreach (KeyValuePair<double, int> pair in frequencyChartData)
            {
                attentionDistributionChart.Series[0].Points.AddXY(pair.Key, pair.Value);
            }
        }
    }
}