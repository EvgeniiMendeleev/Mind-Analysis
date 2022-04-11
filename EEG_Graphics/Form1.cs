using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using NeuroTGAM;

namespace EEG_Graphics
{
    public partial class Form1 : Form
    {
        private Dictionary<BrainDataTitle, Chart> _charts;
        private const int MAX_CHARTS_POINTS = 30;
        private uint _seconds = 0;
        private NeuroDeviceTGAM _neurodevice = new NeuroDeviceTGAM();

        private delegate void ChartDisplayHandler(Chart chart, int seriesNumber, uint seconds, double data);

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
                stopRecordButton.Enabled = true;
                startRecordButton.Enabled = false;
                uploadMindFileButton.Enabled = false;
                uploadSecondBrainFileButton.Enabled = false;
                recordSettingsGroupBox.Enabled = false;
                deleteUploadedGraphicButton.Enabled = false;

                isDeleteNewChartDots = deleteNewPointsCheckBox.Checked;
                isSaveMindDataToFile = saveRecordDataCheckBox.Checked;
            }
        }

        private void SaveFilePathCurrentMindRecord(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Mind Files (*.mind) | *.mind";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;

            if (saveFileDialog.FileName.Length > 3) fullFilePathText.Text = Path.GetFullPath(saveFileDialog.FileName);
            else MessageBox.Show("Имя файла слишком маленькое!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            startRecordButton.Enabled = true;
            stopRecordButton.Enabled = false;
            uploadMindFileButton.Enabled = true;
            uploadSecondBrainFileButton.Enabled = true;
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
            using (OpenFileDialog OPF = new OpenFileDialog())
            {
                OPF.Filter = "Mind Files (*.mind) | *.mind";
                if (OPF.ShowDialog() != DialogResult.OK) return;

                Chart[] charts = _charts.Values.ToArray();
                foreach (Chart chart in charts)
                {
                    chart.Series[seriesNumber].Points.Clear();
                }

                Regex regex = new Regex(@"\\\S+.mind$");
                Match match = regex.Match(OPF.FileName);
                
                using (StreamReader reader = new StreamReader(Path.GetFullPath(OPF.FileName)))
                {
                    while (!reader.EndOfStream)
                    {
                        string strFromMindFile = reader.ReadLine();
                        string[] brainDatasAndTime = strFromMindFile.Split(':');
                        uint time = Convert.ToUInt32(brainDatasAndTime[1]);
                        string[] brainDatas = brainDatasAndTime[0].Split(',');

                        foreach (string brainDataFromFile in brainDatas)
                        {
                            string[] brainDataAndTitle = brainDataFromFile.Split('=');
                            Enum.TryParse(brainDataAndTitle[0], out BrainDataTitle brainDataTitle);
                            double brainValue = Convert.ToInt32(brainDataAndTitle[1]);

                            DisplayBrainDataToChart(_charts[brainDataTitle], seriesNumber, time, brainValue);
                            _charts[brainDataTitle].Series[seriesNumber].Name = match.Value;
                        }
                    }
                }
            }
        }

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
                for(int i = 1; i < chart.Series.Count; i++) chart.Series[i].Points.Clear();
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
            BrainDataTitle[] brainKeys = currentBrainData.Keys.ToArray();
            foreach (BrainDataTitle brainKey in brainKeys)
            {
                
                BeginInvoke(new ChartDisplayHandler(DisplayBrainDataToChart), new object[] { _charts[brainKey], 0, _seconds, currentBrainData[brainKey] });
            }
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
    }
}