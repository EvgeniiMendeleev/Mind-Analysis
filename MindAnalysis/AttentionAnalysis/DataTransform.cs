using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace AttentionAnalysis
{
    internal static class DataTransform
    {
        public static double[] MeanSmoothing(DataPoint[] inputData, int window)
        {
            List<double> outputData = new List<double>();
            for (int i = 0; i + window < inputData.Count(); i++)
            {
                double sum = 0;
                for (int j = i; j < i + window; j++)
                {
                    sum += inputData[j].YValues[0];
                }
                outputData.Add(sum / window);
            }
            return outputData.ToArray();
        }

        public static double[] Diff(DataPoint[] inputData)
        {
            double[] outputData = new double[inputData.Length - 1];
            for (int i = 0; i < inputData.Length - 1; i++)
            {
                outputData[i] = inputData[i + 1].YValues[0] - inputData[i].YValues[0];
            }
            return outputData;
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
