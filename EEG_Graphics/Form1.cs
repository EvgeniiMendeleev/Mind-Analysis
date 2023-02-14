using System;
using System.IO;
using System.Windows.Forms;
using NeuroTGAM;
using CsvHelper;
using System.Drawing;
using System.Globalization;
using CsvHelper.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using MathLibrary;
using System.Linq;

namespace EEG_Graphics
{
    //Общие задачи
    //TODO: Добавить вкладку "О программе"
    public partial class MainForm : Form
    {
        private delegate void DynamicChartDisplay(BrainInfo brainInfo);
        private NeuroSerialPort _neurodevice;
        private BrainCharts _brainCharts;
        private uint _seconds = 0;

        public MainForm()
        {
            InitializeComponent();
            _neurodevice = new NeuroSerialPort();
            _brainCharts = new BrainCharts();
            _neurodevice.OnBrainInfoReceived += (BrainInfo brainInfo) => Invoke(new DynamicChartDisplay(DisplayPointToDynamicGraphic), brainInfo);
            UserControlSystem.GetSystem().Disable(btnStopRecord);

            _brainCharts.AddChart(BrainChartName.Attention, graphicAttention);
            _brainCharts.AddChart(BrainChartName.Meditation, graphicMeditation);
            _brainCharts.AddChart(BrainChartName.Alpha_Low, graphicLowAlpha);
            _brainCharts.AddChart(BrainChartName.Alpha_High, graphicHighAlpha);
            _brainCharts.AddChart(BrainChartName.Beta_Low, graphicLowBeta);
            _brainCharts.AddChart(BrainChartName.Beta_High, graphicHighBeta);
            _brainCharts.AddChart(BrainChartName.Gamma_Low, graphicLowGamma);
            _brainCharts.AddChart(BrainChartName.Gamma_High, graphicHighGamma);
            _brainCharts.AddChart(BrainChartName.Theta, graphicTheta);
            _brainCharts.AddChart(BrainChartName.Delta, graphicDelta);
            _brainCharts.AddChart(BrainChartName.ChartsComparison, chartsComparisonChart);
            _brainCharts.AddChart(BrainChartName.ParamsComparison, paramsComparisonChart);

            domainUpDown1.SelectedItem = "Attention(0)";
            paramsComparisonDomain1.SelectedItem = "Attention(0)";
            paramsComparisonDomain2.SelectedItem = "Attention(0)";
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Подключиться к пауку?", "Паук", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes) _neurodevice.ConnectToSpider(spiderPortTextBox.Text);

                result = MessageBox.Show("Подключиться к нейроинтерфейсу?", "Нейроинтерфейс", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) _neurodevice.Connect(neuroPortTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к одному из устройств", "Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _brainCharts.ClearSerieOnCharts(0);
            UserControlSystem.GetSystem().Disable(btnStartRecord).Enable(btnStopRecord).Disable(btnLoadFirstFile).Disable(groupRecordSettings)
                .Disable(btnClearAllCharts);
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (!_neurodevice.AreDataReading)
            {
                MessageBox.Show("Соединение с ThinkGear Connector не установлено.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _neurodevice.CloseConnection();
            _seconds = 0;
            //getDataTimer.Stop();
            UserControlSystem.GetSystem().Disable(btnStopRecord).Enable(btnStartRecord).Enable(btnLoadFirstFile)
                .Enable(groupRecordSettings).Enable(btnClearAllCharts);
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;

        private void UploadBrainFile(object sender, EventArgs e)
        {
            using (OpenFileDialog OPF = new OpenFileDialog() { Filter = "Csv files (*.csv) | *.csv" })
            {
                if (OPF.ShowDialog() != DialogResult.OK) return;

                int serieNumber = Convert.ToInt32(serieNumericUp.Value);
                _brainCharts.ClearSerieOnCharts(serieNumber);

                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
                using (CsvReader csvReader = new CsvReader(File.OpenText(OPF.FileName), csvConfig))
                {
                    var brainInfos = csvReader.GetRecords<BrainInfo>();
                    foreach (var brainInfo in brainInfos) _brainCharts.DisplayBrainInfo(brainInfo, serieNumber);
                }
            }
            _brainCharts.ScaleCharts();
            UserControlSystem.GetSystem().Enable(btnClearAllCharts);
        }

        private void SaveFilePathOfCurrentMindRecord(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv files (*.csv) | *.csv";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (saveFileDialog.FileName.Length > 3) fullFilePathText.Text = Path.GetFullPath(saveFileDialog.FileName);
            else MessageBox.Show("Имя файла слишком маленькое!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearAllGraphics(object sender, EventArgs e) => _brainCharts.ClearAllSeries();
        private void ClearGraphicsWithSerie(object sender, EventArgs e) => _brainCharts.ClearSerieOnCharts(Convert.ToInt32(serieNumericUp.Value));

        private void DisplayBrainInfo(object sender, EventArgs e)
        {
            _brainCharts.DisplayBrainInfo(_neurodevice.GetCurrentBrainData());
        }

        private void DisplayDataToGraphics(BrainInfo currentBrainData)
        {
            brainInfo.Second = _seconds;

            attentionLevelChart.Series[0].Points.Clear();
            if (brainInfo.Attention > 0 && brainInfo.Attention <= 50) attentionLevelChart.Series[0].Color = Color.Red;
            else if (brainInfo.Attention > 50 && brainInfo.Attention <= 80) attentionLevelChart.Series[0].Color = Color.Yellow;
            else if (brainInfo.Attention > 80) attentionLevelChart.Series[0].Color = Color.Green;
            attentionLevelChart.Series[0].Points.AddXY(1.0f, brainInfo.Attention);

            _brainCharts.DisplayBrainInfo(brainInfo);
            _brainCharts.ScaleCharts();

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

        private void button5_Click(object sender, EventArgs e) => _brainCharts.ClearAllSeriesOnChart(BrainChartName.ChartsComparison);

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv) | *.csv";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            
            filePathComparisonTextBox.Text = Path.GetFullPath(openFileDialog.FileName);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filePath = filePathComparisonTextBox.Text;
            Regex regex = new Regex("[0-9]+");
            Regex filedNameRegex = new Regex("[a-zA-Z]+");

            int fieldNumber = Convert.ToInt32(regex.Match(domainUpDown1.SelectedItem.ToString()).Value);
            string fieldName = filedNameRegex.Match(domainUpDown1.SelectedItem.ToString()).Value;

            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };

            Dictionary<double, double> brainDatas = new Dictionary<double, double>();
            using (CsvReader csvReader = new CsvReader(new StreamReader(filePath), csvConfig))
            {
                _brainCharts.CreateSerieOnComparisonChart(BrainChartName.ChartsComparison, SeriesChartType.Spline, fieldName);
                while (csvReader.Read())
                {
                    var brainInfo = Convert.ToDouble(csvReader.GetField(fieldNumber));
                    var second = Convert.ToDouble(csvReader.GetField(10));
                    brainDatas.Add(second, brainInfo);
                }
            }

            float alpha = fieldName == "Attention" || fieldName == "Meditation" ? 0.07f : 0.08f;
            double[] scaledDatas = MathModel.GetSystem().SoftMaxScaler(brainDatas.Values.ToArray());
            double[] filteredDatas = MathModel.GetSystem().ExponentialSmoothing(scaledDatas, alpha);
            double[] seconds = brainDatas.Keys.ToArray();

            for (int i = 0; i < filteredDatas.Length; i++)
            {
                _brainCharts.AddPointOnComparisonChart(BrainChartName.ChartsComparison, fieldName, new DataPoint(seconds[i], filteredDatas[i]));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Regex filedNameRegex = new Regex("[a-zA-Z]+");
            string fieldName = filedNameRegex.Match(domainUpDown1.SelectedItem.ToString()).Value;
            _brainCharts.ClearSerieOnComparisonChart(BrainChartName.ChartsComparison, fieldName);
        }
        //------------------------Сравнение параметров-------------------------------
        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv files (*.csv) | *.csv";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            paramsComparisontextBox.Text = Path.GetFullPath(openFileDialog.FileName);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string filePath = paramsComparisontextBox.Text;
            Regex regex = new Regex("[0-9]+");
            Regex filedNameRegex = new Regex("[a-zA-Z]+");

            int firstField = Convert.ToInt32(regex.Match(paramsComparisonDomain1.SelectedItem.ToString()).Value);
            string firstFieldName = filedNameRegex.Match(paramsComparisonDomain1.SelectedItem.ToString()).Value;

            int secondField = Convert.ToInt32(regex.Match(paramsComparisonDomain2.SelectedItem.ToString()).Value);
            string secondFieldName = filedNameRegex.Match(paramsComparisonDomain2.SelectedItem.ToString()).Value;

            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = false };
            List<DataPoint> pointsForComparisonChart = new List<DataPoint>();
            using (CsvReader csvReader = new CsvReader(new StreamReader(filePath), csvConfig))
            {
                _brainCharts.CreateSerieOnComparisonChart(BrainChartName.ParamsComparison, SeriesChartType.Point, "Корреляция");
                while (csvReader.Read())
                {
                    var firstFieldValue = Convert.ToDouble(csvReader.GetField(firstField));
                    var secondFieldValue = Convert.ToDouble(csvReader.GetField(secondField));

                    pointsForComparisonChart.Add(new DataPoint(secondFieldValue, firstFieldValue));
                }
            }

            _brainCharts.SetParamsComprisonAxesName(secondFieldName, firstFieldName);

            double[] scaled_X = /*pointsForComparisonChart.Select(point => point.XValue).ToArray()*/MathModel.GetSystem().SoftMaxScaler(pointsForComparisonChart.Select(point => point.XValue).ToArray());
            double[] scaled_Y = /*pointsForComparisonChart.Select(point => point.YValues[0]).ToArray();*/MathModel.GetSystem().SoftMaxScaler(pointsForComparisonChart.Select(point => point.YValues[0]).ToArray());

            double[] filtered_X = MathModel.GetSystem().ExponentialSmoothing(scaled_X, 0.01f);
            double[] filtered_Y = MathModel.GetSystem().ExponentialSmoothing(scaled_Y, 0.01f);

            for (int i = 0; i < pointsForComparisonChart.Count; i++)
            {
                _brainCharts.AddPointOnComparisonChart(BrainChartName.ParamsComparison, "Корреляция", new DataPoint(filtered_X[i], filtered_Y[i]));
            }
        }

        private void button8_Click(object sender, EventArgs e) => _brainCharts.ClearSerieOnComparisonChart(BrainChartName.ParamsComparison, "Корреляция");
    }
}