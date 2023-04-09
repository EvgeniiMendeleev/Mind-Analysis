using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace AttentionAnalysis
{
    internal class DataSmoothing
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
    }
}
