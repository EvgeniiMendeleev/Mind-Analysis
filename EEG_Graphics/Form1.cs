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

        private void OnReadDataFromFiles(object sender, EventArgs e)
        {
            graphicLowAlpha.Series[0].Points.Clear();
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

            StreamReader streamReader = new StreamReader(File.Open(filePath, FileMode.Open));

            while (!streamReader.EndOfStream)
            {
                string fileLine = streamReader.ReadLine();
                string[] testData = Array.ConvertAll(fileLine.Split('='), new Converter<string, string>(str => str.Trim(' ')));

                string time = testData[1].Split(' ')[1].Trim(' ');
                dynamic jsonObject = JObject.Parse(testData[0]);
                
                if (jsonObject.eegPower == null) continue;

                graphicLowAlpha.Series[0].Points.AddXY(time, (int)jsonObject.eegPower["lowAlpha"]);
            }

            streamReader.Close();
        }
    }
}