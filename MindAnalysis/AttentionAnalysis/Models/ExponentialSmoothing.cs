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

            for (int i = 0; i < _ema.Length; i++)
            {
                ModelError += Math.Pow(_ema[i].YValues[0] - X[i].YValues[0], 2);
            }
            ModelError /= _ema.Length;
            ModelError = Math.Sqrt(ModelError);
        }

        public double Predict(TimeSpan X)
        {
            return _ema.Last().XValue * _alpha + (1 - _alpha) * _ema.Last().YValues[0];
        }
    }
}


/*
 *
        public void DebugDrawPoints(Chart chart, int serie)
        {
            for (int i = 0; i < _ema.Length; i++)
            {
                chart.Series[serie].Points.Add(_ema[i]);
            }
        }
 */