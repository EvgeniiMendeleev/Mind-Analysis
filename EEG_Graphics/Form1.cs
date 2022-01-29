using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EEG_Graphics
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void DisplayDataToGraphics(string time, dynamic jsonObject)
        {
            graphicLowAlpha.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["lowAlpha"]);
            graphicHighAlpha.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["highAlpha"]);

            graphicLowBeta.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["lowBeta"]);
            graphicHighBeta.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["highBeta"]);

            graphicLowGamma.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["lowGamma"]);
            graphicHighGamma.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["highGamma"]);

            graphicTheta.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["theta"]);
            graphicDelta.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["delta"]);

            graphicAttention.Series[0].Points.AddXY(time, (int)jsonObject.eSense["attention"]);
            graphicMeditation.Series[0].Points.AddXY(time, (int)jsonObject.eSense["meditation"]);
        }

        private void OnReadDataFromFiles(object sender, EventArgs e)
        {
            //Менделеев Состояние спокойствия открытые глаза
            if (string.IsNullOrEmpty(testName.Text))
            {
                MessageBox.Show("Нет имя теста", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string filePath = $"..\\Tests\\{testName.Text}.txt";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Теста нет в папке", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }

            ClearAllGraphics();

            StreamReader streamReader = new StreamReader(File.Open(filePath, FileMode.Open));

            while (!streamReader.EndOfStream)
            {
                string fileLine = streamReader.ReadLine();
                string[] testData = Array.ConvertAll(fileLine.Split('='), new Converter<string, string>(str => str.Trim(' ')));

                string time = testData[1].Split(' ')[1].Trim(' ');
                dynamic jsonObject = JObject.Parse(testData[0]);
                
                if (jsonObject.blinkStrength != null || jsonObject.mentalEffort != null || jsonObject.familiarity != null) continue;

                DisplayDataToGraphics(time, jsonObject);
            }

            streamReader.Close();
        }
    }
}