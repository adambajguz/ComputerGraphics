namespace ImageProcessor.Pages
{
    using System;
    using System.Threading.Tasks;
    using ImageProcessor.Data;
    using ImageProcessor.Dialogs;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    {
        private async void InvertMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            AddToUndo(WriteableOutputImage.Clone());
            WriteableOutputImage = WriteableOutputImage.Invert();

            await UpdateOutputImage();
        }

        private async void ScaleMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            AddToUndo(WriteableOutputImage.Clone());

            ScaleImageDialog dialog = new ScaleImageDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            InterpolationTypes interpolationTypes = dialog.Interpolation;
            double scale = await dialog.GetScaleValue();
            WriteableOutputImage = BitmapResizeHelper.Resize(WriteableOutputImage, (int)(WriteableOutputImage.PixelWidth * scale), (int)(WriteableOutputImage.PixelHeight * scale), interpolationTypes);

            await UpdateOutputImage();
        }

        private void ExitMenuFlyoutItem_Click(object sender, RoutedEventArgs e) => CoreApplication.Exit();


        private void ToogleOriginalFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            if (OriginalImageColumn.Width.IsStar)
                OriginalImageColumn.Width = new GridLength(0);
            else
                OriginalImageColumn.Width = new GridLength(1, GridUnitType.Star);
        }

        private async void AboutMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog aboutDialog = new ContentDialog
            {
                Title = "ImageProcessor",
                Content = "Author: Adam Bajguz",
                CloseButtonText = "Close"
            };

            await aboutDialog.ShowAsync();
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
    }
}
