namespace MindAnalysis
{
    partial class MarkingDatasetForm
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
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkingDatasetForm));
            this.button2 = new System.Windows.Forms.Button();
            this.dataVolume = new System.Windows.Forms.NumericUpDown();
            this.markingColoumnName = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.markingDatasetPath = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.button2);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(this.dataVolume);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(this.markingColoumnName);
            groupBox1.Controls.Add(this.button1);
            groupBox1.Controls.Add(this.markingDatasetPath);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(502, 166);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Настройте параметры маркировки";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(308, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(185, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Запустить разметку данных";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.RunMarkingProcess);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 97);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(296, 13);
            label3.TabIndex = 6;
            label3.Text = "Выберите объём данных, которые будут размечиваться:";
            // 
            // dataVolume
            // 
            this.dataVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataVolume.Location = new System.Drawing.Point(308, 95);
            this.dataVolume.Name = "dataVolume";
            this.dataVolume.Size = new System.Drawing.Size(185, 20);
            this.dataVolume.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 63);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(170, 13);
            label2.TabIndex = 4;
            label2.Text = "Будем маркировать по столбцу:";
            // 
            // markingColoumnName
            // 
            this.markingColoumnName.DisplayMember = "Внимание";
            this.markingColoumnName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.markingColoumnName.Font = new System.Drawing.Font("Times New Roman", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markingColoumnName.FormattingEnabled = true;
            this.markingColoumnName.Items.AddRange(new object[] {
            "Внимание",
            "Расслабление"});
            this.markingColoumnName.Location = new System.Drawing.Point(182, 58);
            this.markingColoumnName.Name = "markingColoumnName";
            this.markingColoumnName.Size = new System.Drawing.Size(311, 24);
            this.markingColoumnName.TabIndex = 3;
            this.markingColoumnName.ValueMember = "Внимание";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(418, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Загрузить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoadMarkingData);
            // 
            // markingDatasetPath
            // 
            this.markingDatasetPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.markingDatasetPath.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.markingDatasetPath.Font = new System.Drawing.Font("Times New Roman", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.markingDatasetPath.Location = new System.Drawing.Point(137, 24);
            this.markingDatasetPath.Name = "markingDatasetPath";
            this.markingDatasetPath.ReadOnly = true;
            this.markingDatasetPath.ShortcutsEnabled = false;
            this.markingDatasetPath.Size = new System.Drawing.Size(275, 23);
            this.markingDatasetPath.TabIndex = 1;
            this.markingDatasetPath.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 29);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(125, 13);
            label1.TabIndex = 0;
            label1.Text = "Файл для маркировки:";
            // 
            // MarkingDatasetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(526, 189);
            this.Controls.Add(groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MarkingDatasetForm";
            this.Text = "Маркировка сеанса записи";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataVolume)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox markingColoumnName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox markingDatasetPath;
        private System.Windows.Forms.NumericUpDown dataVolume;
        private System.Windows.Forms.Button button2;
    }
}