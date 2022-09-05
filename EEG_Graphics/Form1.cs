using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using NeuroTGAM;
using MindFileSystem;

namespace EEG_Graphics
{
    //Общие задачи
    //TODO: Попробовать реализовать вывод графиков ЭЭГ на печать.
    //TODO: Добавить вкладку "О программе"
    //TODO: Сделать так, чтобы после старта записи данных с нейинтерфейса можно было просмотреть настройки записи, но чтобы нельзя было менять их значения.
    public partial class Form1 : Form
    {
        private delegate void DynamicChartDisplay(EEG_Title chartName, DataPoint point);

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

            _neurodevice.ShowBrainData += DisplayDataToGraphics;
            UserControlSystem.GetSystem().Enable(btnStartRecord).Disable(btnStopRecord).Enable(groupRecordSettings);
            attentionDistributionChart.ChartAreas[0].AxisX.Maximum = 100;
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            try
            {
                _neurodevice.ConnectToConnector();
            }
            catch
            {
                MessageBox.Show("Не был запущен ThinkGear Connector", "Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _brainCharts.ClearSerieOnCharts(0);
            UserControlSystem.GetSystem().Disable(btnStartRecord).Enable(btnStopRecord).Disable(btnLoadFirstFile).Disable(groupRecordSettings)
                .Disable(btnLoadSecondFile).Disable(btnClearAllCharts);
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
            UserControlSystem.GetSystem()
                .Disable(btnStopRecord)
                .Enable(btnStartRecord)
                .Enable(btnLoadFirstFile)
                .Enable(btnLoadSecondFile)
                .Enable(groupRecordSettings)
                .Enable(btnClearAllCharts);
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;
        private void OnChangedValueInMaxGraphPoints(object sender, EventArgs e) => numMaxChartPoints.Enabled = !numMaxChartPoints.Enabled;

        //TODO: Попробовать реализавать систему, которая бы позволяла добавлять сколько угодно серий точек на график для анализа более чем трёх графиков.
        private void UploadFirstBrainDataFile(object sender, EventArgs e) => DisplayBrainCharts(1);
        private void UploadSecondBrainDataFile(object sender, EventArgs e) => DisplayBrainCharts(2);

        private void SaveFilePathOfCurrentMindRecord(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Mind Files (*.mind) | *.mind";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (saveFileDialog.FileName.Length > 3) fullFilePathText.Text = Path.GetFullPath(saveFileDialog.FileName);
            else MessageBox.Show("Имя файла слишком маленькое!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

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

        private void ClearAllGraphics(object sender, EventArgs e) => _brainCharts.ClearAllSeries();

        private void DisplayDataToGraphics(Dictionary<EEG_Title, double> currentBrainData)
        {
            var brainKeys = currentBrainData.Keys;
            foreach (EEG_Title brainKey in brainKeys)
            {
                object[] DisplaingPointParams = new object[] { brainKey, new DataPoint(_seconds, currentBrainData[brainKey]) };
                BeginInvoke(new DynamicChartDisplay(DisplayPointToDynamicGraphic), DisplaingPointParams);
            }

            _seconds++;
            //TODO: Попробовать переписать сохранение данных c нейроинтерфейса с учётом новой библиотеки (де)сериализации данных в виде JSON строк.
            if (chkSaveRecordData.Checked) using (FileStream file = new FileStream(fullFilePathText.Text, FileMode.OpenOrCreate))
                {
                    file.Seek(0, SeekOrigin.End);
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        writer.WriteLine($"{EEG_Title.Attention}={currentBrainData[EEG_Title.Attention]}, {EEG_Title.Meditation}={currentBrainData[EEG_Title.Meditation]}," +
                            $"{EEG_Title.Low_Alpha}={currentBrainData[EEG_Title.Low_Alpha]}, {EEG_Title.High_Alpha}={currentBrainData[EEG_Title.High_Alpha]}," +
                            $"{EEG_Title.Low_Gamma}={currentBrainData[EEG_Title.Low_Gamma]}, {EEG_Title.High_Gamma}={currentBrainData[EEG_Title.High_Gamma]}," +
                            $"{EEG_Title.Low_Beta}={currentBrainData[EEG_Title.Low_Beta]}, {EEG_Title.High_Beta}={currentBrainData[EEG_Title.High_Beta]}," +
                            $"{EEG_Title.Theta}={currentBrainData[EEG_Title.Theta]}, {EEG_Title.Delta}={currentBrainData[EEG_Title.Delta]}:{_seconds}");
                    }
                }
        }

        void DisplayPointToDynamicGraphic(EEG_Title eegTitle, DataPoint point)
        {
            if (numMaxChartPoints.Value < _brainCharts.PointsCount(eegTitle, 0)) _brainCharts.DeletePoint(eegTitle, 0, 0);
            _brainCharts.AddPoint(eegTitle, 0, point);
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

            foreach (KeyValuePair<double, int> pair in frequencyChartData) attentionDistributionChart.Series[0].Points.AddXY(pair.Key, pair.Value);
        }
    }
}