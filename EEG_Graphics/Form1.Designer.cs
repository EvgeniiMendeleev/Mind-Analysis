
namespace EEG_Graphics
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.graphicLowAlpha = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.testName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.graphicLowAlpha)).BeginInit();
            this.SuspendLayout();
            // 
            // graphicLowAlpha
            // 
            this.graphicLowAlpha.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.graphicLowAlpha.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.graphicLowAlpha.Legends.Add(legend1);
            this.graphicLowAlpha.Location = new System.Drawing.Point(17, 67);
            this.graphicLowAlpha.Name = "graphicLowAlpha";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "lowAlphaPoints";
            this.graphicLowAlpha.Series.Add(series1);
            this.graphicLowAlpha.Size = new System.Drawing.Size(750, 311);
            this.graphicLowAlpha.TabIndex = 0;
            this.graphicLowAlpha.Text = "chart1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(565, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Считать данные с файла";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnReadDataFromFiles);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название теста:";
            // 
            // testName
            // 
            this.testName.Location = new System.Drawing.Point(183, 12);
            this.testName.Name = "testName";
            this.testName.Size = new System.Drawing.Size(376, 22);
            this.testName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Low-alpha";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(789, 413);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.testName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.graphicLowAlpha);
            this.Name = "Form1";
            this.Text = "ЭЭГ графики";
            ((System.ComponentModel.ISupportInitialize)(this.graphicLowAlpha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart graphicLowAlpha;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox testName;
        private System.Windows.Forms.Label label2;
    }
}

