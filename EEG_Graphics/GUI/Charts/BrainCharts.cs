using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using NeuroTGAM;
using System.Drawing;

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
        Delta,
        ChartsComparison,
        ParamsComparison
    }

    public partial class BrainCharts
    {
        private Dictionary<BrainChartName, Chart> _brainCharts;
        public int DynamicChartPointsCount { get { return _brainCharts[BrainChartName.Attention].Series[0].Points.Count; } }
        public BrainCharts() => _brainCharts = new Dictionary<BrainChartName, Chart>();

        public void ClearAllSeriesOnChart(BrainChartName chartName) => _brainCharts[chartName].Series.Clear();

        public void AddChart(BrainChartName chartName, Chart chart) => _brainCharts.Add(chartName, chart);

        public void CreateSerieOnComparisonChart(BrainChartName chartName, SeriesChartType chartType, string serieName)
        {
            _brainCharts[chartName].Series.Add(new Series(serieName));
            _brainCharts[chartName].Series[serieName].ChartType = chartType;
            _brainCharts[chartName].Series[serieName].BorderWidth = 3;
            _brainCharts[chartName].Series[serieName].IsVisibleInLegend = true;
        }

        public void ClearSerieOnComparisonChart(BrainChartName chartName, string serie)
        {
            _brainCharts[chartName].Series[serie].Points.Clear();
            Series selectedSerie = _brainCharts[chartName].Series[serie];
            _brainCharts[chartName].Series.Remove(selectedSerie);
        }

        public void AddPointOnComparisonChart(BrainChartName chartName, string serie, DataPoint point) => _brainCharts[chartName].Series.FindByName(serie).Points.Add(point);

        public void SetParamsComprisonAxesName(string firstName, string secondName)
        {
            _brainCharts[BrainChartName.ParamsComparison].ChartAreas[0].AxisX.Title = firstName;
            _brainCharts[BrainChartName.ParamsComparison].ChartAreas[0].AxisY.Title = secondName;
        }

        public void DisplayBrainInfo(BrainInfo brainInfo, int serie = 0)
        {
            _brainCharts[BrainChartName.Attention].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.Attention);
            _brainCharts[BrainChartName.Meditation].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.Meditation);
            _brainCharts[BrainChartName.Alpha_High].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.AlphaHigh);
            _brainCharts[BrainChartName.Alpha_Low].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.AlphaLow);
            _brainCharts[BrainChartName.Beta_High].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.BetaHigh);
            _brainCharts[BrainChartName.Beta_Low].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.BetaLow);
            _brainCharts[BrainChartName.Gamma_High].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.GammaHigh);
            _brainCharts[BrainChartName.Gamma_Low].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.GammaLow);
            _brainCharts[BrainChartName.Delta].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.Delta);
            _brainCharts[BrainChartName.Theta].Series[serie].Points.AddXY(brainInfo.Second, brainInfo.Theta);
        }

        public void ScaleCharts()
        {
            double maxY = 0.0;
            foreach (var chart in _brainCharts)
            {
                if (chart.Key == BrainChartName.ChartsComparison || chart.Key == BrainChartName.ParamsComparison) continue;
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
            foreach (var chart in _brainCharts)
            {
                if (chart.Key == BrainChartName.ChartsComparison || chart.Key == BrainChartName.ParamsComparison) continue;
                chart.Value.Series[serie].Points.Clear();
            }
        }

        public void ClearAllSeries()
        {
            foreach (var chart in _brainCharts)
            {
                if (chart.Key == BrainChartName.ChartsComparison || chart.Key == BrainChartName.ParamsComparison) continue;
                foreach (Series serie in chart.Value.Series) serie.Points.Clear();
            }
        }

        public void DeletePointOnCharts(int serie, int pointNumber)
        {
            foreach (var chart in _brainCharts)
            {
                if (chart.Key == BrainChartName.ChartsComparison || chart.Key == BrainChartName.ParamsComparison) continue;
                chart.Value.Series[serie].Points.RemoveAt(pointNumber);
                chart.Value.ResetAutoValues();
            }
        }
    }
}