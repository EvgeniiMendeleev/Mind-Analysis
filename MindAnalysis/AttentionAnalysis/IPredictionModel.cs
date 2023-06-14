using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis
{
    internal interface IPredictionModel
    {
        double ModelError { get; }
        void Fit(DataPoint[] X);

        double Evaluate(DataPoint[] testData);
    }
}
