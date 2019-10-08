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
    public enum MorphologicalFilterName
    {
        Dylatacja,
        Erozja,
        Otwarcie,
        Domknięcie
    }

    public partial class MorphologicalOperationsForm : Form
    {
        public List<int> MaskSizes { get; set; }
        public List<MorphologicalFilterName> FilterNames { get; set; }
        public Bitmap Image { get; set; }

        public MorphologicalOperationsForm(Bitmap image)
        {
            InitializeComponent();
            this.Image = image;
            MaskSizes = new List<int>();
            FilterNames = new List<MorphologicalFilterName>();

            MaskSizes.Add(3);
            MaskSizes.Add(5);
            MaskSizes.Add(7);
            MaskSizes.Add(9);
            MaskSizes.Add(11);
            MaskSizes.Add(13);
            MaskSizes.Add(15);
            MaskSizes.Add(17);

            FilterNames.Add(MorphologicalFilterName.Domknięcie);
            FilterNames.Add(MorphologicalFilterName.Dylatacja);
            FilterNames.Add(MorphologicalFilterName.Erozja);
            FilterNames.Add(MorphologicalFilterName.Otwarcie);

            cbFilterName.DataSource = FilterNames;
            cbRozmiarFiltra.DataSource = MaskSizes;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int filterSize = Int32.Parse(cbRozmiarFiltra.SelectedItem.ToString());
            MorphologicalFilterName selectedFilter = (MorphologicalFilterName)cbFilterName.SelectedItem;

            switch (selectedFilter)
            {
                case MorphologicalFilterName.Dylatacja:
                    Image = MorphologicalOperations.DilateAndErode(Image, filterSize, selectedFilter, chkbxB.Checked, chkbxG.Checked, chkbxR.Checked);
                    break;
                case MorphologicalFilterName.Erozja:
                    Image = MorphologicalOperations.DilateAndErode(Image, filterSize, selectedFilter, chkbxB.Checked, chkbxG.Checked, chkbxR.Checked);
                    break;
                case MorphologicalFilterName.Otwarcie:
                    Image = MorphologicalOperations.Open(Image, filterSize, chkbxB.Checked, chkbxG.Checked, chkbxR.Checked);
                    break;
                case MorphologicalFilterName.Domknięcie:
                    Image = MorphologicalOperations.Close(Image, filterSize, chkbxB.Checked, chkbxG.Checked, chkbxR.Checked);
                    break;
                default:
                    break;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
