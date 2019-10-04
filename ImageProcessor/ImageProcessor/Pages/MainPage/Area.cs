namespace ImageProcessor.Pages
{
    using System;
    using System.Threading.Tasks;
    using ImageProcessor.Data;
    using ImageProcessor.Dialogs;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage
    {
        private async void PercentageOfGreenMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PercentagAreasData output = PercentageAreasDetectorHelper.CalculateGreen(WriteableOutputImage);
            await CallShowResultDialog(output, "Percentage of green");
        }

        private async void PercentageOfCustomMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ColorAreaDialog dialog = new ColorAreaDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                PercentagAreasData output = PercentageAreasDetectorHelper.Calculate(WriteableOutputImage, 
                    x => 
                    x.H >= dialog.Hmin && x.H <= dialog.Hmax &&
                    x.S >= dialog.Smin && x.S <= dialog.Smax &&
                    x.L >= dialog.Lmin && x.L <= dialog.Lmax);
                await CallShowResultDialog(output, "Percentage of custom query");
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async Task CallShowResultDialog(PercentagAreasData output, string title)
        {
            await ShowResultsDialog(title, $"{output.DetectedPixels}/{output.TotalPixels} | {(output.Percentage * 100).ToString("N2")}%");
        }

        private async Task ShowResultsDialog(string title, string text)
        {
            ContentDialog aboutDialog = new ContentDialog
            {
                Title = title,
                Content = text,
                CloseButtonText = "Close"
            };

            await aboutDialog.ShowAsync();
        }
    }
}
