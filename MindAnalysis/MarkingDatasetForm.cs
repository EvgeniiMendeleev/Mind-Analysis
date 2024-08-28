using MindAnalysis.GUI;
using MindAnalysis.NeuroTGAM;
using System;
using System.IO;
using System.Windows.Forms;

namespace MindAnalysis
{
    public partial class MarkingDatasetForm : Form
    {
        public MarkingDatasetForm()
        {
            InitializeComponent();
        }

        private void LoadMarkingData(object sender, EventArgs e)
        {
            using (OpenFileDialog OPF = new OpenFileDialog() { Filter = "Csv files (*.csv) | *.csv" })
            {
                if (OPF.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                markingDatasetPath.Clear();
                markingDatasetPath.Text = Path.GetFullPath(OPF.FileName);
            }
        }

        private void RunMarkingProcess(object sender, EventArgs e)
        {
            MarkingProcess markingProcess = new MarkingProcess(markingDatasetPath.Text, WaveChart.Attention, (int)dataVolume.Value);
            markingProcess.Show();
            Close();
        }
    }
}
