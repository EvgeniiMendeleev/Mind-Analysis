using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis.Models
{
    internal class PowerRegression
    {
        double A;
        double B;

        public void Fit(DataPoint[] X)
        {
            List<DataPoint> logPoints = new List<DataPoint>(X);
            logPoints.ForEach(point =>
            {
                point.XValue = Math.Log10(point.XValue);
                point.YValues[0] = Math.Log10(point.YValues[0]);
            });

            int n = logPoints.Count;
            double sum_yt = logPoints.Sum(point => point.YValues[0] * point.XValue);
            double sum_y = logPoints.Sum(point => point.YValues[0]);
            double sum_t = logPoints.Sum(point => point.XValue);
            double sum_t_pow = logPoints.Sum(point => Math.Pow(point.XValue, 2));

            A = (sum_y * sum_t - n * sum_yt) / (Math.Pow(sum_t, 2) - n * sum_t_pow);
            B = (sum_y - A * sum_t) / n;
            B = Math.Pow(10, B);
        }

        public double Predict(TimeSpan X)
        {
            double time = X.Seconds + X.Minutes * 60 + X.Hours * 3600;
            return B * Math.Pow(time, A);
        }
    }
}
