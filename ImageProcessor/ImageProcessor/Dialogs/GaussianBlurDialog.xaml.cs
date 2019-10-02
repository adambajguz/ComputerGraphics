using System;
using Windows.UI.Xaml.Controls;

namespace ImageProcessor.Dialogs
{

    public sealed partial class GaussianBlurDialog : ContentDialog
    {
        public GaussianBlurDialog()
        {
            this.InitializeComponent();
            SliderValue2.ValueChanged += SliderValue2_ValueChanged;
            SliderValue2_ValueChanged(null, null);
        }

        public int SValue => (int)SliderValue.Value;

        public double SDValue => SliderValue2.Value;

        private void SliderValue2_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e) => SliderValue2Text.Text = Math.Round(SliderValue2.Value, 2).ToString();
    }
}
