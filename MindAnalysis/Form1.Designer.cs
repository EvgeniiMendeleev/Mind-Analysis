
namespace EEG_Graphics
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine1 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStartRecord = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.chkSaveRecordData = new System.Windows.Forms.CheckBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.fullFilePathText = new System.Windows.Forms.TextBox();
            this.btnChangeSavePath = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numMaxChartPoints = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chartAttention = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.currAttenLevelPage = new System.Windows.Forms.TabPage();
            this.chartAttentionLevel = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AttantionAndMeditationPage = new System.Windows.Forms.TabPage();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьФайлДляАнализаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileMenuStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.обработкаДанныхToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сгладитьГрафикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьВсеГрафикиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьКонкретныйГрафикToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.испытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.первоеИспытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.второеИспытаниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.построитьТрендToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabControl2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxChartPoints)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttention)).BeginInit();
            this.currAttenLevelPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAttentionLevel)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.AttantionAndMeditationPage.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartRecord
            // 
            this.btnStartRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartRecord.Location = new System.Drawing.Point(5, 16);
            this.btnStartRecord.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnStartRecord.Name = "btnStartRecord";
            this.btnStartRecord.Size = new System.Drawing.Size(200, 30);
            this.btnStartRecord.TabIndex = 2;
            this.btnStartRecord.Text = "Начать считывание данных\r\n";
            this.btnStartRecord.UseVisualStyleBackColor = true;
            this.btnStartRecord.Click += new System.EventHandler(this.StartToReadDataFromNeurodevice);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStopRecord.Enabled = false;
            this.btnStopRecord.Location = new System.Drawing.Point(211, 16);
            this.btnStopRecord.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(196, 30);
            this.btnStopRecord.TabIndex = 23;
            this.btnStopRecord.Text = "Остановить считывание данных\r\n";
            this.btnStopRecord.UseVisualStyleBackColor = true;
            this.btnStopRecord.Click += new System.EventHandler(this.StopToReadDataFromNeurodevice);
            // 
            // chkSaveRecordData
            // 
            this.chkSaveRecordData.Location = new System.Drawing.Point(5, 10);
            this.chkSaveRecordData.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.chkSaveRecordData.Name = "chkSaveRecordData";
            this.chkSaveRecordData.Size = new System.Drawing.Size(392, 21);
            this.chkSaveRecordData.TabIndex = 25;
            this.chkSaveRecordData.Text = "Сохранять данные с нейрогарнитуры во время записи";
            this.chkSaveRecordData.UseVisualStyleBackColor = true;
            this.chkSaveRecordData.CheckedChanged += new System.EventHandler(this.OnChangedValueInSaveMindRecord);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(12, 773);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(411, 142);
            this.tabControl2.TabIndex = 31;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gray;
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.fullFilePathText);
            this.tabPage1.Controls.Add(this.chkSaveRecordData);
            this.tabPage1.Controls.Add(this.btnChangeSavePath);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tabPage1.Size = new System.Drawing.Size(403, 116);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Сохранение сеанса";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Выбрать путь сохранения:";
            // 
            // fullFilePathText
            // 
            this.fullFilePathText.Location = new System.Drawing.Point(5, 49);
            this.fullFilePathText.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.fullFilePathText.Name = "fullFilePathText";
            this.fullFilePathText.ReadOnly = true;
            this.fullFilePathText.Size = new System.Drawing.Size(275, 20);
            this.fullFilePathText.TabIndex = 31;
            // 
            // btnChangeSavePath
            // 
            this.btnChangeSavePath.Enabled = false;
            this.btnChangeSavePath.Location = new System.Drawing.Point(286, 46);
            this.btnChangeSavePath.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnChangeSavePath.Name = "btnChangeSavePath";
            this.btnChangeSavePath.Size = new System.Drawing.Size(89, 24);
            this.btnChangeSavePath.TabIndex = 30;
            this.btnChangeSavePath.Text = "Обзор";
            this.btnChangeSavePath.UseVisualStyleBackColor = true;
            this.btnChangeSavePath.Click += new System.EventHandler(this.SaveFilePathOfCurrentMindRecord);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gray;
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.numMaxChartPoints);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tabPage2.Size = new System.Drawing.Size(403, 116);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Настройки отображения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Максимальное количество точек на графике:";
            // 
            // numMaxChartPoints
            // 
            this.numMaxChartPoints.Location = new System.Drawing.Point(251, 6);
            this.numMaxChartPoints.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
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
            this.numMaxChartPoints.Size = new System.Drawing.Size(75, 20);
            this.numMaxChartPoints.TabIndex = 27;
            this.numMaxChartPoints.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.btnStartRecord);
            this.groupBox2.Controls.Add(this.btnStopRecord);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(12, 720);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox2.Size = new System.Drawing.Size(411, 51);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Управление сеансом записи";
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
            this.chartAttention.Location = new System.Drawing.Point(2, 1);
            this.chartAttention.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
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
            series2.Name = "Первый испытуемый";
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "Второй испытуемый";
            this.chartAttention.Series.Add(series1);
            this.chartAttention.Series.Add(series2);
            this.chartAttention.Series.Add(series3);
            this.chartAttention.Size = new System.Drawing.Size(972, 661);
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
            // currAttenLevelPage
            // 
            this.currAttenLevelPage.BackColor = System.Drawing.Color.Gray;
            this.currAttenLevelPage.Controls.Add(this.chartAttentionLevel);
            this.currAttenLevelPage.Location = new System.Drawing.Point(4, 22);
            this.currAttenLevelPage.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.currAttenLevelPage.Name = "currAttenLevelPage";
            this.currAttenLevelPage.Size = new System.Drawing.Size(977, 634);
            this.currAttenLevelPage.TabIndex = 5;
            this.currAttenLevelPage.Text = "Уровень внимания";
            // 
            // chartAttentionLevel
            // 
            this.chartAttentionLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartAttentionLevel.BackColor = System.Drawing.Color.Gray;
            chartArea2.AxisX2.Interval = 10D;
            chartArea2.AxisY.Interval = 5D;
            chartArea2.AxisY.Maximum = 102D;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            chartArea2.Name = "ChartArea1";
            this.chartAttentionLevel.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            legend2.Name = "Legend1";
            this.chartAttentionLevel.Legends.Add(legend2);
            this.chartAttentionLevel.Location = new System.Drawing.Point(5, 1);
            this.chartAttentionLevel.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.chartAttentionLevel.Name = "chartAttentionLevel";
            this.chartAttentionLevel.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Уровень внимания";
            this.chartAttentionLevel.Series.Add(series4);
            this.chartAttentionLevel.Size = new System.Drawing.Size(973, 634);
            this.chartAttentionLevel.TabIndex = 6;
            this.chartAttentionLevel.Text = "chart1";
            title2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            title2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            title2.Font = new System.Drawing.Font("Times New Roman", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            title2.Name = "Title1";
            title2.Text = "Текущий уровень внимания";
            this.chartAttentionLevel.Titles.Add(title2);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.AttantionAndMeditationPage);
            this.tabControl1.Controls.Add(this.currAttenLevelPage);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Location = new System.Drawing.Point(12, 32);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(985, 684);
            this.tabControl1.TabIndex = 26;
            // 
            // AttantionAndMeditationPage
            // 
            this.AttantionAndMeditationPage.BackColor = System.Drawing.Color.Gray;
            this.AttantionAndMeditationPage.Controls.Add(this.chartAttention);
            this.AttantionAndMeditationPage.Location = new System.Drawing.Point(4, 22);
            this.AttantionAndMeditationPage.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.AttantionAndMeditationPage.Name = "AttantionAndMeditationPage";
            this.AttantionAndMeditationPage.Size = new System.Drawing.Size(977, 658);
            this.AttantionAndMeditationPage.TabIndex = 4;
            this.AttantionAndMeditationPage.Text = "График внимания";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.обработкаДанныхToolStripMenuItem,
            this.графикиToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1006, 24);
            this.mainMenuStrip.TabIndex = 31;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьФайлДляАнализаToolStripMenuItem,
            this.loadFileMenuStrip});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьФайлДляАнализаToolStripMenuItem
            // 
            this.загрузитьФайлДляАнализаToolStripMenuItem.Name = "загрузитьФайлДляАнализаToolStripMenuItem";
            this.загрузитьФайлДляАнализаToolStripMenuItem.Size = new System.Drawing.Size(327, 22);
            this.загрузитьФайлДляАнализаToolStripMenuItem.Text = "Загрузить файл для анализа";
            this.загрузитьФайлДляАнализаToolStripMenuItem.Click += new System.EventHandler(this.UploadFirstFileToChart);
            // 
            // loadFileMenuStrip
            // 
            this.loadFileMenuStrip.Name = "loadFileMenuStrip";
            this.loadFileMenuStrip.Size = new System.Drawing.Size(327, 22);
            this.loadFileMenuStrip.Text = "Загрузить дополнительный файл для анализа";
            this.loadFileMenuStrip.Click += new System.EventHandler(this.UploadSecondFileToChart);
            // 
            // обработкаДанныхToolStripMenuItem
            // 
            this.обработкаДанныхToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сгладитьГрафикToolStripMenuItem,
            this.построитьТрендToolStripMenuItem});
            this.обработкаДанныхToolStripMenuItem.Name = "обработкаДанныхToolStripMenuItem";
            this.обработкаДанныхToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.обработкаДанныхToolStripMenuItem.Text = "Обработка данных";
            // 
            // сгладитьГрафикToolStripMenuItem
            // 
            this.сгладитьГрафикToolStripMenuItem.Name = "сгладитьГрафикToolStripMenuItem";
            this.сгладитьГрафикToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.сгладитьГрафикToolStripMenuItem.Text = "Сгладить график";
            // 
            // графикиToolStripMenuItem
            // 
            this.графикиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьВсеГрафикиToolStripMenuItem,
            this.очиститьКонкретныйГрафикToolStripMenuItem});
            this.графикиToolStripMenuItem.Name = "графикиToolStripMenuItem";
            this.графикиToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.графикиToolStripMenuItem.Text = "Графики";
            // 
            // очиститьВсеГрафикиToolStripMenuItem
            // 
            this.очиститьВсеГрафикиToolStripMenuItem.Name = "очиститьВсеГрафикиToolStripMenuItem";
            this.очиститьВсеГрафикиToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.очиститьВсеГрафикиToolStripMenuItem.Text = "Очистить все графики";
            this.очиститьВсеГрафикиToolStripMenuItem.Click += new System.EventHandler(this.ClearAllGraphics);
            // 
            // очиститьКонкретныйГрафикToolStripMenuItem
            // 
            this.очиститьКонкретныйГрафикToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.испытаниеToolStripMenuItem,
            this.первоеИспытаниеToolStripMenuItem,
            this.второеИспытаниеToolStripMenuItem});
            this.очиститьКонкретныйГрафикToolStripMenuItem.Name = "очиститьКонкретныйГрафикToolStripMenuItem";
            this.очиститьКонкретныйГрафикToolStripMenuItem.Size = new System.Drawing.Size(239, 22);
            this.очиститьКонкретныйГрафикToolStripMenuItem.Text = "Очистить конкретный график";
            // 
            // испытаниеToolStripMenuItem
            // 
            this.испытаниеToolStripMenuItem.Name = "испытаниеToolStripMenuItem";
            this.испытаниеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.испытаниеToolStripMenuItem.Text = "Испытание";
            this.испытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearExperiment);
            // 
            // первоеИспытаниеToolStripMenuItem
            // 
            this.первоеИспытаниеToolStripMenuItem.Name = "первоеИспытаниеToolStripMenuItem";
            this.первоеИспытаниеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.первоеИспытаниеToolStripMenuItem.Text = "Первое испытание";
            this.первоеИспытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearFirstLoadedFile);
            // 
            // второеИспытаниеToolStripMenuItem
            // 
            this.второеИспытаниеToolStripMenuItem.Name = "второеИспытаниеToolStripMenuItem";
            this.второеИспытаниеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.второеИспытаниеToolStripMenuItem.Text = "Второе испытание";
            this.второеИспытаниеToolStripMenuItem.Click += new System.EventHandler(this.ClearSecondLoadedFile);
            // 
            // построитьТрендToolStripMenuItem
            // 
            this.построитьТрендToolStripMenuItem.Name = "построитьТрендToolStripMenuItem";
            this.построитьТрендToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.построитьТрендToolStripMenuItem.Text = "Построить тренд";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(429, 720);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox1.Size = new System.Drawing.Size(345, 84);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Характеристики внимания испытуемого";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(429, 808);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.groupBox3.Size = new System.Drawing.Size(345, 107);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Качество построенной модели";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1006, 925);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "MainForm";
            this.Text = "Mind Analysis v1.1.a.0";
            this.tabControl2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxChartPoints)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAttention)).EndInit();
            this.currAttenLevelPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartAttentionLevel)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.AttantionAndMeditationPage.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStartRecord;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.CheckBox chkSaveRecordData;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMaxChartPoints;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fullFilePathText;
        private System.Windows.Forms.Button btnChangeSavePath;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttention;
        private System.Windows.Forms.TabPage currAttenLevelPage;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAttentionLevel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AttantionAndMeditationPage;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem обработкаДанныхToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сгладитьГрафикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem загрузитьФайлДляАнализаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem графикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьВсеГрафикиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьКонкретныйГрафикToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem испытаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem первоеИспытаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem второеИспытаниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem построитьТрендToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}

