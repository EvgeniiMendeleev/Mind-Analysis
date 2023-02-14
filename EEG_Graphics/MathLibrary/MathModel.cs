using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLibrary
{
    internal class MathModel
    {
        private static MathModel instance;

        private MathModel() { }

        public static MathModel GetSystem()
        {
            if (instance == null) instance = new MathModel();
            return instance;
        }

        public double[] ExponentialSmoothing(double[] inputData, float alpha = 0.02f)
        {
            double[] ema = new double[inputData.Length];
            ema[0] = inputData[0];

            for (int i = 0; i < inputData.Length - 1; i++)
            {
                ema[i + 1] = alpha * inputData[i] + (1 - alpha) * ema[i];
            }
            return ema;
        }

        public double[] MaxMinScaler(double[] inputData)
        {
            double maxValue = inputData.Max();
            double minValue = inputData.Min();

            double[] outputData = new double[inputData.Length];

            for (int i = 0; i < inputData.Length; i++)
            {
                outputData[i] = (inputData[i] - minValue) / (maxValue - minValue);
            }

            return outputData;
        }

        public double[] Z_Scaler(double[] inputData)
        {
            double meanSum = inputData.Sum() / inputData.Length;
            double dispersiya = inputData.Sum(data => Math.Pow(data - meanSum, 2)) / (inputData.Length - 1);

            double[] outputData = new double[inputData.Length];

            for (int i = 0; i < inputData.Length; i++)
            {
                outputData[i] = (inputData[i] - meanSum) / Math.Sqrt(dispersiya);
            }
            return outputData;
        }

        public double[] SoftMaxScaler(double[] inputData)
        {
            double[] scaledDatas = Z_Scaler(inputData);
            double[] outputData = new double[inputData.Length];

            for (int i = 0; i < outputData.Length; i++)
            {
                outputData[i] = 1 / (1 + Math.Exp(-scaledDatas[i]));
            }
            return outputData;
        }
    }
}
