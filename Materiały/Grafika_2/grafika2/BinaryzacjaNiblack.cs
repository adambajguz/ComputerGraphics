using grafika2.Utilities;
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
    public partial class BinaryzacjaNiblack : Form
    {
        private Bitmap _image = null;
        public Bitmap Image { get { return _image; } }

        public BinaryzacjaNiblack(Bitmap imgSrc)
        {
            this._image = (Bitmap)imgSrc.Clone();
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int m;
            double k;
            string parK = tbParametrK.Text.Replace('.', ',');

            if (double.TryParse(parK, out k))
            {
                if (k < 0)
                {
                    if (int.TryParse(tbRozmiarOkna.Text, out m))
                    {
                        if (m > 0 && m % 2 == 1)
                        {
                            _image = GlobalFunctions.BinarizeNiblack(_image, m, k);
                            this.DialogResult = DialogResult.OK;
                        }
                        else MessageBox.Show("Błędna wartość rozmiaru okna. Rozmiar okna musi być wartośćią niepażystą.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Błędna wartość rozmiaru okna.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else MessageBox.Show("Błędna wartość parametru K. K musi być mniejsze od zera!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Błędna wartość parametru K. K musi być mniejsze od zera!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
