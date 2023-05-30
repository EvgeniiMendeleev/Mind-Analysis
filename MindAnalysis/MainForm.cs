using System;
using System.IO;
using System.Windows.Forms;
using NeuroTGAM;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using AttentionAnalysis;
using IronPython.Hosting;
using MindAnalysis.AttentionAnalysis.Models;
using MindAnalysis.AttentionAnalysis;

namespace MindAnalysis
{
    //Общие задачи
    //TODO: [Опционально]. Добавить вкладку "О программе".

    public partial class MainForm : Form
    {
        private delegate void DynamicChartDisplay(BrainInfo brainInfo);
        private Neurointerface _neurodevice = new Neurointerface();
        private DateTime _startTime;

        public MainForm()
        {
            InitializeComponent();
            _neurodevice.OnBrainInfoReceived += (BrainInfo brainInfo) =>
            {
                Invoke(new DynamicChartDisplay(DisplayPointToDynamicGraphic), brainInfo);
                if (chkSaveRecordData.Checked) SaveBrainInfoToFile(brainInfo);
            };
        }

        private void CreateSinPoints(double radianLimit)
        {
            for (double angle = 0; angle < radianLimit; angle++)
            {
                chartAttention.Series[0].Points.AddXY(angle, Math.Sin(angle * 180.0d / Math.PI) * 20.0d);
            }
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            try
            {
                _neurodevice.Connect();
                _startTime = DateTime.Now;
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к нейроинтерфейсу", "Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            chartAttention.Series[0].Points.Clear();
            btnStartRecord.Enabled = false;
            btnStopRecord.Enabled = true;
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (!_neurodevice.IsDataReading)
            {
                MessageBox.Show("Соединение с ThinkGear Connector не установлено.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _neurodevice.CloseConnection();
            btnStartRecord.Enabled = true;
            btnStopRecord.Enabled = false;
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;

        private void UploadFileToChart(object sender, EventArgs e)
        {
            ClearAllGraphics(sender, e);
            using (OpenFileDialog OPF = new OpenFileDialog() { Filter = "Csv files (*.csv) | *.csv" })
            {
                if (OPF.ShowDialog() != DialogResult.OK) return;
                chartAttention.Series[1].Points.Clear();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
                using (CsvReader csvReader = new CsvReader(File.OpenText(OPF.FileName), csvConfig))
                {
                    var brainInfos = csvReader.GetRecords<BrainInfo>();
                    foreach (var brainInfo in brainInfos)
                    {
                        int time = brainInfo.Second.Hours * 3600 + brainInfo.Second.Minutes * 60 + brainInfo.Second.Seconds;
                        chartAttention.Series[1].Points.AddXY(time, brainInfo.Attention);
                    }
                }
            }

            chartAttentionDiff.Series[0].Points.Clear();
            chartAttentionDiff.Series[1].Points.Clear();

            double alpha = 2.0d / (chartAttention.Series[1].Points.Count + 1);
            IPredictionModel model = new ExponentialSmoothing(alpha: alpha, isMeanValue: true);
            //IPredictionModel model = new ExponentialSmoothing(alpha: alpha, isMeanValue: true);
            model.Fit(chartAttention.Series[1].Points.ToArray());
            Console.WriteLine($"Ошибка построенной модели: {model.ModelError}");
            //Console.WriteLine($"При alpha = {alpha} относительная ошибка модели составляет: {model.ModelError}");
        }

        private void SaveFilePathOfCurrentMindRecord(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv files (*.csv) | *.csv";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (saveFileDialog.FileName.Length > 3) fullFilePathText.Text = Path.GetFullPath(saveFileDialog.FileName);
            else MessageBox.Show("Имя файла слишком маленькое!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearExperiment(object sender, EventArgs e) => chartAttention.Series[0].Points.Clear();
        private void ClearFirstLoadedFile(object sender, EventArgs e) => chartAttention.Series[1].Points.Clear();

        private void ClearAllGraphics(object sender, EventArgs e)
        {
            foreach (Series serie in chartAttention.Series)
            {
                serie.Points.Clear();
            }
        }

        void DisplayPointToDynamicGraphic(BrainInfo brainInfo)
        {
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            elapsedTime = new TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
            brainInfo.Second = elapsedTime;

            if (numMaxChartPoints.Value < chartAttention.Series[0].Points.Count)
            {
                chartAttention.Series[0].Points.RemoveAt(0);
                chartAttention.ResetAutoValues();
            }
            chartAttention.Series[0].Points.AddXY(brainInfo.Second.ToString(), brainInfo.Attention);
        }

        private void SaveBrainInfoToFile(BrainInfo brainInfo)
        {
            using (FileStream file = new FileStream(fullFilePathText.Text, FileMode.OpenOrCreate, FileAccess.Write))
            {
                file.Seek(0, SeekOrigin.End);
                using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(file), CultureInfo.CurrentCulture))
                {
                    csvWriter.WriteRecord(brainInfo);
                    csvWriter.NextRecord();
                }
            }
        }
    }
}

/*
        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;
        private void UploadFileToChart(object sender, EventArgs e)
        private void SaveFilePathOfCurrentMindRecord(object sender, EventArgs e)
        private void ClearExperiment(object sender, EventArgs e)
        private void ClearFirstLoadedFile(object sender, EventArgs e)
        private void ClearAllGraphics(object sender, EventArgs e)
        private void DisplayPointToDynamicGraphic(BrainInfo brainInfo)
        private void SaveBrainInfoToFile(BrainInfo brainInfo)*/

/*          int firstFiveMinutes = 300;
            double firstMean = 0;
            double secondMean = 0;

            int length = chartAttention.Series[1].Points.Count;
            for (int i = 0; i < length; i++)
            {
                if (i < firstFiveMinutes)
                {
                    firstMean += chartAttention.Series[1].Points[i].YValues[0];
                }
                else 
                {
                    secondMean += chartAttention.Series[1].Points[i].YValues[0];
                }
            }

            firstMean /= firstFiveMinutes;
            secondMean /= (length - firstFiveMinutes);

            Console.WriteLine($"Первые пять минут = {firstMean}, а последние минуты равны {secondMean}");
 */