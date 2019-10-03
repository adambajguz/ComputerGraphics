using System;
using System.Threading.Tasks;
using ImageProcessor.Data;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageProcessor.Dialogs
{

    public sealed partial class ScaleImageDialog : ContentDialog
    {
        public ScaleImageDialog()
        {
            this.InitializeComponent();
        }

        public InterpolationTypes Interpolation { get => (InterpolationTypes)Combo.SelectedIndex; }

        public async Task<double> GetScaleValue()
        {
            try
            {
                return Double.Parse(V1.Text);
            }
            catch (FormatException)
            {
                await ShowError("One or more values is not an integer!");
            }
            catch (OverflowException)
            {
                await ShowError("One or more values value is out of range of the Int32 type!");
            }

            return 0;
        }

        private async Task ShowError(string text)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Error",
                Content = text,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
