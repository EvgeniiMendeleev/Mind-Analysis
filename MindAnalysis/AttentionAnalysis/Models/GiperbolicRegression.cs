using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis.Models
{
    internal class GiperbolicRegression
    {
        double a0;
        double a1;

        public void Fit(DataPoint[] X)
        {
            double n = X.Length;
            double sumY = X.Sum(point => point.YValues[0]);
            double sumX = X.Sum(point => 1.0d / point.XValue);
            double sumYX = X.Sum(point => point.YValues[0] / point.XValue);
            double sumX_Pow = X.Sum(point => 1.0d / Math.Pow(point.XValue, 2));

            a1 = (sumY * sumX - n * sumYX) / (Math.Pow(sumX, 2) - n * sumX_Pow);
            a0 = (sumY - a1 * sumX) / n;
        }

        public double Predict(TimeSpan X)
        {
            double time = X.Seconds + X.Minutes * 60 + X.Hours * 3600;
            return a0 + a1 / time;
        }
    }
}
