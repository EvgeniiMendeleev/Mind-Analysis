using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis.Models
{
    internal class LinearRegression : IPredictionModel
    {
        double k;
        double b;
        public double ModelError { get; private set; } = 0.0d;

        public LinearRegression() { }

        public void Fit(DataPoint[] X)
        {
            int n = X.Length;
            double sum_yt = X.Sum(point => point.YValues[0] * point.XValue);
            double sum_y = X.Sum(point => point.YValues[0]);
            double sum_t = X.Sum(point => point.XValue);
            double sum_t_pow = X.Sum(point => Math.Pow(point.XValue, 2));

            k = (sum_y * sum_t - n * sum_yt) / (Math.Pow(sum_t, 2) - n * sum_t_pow);
            b = (sum_y - k * sum_t) / n;

            for (int i = 0; i < X.Length; i++)
            {
                ModelError += Math.Pow((k * i + b) - X[i].YValues[0], 2);
            }
            ModelError /= X.Length;
            ModelError = Math.Sqrt(ModelError);
        }

        public double Predict(TimeSpan X)
        {
            double time = X.Seconds + X.Minutes * 60 + X.Hours * 3600;
            return k * time + b;
        }
    }
}
