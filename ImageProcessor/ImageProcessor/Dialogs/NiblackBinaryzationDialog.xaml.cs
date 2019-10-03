using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ImageProcessor.Dialogs
{

    public sealed partial class NiblackBinaryzationDialog : ContentDialog
    {
        public NiblackBinaryzationDialog()
        {
            this.InitializeComponent();
            SliderValue2.ValueChanged += SliderValue2_ValueChanged;
            SliderValue2_ValueChanged(null, null);
        }

        public int SValue => (int)SliderValue.Value;

        public double KValue => SliderValue2.Value;

        private void SliderValue2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) => SliderValue2Text.Text = Math.Round(SliderValue2.Value, 2).ToString();
    }
}
