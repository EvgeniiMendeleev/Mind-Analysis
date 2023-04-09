using System;
using System.IO;
using System.Windows.Forms;
using NeuroTGAM;
using CsvHelper;
using System.Drawing;
using System.Globalization;
using CsvHelper.Configuration;
using System.Windows.Forms.DataVisualization.Charting;
using AttentionModel;
using System.Linq;
using MindAnalysis.Forms;
using AttentionAnalysis;

namespace MindAnalysis
{
    //Общие задачи
    //TODO: [Опционально]. Добавить вкладку "О программе".
    //TODO: Добавить мастер построения трендов с сглаживанием.
    //TODO: Добавить оценки качества построенной модели.
    //TODO: Добавить параметры (цепной и базисный темп/прирост) изменения внимания.
    //TODO: Решить проблему с "резиновым интерфейсом". Расположение и размер элементов меняется при разработке на мониторах с разными диагоналями.
    public partial class MainForm : Form
    {
        private delegate void DynamicChartDisplay(BrainInfo brainInfo);
        private Neurointerface _neurodevice;
        private DateTime _startTime;

        public MainForm()
        {
            InitializeComponent();
            _neurodevice = new Neurointerface();
            _neurodevice.OnBrainInfoReceived += (BrainInfo brainInfo) =>
            {
                Invoke(new DynamicChartDisplay(DisplayPointToDynamicGraphic), brainInfo);
                if (chkSaveRecordData.Checked) SaveBrainInfoToFile(brainInfo);
            };
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
                chartAttention.Series[serie].Points.Clear();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
                using (CsvReader csvReader = new CsvReader(File.OpenText(OPF.FileName), csvConfig))
                {
                    var brainInfos = csvReader.GetRecords<BrainInfo>();
                    foreach (var brainInfo in brainInfos)
                    {
                        chartAttention.Series[serie].Points.AddXY(brainInfo.Second.ToString(), brainInfo.Attention);
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

        private void BuildTrendOnChart(object sender, EventArgs e)
        {
            TrendBuilder trendBuilder = new TrendBuilder();
            if (trendBuilder.ShowDialog() != DialogResult.Yes) return;
            trendBuilder.Dispose();

            TrendParams trendParams = trendBuilder.GetTrendParams();
            if (chartAttention.Series["Загруженные данные"].Points.Count == 0)
            {
                MessageBox.Show("Данные не были загружены в программу!", "Ошибка построения тренда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataPoint[] points = chartAttention.Series["Загруженные данные"].Points.ToArray();
            switch (trendParams.trendSmoothingMode)
            {
                case TrendSmoothingMode.MovingAverage:
                    break;
                case TrendSmoothingMode.ExponentialSmoothing:
                    points = DataSmoothing.ExponentialSmoothing(points, (float)trendParams.smoothingParam);
                    break;
            }

            int pointsCount = chartAttention.Series["Загруженные данные"].Points.Count;
            DataPoint[] copyPoints = new DataPoint[points.Length];
            for (int i = 0; i < pointsCount; i++)
            {
                copyPoints[i] = points[i].Clone();
                copyPoints[i].XValue = i + 1;
                copyPoints[i].YValues[0] = points[i].YValues[0];
            }

            if (trendParams.trendSmoothingMode != TrendSmoothingMode.None)
            {
                chartAttention.Series["Сглаженная запись"].Points.Clear();
                for(int i = 0; i < pointsCount; i++) chartAttention.Series["Сглаженная запись"].Points.AddXY(points[i].XValue, copyPoints[i].YValues[0]);
            }

            chartAttention.Series["Тренд испытуемого"].Points.Clear();
            switch (trendParams.trendMode)
            {
                case TrendMode.Linear:
                    (double k, double b) = Extrapolation.LinearRegression(copyPoints);
                    for (int i = 0; i < pointsCount; i++) chartAttention.Series["Тренд испытуемого"].Points.AddXY(points[i].XValue, k * i + b);
                    break;
                case TrendMode.Giperbolic:
                    (double a0, double a1) = Extrapolation.GiperbolicRegression(copyPoints);
                    for (int i = 1; i < pointsCount; i++) chartAttention.Series["Тренд испытуемого"].Points.AddXY(points[i].XValue, a0 + a1 / i);
                    break;
                case TrendMode.Power:
                    (double A, double B) = Extrapolation.PowerRegression(copyPoints);
                    if (A.Equals(double.NaN) || B.Equals(double.NaN))
                    {
                        MessageBox.Show("Невозможно построить тренд: необходимо сгладить исходные данные.", "Ошибка построения тренда", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        for (int i = 2; i < chartAttention.Series.Count; i++) chartAttention.Series[i].Points.Clear();
                        return;
                    }
                    for (int i = 1; i < pointsCount; i++) chartAttention.Series["Тренд испытуемого"].Points.AddXY(points[i].XValue, B * Math.Pow(i, A));
                    break;
            }
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