using NeuroTGAM;
using System.Windows.Forms.DataVisualization.Charting;

namespace EEG_Graphics
{
    public class AttentionChart
    {
        private Chart _chartAttention;
        public int DynamicChartPointsCount 
        { 
            get 
            { 
                return _chartAttention.Series[0].Points.Count; 
            } 
        }
        
        public AttentionChart(Chart chartAttention)
        {
            _chartAttention = chartAttention;
        }

        public void DisplayBrainInfo(BrainInfo brainInfo, int serie = 0)
        {
            _chartAttention.Series[serie].Points.AddXY(brainInfo.Second.ToString(), brainInfo.Attention);
        }

        public void ClearSerie(int serie)
        {
            _chartAttention.Series[serie].Points.Clear();
        }

        public void ClearAllSeries()
        {
            foreach (Series serie in _chartAttention.Series)
            {
                serie.Points.Clear();
            }
        }

        public void DeletePoint(int serie, int pointNumber)
        {
            _chartAttention.Series[serie].Points.RemoveAt(pointNumber);
            _chartAttention.ResetAutoValues();
        }
    }
}