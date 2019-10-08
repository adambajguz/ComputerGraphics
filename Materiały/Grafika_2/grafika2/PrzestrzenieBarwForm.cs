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
    public partial class PrzestrzenieBarwForm : Form
    {
        public PrzestrzenieBarwForm()
        {
            InitializeComponent();
        }

        //cmyk ma wartosci w przedziale 0 - 1!
        private void btnRGBtoCMYK_Click(object sender, EventArgs e)
        {
            int r, g, b;
            if (!int.TryParse(tbRGB_R.Text, out r) || !int.TryParse(tbRGB_G.Text, out g) || !int.TryParse(tbRGB_B.Text, out b))
            {
                MessageBox.Show("Niepoprawne dane wejściowe!");
                return;
            }
            if (r < 0 || r > 255 || g < 0 || g > 255 || b < 0 || b > 255)
            {
                MessageBox.Show("Niepoprawne dane wejściowe!");
                return;
            }

            decimal c, m, y, k;

            k = Math.Min(1M - Convert.ToDecimal(r / 255M), Math.Min(1M - Convert.ToDecimal(g / 255M), 1M - Convert.ToDecimal(b / 255M)));
            c = (1M - Convert.ToDecimal(r / 255M) - k) / (1M - k);
            m = (1M - Convert.ToDecimal(g / 255M) - k) / (1M - k);
            y = (1M - Convert.ToDecimal(b / 255M) - k) / (1M - k);

            tbCMYK_C.Text = c.ToString();
            tbCMYK_M.Text = m.ToString();
            tbCMYK_Y.Text = y.ToString();
            tbCMYK_K.Text = k.ToString();
            ShowColor();
        }

        private void btnCMYKToRGB_Click(object sender, EventArgs e)
        {
            decimal c, m, y, k;
            if (!decimal.TryParse(tbCMYK_C.Text, out c) || !decimal.TryParse(tbCMYK_M.Text, out m) || !decimal.TryParse(tbCMYK_Y.Text, out y) || !decimal.TryParse(tbCMYK_K.Text, out k))
            {
                MessageBox.Show("Niepoprawne dane wejściowe!");
                return;
            }
            if (c < 0 || c > 1 || m < 0 || m > 1 || y < 0 || y > 1 || k < 0 || k > 1)
            {
                MessageBox.Show("Niepoprawne dane wejściowe!");
                return;
            }

            decimal r, g, b;

            r = 255 * (1 - Math.Min(1M, c * (1M - k) + k));
            g = 255 * (1 - Math.Min(1M, m * (1M - k) + k));
            b = 255 * (1 - Math.Min(1M, y * (1M - k) + k));

            tbRGB_B.Text = Math.Round(b, 0).ToString();
            tbRGB_G.Text = Math.Round(g, 0).ToString();
            tbRGB_R.Text = Math.Round(r, 0).ToString();
            ShowColor();
        }

        private void ShowColor()
        {
            Color newColor = Color.FromArgb(255, Convert.ToInt32(tbRGB_R.Text), Convert.ToInt32(tbRGB_G.Text), Convert.ToInt32(tbRGB_B.Text));
            pbColor.BackColor = newColor;
        }
    }
}
