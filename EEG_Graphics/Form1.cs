using System;
using System.IO;
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
            ClearAllGraphics();
            
            _neurodevice.ConnectToConnector();

            startRecordButton.Enabled = false;
            stopRecordButton.Enabled = true;
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (_neurodevice.AreDataReading)
            {
                _neurodevice.DisconnectFromConnector();
                _seconds = 0;
                startRecordButton.Enabled = true;
                stopRecordButton.Enabled = false;
            }
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
            if (chart.Series[0].Points.Count >= MAX_CHARTS_POINTS) chart.Series[0].Points.RemoveAt(0);

            chart.Series[0].Points.AddXY(_seconds.ToString(), data);

            DataPointCollection points = chart.Series[0].Points;
            int scale = chart != graphicMeditation && chart != graphicAttention ? 15000 : 5;

            chart.ChartAreas[0].AxisY.Maximum = points.Max(x => x.YValues[0]) + scale;
        }
    }
}