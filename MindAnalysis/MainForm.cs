using MindAnalysis.GUI;
using MindAnalysis.NeuroTGAM;
using NeuroTGAM;
using System;
using System.IO;
using System.Windows.Forms;

namespace MindAnalysis
{
    public partial class MainForm : Form
    {
        private delegate void SessionChartDisplay(BrainInfo brainInfo);
        
        private Neurointerface _neurodevice;
        private WavesCharts _wavesCharts;
        private MindFile _mindFile;

        public MainForm()
        {
            InitializeComponent();
            InitializeChartsStorage();
        }

        private void InitializeChartsStorage()
        {
            _wavesCharts = new WavesCharts
                (
                new WaveChartDescription { waveChartType = WaveChart.Attention, waveChart = AttentionAndMeditationChart },
                new WaveChartDescription { waveChartType = WaveChart.Meditation, waveChart = AttentionAndMeditationChart },
                new WaveChartDescription { waveChartType = WaveChart.HighAlpha, waveChart = AlphaWaveChart },
                new WaveChartDescription { waveChartType = WaveChart.LowAlpha, waveChart = AlphaWaveChart },
                new WaveChartDescription { waveChartType = WaveChart.HighBeta, waveChart = BetaWaveChart },
                new WaveChartDescription { waveChartType = WaveChart.LowBeta, waveChart = BetaWaveChart },
                new WaveChartDescription { waveChartType = WaveChart.HighGamma, waveChart = GammaWaveChart },
                new WaveChartDescription { waveChartType = WaveChart.LowGamma, waveChart = GammaWaveChart },
                new WaveChartDescription { waveChartType = WaveChart.Theta, waveChart = ThetaAndDeltaWavesChart },
                new WaveChartDescription { waveChartType = WaveChart.Delta, waveChart = ThetaAndDeltaWavesChart }
                );
        }

        private void StartRecording(object sender, EventArgs e)
        {
            try
            {
                _neurodevice = new Neurointerface();
                _neurodevice.OnBrainDataReceived += OnBrainDataReceived;
                _neurodevice.Connect();
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к нейроинтерфейсу", "Ошибка соединения!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _wavesCharts.MaxPointsOnCharts = numMaxChartPoints.Value;
            _wavesCharts.StartTime = DateTime.Now;
            _wavesCharts.ClearSessionRecord();

            if (chkSaveRecords.Checked) _mindFile = new MindFile(txtBoxFilePath.Text);

            UserControlSystem.GetSystem()
                .Disable(btnStartRecord)
                .Enable(btnStopRecord)
                .Disable(numMaxChartPoints)
                .Disable(chkSaveRecords)
                .Disable(txtBoxFilePath)
                .Disable(mainMenuStrip);
        }

        private void OnBrainDataReceived(BrainInfo brainInfo)
        {
            Invoke(new SessionChartDisplay(_wavesCharts.AddPointOnSessionCharts), brainInfo);
            _mindFile?.AppendRecord(brainInfo);
        }

        private void StopRecording(object sender, EventArgs e)
        {
            if (!_neurodevice.IsDataReading)
            {
                MessageBox.Show("Соединение с ThinkGear Connector не установлено.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _neurodevice.CloseConnection();
            _neurodevice = null;
            _wavesCharts.ClearSessionRecord();
            _mindFile?.Close();
            _mindFile = null;

            UserControlSystem.GetSystem()
                .Disable(btnStopRecord)
                .Enable(btnStartRecord)
                .Enable(numMaxChartPoints)
                .Enable(chkSaveRecords)
                .Enable(txtBoxFilePath)
                .Enable(mainMenuStrip);
        }

        private void OnChangedValueInSaveMindRecord(object sender, EventArgs e)
        {
            UserControlSystem GUISystem = UserControlSystem.GetSystem();

            if (chkSaveRecords.Checked) GUISystem.Enable(btnChangeSavePath).Enable(txtBoxFilePath);
            else GUISystem.Disable(btnChangeSavePath).Disable(txtBoxFilePath);
        }

        private void LoadFileOnChart(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Csv files (*.csv) | *.csv" })
            {
                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                _wavesCharts.LoadFileOnCharts(openFileDialog.FileName);
            }
        }

        private void SaveFilePathForRecording(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Csv files (*.csv) | *.csv" };
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (saveFileDialog.FileName.Length > 3) txtBoxFilePath.Text = Path.GetFullPath(saveFileDialog.FileName);
            else MessageBox.Show("Имя файла слишком маленькое!", "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ClearLoadedRecords(object sender, EventArgs e) => _wavesCharts.ClearLoadedRecords();
    }
}