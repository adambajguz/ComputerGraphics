namespace ImageProcessor.Pages
{
    using System;
    using ImageProcessor.Data;
    using ImageProcessor.Dialogs;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    { 
        private async void OpenPixelManagerDialogMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PixelManagerDialog dialog = new PixelManagerDialog(WriteableOutputImage.Clone());
            await dialog.ShowAsync();

            if (dialog.ExitResult == PixelManagerDialogExitResult.BitmapChanged)
            {
                AddToUndo(dialog.editingBitmap);
                WriteableOutputImage = dialog.editingBitmap;

                await UpdateOutputImage();
            }
        }

        private void OpenPixelManagerPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e) => NavView_Navigate(PixelManagerTag, WriteableOutputImage);
        private void OpenHistogramsPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e) => NavView_Navigate(HistogramManipulationTag, WriteableOutputImage);
        private void FingerprintMenuFlyoutItem_Click(object sender, RoutedEventArgs e) => NavView_Navigate(FingerprintTag, WriteableOutputImage);
    }
}
