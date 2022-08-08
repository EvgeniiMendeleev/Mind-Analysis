using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
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

        public void ClearSerieOnChart(BrainDataTitle chartName, int serie)
        {
            _brainCharts[chartName].Series[serie].Points.Clear();
        }

        public void AddPoint(BrainDataTitle chartName, int serie, DataPoint chartPoint)
        {
            _brainCharts[chartName].Series[serie].Points.Add(chartPoint);
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
