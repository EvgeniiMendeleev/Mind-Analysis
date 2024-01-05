using CsvHelper;
using CsvHelper.Configuration;
using NeuroTGAM;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis
{
    public partial class MainForm : Form
    {
        private delegate void DynamicChartDisplay(BrainInfo brainInfo);
        private Neurointerface _neurodevice = new Neurointerface();
        private DateTime _startTime;

        public MainForm()
        {
            InitializeComponent();
            _neurodevice.OnBrainDataReceived += (BrainInfo brainInfo) =>
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