using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using NeuroTGAM;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
using System.Linq;
using AttentionAnalysis;
using MindAnalysis.AttentionAnalysis.Models;
using MindAnalysis.AttentionAnalysis;
using System.Drawing;
using IronPython.Modules;
using ConsoleTables;

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

        private void CalculateAllErrors(object sender, EventArgs e)
        {
            string[] filesNames = new string[] { 
                "B01.csv", "B12.csv", "C01.csv", "C02.csv", "C03.csv",
                "C04.csv", "C05.csv", "C06.csv", "Бурцева_Проба.csv",
                "Илья_Проба.csv", "Киселёв_Проба.csv", "Мисилин_Проба.csv",
                "Яна_Проба.csv"
            };

            List<double> modelTrainErrors = new List<double>();
            List<List<double>> modelTestErrors = new List<List<double>>();
            int[] horizonts = new int[] { 15, 20, 25, 30, 35, 40, 45 };

            var table = new ConsoleTable("Датасет", "Ошибка тренировки", "Прогноз 15 с", "Прогноз 25 с", "Прогноз 35 с", "Прогноз 45 с");
            foreach (string fileName in filesNames)
            {
                string fullPath = $"D:\\JupyterProjects\\AttentionDatasets\\{fileName}";

                List<DataPoint> attentionData = new List<DataPoint>();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
                using (CsvReader csvReader = new CsvReader(File.OpenText(fullPath), csvConfig))
                {
                    var brainInfos = csvReader.GetRecords<BrainInfo>();
                    foreach (var brainInfo in brainInfos)
                    {
                        int timeInSeconds = 3600 * brainInfo.Second.Hours+ 60 * brainInfo.Second.Minutes + brainInfo.Second.Seconds;
                        attentionData.Add(new DataPoint(timeInSeconds, brainInfo.Attention));
                    }
                }

                double alpha = 1.0d; //2.0d / (attentionData.Count + 1);
                // new ExponentialSmoothing(alpha: alpha, isMeanValue: false);
                List<double> forecastErrors = new List<double>();
                IPredictionModel model = new ExponentialSmoothing(alpha: alpha, isMeanValue: false);

                foreach (int horizont in horizonts)
                {
                    (DataPoint[] trainData, DataPoint[] testData) = train_data_split(attentionData.ToArray(), 0.7f, horizont);
                    model.Fit(trainData);
                    forecastErrors.Add(model.Evaluate(testData));
                }

                table.AddRow(fileName, model.ModelError, forecastErrors[0], forecastErrors[1], forecastErrors[2], forecastErrors[3]);
                modelTrainErrors.Add(model.ModelError);
                modelTestErrors.Add(forecastErrors);
            }

            table.Write(Format.Alternative);

            double meanModelError = modelTrainErrors.Average();
            double orkloneniyaErrors = modelTrainErrors.Sum(error => Math.Pow(error - meanModelError, 2)) / modelTrainErrors.Count;

            List<double> meanForecastErrors = new List<double>();
            for (int j = 0; j < horizonts.Length; j++)
            {
                double sum = 0.0d;
                for (int i = 0; i < modelTestErrors.Count; i++)
                {
                    sum += modelTestErrors[i][j];
                }
                meanForecastErrors.Add(sum / modelTestErrors.Count);
            }

            Console.WriteLine("--------------------------------------------[Вывод по модели]--------------------------------------------");
            Console.WriteLine($"Средняя ошибка по датасету {meanModelError}, отклонение ошибки {orkloneniyaErrors}");
            Console.Write($"Ошибки прогноза: ");
            for (int i = 0; i < meanForecastErrors.Count; i++)
            {
                Console.Write($"{meanForecastErrors[i]}");
                if (i != meanForecastErrors.Count - 1) Console.Write(",");
            }
            Console.WriteLine("\n---------------------------------------------------------------------------------------------------------");
        }

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
                        chartAttention.Series[1].Points.AddXY(brainInfo.Second.ToString(), brainInfo.Attention);
                    }
                }
            }
        }

        private (DataPoint[] trainData, DataPoint[] testData) train_data_split(DataPoint[] data, float trainPercent, int horizont)
        {
            int trainDataCount = Convert.ToInt32(data.Length * trainPercent);
            DataPoint[] trainData = data.Take(trainDataCount).ToArray();
            DataPoint[] testData = data.Skip(trainDataCount).Take(horizont).ToArray();

            return (trainData, testData);
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