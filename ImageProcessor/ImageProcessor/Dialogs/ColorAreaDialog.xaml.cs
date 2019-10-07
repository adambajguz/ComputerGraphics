using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ImageProcessor.Dialogs
{
    public sealed partial class ColorAreaDialog : ContentDialog
    {
        public ColorAreaDialog()
        {
            this.InitializeComponent();

            SliderValueMinH.ValueChanged += SliderValueMinH_ValueChanged;
            SliderValueMinS.ValueChanged += SliderValueMinS_ValueChanged;
            SliderValueMinL.ValueChanged += SliderValueMinL_ValueChanged;
            SliderValueMinH_ValueChanged(null, null);
            SliderValueMinS_ValueChanged(null, null);
            SliderValueMinL_ValueChanged(null, null);

            SliderValueMaxH.ValueChanged += SliderValueMaxH_ValueChanged;
            SliderValueMaxS.ValueChanged += SliderValueMaxS_ValueChanged;
            SliderValueMaxL.ValueChanged += SliderValueMaxL_ValueChanged;
            SliderValueMaxH_ValueChanged(null, null);
            SliderValueMaxS_ValueChanged(null, null);
            SliderValueMaxL_ValueChanged(null, null);
        }

        public double Hmin { get => SliderValueMinH.Value; private set => SliderValueMinL.Value = value; }
        public double Smin { get => SliderValueMinS.Value; private set => SliderValueMinS.Value = value; }
        public double Lmin { get => SliderValueMinL.Value; private set => SliderValueMinL.Value = value; }

        public double Hmax { get => SliderValueMaxH.Value; private set => SliderValueMaxH.Value = value; }
        public double Smax { get => SliderValueMaxS.Value; private set => SliderValueMaxS.Value = value; }
        public double Lmax { get => SliderValueMaxL.Value; private set => SliderValueMaxL.Value = value; }

        private void SliderValueMinH_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Hmin > Hmax)
                Hmin = Hmax;

            SliderValueMinHText.Text = Hmin.ToString();
        }

        private void SliderValueMinS_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Smin > Smax)
                Smin = Smax;

            SliderValueMinSText.Text = Smin.ToString();
        }

        private void SliderValueMinL_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Lmin > Lmax)
                Lmin = Lmax;

            SliderValueMinLText.Text = Lmin.ToString();
        }

        private void SliderValueMaxH_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Hmax < Hmin)
                Hmax = Hmin;

            SliderValueMaxHText.Text = Hmax.ToString();
        }

        private void SliderValueMaxS_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Smax < Smin)
                Smax = Smin;

            SliderValueMaxSText.Text = Smax.ToString();
        }

        private void SliderValueMaxL_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (Lmax < Lmin)
                Lmax = Lmin;

            SliderValueMaxLText.Text = Lmax.ToString();
        }
    }
}
