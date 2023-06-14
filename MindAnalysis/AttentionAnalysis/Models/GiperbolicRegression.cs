using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis.Models
{
    internal class GiperbolicRegression : IPredictionModel
    {
        double a0;
        double a1;

        public GiperbolicRegression() { }

        public double ModelError { get; private set; }

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

        public void Fit(DataPoint[] X)
        {
            double n = X.Length;
            double sumY = X.Sum(point => point.YValues[0]);
            double sumX = X.Sum(point => 1.0d / point.XValue);
            double sumYX = X.Sum(point => point.YValues[0] / point.XValue);
            double sumX_Pow = X.Sum(point => 1.0d / Math.Pow(point.XValue, 2));

            a1 = (sumY * sumX - n * sumYX) / (Math.Pow(sumX, 2) - n * sumX_Pow);
            a0 = (sumY - a1 * sumX) / n;

            DataPoint[] predictions = new DataPoint[X.Length];
            for (int i = 0; i < predictions.Length; i++)
            {
                predictions[i] = new DataPoint(X[i].XValue, a0 + a1 / X[i].XValue);
            }

            ModelError = CalculateModelError(X, predictions);
        }

        public double Evaluate(DataPoint[] testData)
        {
            int horizont = testData.Length;
            DataPoint[] predictions = new DataPoint[horizont];
            for (int i = 0; i < horizont; i++)
            {
                predictions[i] = new DataPoint(testData[i].XValue, (a0 + a1 / testData[i].XValue));
            }
            return CalculateModelError(testData, predictions);
        }
    }
}
