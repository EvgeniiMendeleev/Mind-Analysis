using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Forms
{
    public enum TrendMode
    {
        Linear,
        Giperbolic,
        Power
    }
    public enum TrendSmoothingMode
    {
        None,
        MovingAverage,
        ExponentialSmoothing
    }

    public struct TrendParams
    {
        public TrendSmoothingMode trendSmoothingMode;
        public decimal smoothingParam;
        public TrendMode trendMode;
    }
    public partial class TrendBuilder : Form
    {
        private Dictionary<string, TrendSmoothingMode> smoothingModes;
        private Dictionary<string, TrendMode> trendModes;
        public TrendBuilder()
        {
            InitializeComponent();
            smoothingModeBox.SelectedIndex = 0;
            trendModeBox.SelectedIndex = 0;
            smoothingModes = new Dictionary<string, TrendSmoothingMode>()
            {
                [smoothingModeBox.Items[0].ToString()] = TrendSmoothingMode.None,
                [smoothingModeBox.Items[1].ToString()] = TrendSmoothingMode.MovingAverage,
                [smoothingModeBox.Items[2].ToString()] = TrendSmoothingMode.ExponentialSmoothing
            };
            trendModes = new Dictionary<string, TrendMode>()
            {
                [trendModeBox.Items[0].ToString()] = TrendMode.Linear,
                [trendModeBox.Items[1].ToString()] = TrendMode.Power,
                [trendModeBox.Items[2].ToString()] = TrendMode.Giperbolic
            };
        }

        public TrendParams GetTrendParams()
        {
            TrendParams trendParams = new TrendParams();
            trendParams.trendSmoothingMode = smoothingModes[smoothingModeBox.SelectedItem.ToString()];
            trendParams.smoothingParam = smoothingParamNumeric.Value;
            trendParams.trendMode = trendModes[trendModeBox.SelectedItem.ToString()];
            return trendParams;
        }

        private void BuildTrend(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }
    }
}
