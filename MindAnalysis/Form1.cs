using System;
using System.IO;
using System.Windows.Forms;
using NeuroTGAM;
using CsvHelper;
using System.Drawing;
using System.Globalization;
using CsvHelper.Configuration;
using System.Collections.Generic;
using MindLinkAnalyze;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.ML;
using AttentionModel;
using System.Linq;

namespace EEG_Graphics
{
    //Общие задачи
    //TODO: Добавить вкладку "О программе"
    public partial class MainForm : Form
    {
        private delegate void DynamicChartDisplay(BrainInfo brainInfo);
        private Neurointerface _neurodevice;
        private AttentionChart _chartAttention;
        private uint _seconds = 0;
        private DateTime _startTime;

        public MainForm()
        {
            InitializeComponent();
            _neurodevice = new Neurointerface();
            _neurodevice.OnBrainInfoReceived += (BrainInfo brainInfo) => Invoke(new DynamicChartDisplay(DisplayPointToDynamicGraphic), brainInfo);
            _chartAttention = new AttentionChart(chartAttention);
            Console.WriteLine(DateTime.Now);
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
            _chartAttention.ClearSerie(0);
            UserControlSystem.GetSystem().Disable(btnStartRecord).Enable(btnStopRecord);
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (!_neurodevice.IsDataReading)
            {
                MessageBox.Show("Соединение с ThinkGear Connector не установлено.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _neurodevice.CloseConnection();
            _seconds = 0;
            UserControlSystem.GetSystem().Disable(btnStopRecord).Enable(btnStartRecord);
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;

        private void UploadFirstFileToChart(object sender, EventArgs e) => UploadBrainFile(1);

        private void UploadSecondFileToChart(object sender, EventArgs e) => UploadBrainFile(2);

        private void UploadBrainFile(int serie)
        {
            using (OpenFileDialog OPF = new OpenFileDialog() { Filter = "Csv files (*.csv) | *.csv" })
            {
                if (OPF.ShowDialog() != DialogResult.OK) return;
                _chartAttention.ClearSerie(serie);
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
                using (CsvReader csvReader = new CsvReader(File.OpenText(OPF.FileName), csvConfig))
                {
                    var brainInfos = csvReader.GetRecords<BrainInfo>();
                    foreach (var brainInfo in brainInfos)
                    {
                        _chartAttention.DisplayBrainInfo(brainInfo, serie);
                    }
                }
            }
        }

        private void SaveFilePathOfCurrentMindRecord(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv files (*.csv) | *.csv";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (saveFileDialog.FileName.Length > 3) fullFilePathText.Text = Path.GetFullPath(saveFileDialog.FileName);
            else MessageBox.Show("Имя файла слишком маленькое!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearExperiment(object sender, EventArgs e) => _chartAttention.ClearSerie(0);
        private void ClearFirstLoadedFile(object sender, EventArgs e) => _chartAttention.ClearSerie(1);
        private void ClearSecondLoadedFile(object sender, EventArgs e) => _chartAttention.ClearSerie(2);

        private void ClearAllGraphics(object sender, EventArgs e)
        {
            _chartAttention.ClearAllSeries();
            return;
        }

        void DisplayPointToDynamicGraphic(BrainInfo brainInfo)
        {
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            elapsedTime = new TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
            brainInfo.Second = elapsedTime;

            chartAttentionLevel.Series[0].Points.Clear();
            if (brainInfo.Attention > 0 && brainInfo.Attention <= 50) chartAttentionLevel.Series[0].Color = Color.Red;
            else if (brainInfo.Attention > 50 && brainInfo.Attention <= 80) chartAttentionLevel.Series[0].Color = Color.Yellow;
            else if (brainInfo.Attention > 80) chartAttentionLevel.Series[0].Color = Color.Green;
            chartAttentionLevel.Series[0].Points.AddXY(1.0f, brainInfo.Attention);

            if (numMaxChartPoints.Value < _chartAttention.DynamicChartPointsCount) _chartAttention.DeletePoint(0, 0);
            _chartAttention.DisplayBrainInfo(brainInfo);

            _seconds++;

            if (!chkSaveRecordData.Checked) return;
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

/*DataPoint[] points = _brainCharts.GetPoints(BrainChartName.Attention, 0);
DataPoint[] smoothedPoints = AttentionAnalyze.ExponentialSmoothing(points, 0.07f);
(double k, double b) = AttentionAnalyze.GetLinearFunctionParams(smoothedPoints);

AttentionBehaviourModel.ModelInput sampleData = new AttentionBehaviourModel.ModelInput()
{
    K_angle = Convert.ToSingle(k),
    B_offset = Convert.ToSingle(b),
};

var predictionResult = AttentionBehaviourModel.Predict(sampleData);
Console.WriteLine($"Классификатор: по моим предположениям внимание {predictionResult.PredictedLabel}");*/

/* DataPoint[] filteredData = AttentionAnalyze.ExponentialSmoothing(brainDatas.ToArray(), 0.07f);
 (double a, double b) = AttentionAnalyze.GetPowFunctionParams(filteredData);

 Console.WriteLine($"a = {a}, b = {b}");

 double sumPow_2_Calculated = 0;
 double sumPow_2_Input = 0;
 double meanPoints = brainDatas.ToArray().Sum(point => point.YValues[0]) / brainDatas.Count;

 double error = 0;
 for (int i = 1; i < 300; i++)
 {
     DataPoint point = new DataPoint(i, b * Math.Pow(i, a));
     //_brainCharts.AddPoint(BrainChartName.Attention, 2, point);
     //DataPoint chartPoint = _brainCharts.GetPoint(BrainChartName.Attention, 1, i);
     //error += Math.Pow(chartPoint.YValues[0] - point.YValues[0], 2);

     sumPow_2_Calculated += Math.Pow(point.YValues[0] - meanPoints, 2);
 }

 sumPow_2_Input = error;
 double fisherParam = sumPow_2_Calculated * 298 / sumPow_2_Input;
 Console.WriteLine($"Критерий Фишера равен {fisherParam}");
 Console.WriteLine($"Ошибка у данного испытуемого составляет: {error}");
*/