namespace MyBezier
{
    partial class MainForm
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
            this.picCanvas = new System.Windows.Forms.PictureBox();
            this.buttonHideHelpLines = new System.Windows.Forms.Button();
            this.buttonClearPoints = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPoints = new System.Windows.Forms.Label();
            this.textBoxPointX = new System.Windows.Forms.TextBox();
            this.textBoxPointY = new System.Windows.Forms.TextBox();
            this.buttonAddPoint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // picCanvas
            // 
            this.picCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picCanvas.Location = new System.Drawing.Point(12, 44);
            this.picCanvas.Name = "picCanvas";
            this.picCanvas.Size = new System.Drawing.Size(678, 303);
            this.picCanvas.TabIndex = 1;
            this.picCanvas.TabStop = false;
            this.picCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.picCanvas_Paint);
            this.picCanvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseClick);
            this.picCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picCanvas_MouseMove);
            // 
            // buttonHideHelpLines
            // 
            this.buttonHideHelpLines.Location = new System.Drawing.Point(93, 12);
            this.buttonHideHelpLines.Name = "buttonHideHelpLines";
            this.buttonHideHelpLines.Size = new System.Drawing.Size(130, 23);
            this.buttonHideHelpLines.TabIndex = 3;
            this.buttonHideHelpLines.Text = "Ukryj linie pomocnicze";
            this.buttonHideHelpLines.UseVisualStyleBackColor = true;
            this.buttonHideHelpLines.Click += new System.EventHandler(this.buttonHideHelpLines_Click);
            // 
            // buttonClearPoints
            // 
            this.buttonClearPoints.Location = new System.Drawing.Point(12, 12);
            this.buttonClearPoints.Name = "buttonClearPoints";
            this.buttonClearPoints.Size = new System.Drawing.Size(75, 23);
            this.buttonClearPoints.TabIndex = 4;
            this.buttonClearPoints.Text = "Wyczyść";
            this.buttonClearPoints.UseVisualStyleBackColor = true;
            this.buttonClearPoints.Click += new System.EventHandler(this.buttonClearPoints_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(628, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Punkty:";
            // 
            // labelPoints
            // 
            this.labelPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPoints.AutoSize = true;
            this.labelPoints.Location = new System.Drawing.Point(677, 17);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(13, 13);
            this.labelPoints.TabIndex = 6;
            this.labelPoints.Text = "0";
            // 
            // textBoxPointX
            // 
            this.textBoxPointX.Location = new System.Drawing.Point(397, 14);
            this.textBoxPointX.Name = "textBoxPointX";
            this.textBoxPointX.Size = new System.Drawing.Size(41, 20);
            this.textBoxPointX.TabIndex = 7;
            // 
            // textBoxPointY
            // 
            this.textBoxPointY.Location = new System.Drawing.Point(467, 14);
            this.textBoxPointY.Name = "textBoxPointY";
            this.textBoxPointY.Size = new System.Drawing.Size(40, 20);
            this.textBoxPointY.TabIndex = 8;
            // 
            // buttonAddPoint
            // 
            this.buttonAddPoint.Location = new System.Drawing.Point(293, 12);
            this.buttonAddPoint.Name = "buttonAddPoint";
            this.buttonAddPoint.Size = new System.Drawing.Size(75, 23);
            this.buttonAddPoint.TabIndex = 9;
            this.buttonAddPoint.Text = "Dodaj";
            this.buttonAddPoint.UseVisualStyleBackColor = true;
            this.buttonAddPoint.Click += new System.EventHandler(this.buttonAddPoint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(444, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Y:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 359);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonAddPoint);
            this.Controls.Add(this.textBoxPointY);
            this.Controls.Add(this.textBoxPointX);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClearPoints);
            this.Controls.Add(this.buttonHideHelpLines);
            this.Controls.Add(this.picCanvas);
            this.Name = "Form1";
            this.Text = "MyBezier";
            ((System.ComponentModel.ISupportInitialize)(this.picCanvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picCanvas;
        private System.Windows.Forms.Button buttonHideHelpLines;
        private System.Windows.Forms.Button buttonClearPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPoints;
        private System.Windows.Forms.TextBox textBoxPointX;
        private System.Windows.Forms.TextBox textBoxPointY;
        private System.Windows.Forms.Button buttonAddPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

