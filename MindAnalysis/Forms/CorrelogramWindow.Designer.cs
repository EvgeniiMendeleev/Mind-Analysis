namespace MindAnalysis.Forms
{
    partial class CorrelogramWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CorrelogramWindow));
            this.autocorrelactionChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.autocorrelactionChart)).BeginInit();
            this.SuspendLayout();
            // 
            // autocorrelactionChart
            // 
            this.autocorrelactionChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autocorrelactionChart.BackColor = System.Drawing.Color.Gray;
            this.autocorrelactionChart.BorderlineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX.Interval = 3D;
            chartArea1.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.Title = "Коэффициент автокорреляции";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea1.Name = "correlogramArea";
            this.autocorrelactionChart.ChartAreas.Add(chartArea1);
            this.autocorrelactionChart.Location = new System.Drawing.Point(12, 12);
            this.autocorrelactionChart.Name = "autocorrelactionChart";
            series1.ChartArea = "correlogramArea";
            series1.Font = new System.Drawing.Font("Times New Roman", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            series1.IsXValueIndexed = true;
            series1.Name = "Значение автокорреляции";
            series1.SmartLabelStyle.Enabled = false;
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.autocorrelactionChart.Series.Add(series1);
            this.autocorrelactionChart.Size = new System.Drawing.Size(776, 465);
            this.autocorrelactionChart.TabIndex = 0;
            this.autocorrelactionChart.Text = "chart1";
            title1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            title1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            title1.Font = new System.Drawing.Font("Times New Roman", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title1.Name = "correlogramTitle";
            title1.Text = "Коррелограмма";
            this.autocorrelactionChart.Titles.Add(title1);
            // 
            // CorrelogramWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 489);
            this.Controls.Add(this.autocorrelactionChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CorrelogramWindow";
            this.Text = "Коррелограмма загруженных данных";
            ((System.ComponentModel.ISupportInitialize)(this.autocorrelactionChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart autocorrelactionChart;
    }
}