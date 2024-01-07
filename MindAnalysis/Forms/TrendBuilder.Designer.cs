namespace Forms
{
    partial class TrendBuilder
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
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrendBuilder));
            this.smoothingParamNumeric = new System.Windows.Forms.NumericUpDown();
            this.smoothingModeBox = new System.Windows.Forms.ComboBox();
            this.trendModeBox = new System.Windows.Forms.ComboBox();
            this.btnBuildTrend = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smoothingParamNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 27);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(179, 16);
            label1.TabIndex = 0;
            label1.Text = "Произвести сглаживание:";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(87, 122);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(122, 16);
            label2.TabIndex = 2;
            label2.Text = "Построить тренд:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 56);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(191, 16);
            label3.TabIndex = 4;
            label3.Text = "Коэффициент сглаживания:";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.smoothingParamNumeric);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(this.smoothingModeBox);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(547, 97);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Параметры сглаживания";
            // 
            // smoothingParamNumeric
            // 
            this.smoothingParamNumeric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothingParamNumeric.DecimalPlaces = 5;
            this.smoothingParamNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.smoothingParamNumeric.Location = new System.Drawing.Point(215, 54);
            this.smoothingParamNumeric.Maximum = new decimal(new int[] {
            366,
            0,
            0,
            0});
            this.smoothingParamNumeric.Name = "smoothingParamNumeric";
            this.smoothingParamNumeric.Size = new System.Drawing.Size(93, 22);
            this.smoothingParamNumeric.TabIndex = 5;
            // 
            // smoothingModeBox
            // 
            this.smoothingModeBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.smoothingModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.smoothingModeBox.FormattingEnabled = true;
            this.smoothingModeBox.Items.AddRange(new object[] {
            "Сглаживание не требуется",
            "Скользящая средняя",
            "Экспоненциальное сглаживание"});
            this.smoothingModeBox.Location = new System.Drawing.Point(215, 24);
            this.smoothingModeBox.Name = "smoothingModeBox";
            this.smoothingModeBox.Size = new System.Drawing.Size(326, 24);
            this.smoothingModeBox.TabIndex = 1;
            // 
            // trendModeBox
            // 
            this.trendModeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.trendModeBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trendModeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trendModeBox.FormattingEnabled = true;
            this.trendModeBox.Items.AddRange(new object[] {
            "Линейный",
            "Степенной",
            "Гиперболический"});
            this.trendModeBox.Location = new System.Drawing.Point(227, 119);
            this.trendModeBox.Name = "trendModeBox";
            this.trendModeBox.Size = new System.Drawing.Size(326, 24);
            this.trendModeBox.TabIndex = 3;
            // 
            // btnBuildTrend
            // 
            this.btnBuildTrend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuildTrend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuildTrend.Location = new System.Drawing.Point(402, 149);
            this.btnBuildTrend.Name = "btnBuildTrend";
            this.btnBuildTrend.Size = new System.Drawing.Size(151, 29);
            this.btnBuildTrend.TabIndex = 6;
            this.btnBuildTrend.Text = "Построить тренд";
            this.btnBuildTrend.UseVisualStyleBackColor = true;
            this.btnBuildTrend.Click += new System.EventHandler(this.BuildTrend);
            // 
            // TrendBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(571, 190);
            this.Controls.Add(this.btnBuildTrend);
            this.Controls.Add(groupBox1);
            this.Controls.Add(this.trendModeBox);
            this.Controls.Add(label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TrendBuilder";
            this.Text = "Построитель тренда";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.smoothingParamNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox trendModeBox;
        private System.Windows.Forms.NumericUpDown smoothingParamNumeric;
        private System.Windows.Forms.Button btnBuildTrend;
        private System.Windows.Forms.ComboBox smoothingModeBox;
    }
}