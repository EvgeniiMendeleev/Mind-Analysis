using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.AttentionAnalysis
{
    internal interface IPredictionModel
    {
        double ModelError { get; }
        void Fit(DataPoint[] X);
        double Predict(TimeSpan X);
    }
}
