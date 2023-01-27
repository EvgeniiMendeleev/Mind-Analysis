using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using NeuroTGAM;

namespace EEG_Graphics
{
    public enum BrainChartName 
    { 
        Meditation,
        Attention,
        Alpha_Low,
        Alpha_High,
        Beta_Low,
        Beta_High,
        Gamma_Low,
        Gamma_High,
        Theta,
        Delta
    }

    public partial class BrainCharts
    {
        private Dictionary<BrainChartName, Chart> _brainCharts;
        public int DynamicChartPointsCount { get; private set; } = 0;
        public int MaxPointOnCharts { get; private set; }
        public BrainCharts(int maxPointsOnCharts)
        {
            _brainCharts = new Dictionary<BrainChartName, Chart>();
            MaxPointOnCharts = maxPointsOnCharts;
        }

        public void AddGraphic(BrainChartName chartName, Chart chart) => _brainCharts.Add(chartName, chart);

        public void DisplayBrainInfo(BrainInfo brainInfo, int serie = 0)
        {
            if (MaxPointOnCharts < DynamicChartPointsCount) DeletePointOnCharts(0, 0);

            DynamicChartPointsCount++;
            _brainCharts[BrainChartName.Attention].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.attention);
            _brainCharts[BrainChartName.Meditation].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.meditation);
            _brainCharts[BrainChartName.Alpha_High].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.alphaHigh);
            _brainCharts[BrainChartName.Alpha_Low].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.alphaLow);
            _brainCharts[BrainChartName.Beta_High].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.betaHigh);
            _brainCharts[BrainChartName.Beta_Low].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.betaLow);
            _brainCharts[BrainChartName.Gamma_High].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.gammaHigh);
            _brainCharts[BrainChartName.Gamma_Low].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.gammaLow);
            _brainCharts[BrainChartName.Delta].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.delta);
            _brainCharts[BrainChartName.Theta].Series[serie].Points.AddXY(DynamicChartPointsCount, brainInfo.theta);

            ScaleCharts();
        }

        public void ScaleCharts()
        {
            double maxY = 0.0;
            foreach (var chart in _brainCharts)
            {
                foreach (Series series in chart.Value.Series)
                {
                    if (series.Points.Count == 0) continue;
                    double y = series.Points.Max(point => point.YValues[0]);
                    if (maxY < y) maxY = y;
                }

                int scale = chart.Key != BrainChartName.Meditation && chart.Key != BrainChartName.Attention ? 100000 : 10;
                chart.Value.ChartAreas[0].AxisY.Maximum = maxY + scale;
            }
        }

        public void ClearSerieOnCharts(int serie)
        {
            var charts = _brainCharts.Values;
            foreach (Chart chart in charts) chart.Series[serie].Points.Clear();
        }

        public void ClearAllSeries()
        {
            var charts = _brainCharts.Values;
            foreach (Chart chart in charts)
            {
                foreach (Series serie in chart.Series) serie.Points.Clear();
            }
        }

        public void DeletePointOnCharts(int serie, int pointNumber)
        {
            foreach (var chart in _brainCharts)
            {
                chart.Value.Series[serie].Points.RemoveAt(pointNumber);
            }
        }
    }
}