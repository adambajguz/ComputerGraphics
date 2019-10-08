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
    public partial class BinaryzacjaZRecznymProgiem : Form
    {
        private Bitmap _image = null;
        public Bitmap Image { get { return _image; } }
        public BinaryzacjaZRecznymProgiem(Bitmap Image)
        {
            InitializeComponent();
            _image = (Bitmap)Image.Clone();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
