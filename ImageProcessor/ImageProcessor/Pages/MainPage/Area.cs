namespace ImageProcessor.Pages
{
    using System;
    using System.Threading.Tasks;
    using ImageProcessor.Data;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage
    {
        private async void PercentageOfGreenMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PercentagAreasData output = PercentageAreasDetectorHelper.CalculateGreen(WriteableOutputImage);
            await CallShowResultDialog(output, "Percentage of green");
        }


        private async void PercentageOfRedMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PercentagAreasData output = PercentageAreasDetectorHelper.CalculateGreen(WriteableOutputImage);
            await CallShowResultDialog(output, "Percentage of red");
        }

        private async void PercentageOfBlueMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PercentagAreasData output = PercentageAreasDetectorHelper.CalculateGreen(WriteableOutputImage);
            await CallShowResultDialog(output, "Percentage of blue");
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
