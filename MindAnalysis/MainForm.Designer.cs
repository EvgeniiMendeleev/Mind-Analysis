
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
            System.Windows.Forms.TabControl tabControl2;
            System.Windows.Forms.TabPage tabPage1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.TabPage tabPage2;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.MenuStrip mainMenuStrip;
            System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem загрузитьФайлДляАнализаToolStripMenuItem;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripMenuItem выйтиToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem обработкаДанныхToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem построитьТрендToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem createCorrelogrampMenuItem;
            System.Windows.Forms.ToolStripMenuItem графикиToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem очиститьВсеГрафикиToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem очиститьКонкретныйГрафикToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem испытаниеToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem первоеИспытаниеToolStripMenuItem;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine1 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fullFilePathText = new System.Windows.Forms.TextBox();
            this.chkSaveRecordData = new System.Windows.Forms.CheckBox();
            this.btnChangeSavePath = new System.Windows.Forms.Button();
            this.numMaxChartPoints = new System.Windows.Forms.NumericUpDown();
            this.btnStartRecord = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.chartAttention = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tabControl2 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            label2 = new System.Windows.Forms.Label();
            tabPage2 = new System.Windows.Forms.TabPage();
            label1 = new System.Windows.Forms.Label();
            mainMenuStrip = new System.Windows.Forms.MenuStrip();
            файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            загрузитьФайлДляАнализаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            выйтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            обработкаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            построитьТрендToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            createCorrelogrampMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            графикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            очиститьВсеГрафикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            очиститьКонкретныйГрафикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            испытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            первоеИспытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            groupBox2 = new System.Windows.Forms.GroupBox();
            tabControl2.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxChartPoints)).BeginInit();
            mainMenuStrip.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttention)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl2
            // 
            tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            tabControl2.Controls.Add(tabPage1);
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Location = new System.Drawing.Point(9, 353);
            tabControl2.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new System.Drawing.Size(416, 169);
            tabControl2.TabIndex = 31;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.Gray;
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(this.fullFilePathText);
            tabPage1.Controls.Add(this.chkSaveRecordData);
            tabPage1.Controls.Add(this.btnChangeSavePath);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(9, 1, 9, 1);
            tabPage1.Size = new System.Drawing.Size(408, 143);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Сохранение сеанса";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(9, 33);
            label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(141, 13);
            label2.TabIndex = 32;
            label2.Text = "Выбрать путь сохранения:";
            // 
            // fullFilePathText
            // 
            this.fullFilePathText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fullFilePathText.Location = new System.Drawing.Point(9, 50);
            this.fullFilePathText.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            this.fullFilePathText.Name = "fullFilePathText";
            this.fullFilePathText.ReadOnly = true;
            this.fullFilePathText.Size = new System.Drawing.Size(295, 20);
            this.fullFilePathText.TabIndex = 31;
            // 
            // chkSaveRecordData
            // 
            this.chkSaveRecordData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSaveRecordData.Location = new System.Drawing.Point(9, 9);
            this.chkSaveRecordData.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            this.chkSaveRecordData.Name = "chkSaveRecordData";
            this.chkSaveRecordData.Size = new System.Drawing.Size(370, 24);
            this.chkSaveRecordData.TabIndex = 25;
            this.chkSaveRecordData.Text = "Сохранять данные с нейрогарнитуры во время сеанса записи";
            this.chkSaveRecordData.UseVisualStyleBackColor = true;
            this.chkSaveRecordData.CheckedChanged += new System.EventHandler(this.OnChangedValueInSaveMindRecord);
            // 
            // btnChangeSavePath
            // 
            this.btnChangeSavePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeSavePath.Enabled = false;
            this.btnChangeSavePath.Location = new System.Drawing.Point(308, 48);
            this.btnChangeSavePath.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            this.btnChangeSavePath.Name = "btnChangeSavePath";
            this.btnChangeSavePath.Size = new System.Drawing.Size(71, 24);
            this.btnChangeSavePath.TabIndex = 30;
            this.btnChangeSavePath.Text = "Обзор";
            this.btnChangeSavePath.UseVisualStyleBackColor = true;
            this.btnChangeSavePath.Click += new System.EventHandler(this.SaveFilePathOfCurrentMindRecord);
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.Gray;
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(this.numMaxChartPoints);
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(9, 1, 9, 1);
            tabPage2.Size = new System.Drawing.Size(408, 143);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Настройки отображения";
            // 
            // label1
            // 
            label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(4, 18);
            label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(240, 13);
            label1.TabIndex = 28;
            label1.Text = "Максимальное количество точек на графике:";
            // 
            // numMaxChartPoints
            // 
            this.numMaxChartPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numMaxChartPoints.Location = new System.Drawing.Point(242, 16);
            this.numMaxChartPoints.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
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
            this.numMaxChartPoints.Size = new System.Drawing.Size(52, 20);
            this.numMaxChartPoints.TabIndex = 27;
            this.numMaxChartPoints.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // mainMenuStrip
            // 
            mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            файлToolStripMenuItem,
            обработкаДанныхToolStripMenuItem,
            графикиToolStripMenuItem});
            mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            mainMenuStrip.Name = "mainMenuStrip";
            mainMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            mainMenuStrip.Size = new System.Drawing.Size(626, 24);
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
            файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьФайлДляАнализаToolStripMenuItem
            // 
            загрузитьФайлДляАнализаToolStripMenuItem.Name = "загрузитьФайлДляАнализаToolStripMenuItem";
            загрузитьФайлДляАнализаToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            загрузитьФайлДляАнализаToolStripMenuItem.Text = "Загрузить файл для анализа";
            загрузитьФайлДляАнализаToolStripMenuItem.Click += new System.EventHandler(this.UploadFirstFileToChart);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // выйтиToolStripMenuItem
            // 
            выйтиToolStripMenuItem.Name = "выйтиToolStripMenuItem";
            выйтиToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            выйтиToolStripMenuItem.Text = "Выйти";
            // 
            // обработкаДанныхToolStripMenuItem
            // 
            обработкаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            построитьТрендToolStripMenuItem,
            createCorrelogrampMenuItem});
            обработкаДанныхToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            обработкаДанныхToolStripMenuItem.Name = "обработкаДанныхToolStripMenuItem";
            обработкаДанныхToolStripMenuItem.Size = new System.Drawing.Size(194, 20);
            обработкаДанныхToolStripMenuItem.Text = "Обработка загруженных данных";
            // 
            // построитьТрендToolStripMenuItem
            // 
            построитьТрендToolStripMenuItem.Name = "построитьТрендToolStripMenuItem";
            построитьТрендToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            построитьТрендToolStripMenuItem.Text = "Построить тренд";
            построитьТрендToolStripMenuItem.Click += new System.EventHandler(this.BuildTrendOnChart);
            // 
            // createCorrelogrampMenuItem
            // 
            createCorrelogrampMenuItem.Name = "createCorrelogrampMenuItem";
            createCorrelogrampMenuItem.Size = new System.Drawing.Size(219, 22);
            createCorrelogrampMenuItem.Text = "Построить коррелограмму";
            createCorrelogrampMenuItem.Click += new System.EventHandler(this.BuildCorrelogram);
            // 
            // графикиToolStripMenuItem
            // 
            графикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            очиститьВсеГрафикиToolStripMenuItem,
            очиститьКонкретныйГрафикToolStripMenuItem});
            графикиToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 7.8F);
            графикиToolStripMenuItem.Name = "графикиToolStripMenuItem";
            графикиToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            графикиToolStripMenuItem.Text = "Графики";
            // 
            // очиститьВсеГрафикиToolStripMenuItem
            // 
            очиститьВсеГрафикиToolStripMenuItem.Name = "очиститьВсеГрафикиToolStripMenuItem";
            очиститьВсеГрафикиToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            очиститьВсеГрафикиToolStripMenuItem.Text = "Очистить все графики";
            очиститьВсеГрафикиToolStripMenuItem.Click += new System.EventHandler(this.ClearAllGraphics);
            // 
            // очиститьКонкретныйГрафикToolStripMenuItem
            // 
            очиститьКонкретныйГрафикToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            испытаниеToolStripMenuItem,
            первоеИспытаниеToolStripMenuItem});
            очиститьКонкретныйГрафикToolStripMenuItem.Name = "очиститьКонкретныйГрафикToolStripMenuItem";
            очиститьКонкретныйГрафикToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            очиститьКонкретныйГрафикToolStripMenuItem.Text = "Очистить конкретный график";
            // 
            // испытаниеToolStripMenuItem
            // 
            испытаниеToolStripMenuItem.Name = "испытаниеToolStripMenuItem";
            испытаниеToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            испытаниеToolStripMenuItem.Text = "Сеанс записи";
            испытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearExperiment);
            // 
            // первоеИспытаниеToolStripMenuItem
            // 
            первоеИспытаниеToolStripMenuItem.Name = "первоеИспытаниеToolStripMenuItem";
            первоеИспытаниеToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            первоеИспытаниеToolStripMenuItem.Text = "Загруженные данные";
            первоеИспытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearFirstLoadedFile);
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            groupBox2.Controls.Add(this.btnStartRecord);
            groupBox2.Controls.Add(this.btnStopRecord);
            groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            groupBox2.Location = new System.Drawing.Point(9, 307);
            groupBox2.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(9, 1, 9, 1);
            groupBox2.Size = new System.Drawing.Size(416, 44);
            groupBox2.TabIndex = 30;
            groupBox2.TabStop = false;
            groupBox2.Text = "Управление сеансом записи";
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartRecord.Location = new System.Drawing.Point(3, 14);
            this.btnStartRecord.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(196, 24);
            this.btnStartRecord.TabIndex = 2;
            this.btnStartRecord.Text = "Начать считывание данных\r\n";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.StartToReadDataFromNeurodevice);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopRecord.Enabled = false;
            this.btnStopRecord.Location = new System.Drawing.Point(202, 14);
            this.btnStopRecord.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(205, 24);
            this.btnStopRecord.TabIndex = 23;
            this.btnStopRecord.Text = "Остановить считывание данных\r\n";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.StopToReadDataFromNeurodevice);
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
            this.chartAttention.Location = new System.Drawing.Point(9, 28);
            this.chartAttention.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
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
            this.chartAttention.Size = new System.Drawing.Size(610, 277);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(626, 531);
            this.Controls.Add(this.chartAttention);
            this.Controls.Add(groupBox2);
            this.Controls.Add(tabControl2);
            this.Controls.Add(mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(9, 1, 9, 1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mind Analysis v1.1.a.0";
            tabControl2.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxChartPoints)).EndInit();
            mainMenuStrip.ResumeLayout(false);
            mainMenuStrip.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAttention)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.CheckBox chkSaveRecordData;
        private System.Windows.Forms.NumericUpDown numMaxChartPoints;
        private System.Windows.Forms.TextBox fullFilePathText;
        private System.Windows.Forms.Button btnChangeSavePath;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttention;
    }
}

