using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
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

        private delegate void ChartDisplayHandler(Chart chart, double data);

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
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            try
            {
                _neurodevice.ConnectToConnector();
                ClearAllGraphics();
            }
            finally
            {
                stopRecordButton.Enabled = true;
                startRecordButton.Enabled = false;
            }
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (_neurodevice.AreDataReading)
            {
                MessageBox.Show("Ошибка!", "Соединение с ThinkGear Connector не установлено.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _neurodevice.DisconnectFromConnector();
            _seconds = 0;
            startRecordButton.Enabled = true;
            stopRecordButton.Enabled = false;
        }

        private void ClearAllGraphics()
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
                BeginInvoke(new ChartDisplayHandler(DisplayBrainDataToChart), new object[] { _charts[brainKey], currentBrainData[brainKey] });
            }
            _seconds++;
        }

        private void DisplayBrainDataToChart(Chart chart, double data)
        {
            stopRecordButton.Enabled = true;
            if (chart.Series[0].Points.Count >= MAX_CHARTS_POINTS) chart.Series[0].Points.RemoveAt(0);

            chart.Series[0].Points.AddXY(_seconds.ToString(), data);

            DataPointCollection points = chart.Series[0].Points;
            int scale = chart != graphicMeditation && chart != graphicAttention ? 15000 : 5;

            chart.ChartAreas[0].AxisY.Maximum = points.Max(x => x.YValues[0]) + scale;
        }

        private void UploadBrainDataFile(object sender, EventArgs e)
        {
            if (File.Exists("MyFile.mind"))
            {
                OpenFileDialog OPF = new OpenFileDialog();
                OPF.Filter = "Mind Files (*.mind) | *.mind";
                if (OPF.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader str = new StreamReader("MyFile.mind", Encoding.UTF8))
                    {
                        MessageBox.Show(str.ReadLine(), "Сообщение");
                    }
                }
                return;
            }

            FileStream file = File.Open("MyFile.mind", FileMode.Create);
            using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
            {
                for (int i = 1; i <= 20; i++)
                {
                    string str = $"Это строка №{i}";
                    Console.WriteLine(str);
                    writer.WriteLine(str);
                }
            }
        }
    }
}