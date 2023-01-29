using System;
using System.IO;
using System.Windows.Forms;
using NeuroTGAM;
using CsvHelper;
using System.Drawing;
using System.Globalization;

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
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            try
            {
                _neurodevice.SetPorts(neuroPortTextBox.Text, spiderPortTextBox.Text);

                DialogResult result = MessageBox.Show("Подключиться к пауку?", "Паук", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes) _neurodevice.Connect();

                result = MessageBox.Show("Подключиться к нейроинтерфейсу?", "Нейроинтерфейс", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes) _neurodevice.ConnectToSpider();
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
            UserControlSystem.GetSystem().Disable(btnStopRecord).Enable(btnStartRecord).Enable(btnLoadFirstFile)
                .Enable(groupRecordSettings).Enable(btnClearAllCharts);
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e) => btnChangeSavePath.Enabled = !btnChangeSavePath.Enabled;

        private void UploadBrainFile(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Csv files (*.csv) | *.csv";
            if (OPF.ShowDialog() != DialogResult.OK) return;

            int serieNumber = Convert.ToInt32(serieNumericUp.Value);

            _brainCharts.ClearSerieOnCharts(serieNumber);
            using (CsvReader csvReader = new CsvReader(File.OpenText(OPF.FileName), CultureInfo.CurrentCulture))
            {
                var brainInfos = csvReader.GetRecords<BrainInfo>();
                foreach (var brainInfo in brainInfos) _brainCharts.DisplayBrainInfo(brainInfo, serieNumber);
            }

            _brainCharts.ScaleCharts();
            OPF.Dispose();
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
        private void ClearGraphicsWithSerie(object sender, EventArgs e)
        {
            int serieNumber = Convert.ToInt32(serieNumericUp.Value);
            _brainCharts.ClearSerieOnCharts(serieNumber);
        }

        void DisplayPointToDynamicGraphic(BrainInfo brainInfo)
        {
            brainInfo.second = _seconds;

            attentionLevelChart.Series[0].Points.Clear();
            if (brainInfo.attention > 0 && brainInfo.attention <= 50) attentionLevelChart.Series[0].Color = Color.Red;
            else if (brainInfo.attention > 50 && brainInfo.attention <= 80) attentionLevelChart.Series[0].Color = Color.Yellow;
            else if (brainInfo.attention > 80) attentionLevelChart.Series[0].Color = Color.Green;
            attentionLevelChart.Series[0].Points.AddXY(1.0f, brainInfo.attention);

            if (numMaxChartPoints.Value < _brainCharts.DynamicChartPointsCount) _brainCharts.DeletePointOnCharts(0, 0);
            _brainCharts.DisplayBrainInfo(brainInfo);
            _brainCharts.ScaleCharts();

            _seconds++;

            if (chkSaveRecordData.Checked) return;
            using (FileStream file = new FileStream(fullFilePathText.Text, FileMode.OpenOrCreate))
            {
                file.Seek(0, SeekOrigin.End);
                using (CsvWriter csvWriter = new CsvWriter(new StreamWriter(file), CultureInfo.CurrentCulture)) csvWriter.WriteRecord(brainInfo);
            }
        }
    }
}