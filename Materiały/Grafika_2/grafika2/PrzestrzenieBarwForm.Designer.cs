namespace grafika2
{
    partial class PrzestrzenieBarwForm
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
            this.tbCMYK_K = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCMYKToRGB = new System.Windows.Forms.Button();
            this.tbCMYK_Y = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCMYK_M = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCMYK_C = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRGBtoCMYK = new System.Windows.Forms.Button();
            this.tbRGB_B = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRGB_G = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRGB_R = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbColor = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            this.SuspendLayout();
            // 
            // tbCMYK_K
            // 
            this.tbCMYK_K.Location = new System.Drawing.Point(160, 103);
            this.tbCMYK_K.MaxLength = 3;
            this.tbCMYK_K.Name = "tbCMYK_K";
            this.tbCMYK_K.Size = new System.Drawing.Size(100, 20);
            this.tbCMYK_K.TabIndex = 39;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(139, 106);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(14, 13);
            this.label9.TabIndex = 38;
            this.label9.Text = "K";
            // 
            // btnCMYKToRGB
            // 
            this.btnCMYKToRGB.Location = new System.Drawing.Point(160, 129);
            this.btnCMYKToRGB.Name = "btnCMYKToRGB";
            this.btnCMYKToRGB.Size = new System.Drawing.Size(100, 23);
            this.btnCMYKToRGB.TabIndex = 37;
            this.btnCMYKToRGB.Text = "CMYK -> RGB";
            this.btnCMYKToRGB.UseVisualStyleBackColor = true;
            this.btnCMYKToRGB.Click += new System.EventHandler(this.btnCMYKToRGB_Click);
            // 
            // tbCMYK_Y
            // 
            this.tbCMYK_Y.Location = new System.Drawing.Point(160, 77);
            this.tbCMYK_Y.MaxLength = 3;
            this.tbCMYK_Y.Name = "tbCMYK_Y";
            this.tbCMYK_Y.Size = new System.Drawing.Size(100, 20);
            this.tbCMYK_Y.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(139, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Y";
            // 
            // tbCMYK_M
            // 
            this.tbCMYK_M.Location = new System.Drawing.Point(160, 51);
            this.tbCMYK_M.MaxLength = 3;
            this.tbCMYK_M.Name = "tbCMYK_M";
            this.tbCMYK_M.Size = new System.Drawing.Size(100, 20);
            this.tbCMYK_M.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(139, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "M";
            // 
            // tbCMYK_C
            // 
            this.tbCMYK_C.Location = new System.Drawing.Point(160, 25);
            this.tbCMYK_C.MaxLength = 3;
            this.tbCMYK_C.Name = "tbCMYK_C";
            this.tbCMYK_C.Size = new System.Drawing.Size(100, 20);
            this.tbCMYK_C.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(139, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "C";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(139, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "RGB";
            // 
            // btnRGBtoCMYK
            // 
            this.btnRGBtoCMYK.Location = new System.Drawing.Point(33, 103);
            this.btnRGBtoCMYK.Name = "btnRGBtoCMYK";
            this.btnRGBtoCMYK.Size = new System.Drawing.Size(100, 23);
            this.btnRGBtoCMYK.TabIndex = 29;
            this.btnRGBtoCMYK.Text = "RGB -> CMYK";
            this.btnRGBtoCMYK.UseVisualStyleBackColor = true;
            this.btnRGBtoCMYK.Click += new System.EventHandler(this.btnRGBtoCMYK_Click);
            // 
            // tbRGB_B
            // 
            this.tbRGB_B.Location = new System.Drawing.Point(33, 77);
            this.tbRGB_B.MaxLength = 3;
            this.tbRGB_B.Name = "tbRGB_B";
            this.tbRGB_B.Size = new System.Drawing.Size(100, 20);
            this.tbRGB_B.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "B";
            // 
            // tbRGB_G
            // 
            this.tbRGB_G.Location = new System.Drawing.Point(33, 51);
            this.tbRGB_G.MaxLength = 3;
            this.tbRGB_G.Name = "tbRGB_G";
            this.tbRGB_G.Size = new System.Drawing.Size(100, 20);
            this.tbRGB_G.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "G";
            // 
            // tbRGB_R
            // 
            this.tbRGB_R.Location = new System.Drawing.Point(33, 25);
            this.tbRGB_R.MaxLength = 3;
            this.tbRGB_R.Name = "tbRGB_R";
            this.tbRGB_R.Size = new System.Drawing.Size(100, 20);
            this.tbRGB_R.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "R";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "RGB";
            // 
            // pbColor
            // 
            this.pbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbColor.Location = new System.Drawing.Point(266, 12);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(100, 100);
            this.pbColor.TabIndex = 40;
            this.pbColor.TabStop = false;
            // 
            // PrzestrzenieBarwForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 158);
            this.Controls.Add(this.pbColor);
            this.Controls.Add(this.tbCMYK_K);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnCMYKToRGB);
            this.Controls.Add(this.tbCMYK_Y);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbCMYK_M);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbCMYK_C);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnRGBtoCMYK);
            this.Controls.Add(this.tbRGB_B);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRGB_G);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRGB_R);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PrzestrzenieBarwForm";
            this.Text = "PrzestrzenieBarwForm";
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.TextBox tbCMYK_K;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCMYKToRGB;
        private System.Windows.Forms.TextBox tbCMYK_Y;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCMYK_M;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCMYK_C;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnRGBtoCMYK;
        private System.Windows.Forms.TextBox tbRGB_B;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRGB_G;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRGB_R;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}