
namespace MindAnalysis
{
    partial class MainForm
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
            System.Windows.Forms.MenuStrip mainMenuStrip;
            System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem загрузитьФайлДляАнализаToolStripMenuItem;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem графикиToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem очиститьВсеГрафикиToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem очиститьКонкретныйГрафикToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem испытаниеToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem первоеИспытаниеToolStripMenuItem;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine1 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine2 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStartRecord = new System.Windows.Forms.Button();
            this.numMaxChartPoints = new System.Windows.Forms.NumericUpDown();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.chartAttention = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this._attentionPage = new System.Windows.Forms.TabPage();
            this._alphaWavePage = new System.Windows.Forms.TabPage();
            this.AlphaChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnChangeSavePath = new System.Windows.Forms.Button();
            this.chkSaveRecordData = new System.Windows.Forms.CheckBox();
            this.fullFilePathText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            mainMenuStrip = new System.Windows.Forms.MenuStrip();
            файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            загрузитьФайлДляАнализаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            графикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            очиститьВсеГрафикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            очиститьКонкретныйГрафикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            испытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            первоеИспытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            groupBox2 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            mainMenuStrip.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxChartPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttention)).BeginInit();
            this.tabControl1.SuspendLayout();
            this._attentionPage.SuspendLayout();
            this._alphaWavePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            файлToolStripMenuItem,
            графикиToolStripMenuItem});
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            mainMenuStrip.Size = new System.Drawing.Size(945, 25);
            mainMenuStrip.TabIndex = 31;
            mainMenuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            загрузитьФайлДляАнализаToolStripMenuItem,
            toolStripSeparator1,
            выйтиToolStripMenuItem});
            файлToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьФайлДляАнализаToolStripMenuItem
            // 
            загрузитьФайлДляАнализаToolStripMenuItem.Name = "загрузитьФайлДляАнализаToolStripMenuItem";
            загрузитьФайлДляАнализаToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            загрузитьФайлДляАнализаToolStripMenuItem.Text = "Загрузить файл для анализа";
            загрузитьФайлДляАнализаToolStripMenuItem.Click += new System.EventHandler(this.UploadFirstFileToChart);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // выйтиToolStripMenuItem
            // 
            выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            выйтиToolStripMenuItem.Size = new System.Drawing.Size(259, 26);
            выйтиToolStripMenuItem.Text = "Выйти";
            // 
            // графикиToolStripMenuItem
            // 
            графикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            очиститьВсеГрафикиToolStripMenuItem,
            очиститьКонкретныйГрафикToolStripMenuItem});
            графикиToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            графикиToolStripMenuItem.Name = "графикиToolStripMenuItem";
            графикиToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            графикиToolStripMenuItem.Text = "Графики";
            // 
            // очиститьВсеГрафикиToolStripMenuItem
            // 
            очиститьВсеГрафикиToolStripMenuItem.Name = "очиститьВсеГрафикиToolStripMenuItem";
            очиститьВсеГрафикиToolStripMenuItem.Size = new System.Drawing.Size(264, 26);
            очиститьВсеГрафикиToolStripMenuItem.Text = "Очистить все графики";
            очиститьВсеГрафикиToolStripMenuItem.Click += new System.EventHandler(this.ClearAllGraphics);
            // 
            // очиститьКонкретныйГрафикToolStripMenuItem
            // 
            очиститьКонкретныйГрафикToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            испытаниеToolStripMenuItem,
            первоеИспытаниеToolStripMenuItem});
            очиститьКонкретныйГрафикToolStripMenuItem.Name = "очиститьКонкретныйГрафикToolStripMenuItem";
            очиститьКонкретныйГрафикToolStripMenuItem.Size = new System.Drawing.Size(264, 26);
            очиститьКонкретныйГрафикToolStripMenuItem.Text = "Очистить конкретный график";
            // 
            // испытаниеToolStripMenuItem
            // 
            испытаниеToolStripMenuItem.Name = "испытаниеToolStripMenuItem";
            испытаниеToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            испытаниеToolStripMenuItem.Text = "Сеанс записи";
            испытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearExperiment);
            // 
            // первоеИспытаниеToolStripMenuItem
            // 
            первоеИспытаниеToolStripMenuItem.Name = "первоеИспытаниеToolStripMenuItem";
            первоеИспытаниеToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            первоеИспытаниеToolStripMenuItem.Text = "Загруженные данные";
            первоеИспытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearFirstLoadedFile);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            groupBox2.Controls.Add(this.btnStartRecord);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(this.numMaxChartPoints);
            groupBox2.Controls.Add(this.btnStopRecord);
            groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox2.Location = new System.Drawing.Point(13, 556);
            groupBox2.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(12, 1, 12, 1);
            groupBox2.Size = new System.Drawing.Size(295, 116);
            groupBox2.TabIndex = 30;
            groupBox2.TabStop = false;
            groupBox2.Text = "Управление сеансом записи";
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStartRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartRecord.Location = new System.Drawing.Point(16, 44);
            this.btnStartRecord.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(261, 30);
            this.btnStartRecord.TabIndex = 2;
            this.btnStartRecord.Text = "Начать считывание данных\r\n";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.StartToReadDataFromNeurodevice);
            // 
            // label1
            // 
            label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 22);
            label1.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(207, 16);
            label1.TabIndex = 34;
            label1.Text = "Количество точек на графике:";
            // 
            // numMaxChartPoints
            // 
            this.numMaxChartPoints.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numMaxChartPoints.Location = new System.Drawing.Point(224, 20);
            this.numMaxChartPoints.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.numMaxChartPoints.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numMaxChartPoints.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numMaxChartPoints.Name = "numMaxChartPoints";
            this.numMaxChartPoints.Size = new System.Drawing.Size(52, 22);
            this.numMaxChartPoints.TabIndex = 33;
            this.numMaxChartPoints.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnStopRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopRecord.Enabled = false;
            this.btnStopRecord.Location = new System.Drawing.Point(16, 76);
            this.btnStopRecord.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(261, 30);
            this.btnStopRecord.TabIndex = 23;
            this.btnStopRecord.Text = "Остановить считывание данных\r\n";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.StopToReadDataFromNeurodevice);
            // 
            // label2
            // 
            label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(49, 53);
            label2.Margin = new System.Windows.Forms.Padding(12, 0, 12, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(121, 16);
            label2.TabIndex = 32;
            label2.Text = "Путь сохранения:";
            // 
            // chartAttention
            // 
            this.chartAttention.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartAttention.BackColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.Title = "Время испытания";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.AxisY.Interval = 10D;
            chartArea1.AxisY.Maximum = 103D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.StripLines.Add(stripLine1);
            chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;
            chartArea1.AxisY.Title = "Процент внимания";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea1.Name = "ChartArea1";
            this.chartAttention.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            legend1.Name = "Legend1";
            this.chartAttention.Legends.Add(legend1);
            this.chartAttention.Location = new System.Drawing.Point(0, 0);
            this.chartAttention.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.chartAttention.Name = "chartAttention";
            this.chartAttention.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Сеанс записи";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Загруженные данные";
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Color = System.Drawing.Color.Red;
            series3.Legend = "Legend1";
            series3.Name = "Тренд испытуемого";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Color = System.Drawing.Color.Cyan;
            series4.Legend = "Legend1";
            series4.Name = "Сглаженная запись";
            this.chartAttention.Series.Add(series1);
            this.chartAttention.Series.Add(series2);
            this.chartAttention.Series.Add(series3);
            this.chartAttention.Series.Add(series4);
            this.chartAttention.Size = new System.Drawing.Size(919, 492);
            this.chartAttention.TabIndex = 19;
            this.chartAttention.Text = "chart1";
            title1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            title1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            title1.BackSecondaryColor = System.Drawing.Color.White;
            title1.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title1.Name = "Title1";
            title1.Text = "Изменение внимания испытуемого";
            this.chartAttention.Titles.Add(title1);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this._attentionPage);
            this.tabControl1.Controls.Add(this._alphaWavePage);
            this.tabControl1.Location = new System.Drawing.Point(6, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(927, 522);
            this.tabControl1.TabIndex = 32;
            // 
            // _attentionPage
            // 
            this._attentionPage.Controls.Add(this.chartAttention);
            this._attentionPage.Location = new System.Drawing.Point(4, 25);
            this._attentionPage.Name = "_attentionPage";
            this._attentionPage.Padding = new System.Windows.Forms.Padding(3);
            this._attentionPage.Size = new System.Drawing.Size(919, 493);
            this._attentionPage.TabIndex = 0;
            this._attentionPage.Text = "Уровень внимания";
            this._attentionPage.UseVisualStyleBackColor = true;
            // 
            // _alphaWavePage
            // 
            this._alphaWavePage.Controls.Add(this.AlphaChart);
            this._alphaWavePage.Location = new System.Drawing.Point(4, 25);
            this._alphaWavePage.Name = "_alphaWavePage";
            this._alphaWavePage.Padding = new System.Windows.Forms.Padding(3);
            this._alphaWavePage.Size = new System.Drawing.Size(919, 493);
            this._alphaWavePage.TabIndex = 1;
            this._alphaWavePage.Text = "Альфа волна";
            this._alphaWavePage.UseVisualStyleBackColor = true;
            // 
            // AlphaChart
            // 
            this.AlphaChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AlphaChart.BackColor = System.Drawing.Color.Gray;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea2.AxisY.Interval = 10D;
            chartArea2.AxisY.Maximum = 103D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.StripLines.Add(stripLine2);
            chartArea2.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;
            chartArea2.AxisY.Title = "Уровень волны";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea2.Name = "AlphaHigh";
            chartArea3.AxisX.Title = "Время испытания";
            chartArea3.AxisY.Title = "Уровень волны";
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 13F);
            chartArea3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea3.Name = "AlphaLow";
            this.AlphaChart.ChartAreas.Add(chartArea2);
            this.AlphaChart.ChartAreas.Add(chartArea3);
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            legend2.Position.Auto = false;
            legend2.Position.Height = 9.368635F;
            legend2.Position.Width = 21.4597F;
            legend2.Position.X = 75.54031F;
            legend2.Position.Y = 11.83471F;
            legend3.Alignment = System.Drawing.StringAlignment.Center;
            legend3.IsDockedInsideChartArea = false;
            legend3.Name = "Legend2";
            this.AlphaChart.Legends.Add(legend2);
            this.AlphaChart.Legends.Add(legend3);
            this.AlphaChart.Location = new System.Drawing.Point(0, 0);
            this.AlphaChart.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.AlphaChart.Name = "AlphaChart";
            this.AlphaChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series5.BorderWidth = 3;
            series5.ChartArea = "AlphaHigh";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Legend = "Legend1";
            series5.Name = "Сеанс записи";
            series6.BorderWidth = 3;
            series6.ChartArea = "AlphaHigh";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Legend = "Legend1";
            series6.Name = "Загруженные данные";
            this.AlphaChart.Series.Add(series5);
            this.AlphaChart.Series.Add(series6);
            this.AlphaChart.Size = new System.Drawing.Size(919, 492);
            this.AlphaChart.TabIndex = 20;
            this.AlphaChart.Text = "AlphaChart";
            title2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            title2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            title2.BackSecondaryColor = System.Drawing.Color.White;
            title2.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title2.Name = "AlphaHighTitle";
            title2.Text = "Изменение внимания испытуемого";
            this.AlphaChart.Titles.Add(title2);
            // 
            // btnChangeSavePath
            // 
            this.btnChangeSavePath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChangeSavePath.Enabled = false;
            this.btnChangeSavePath.Location = new System.Drawing.Point(459, 72);
            this.btnChangeSavePath.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.btnChangeSavePath.Name = "btnChangeSavePath";
            this.btnChangeSavePath.Size = new System.Drawing.Size(95, 30);
            this.btnChangeSavePath.TabIndex = 30;
            this.btnChangeSavePath.Text = "Обзор";
            this.btnChangeSavePath.UseVisualStyleBackColor = true;
            this.btnChangeSavePath.Click += new System.EventHandler(this.SaveFilePathOfCurrentMindRecord);
            // 
            // chkSaveRecordData
            // 
            this.chkSaveRecordData.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkSaveRecordData.Location = new System.Drawing.Point(52, 22);
            this.chkSaveRecordData.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.chkSaveRecordData.Name = "chkSaveRecordData";
            this.chkSaveRecordData.Size = new System.Drawing.Size(493, 30);
            this.chkSaveRecordData.TabIndex = 25;
            this.chkSaveRecordData.Text = "Сохранять данные с нейрогарнитуры во время сеанса записи";
            this.chkSaveRecordData.UseVisualStyleBackColor = true;
            this.chkSaveRecordData.CheckedChanged += new System.EventHandler(this.OnChangedValueInSaveMindRecord);
            // 
            // fullFilePathText
            // 
            this.fullFilePathText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.fullFilePathText.Location = new System.Drawing.Point(51, 76);
            this.fullFilePathText.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.fullFilePathText.Name = "fullFilePathText";
            this.fullFilePathText.ReadOnly = true;
            this.fullFilePathText.Size = new System.Drawing.Size(401, 22);
            this.fullFilePathText.TabIndex = 31;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.btnChangeSavePath);
            this.groupBox1.Controls.Add(this.chkSaveRecordData);
            this.groupBox1.Controls.Add(this.fullFilePathText);
            this.groupBox1.Location = new System.Drawing.Point(315, 556);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 116);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки сохранения";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(945, 684);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(groupBox2);
            this.Controls.Add(mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(12, 1, 12, 1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mind Analysis v1.1.a.0";
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxChartPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttention)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this._attentionPage.ResumeLayout(false);
            this._alphaWavePage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AlphaChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttention;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage _attentionPage;
        private System.Windows.Forms.TabPage _alphaWavePage;
        private System.Windows.Forms.Button btnChangeSavePath;
        private System.Windows.Forms.CheckBox chkSaveRecordData;
        private System.Windows.Forms.TextBox fullFilePathText;
        private System.Windows.Forms.NumericUpDown numMaxChartPoints;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart AlphaChart;
    }
}

