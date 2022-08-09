using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using NeuroTGAM;
using System.Threading;
using MindFileSystem;

namespace EEG_Graphics
{
    public partial class Form1 : Form
    {
        private uint _seconds = 0;
        private NeuroDeviceTGAM _neurodevice;
        private BrainCharts _brainCharts;

        private delegate void ChartDisplayHandler(Chart chart, int seriesNumber, uint seconds, double data);

        //TODO: Попробовать реализовать enum, который содержал бы в себе настройки, связанные с удалением новых точек на графиках и сохранение данных ЭЭГ в файл.
        bool isDeleteNewChartDots;
        bool isSaveMindDataToFile;

        public Form1()
        {
            InitializeComponent();

            _neurodevice = new NeuroDeviceTGAM();
            _brainCharts = new BrainCharts(new Dictionary<BrainDataTitle, Chart>()
            {
                [BrainDataTitle.Low_Alpha] = graphicLowAlpha,
                [BrainDataTitle.High_Alpha] = graphicHighAlpha,
                [BrainDataTitle.Low_Beta] = graphicLowBeta,
                [BrainDataTitle.High_Beta] = graphicHighBeta,
                [BrainDataTitle.Low_Gamma] = graphicLowGamma,
                [BrainDataTitle.High_Gamma] = graphicHighGamma,
                [BrainDataTitle.Theta] = graphicTheta,
                [BrainDataTitle.Delta] = graphicDelta,
                [BrainDataTitle.Attention] = graphicAttention,
                [BrainDataTitle.Meditation] = graphicMeditation
            });

            _neurodevice.ShowBrainData += DisplayDataToGraphics;
            startRecordButton.Enabled = true;
            stopRecordButton.Enabled = false;
            recordSettingsGroupBox.Enabled = true;
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
                //TODO: Придумать/найти решение, позволяюее избавиться от многострочной инициализации
                stopRecordButton.Enabled = true;
                startRecordButton.Enabled = false;
                uploadMindFileButton.Enabled = false;
                recordSettingsGroupBox.Enabled = false;

                isDeleteNewChartDots = deleteNewPointsCheckBox.Checked;
                isSaveMindDataToFile = saveRecordDataCheckBox.Checked;
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
            //TODO: Также придумать способ или найти решение, которое бы позволило избавить от многострочной инициализации.
            startRecordButton.Enabled = true;
            stopRecordButton.Enabled = false;
            uploadMindFileButton.Enabled = true;
            recordSettingsGroupBox.Enabled = true;
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => saveMindRecordButton.Enabled = !saveMindRecordButton.Enabled;
        private void OnChangedValueInMaxGraphPoints(object sender, EventArgs e) => maxGraphPointsNumeric.Enabled = !maxGraphPointsNumeric.Enabled;
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

            deleteAllGraphicsButton.Enabled = true;
        }

        private void ClearAllGraphics(object sender, EventArgs e)
        {
            _brainCharts.ClearAllSeries();
            deleteAllGraphicsButton.Enabled = false;
        }

        private void DisplayDataToGraphics(Dictionary<BrainDataTitle, double> currentBrainData)
        {
            //TODO: Сделать рефакторинг. Уменьшить код функции без потери функционала.
            BrainDataTitle[] brainKeys = currentBrainData.Keys.ToArray();
            //TODO: Сделать отображение точек на графике.
            //foreach (BrainDataTitle brainKey in brainKeys) BeginInvoke(new ChartDisplayHandler(DisplayBrainDataToChart), new object[] { _charts[brainKey], 0, _seconds, currentBrainData[brainKey] });
             _seconds++;
            
            if (!isSaveMindDataToFile || fullFilePathText.Text.Length == 0) return;
            
            using (FileStream file = new FileStream(fullFilePathText.Text, FileMode.OpenOrCreate))
            {
                file.Seek(0, SeekOrigin.End);
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine(
                        $"{BrainDataTitle.Attention}={currentBrainData[BrainDataTitle.Attention]}," +
                        $"{BrainDataTitle.Meditation}={currentBrainData[BrainDataTitle.Meditation]}," +
                        $"{BrainDataTitle.Low_Alpha}={currentBrainData[BrainDataTitle.Low_Alpha]}," +
                        $"{BrainDataTitle.High_Alpha}={currentBrainData[BrainDataTitle.High_Alpha]}," +
                        $"{BrainDataTitle.Low_Gamma}={currentBrainData[BrainDataTitle.Low_Gamma]}," +
                        $"{BrainDataTitle.High_Gamma}={currentBrainData[BrainDataTitle.High_Gamma]}," +
                        $"{BrainDataTitle.Low_Beta}={currentBrainData[BrainDataTitle.Low_Beta]}," +
                        $"{BrainDataTitle.High_Beta}={currentBrainData[BrainDataTitle.High_Beta]}," +
                        $"{BrainDataTitle.Theta}={currentBrainData[BrainDataTitle.Theta]}," +
                        $"{BrainDataTitle.Delta}={currentBrainData[BrainDataTitle.Delta]}:" + 
                        _seconds.ToString()
                        );
                }
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
                if (brainData._title == BrainDataTitle.Attention)
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