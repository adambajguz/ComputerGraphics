using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageProcessor.Dialogs
{
    public sealed partial class AnySizeCustomConvolutionFilterDialog : ContentDialog
    {
        private double[,] SampleKernel = new double[,]
                                {
                                    {1,1,1,1,1},
                                    {1,1,1,1,1},
                                    {1,1,1,1,1},
                                    {1,1,1,1,1},
                                    {1,1,1,1,1}
                                };

        public double[,] Kernel { get; private set; }

        public AnySizeCustomConvolutionFilterDialog()
        {
            this.InitializeComponent();
            this.Closed += AnySizeCustomConvolutionFilterDialog_Closed;
            PlainText = JsonConvert.SerializeObject(SampleKernel);
        }

        private async void AnySizeCustomConvolutionFilterDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args)
        {
            try
            {
                Kernel = JsonConvert.DeserializeObject<double[,]>(PlainText);
            }
            catch (Exception)
            {
                await ShowErrorDialog("An error occured during deserialization! Incorrect structure!");
                return;
            }

            int w = Kernel.GetLength(0);
            int h = Kernel.GetLength(1);
            if (w!= h)
            {
                Kernel = null;
                await ShowErrorDialog($"Incorrect size ({w}x{h}) of kernel! Kernel must have equal width and height!");
            }
        }

        private async Task ShowErrorDialog(string text)
        {
            ContentDialog aboutDialog = new ContentDialog
            {
                Title = "Error",
                Content = text,
                CloseButtonText = "Close"
            };

            await aboutDialog.ShowAsync();
        }

        public string PlainText { get => TextValue.Text; set => TextValue.Text = value; }
    }
}
