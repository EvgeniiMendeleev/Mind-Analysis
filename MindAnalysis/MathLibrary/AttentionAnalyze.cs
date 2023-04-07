using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindLinkAnalyze
{
    internal static class AttentionAnalyze
    {
        public static DataPoint[] ExponentialSmoothing(DataPoint[] inputData, float alpha = 0.02f)
        {
            DataPoint[] ema = inputData.Select(point => point.Clone()).ToArray();
            for (int i = 0; i < inputData.Length - 1; i++)
            {
                ema[i + 1].YValues[0] = alpha * inputData[i].YValues[0] + (1 - alpha) * ema[i].YValues[0];
            }
            return ema;
        }

        public static (double k, double b) GetLinearFunctionParams(DataPoint[] inputPoints)
        {
            double k = 0, b = 0;

            int n = inputPoints.Length;
            double sum_yt = inputPoints.Select(point => point.YValues[0] * point.XValue).Sum();
            double sum_y = inputPoints.Select(point => point.YValues[0]).Sum();
            double sum_t = inputPoints.Select(point => point.XValue).Sum();
            double sum_t_pow = inputPoints.Select(point => Math.Pow(point.XValue, 2)).Sum();

            k = (n * sum_yt - sum_y * sum_t) / (n * sum_t_pow - Math.Pow(sum_t, 2));
            b = (sum_y - k * sum_t) / n;

            return (k, b);
        }

        public static (double A, double B) GetPowFunctionParams(DataPoint[] inputPoints)
        {
            List<DataPoint> logPoints = new List<DataPoint>(inputPoints);
            logPoints.ForEach(point =>
            {
                point.XValue = Math.Log10(point.XValue);
                point.YValues[0] = Math.Log10(point.YValues[0]);
            });

            (double A, double B) = GetLinearFunctionParams(logPoints.ToArray());

            return (A, Math.Pow(10, B));
        }

        public static (double a0, double a1) GetGiperbolicParams(DataPoint[] inputPoints)
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

        public static float[] MaxMinScaler(float[] inputData)
        {
            float maxValue = inputData.Max();
            float minValue = inputData.Min();

            float[] outputData = new float[inputData.Length];

            for (int i = 0; i < inputData.Length; i++)
            {
                outputData[i] = (inputData[i] - minValue) / (maxValue - minValue);
            }

            return outputData;
        }

        public static float[] Z_Scaler(float[] inputData)
        {
            float meanSum = inputData.Sum() / inputData.Length;
            float dispersiya = Convert.ToSingle(inputData.Sum(data => Math.Pow(data - meanSum, 2)) / (inputData.Length - 1));

            float[] outputData = new float[inputData.Length];

            for (int i = 0; i < inputData.Length; i++)
            {
                outputData[i] = Convert.ToSingle((inputData[i] - meanSum) / Math.Sqrt(dispersiya));
            }
            return outputData;
        }

        public static float[] SoftMaxScaler(float[] inputData)
        {
            float[] scaledDatas = Z_Scaler(inputData);
            float[] outputData = new float[inputData.Length];

            outputData = scaledDatas.Select(data => Convert.ToSingle(1 / (1 + Math.Exp(-data)))).ToArray();

            for (int i = 0; i < outputData.Length; i++)
            {
                outputData[i] = Convert.ToSingle(1 / (1 + Math.Exp(-scaledDatas[i])));
            }
            return outputData;
        }
    }
}
