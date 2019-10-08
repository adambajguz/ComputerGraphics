namespace grafika2
{
    partial class ValuePopup
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
            this.lblValue = new System.Windows.Forms.Label();
            this.tbValueR = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbValueG = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbValueB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(12, 9);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(15, 13);
            this.lblValue.TabIndex = 0;
            this.lblValue.Text = "R";
            // 
            // tbValueR
            // 
            this.tbValueR.Location = new System.Drawing.Point(33, 6);
            this.tbValueR.Name = "tbValueR";
            this.tbValueR.Size = new System.Drawing.Size(100, 20);
            this.tbValueR.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(58, 84);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbValueG
            // 
            this.tbValueG.Location = new System.Drawing.Point(33, 32);
            this.tbValueG.Name = "tbValueG";
            this.tbValueG.Size = new System.Drawing.Size(100, 20);
            this.tbValueG.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "G";
            // 
            // tbValueB
            // 
            this.tbValueB.Location = new System.Drawing.Point(33, 58);
            this.tbValueB.Name = "tbValueB";
            this.tbValueB.Size = new System.Drawing.Size(100, 20);
            this.tbValueB.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "B";
            // 
            // ValuePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(141, 116);
            this.Controls.Add(this.tbValueB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbValueG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbValueR);
            this.Controls.Add(this.lblValue);
            this.Name = "ValuePopup";
            this.Text = "Wartość";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox tbValueR;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbValueG;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbValueB;
        private System.Windows.Forms.Label label2;
    }
}