using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.Forms
{
    public partial class CorrelogramWindow : Form
    {
        public CorrelogramWindow()
        {
            InitializeComponent();
        }

        public void BuildCorrelogram(DataPoint[] points, string fileName = "Загруженные данные")
        {
            int autocorrelationCoefficientsCount = points.Length / 4;
            for (int lag = 1; lag < autocorrelationCoefficientsCount; lag++)
            {
                DataPoint[] notLagPoints = points.Reverse().Skip(lag).Reverse().ToArray();
                DataPoint[] lagPoints = points.Skip(lag).ToArray();

                double meanNotLagPoints = notLagPoints.Average(point => point.YValues[0]);
                double meanLagPoints = lagPoints.Average(point => point.YValues[0]);

                double dispersionNotLagPoints = notLagPoints.Sum(point => Math.Pow(point.YValues[0] - meanNotLagPoints, 2));
                double dispersionLagPoints = lagPoints.Sum(point => Math.Pow(point.YValues[0] - meanLagPoints, 2));

                double covariation = notLagPoints.Zip(lagPoints, (lagPoint, notLagPoint) => (lagPoint.YValues[0] - meanLagPoints) * (notLagPoint.YValues[0] - meanNotLagPoints)).Sum();

                double r = covariation / Math.Sqrt(dispersionNotLagPoints * dispersionLagPoints);
                autocorrelactionChart.Series[0].Points.AddY(r);
            }

            Text = $"Коррелограмма {fileName}";
        }
    }
}
