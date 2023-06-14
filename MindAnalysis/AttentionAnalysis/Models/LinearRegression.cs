using System;
using System.Drawing;
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

            DataPoint[] predictions = new DataPoint[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                predictions[i] = new DataPoint(X[i].XValue, k * X[i].XValue + b);
            }

            ModelError = CalculateModelError(X, predictions);
        }

        private double CalculateModelError(DataPoint[] testData, DataPoint[] predictions)
        {
            int horizont = testData.Length;
            double modelError = 0.0d;
            for (int i = 0; i < horizont; i++)
            {
                modelError += Math.Abs(testData[i].YValues[0] - predictions[i].YValues[0]);
            }
            modelError /= horizont;
            return modelError;
        }

        public double Evaluate(DataPoint[] testData)
        {
            int horizont = testData.Length;
            DataPoint[] predictions = new DataPoint[horizont];
            for (int i = 0; i < horizont; i++)
            {
                predictions[i] = new DataPoint(testData[i].XValue, k * testData[i].XValue + b);
            }
            return CalculateModelError(testData, predictions);
        }
    }
}
