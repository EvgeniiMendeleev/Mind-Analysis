using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis.Models
{
    internal class ExponentialSmoothing : IPredictionModel
    {
        private double _alpha;
        private bool _isMeanValue;
        public double ModelError { get; private set; } = 0.0d;
        private DataPoint[] _ema;

        public ExponentialSmoothing(double alpha = 0.0f, bool isMeanValue = false)
        {
            _alpha = alpha;
            _isMeanValue = isMeanValue;
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

        public void Fit(DataPoint[] X)
        {
            _ema = X.Select(point => point.Clone()).ToArray();
            if (_isMeanValue)
            {
                _ema[0].YValues[0] = X.Average(point => point.YValues[0]);
            }
            for (int i = 0; i < X.Length - 1; i++)
            {
                _ema[i + 1].YValues[0] = _alpha * X[i].YValues[0] + (1 - _alpha) * _ema[i].YValues[0];
            }

            ModelError = CalculateModelError(X, _ema);
        }

        public void DebugDrawPoints(Chart chart, int serie)
        {
            for (int i = 0; i < _ema.Length; i++)
            {
                chart.Series[serie].Points.Add(_ema[i]);
            }
        }

        public double Evaluate(DataPoint[] testData)
        {
            int horizont = testData.Length;
            DataPoint[] new_ema = new DataPoint[horizont];
            new_ema[0] = _ema.Last().Clone();

            for (int i = 0; i < horizont - 1; i++)
            {
                new_ema[i + 1] = new DataPoint();
                new_ema[i + 1].YValues[0] = _alpha * testData[i].YValues[0] + (1 - _alpha) * new_ema[i].YValues[0];
            }

            return CalculateModelError(testData, new_ema);
        }
    }
}

//Вычисление среднеквадратичной ошибки
/*double modelError = 0.0d;
for (int i = 0; i < predictions.Length; i++)
{
    modelError += Math.Pow(predictions[i].YValues[0] - input[i].YValues[0], 2);
}
modelError /= predictions.Length;
modelError = Math.Sqrt(modelError);

return modelError;*/