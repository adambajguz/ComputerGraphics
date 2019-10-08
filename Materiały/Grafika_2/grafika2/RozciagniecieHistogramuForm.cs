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
    public partial class RozciagniecieHistogramuForm : Form
    {
        public int AValue { get; set; }
        public int BValue { get; set; }
        public RozciagniecieHistogramuForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int a, b;
            if (int.TryParse(tbA.Text, out a) && int.TryParse(tbB.Text, out b) && !String.IsNullOrEmpty(tbA.Text) && !String.IsNullOrEmpty(tbB.Text))
            {
                if (a < b && a >= 0 && b <= 255)
                {
                    AValue = a;
                    BValue = b;
                    this.DialogResult = DialogResult.OK;
                }
                else MessageBox.Show("Błąd! Wprowadzone dane są niepoprawne");

            }
            else
            {
                MessageBox.Show("Błąd! Wprowadzone dane są niepoprawne");
            }
        }
    }
}
