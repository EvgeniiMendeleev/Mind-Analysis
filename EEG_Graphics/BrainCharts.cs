using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;
using MindFileSystem;
using NeuroTGAM;

namespace EEG_Graphics
{
    class BrainCharts
    {
        private Dictionary<BrainDataTitle, Chart> _brainCharts = new Dictionary<BrainDataTitle, Chart>();

        public BrainCharts(Dictionary<BrainDataTitle, Chart> brainCharts)
        {
            _brainCharts = brainCharts;
        }

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
                _brainCharts[brainData._title].Series[serie].Name = Path.GetFileNameWithoutExtension(OPF.FileName);
            }
        }

        public void AddPoint(BrainDataTitle chartName, int serie, DataPoint chartPoint)
        {
            //if (isDeleteNewChartDots && chart.Series[0].Points.Count >= maxGraphPointsNumeric.Value) chart.Series[0].Points.RemoveAt(0);
            Chart brainChart = _brainCharts[chartName];
            brainChart.Series[serie].Points.Add(chartPoint);

            double maxY = 0.0;
            foreach (Series series in brainChart.Series)
            {
                DataPointCollection points = brainChart.Series[serie].Points;
                double y = points.Max(point => point.YValues[0]);
                if (maxY < y) maxY = y;
            }

            int scale = chartName != BrainDataTitle.Meditation && chartName != BrainDataTitle.Attention ? 100000 : 5;
            brainChart.ChartAreas[0].AxisY.Maximum = maxY + scale;
        }


        public void SetSerieName(BrainDataTitle chartName, int serie, string name)
        {
            _brainCharts[chartName].Series[serie].Name = name;
        }

        public Chart GetChart(BrainDataTitle chartName)
        {
            return _brainCharts[chartName];
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
