namespace clipping
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.FirstLabaRadio = new System.Windows.Forms.RadioButton();
            this.ThirdLabRadio = new System.Windows.Forms.RadioButton();
            this.CountStepBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountStepBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(46, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(799, 582);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // StartButton
            // 
            this.StartButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StartButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.StartButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StartButton.ForeColor = System.Drawing.Color.Yellow;
            this.StartButton.Location = new System.Drawing.Point(500, 16);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(146, 31);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Старт!";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(421, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(73, 21);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "След";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // FirstLabaRadio
            // 
            this.FirstLabaRadio.AutoSize = true;
            this.FirstLabaRadio.Checked = true;
            this.FirstLabaRadio.Location = new System.Drawing.Point(35, 5);
            this.FirstLabaRadio.Name = "FirstLabaRadio";
            this.FirstLabaRadio.Size = new System.Drawing.Size(58, 17);
            this.FirstLabaRadio.TabIndex = 6;
            this.FirstLabaRadio.TabStop = true;
            this.FirstLabaRadio.Text = "Поумолчанию";
            this.FirstLabaRadio.UseVisualStyleBackColor = true;
            this.FirstLabaRadio.CheckedChanged += new System.EventHandler(this.FirstLabaRadio_CheckedChanged);
            // 
            // ThirdLabRadio
            // 
            this.ThirdLabRadio.AutoSize = true;
            this.ThirdLabRadio.Location = new System.Drawing.Point(99, 5);
            this.ThirdLabRadio.Name = "ThirdLabRadio";
            this.ThirdLabRadio.Size = new System.Drawing.Size(58, 17);
            this.ThirdLabRadio.TabIndex = 7;
            this.ThirdLabRadio.Text = "Отсечение";
            this.ThirdLabRadio.UseVisualStyleBackColor = true;
            this.ThirdLabRadio.CheckedChanged += new System.EventHandler(this.ThirdLabRadio_CheckedChanged);
            // 
            // CountStepBar
            // 
            this.CountStepBar.Location = new System.Drawing.Point(269, 7);
            this.CountStepBar.Maximum = 100;
            this.CountStepBar.Minimum = 10;
            this.CountStepBar.Name = "CountStepBar";
            this.CountStepBar.Size = new System.Drawing.Size(125, 45);
            this.CountStepBar.TabIndex = 11;
            this.CountStepBar.Value = 10;
            this.CountStepBar.Scroll += new System.EventHandler(this.CountStepBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Количество шагов";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 661);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CountStepBar);
            this.Controls.Add(this.ThirdLabRadio);
            this.Controls.Add(this.FirstLabaRadio);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Лабораторная работа №1-3 Вариант №17";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CountStepBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton FirstLabaRadio;
        private System.Windows.Forms.RadioButton ThirdLabRadio;
        private System.Windows.Forms.TrackBar CountStepBar;
        private System.Windows.Forms.Label label1;
    }
}

