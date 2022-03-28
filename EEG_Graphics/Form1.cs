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
        private const int MAX_CHARTS_POINTS = 40;
        private NeuroDeviceTGAM _neurodevice = new NeuroDeviceTGAM();

        private delegate void ChartDisplayHandler(Chart chart, string time, double data);

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
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (!_neurodevice.AreDataReading)
            {
                ClearAllGraphics();
                _neurodevice.ConnectToConnector();
                _neurodevice.ShowBrainData += DisplayDataToGraphics;
            }
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (_neurodevice.AreDataReading)
            {
                _neurodevice.DisconnectFromConnector();
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
            string time = DateTime.Now.ToString().Split()[1];
           
            BrainDataTitle[] brainKeys = currentBrainData.Keys.ToArray();
            foreach (BrainDataTitle brainKey in brainKeys)
            {
                BeginInvoke(new ChartDisplayHandler(DisplayBrainDataToChart), new object[] { _charts[brainKey], time, currentBrainData[brainKey] });
            }
        }

        private void DisplayBrainDataToChart(Chart chart, string time, double data)
        {
            if (chart.Series[0].Points.Count > MAX_CHARTS_POINTS) chart.Series[0].Points.RemoveAt(0);
            chart.Series[0].Points.AddXY(time, data);
            var points = chart.Series[0].Points;
            chart.ChartAreas[0].AxisY.Maximum = points.Max(x => x.YValues[0] + 1);
        }
    }
}