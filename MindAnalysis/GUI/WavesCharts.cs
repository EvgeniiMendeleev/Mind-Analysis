using CsvHelper;
using CsvHelper.Configuration;
using NeuroTGAM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace MindAnalysis.GUI
{
    public enum WaveChart
    {
        Attention,
        Meditation,
        HighAlpha,
        LowAlpha,
        HighBeta,
        LowBeta,
        HighGamma,
        LowGamma,
        Theta,
        Delta
    }

    internal struct WaveChartDescription
    {
        public WaveChart waveChartType;
        public Chart waveChart;
    }

    internal class WavesCharts
    {
        private Dictionary<WaveChart, Chart> _charts;
        public decimal MaxPointsOnCharts { private get; set; }
        public DateTime StartTime { private get; set; }
        public WavesCharts(params WaveChartDescription[] charts)
        {
            _charts = new Dictionary<WaveChart, Chart>();
            foreach (var chart in charts)
            {
                _charts.Add(chart.waveChartType, chart.waveChart);
            }
        }

        public void AddPointOnSessionCharts(BrainInfo brainRecord)
        {
            TimeSpan elapsedTime = DateTime.Now - StartTime;
            elapsedTime = new TimeSpan(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
            brainRecord.Second = elapsedTime;

            if (MaxPointsOnCharts < _charts[WaveChart.HighAlpha].Series["HighAlpha"].Points.Count)
            {
                DeleteFirstPointsOnSessionCharts();
            }

            AddBrainRecordToCharts(brainRecord);
        }

        private void DeleteFirstPointsOnSessionCharts()
        {
            foreach (var chart in _charts.Keys)
            {
                string serieName = chart.ToString();
                _charts[chart].Series[serieName].Points.RemoveAt(0);
                _charts[chart].ResetAutoValues();
            }
        }

        private void AddBrainRecordToCharts(BrainInfo brainRecord, string seriesPref = "")
        {
            AddXY(WaveChart.Meditation, seriesPref, brainRecord.Second.ToString(), brainRecord.Meditation);
            AddXY(WaveChart.Attention, seriesPref, brainRecord.Second.ToString(), brainRecord.Attention);
            AddXY(WaveChart.HighAlpha, seriesPref, brainRecord.Second.ToString(), brainRecord.HighAlpha);
            AddXY(WaveChart.LowAlpha, seriesPref, brainRecord.Second.ToString(), brainRecord.LowAlpha);
            AddXY(WaveChart.HighBeta, seriesPref, brainRecord.Second.ToString(), brainRecord.HighBeta);
            AddXY(WaveChart.LowBeta, seriesPref, brainRecord.Second.ToString(), brainRecord.LowBeta);
            AddXY(WaveChart.HighGamma, seriesPref, brainRecord.Second.ToString(), brainRecord.HighGamma);
            AddXY(WaveChart.LowGamma, seriesPref, brainRecord.Second.ToString(), brainRecord.LowGamma);
            AddXY(WaveChart.Theta, seriesPref, brainRecord.Second.ToString(), brainRecord.Theta);
            AddXY(WaveChart.Delta, seriesPref, brainRecord.Second.ToString(), brainRecord.Delta);
        }

        private void AddXY(WaveChart waveChartType, string seriesPref, object XValue, object YValue)
        {
            string serieName = waveChartType.ToString();
            _charts[waveChartType].Series[seriesPref + serieName].Points.AddXY(XValue, YValue);
        }

        public void SetIntervalOX(WaveChart waveChart, uint interval)
        {
            string chartAreaName = waveChart.ToString();
            _charts[WaveChart.Attention].ChartAreas[chartAreaName].AxisX.Interval = interval;
        }

        public void LoadFileOnCharts(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            if (fileExtension != ".csv")
            {
                throw new Exception("WavesCharts.LoadFileToCharts(): Error extension file! CSV extension is need.");
            }
            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture) { HasHeaderRecord = true };
            using (CsvReader csvReader = new CsvReader(File.OpenText(filePath), csvConfig))
            {
                var brainRecords = csvReader.GetRecords<BrainInfo>();
                foreach (var brainRecord in brainRecords)
                {
                    AddBrainRecordToCharts(brainRecord, "Loaded");
                }
            }
        }

        public void ClearAllCharts()
        {
            foreach (var chartType in _charts.Keys)
            {
                string waveName = chartType.ToString();
                _charts[chartType].Series[waveName].Points.Clear();
                _charts[chartType].Series[$"Loaded{waveName}"].Points.Clear();
            }
        }

        public void ClearSessionRecord()
        {
            foreach (var chartType in _charts.Keys)
            {
                string waveName = chartType.ToString();
                _charts[chartType].Series[waveName].Points.Clear();
            }
        }

        public void ClearLoadedRecords()
        {
            foreach (var chartType in _charts.Keys)
            {
                string waveName = chartType.ToString();
                _charts[chartType].Series[$"Loaded{waveName}"].Points.Clear();
            }
        }
    }
}
