using ExampleGallery.Infrastructure;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace PaintCube
{
    public sealed partial class ColorConverter : UserControl
    {
        public ColorConverter()
        {
            this.InitializeComponent();

            NewColorPreviewTooltip.Content = "#FFFFFF";

            SliderValueR.ValueChanged += SliderValueR_ValueChanged;
            SliderValueG.ValueChanged += SliderValueG_ValueChanged;
            SliderValueB.ValueChanged += SliderValueB_ValueChanged;

            SliderValueC.ValueChanged += SliderValueC_ValueChanged;
            SliderValueM.ValueChanged += SliderValueM_ValueChanged;
            SliderValueY.ValueChanged += SliderValueY_ValueChanged;
            SliderValueK.ValueChanged += SliderValueK_ValueChanged;

            SliderValueR_ValueChanged(null, null);
            SliderValueG_ValueChanged(null, null);
            SliderValueB_ValueChanged(null, null);

            SliderValueC_ValueChanged(null, null);
            SliderValueM_ValueChanged(null, null);
            SliderValueY_ValueChanged(null, null);
            SliderValueK_ValueChanged(null, null);

            RGBColor = Colors.White;
            R = RGBColor.R;
            G = RGBColor.G;
            B = RGBColor.B;
            UpdateCMYK();
        }

        public Color RGBColor { get; set; }

        public byte R
        {
            get => (byte)SliderValueR.Value;
            private set
            {
                SliderValueR.Value = (byte)value;
                SliderValueRText.Text = value.ToString();
            }
        }
        public byte G
        {
            get => (byte)SliderValueG.Value;
            private set
            {
                SliderValueG.Value = (byte)value;
                SliderValueGText.Text = value.ToString();
            }
        }
        public byte B
        {
            get => (byte)SliderValueB.Value;
            private set
            {
                SliderValueB.Value = (byte)value;
                SliderValueBText.Text = value.ToString();
            }
        }

        public byte C
        {
            get => (byte)SliderValueC.Value;
            private set
            {
                SliderValueC.Value = (byte)value;
                SliderValueCText.Text = value.ToString();
            }
        }
        public byte M
        {
            get => (byte)SliderValueM.Value;
            private set
            {
                SliderValueM.Value = (byte)value;
                SliderValueMText.Text = value.ToString();
            }
        }
        public byte Y
        {
            get => (byte)SliderValueY.Value;
            private set
            {
                SliderValueY.Value = (byte)value;
                SliderValueYText.Text = value.ToString();
            }
        }

        public byte K
        {
            get => (byte)SliderValueK.Value;
            private set
            {
                SliderValueK.Value = (byte)value;
                SliderValueKText.Text = value.ToString();
            }
        }

        private void SliderValueR_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueRText.Text = R.ToString();
            UpdateCMYK();
        }

        private void SliderValueG_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueGText.Text = G.ToString();
            UpdateCMYK();
        }

        private void SliderValueB_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueBText.Text = B.ToString();
            UpdateCMYK();
        }

        private void SliderValueC_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueCText.Text = C.ToString();
            UpdateRGB();
        }

        private void SliderValueM_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueMText.Text = M.ToString();
            UpdateRGB();
        }

        private void SliderValueY_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueYText.Text = Y.ToString();
            UpdateRGB();
        }

        private void SliderValueK_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SliderValueKText.Text = K.ToString();
            UpdateRGB();
        }

        private void UpdateRGB()
        {
            SliderValueR.ValueChanged -= SliderValueR_ValueChanged;
            SliderValueG.ValueChanged -= SliderValueG_ValueChanged;
            SliderValueB.ValueChanged -= SliderValueB_ValueChanged;

            Color rgb = RGBHelper.CmykToRgb(C, M, Y, K);
            R = rgb.R;
            G = rgb.G;
            B = rgb.B;
            RGBColor = rgb;

            SliderValueR.ValueChanged += SliderValueR_ValueChanged;
            SliderValueG.ValueChanged += SliderValueG_ValueChanged;
            SliderValueB.ValueChanged += SliderValueB_ValueChanged;

            NewColorPreview.Fill = new SolidColorBrush(RGBColor);
            PixelColorPicker.Color = RGBColor;
        }

        private void UpdateCMYK()
        {
            SliderValueC.ValueChanged -= SliderValueC_ValueChanged;
            SliderValueM.ValueChanged -= SliderValueM_ValueChanged;
            SliderValueY.ValueChanged -= SliderValueY_ValueChanged;
            SliderValueK.ValueChanged -= SliderValueK_ValueChanged;

            RGBColor = Color.FromArgb(255, R, G, B);
            byte[] cmyk = RGBHelper.RgbToCmyk(R, G, B);
            C = cmyk[0];
            M = cmyk[1];
            Y = cmyk[2];
            K = cmyk[3];

            SliderValueC.ValueChanged += SliderValueC_ValueChanged;
            SliderValueM.ValueChanged += SliderValueM_ValueChanged;
            SliderValueY.ValueChanged += SliderValueY_ValueChanged;
            SliderValueK.ValueChanged += SliderValueK_ValueChanged;

            NewColorPreview.Fill = new SolidColorBrush(RGBColor);
            PixelColorPicker.Color = RGBColor;
        }

        private void ColorPicker_ColorChanged(Windows.UI.Xaml.Controls.ColorPicker sender, ColorChangedEventArgs args)
        {
            //    NewColorPreview.Fill = new SolidColorBrush(sender.Color);
            //    NewColorPreviewTooltip.Content = sender.Color.ToString();
        }
    }
}
