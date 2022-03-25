using System;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using NeuroTGAM;

namespace EEG_Graphics
{
    public partial class Form1 : Form
    {
        private NeuroDeviceTGAM neurodevice = new NeuroDeviceTGAM();

        public Form1()
        {
            InitializeComponent();
        }

        private void StartToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (!neurodevice.AreDataReading)
            {
                ClearAllGraphics();
                neurodevice.ConnectToConnector();
                neurodevice.ShowBrainData += DisplayDataToGraphics;
            }
        }

        private void StopToReadDataFromNeurodevice(object sender, EventArgs e)
        {
            if (neurodevice.AreDataReading) neurodevice.DisconnectFromConnector();
        }

        private void ClearAllGraphics()
        {
            graphicLowAlpha.Series[0].Points.Clear();
            graphicHighAlpha.Series[0].Points.Clear();

            graphicLowBeta.Series[0].Points.Clear();
            graphicHighBeta.Series[0].Points.Clear();

            graphicLowGamma.Series[0].Points.Clear();
            graphicHighGamma.Series[0].Points.Clear();

            graphicTheta.Series[0].Points.Clear();
            graphicDelta.Series[0].Points.Clear();

            graphicAttention.Series[0].Points.Clear();
            graphicMeditation.Series[0].Points.Clear();
        }

        private void DisplayDataToGraphics(Dictionary<BrainDataTitle, double> currentBrainData)
        {
            string time = DateTime.Now.ToString().Split()[1];

            graphicLowAlpha.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Low_Alpha]);
            graphicHighAlpha.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.High_Alpha]);

            graphicLowBeta.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Low_Beta]);
            graphicHighBeta.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.High_Beta]);

            graphicLowGamma.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Low_Gamma]);
            graphicHighGamma.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.High_Gamma]);

            graphicTheta.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Theta]);
            graphicDelta.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Delta]);

            graphicAttention.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Attention]);
            graphicMeditation.Series[0].Points.AddXY(time, (int)currentBrainData[BrainDataTitle.Meditation]);
        }
    }
}