using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis.Models
{
    internal class PowerRegression : IPredictionModel
    {
        double A;
        double B;
        public double ModelError { get; private set; }

        public PowerRegression() { }

        public void Fit(DataPoint[] X)
        {
            List<DataPoint> logPoints = X.Select(point => point.Clone()).ToList();
            logPoints.ForEach(point =>
            {
                point.XValue = Math.Log10(point.XValue);
                point.YValues[0] = point.YValues[0] != 0.0d ? Math.Log10(point.YValues[0]) : point.YValues[0];
            });

            int n = logPoints.Count;
            double sum_yt = logPoints.Sum(point => point.YValues[0] * point.XValue);
            double sum_y = logPoints.Sum(point => point.YValues[0]);
            double sum_t = logPoints.Sum(point => point.XValue);
            double sum_t_pow = logPoints.Sum(point => Math.Pow(point.XValue, 2));

            A = (sum_y * sum_t - n * sum_yt) / (Math.Pow(sum_t, 2) - n * sum_t_pow);
            B = (sum_y - A * sum_t) / n;
            B = Math.Pow(10, B);

            DataPoint[] predictions = new DataPoint[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                predictions[i] = new DataPoint(X[i].XValue, B * Math.Pow(X[i].XValue, A));
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
                predictions[i] = new DataPoint(testData[i].XValue, B * Math.Pow(testData[i].XValue, A));
            }
            return CalculateModelError(testData, predictions);
        }
    }
}
