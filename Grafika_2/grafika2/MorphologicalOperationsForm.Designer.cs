namespace grafika2
{
    partial class MorphologicalOperationsForm
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
            this.cbFilterName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRozmiarFiltra = new System.Windows.Forms.ComboBox();
            this.chkbxR = new System.Windows.Forms.CheckBox();
            this.chkbxG = new System.Windows.Forms.CheckBox();
            this.chkbxB = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbFilterName
            // 
            this.cbFilterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterName.FormattingEnabled = true;
            this.cbFilterName.Location = new System.Drawing.Point(85, 12);
            this.cbFilterName.Name = "cbFilterName";
            this.cbFilterName.Size = new System.Drawing.Size(121, 21);
            this.cbFilterName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filtr";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Rozmiar filtra";
            // 
            // cbRozmiarFiltra
            // 
            this.cbRozmiarFiltra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRozmiarFiltra.FormattingEnabled = true;
            this.cbRozmiarFiltra.Location = new System.Drawing.Point(85, 39);
            this.cbRozmiarFiltra.Name = "cbRozmiarFiltra";
            this.cbRozmiarFiltra.Size = new System.Drawing.Size(121, 21);
            this.cbRozmiarFiltra.TabIndex = 3;
            // 
            // chkbxR
            // 
            this.chkbxR.AutoSize = true;
            this.chkbxR.Checked = true;
            this.chkbxR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxR.Location = new System.Drawing.Point(12, 76);
            this.chkbxR.Name = "chkbxR";
            this.chkbxR.Size = new System.Drawing.Size(34, 17);
            this.chkbxR.TabIndex = 4;
            this.chkbxR.Text = "R";
            this.chkbxR.UseVisualStyleBackColor = true;
            // 
            // chkbxG
            // 
            this.chkbxG.AutoSize = true;
            this.chkbxG.Checked = true;
            this.chkbxG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxG.Location = new System.Drawing.Point(52, 76);
            this.chkbxG.Name = "chkbxG";
            this.chkbxG.Size = new System.Drawing.Size(34, 17);
            this.chkbxG.TabIndex = 5;
            this.chkbxG.Text = "G";
            this.chkbxG.UseVisualStyleBackColor = true;
            // 
            // chkbxB
            // 
            this.chkbxB.AutoSize = true;
            this.chkbxB.Checked = true;
            this.chkbxB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbxB.Location = new System.Drawing.Point(92, 76);
            this.chkbxB.Name = "chkbxB";
            this.chkbxB.Size = new System.Drawing.Size(33, 17);
            this.chkbxB.TabIndex = 6;
            this.chkbxB.Text = "B";
            this.chkbxB.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(131, 70);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // MorphologicalOperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 98);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkbxB);
            this.Controls.Add(this.chkbxG);
            this.Controls.Add(this.chkbxR);
            this.Controls.Add(this.cbRozmiarFiltra);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFilterName);
            this.Name = "MorphologicalOperationsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MorphologicalOperationsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbFilterName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRozmiarFiltra;
        private System.Windows.Forms.CheckBox chkbxR;
        private System.Windows.Forms.CheckBox chkbxG;
        private System.Windows.Forms.CheckBox chkbxB;
        private System.Windows.Forms.Button btnOK;
    }
}