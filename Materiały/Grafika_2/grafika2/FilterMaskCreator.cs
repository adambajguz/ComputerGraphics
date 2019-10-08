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
    public partial class FilterMaskCreator : Form
    {
        double val00;
        public double Val00 { get { return val00; } }
        double val01;
        public double Val01 { get { return val01; } }
        double val02;
        public double Val02 { get { return val02; } }
        double val10;
        public double Val10 { get { return val10; } }
        double val11;
        public double Val11 { get { return val11; } }
        double val12;
        public double Val12 { get { return val12; } }
        double val20;
        public double Val20 { get { return val20; } }
        double val21;
        public double Val21 { get { return val21; } }
        double val22;
        public double Val22 { get { return val22; } }


        public FilterMaskCreator()
        {
            InitializeComponent();
        }

        public FilterMaskCreator(double v00, double v01, double v02, double v10, double v11, double v12, double v20, double v21, double v22)
        {
            InitializeComponent();
            tb00.Text = v00.ToString();
            tb01.Text = v01.ToString();
            tb02.Text = v02.ToString();
            tb10.Text = v10.ToString();
            tb11.Text = v11.ToString();
            tb12.Text = v12.ToString();
            tb20.Text = v20.ToString();
            tb21.Text = v21.ToString();
            tb22.Text = v22.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!ValidateData())
            {
                MessageBox.Show("Podane wartośći są nieprawidłowe", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else this.DialogResult = DialogResult.OK;
        }

        private bool ValidateData()
        {
            bool result = true;

            result = result && !String.IsNullOrEmpty(tb00.Text) && !String.IsNullOrEmpty(tb01.Text) && !String.IsNullOrEmpty(tb02.Text)
                && !String.IsNullOrEmpty(tb10.Text) && !String.IsNullOrEmpty(tb11.Text) && !String.IsNullOrEmpty(tb12.Text)
                && !String.IsNullOrEmpty(tb20.Text) && !String.IsNullOrEmpty(tb21.Text) && !String.IsNullOrEmpty(tb22.Text);

            try
            {
                val00 = double.Parse(tb00.Text.Replace('.', ','));
                val01 = double.Parse(tb01.Text.Replace('.', ','));
                val02 = double.Parse(tb02.Text.Replace('.', ','));
                val10 = double.Parse(tb10.Text.Replace('.', ','));
                val11 = double.Parse(tb11.Text.Replace('.', ','));
                val12 = double.Parse(tb12.Text.Replace('.', ','));
                val20 = double.Parse(tb20.Text.Replace('.', ','));
                val21 = double.Parse(tb21.Text.Replace('.', ','));
                val22 = double.Parse(tb22.Text.Replace('.', ','));
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
