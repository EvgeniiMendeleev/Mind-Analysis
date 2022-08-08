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
        private Dictionary<BrainDataTitle, Chart> _charts;
        private const int MAX_CHARTS_POINTS = 30;
        private uint _seconds = 0;
        private NeuroDeviceTGAM _neurodevice = new NeuroDeviceTGAM();

        private delegate void ChartDisplayHandler(Chart chart, int seriesNumber, uint seconds, double data);

        //TODO: Попробовать реализовать enum, который содержал бы в себе настройки, связанные с удалением новых точек на графиках и сохранение данных ЭЭГ в файл.
        bool isDeleteNewChartDots;
        bool isSaveMindDataToFile;

        public Form1()
        {
            InitializeComponent();
            _charts = new Dictionary<BrainDataTitle, Chart>()
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
            };

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
                ClearDynamicGraphics();
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

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e)
        {
            saveMindRecordButton.Enabled = !saveMindRecordButton.Enabled;
        }

        private void OnChangedValueInMaxGraphPoints(object sender, EventArgs e)
        {
            maxGraphPointsNumeric.Enabled = !maxGraphPointsNumeric.Enabled;
        }

        void DisplayBrainCharts(int seriesNumber)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Mind Files (*.mind) | *.mind";
            if (OPF.ShowDialog() != DialogResult.OK) return;
            
            //TODO: Попробовать сделать отдельную сущность BrainCharts, например, который бы содержал API для работы с графиками на форме.
            Chart[] charts = _charts.Values.ToArray();
            foreach (Chart chart in charts) chart.Series[seriesNumber].Points.Clear();

            MindFileReader mindFile = new MindFileReader(OPF.FileName);
            foreach (var brainData in mindFile)
            {
                DisplayBrainDataToChart(_charts[brainData._title], seriesNumber, brainData._time, brainData._brainValue);
                _charts[brainData._title].Series[seriesNumber].Name = Path.GetFileNameWithoutExtension(OPF.FileName);
            }

            OPF.Dispose();
            mindFile.Close();
        }


        //TODO: Попробовакть избавиться как - то от двух обрабочиков UploadFirstBrainDataFile для одно кнопки и UploadSecondBrainDataFile для другой кнопки, чтобы это был один обработчик.
        private void UploadFirstBrainDataFile(object sender, EventArgs e)
        {
            DisplayBrainCharts(1);
            deleteUploadedGraphicButton.Enabled = true;
        }

        private void UploadSecondBrainDataFile(object sender, EventArgs e)
        {
            DisplayBrainCharts(2);
            deleteUploadedGraphicButton.Enabled = true;
        }

        private void ClearUploadedGraphic(object sender, EventArgs e)
        {
            Chart[] charts = _charts.Values.ToArray();
            foreach (Chart chart in charts)
            {
                for(int i = 0; i < chart.Series.Count; i++) chart.Series[i].Points.Clear();
            }
            deleteUploadedGraphicButton.Enabled = false;
        }

        private void ClearDynamicGraphics()
        {
            Chart[] charts = _charts.Values.ToArray();
            foreach (Chart chart in charts)
            {
                chart.Series[0].Points.Clear();
            }
        }

        private void DisplayDataToGraphics(Dictionary<BrainDataTitle, double> currentBrainData)
        {
            //TODO: Сделать рефакторинг. Уменьшить код функции без потери функционала.
            //TODO: Возможно, можно будет попробовать избавиться от асинхронного метода BeginInvoke().
            BrainDataTitle[] brainKeys = currentBrainData.Keys.ToArray();
            foreach (BrainDataTitle brainKey in brainKeys) BeginInvoke(new ChartDisplayHandler(DisplayBrainDataToChart), new object[] { _charts[brainKey], 0, _seconds, currentBrainData[brainKey] });
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

        private void DisplayBrainDataToChart(Chart chart, int seriesNumber, uint seconds, double data)
        {
            if (isDeleteNewChartDots && chart.Series[0].Points.Count >= maxGraphPointsNumeric.Value) chart.Series[0].Points.RemoveAt(0);
            chart.Series[seriesNumber].Points.AddXY(seconds.ToString(), data);

            double maxY = 0.0;
            foreach (Series series in chart.Series)
            {
                DataPointCollection points = chart.Series[seriesNumber].Points;
                double y = points.Max(point => point.YValues[0]);
                if (maxY < y) maxY = y;
            }
            
            int scale = chart != graphicMeditation && chart != graphicAttention ? 100000 : 5;

            chart.ChartAreas[0].AxisY.Maximum = maxY + scale;
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