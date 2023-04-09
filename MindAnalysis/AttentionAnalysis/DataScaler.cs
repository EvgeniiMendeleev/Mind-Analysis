using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttentionAnalysis
{
    internal static class DataScaler
    {

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
