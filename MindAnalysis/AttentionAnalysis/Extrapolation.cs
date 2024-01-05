using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace AttentionAnalysis
{
    internal static class Extrapolation
    {
        public static (double k, double b) LinearRegression(DataPoint[] inputPoints)
        {
            double k = 0, b = 0;

            int n = inputPoints.Length;
            double sum_yt = inputPoints.Sum(point => point.YValues[0] * point.XValue);
            double sum_y = inputPoints.Sum(point => point.YValues[0]);
            double sum_t = inputPoints.Sum(point => point.XValue);
            double sum_t_pow = inputPoints.Sum(point => Math.Pow(point.XValue, 2));

            k = (sum_y * sum_t - n * sum_yt) / (Math.Pow(sum_t, 2) - n * sum_t_pow);
            b = (sum_y - k * sum_t) / n;

            return (k, b);
        }

        public static (double A, double B) PowerRegression(DataPoint[] inputPoints)
        {
            List<DataPoint> logPoints = new List<DataPoint>(inputPoints);
            logPoints.ForEach(point =>
            {
                point.XValue = Math.Log10(point.XValue);
                point.YValues[0] = Math.Log10(point.YValues[0]);
            });

            (double A, double B) = LinearRegression(logPoints.ToArray());
            return (A, Math.Pow(10, B));
        }

        public static (double a0, double a1) GiperbolicRegression(DataPoint[] inputPoints)
        {
            double n = inputPoints.Length;
            double sumY = inputPoints.Sum(point => point.YValues[0]);
            double sumX = inputPoints.Sum(point => 1.0d / point.XValue);
            double sumYX = inputPoints.Sum(point => point.YValues[0] / point.XValue);
            double sumX_Pow = inputPoints.Sum(point => 1.0d / Math.Pow(point.XValue, 2));

            double a1 = (sumY * sumX - n * sumYX) / (Math.Pow(sumX, 2) - n * sumX_Pow);
            double a0 = (sumY - a1 * sumX) / n;

            return (a0, a1);
        }
    }
}
