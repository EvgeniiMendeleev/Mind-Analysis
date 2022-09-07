using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using MindFileSystem;
using NeuroTGAM;
using System;

namespace EEG_Graphics
{
    class BrainCharts
    {
        private Dictionary<EEG_Title, Chart> _brainCharts = new Dictionary<EEG_Title, Chart>();
        public BrainCharts(Dictionary<EEG_Title, Chart> brainCharts) => _brainCharts = brainCharts;

        public void ClearSerieOnCharts(int serie)
        {
            var charts = _brainCharts.Values;
            foreach (Chart chart in charts) chart.Series[serie].Points.Clear();
        }

        public void DisplayMindFileOnCharts(MindFileReader mindFile, int serie)
        {
            foreach (var brainData in mindFile)
            {
                AddPoint(brainData._title, serie, new DataPoint(brainData._time, brainData._brainValue));
                string serieName = !_brainCharts[brainData._title].Series.IsUniqueName(mindFile.FileName) ? $"{mindFile.FileName} (1)" : mindFile.FileName;
                _brainCharts[brainData._title].Series[serie].Name = serieName;
            }
        }

        public int PointsCount(EEG_Title eegTitle, int serie) => _brainCharts[eegTitle].Series[0].Points.Count;

        public void DeletePoint(EEG_Title eegTitle, int serie, int pointPosition) => _brainCharts[eegTitle].Series[serie].Points.RemoveAt(pointPosition);

        public void AddPoint(EEG_Title chartName, int serie, DataPoint chartPoint)
        {
            Chart brainChart = _brainCharts[chartName];
            brainChart.Series[serie].Points.AddXY(chartPoint.XValue.ToString(), chartPoint.YValues[0]);

            double maxY = 0.0;
            foreach (Series series in brainChart.Series)
            {
                if (series.Points.Count == 0) continue;
                double y = series.Points.Max(point => point.YValues[0]);
                if (maxY < y) maxY = y;
            }

            int scale = chartName != EEG_Title.Meditation && chartName != EEG_Title.Attention ? 100000 : 10;
            brainChart.ChartAreas[0].AxisY.Maximum = maxY + scale;
        }

        public void ClearAllSeries()
        {
            var charts = _brainCharts.Values;
            foreach (Chart chart in charts)
            {
                foreach(Series serie in chart.Series) serie.Points.Clear();
            }
        }
    }
}