namespace wire-model
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
            this.goButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ColorBox = new System.Windows.Forms.ColorDialog();
            this.ColorPrism = new System.Windows.Forms.ColorDialog();
            this.BoxColor = new System.Windows.Forms.PictureBox();
            this.PrismColor = new System.Windows.Forms.PictureBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ThetaTxt = new System.Windows.Forms.TextBox();
            this.PhiTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lLengthTrackBar = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoxColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrismColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lLengthTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(650, 556);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // goButton
            // 
            this.goButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.goButton.BackColor = System.Drawing.Color.Silver;
            this.goButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.goButton.Location = new System.Drawing.Point(543, 4);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(119, 45);
            this.goButton.TabIndex = 1;
            this.goButton.Text = "Начать";
            this.goButton.UseVisualStyleBackColor = false;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BoxColor
            // 
            this.BoxColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BoxColor.Location = new System.Drawing.Point(12, 25);
            this.BoxColor.Name = "BoxColor";
            this.BoxColor.Size = new System.Drawing.Size(21, 24);
            this.BoxColor.TabIndex = 3;
            this.BoxColor.TabStop = false;
            this.BoxColor.Click += new System.EventHandler(this.BoxColor_Click);
            // 
            // PrismColor
            // 
            this.PrismColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PrismColor.Location = new System.Drawing.Point(39, 25);
            this.PrismColor.Name = "PrismColor";
            this.PrismColor.Size = new System.Drawing.Size(21, 24);
            this.PrismColor.TabIndex = 4;
            this.PrismColor.TabStop = false;
            this.PrismColor.Click += new System.EventHandler(this.PrismColor_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(387, 31);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Отображать оси ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ThetaTxt
            // 
            this.ThetaTxt.Location = new System.Drawing.Point(66, 29);
            this.ThetaTxt.Name = "ThetaTxt";
            this.ThetaTxt.Size = new System.Drawing.Size(81, 20);
            this.ThetaTxt.TabIndex = 6;
            this.ThetaTxt.Text = "20";
            // 
            // PhiTxt
            // 
            this.PhiTxt.Location = new System.Drawing.Point(153, 29);
            this.PhiTxt.Name = "PhiTxt";
            this.PhiTxt.Size = new System.Drawing.Size(81, 20);
            this.PhiTxt.TabIndex = 7;
            this.PhiTxt.Text = "20";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Theta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Phi";
            // 
            // lLengthTrackBar
            // 
            this.lLengthTrackBar.Location = new System.Drawing.Point(262, 9);
            this.lLengthTrackBar.Maximum = 700;
            this.lLengthTrackBar.Minimum = 100;
            this.lLengthTrackBar.Name = "lLengthTrackBar";
            this.lLengthTrackBar.Size = new System.Drawing.Size(119, 45);
            this.lLengthTrackBar.TabIndex = 10;
            this.lLengthTrackBar.Value = 400;
            this.lLengthTrackBar.Scroll += new System.EventHandler(this.lLengthTrackBar_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(240, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "L:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(671, 623);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lLengthTrackBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PhiTxt);
            this.Controls.Add(this.ThetaTxt);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.PrismColor);
            this.Controls.Add(this.BoxColor);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BoxColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrismColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lLengthTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ColorDialog ColorBox;
        private System.Windows.Forms.ColorDialog ColorPrism;
        private System.Windows.Forms.PictureBox BoxColor;
        private System.Windows.Forms.PictureBox PrismColor;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox ThetaTxt;
        private System.Windows.Forms.TextBox PhiTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar lLengthTrackBar;
        private System.Windows.Forms.Label label4;
    }
}

