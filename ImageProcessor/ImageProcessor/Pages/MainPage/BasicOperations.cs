namespace ImageProcessor.Pages
{
    using System;
    using System.Threading.Tasks;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage
    {
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

        private async Task ShowFileOperationErrorDialog(string text)
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
