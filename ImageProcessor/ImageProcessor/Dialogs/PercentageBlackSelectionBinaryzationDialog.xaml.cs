using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageProcessor.Dialogs
{

    public sealed partial class PercentageBlackSelectionBinaryzationDialog : ContentDialog
    {
        public PercentageBlackSelectionBinaryzationDialog()
        {
            this.InitializeComponent();
            SliderValue.ValueChanged += SliderValue_ValueChanged;
            SliderValue_ValueChanged(null, null);
        }

        public double TresholdValue => SliderValue.Value;

        private void SliderValue_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) => SliderValue2Text.Text = (Math.Round(SliderValue.Value, 2) * 100).ToString() + "%";
    }
}
