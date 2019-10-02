using Windows.UI.Xaml.Controls;

namespace ImageProcessor.Dialogs
{

    public sealed partial class MedianFilterDialog : ContentDialog
    {
        public MedianFilterDialog()
        {
            this.InitializeComponent();
        }

        public int MaskSize => (int)SliderValue.Value;
    }
}
