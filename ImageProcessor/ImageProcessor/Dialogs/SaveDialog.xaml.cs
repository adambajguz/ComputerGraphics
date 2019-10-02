using System;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Controls;

namespace ImageProcessor.Dialogs
{
    public sealed partial class SaveDialog : ContentDialog
    {
        public SaveDialog(bool? jpegxr)
        {
            this.InitializeComponent();
            if (jpegxr != null && jpegxr == false)
            {
                CheckBoxValue.IsEnabled = false;
                CheckBoxValue.Opacity = 0.4;
            }
            else if (jpegxr == null)
            {
                SliderValue.IsEnabled = false;
                SliderValue.Opacity = 0.4;

                CheckBoxValue.IsEnabled = false;
                CheckBoxValue.Opacity = 0.4;
            }

            SliderValue.ValueChanged += SliderValue_ValueChanged;
            SliderValue_ValueChanged(null, null);
        }


        public double Quality { get => SliderValue.Value; }
        public bool Lossless { get => (bool)CheckBoxValue.IsChecked; }
        public BitmapInterpolationMode Interpolation { get => (BitmapInterpolationMode)InterpolationValue.SelectedIndex; }

        private void SliderValue_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e) => SliderValueText.Text = Math.Round(Quality, 2).ToString();
    }
}
