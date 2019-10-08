using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafika2
{
    public partial class ValuePopup : Form
    {
        public double ValueR { get; set; }
        public double ValueG { get; set; }
        public double ValueB { get; set; }
        public ValuePopup()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            ValueR = double.Parse(tbValueR.Text);
            ValueG = double.Parse(tbValueG.Text);
            ValueB = double.Parse(tbValueB.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
