namespace ImageProcessor.Pages
{
    using System;
    using ImageProcessor.Data;
    using ImageProcessor.Dialogs;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    {
        //TODO clamp to 255

        private async void AddPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ThreeIntsDialog dialog = new ThreeIntsDialog("Add", "Default value for empty cell is 0", "Red", "Green", "Blue", true, false);
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                int[] c = await dialog.GetValues();

                AddToUndo(WriteableOutputImage.Clone());
                ImageArithmeticHelper.AddConstToImage(WriteableOutputImage, c[0], c[1], c[2]);

                await UpdateOutputImage();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async void SubtractPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ThreeIntsDialog dialog = new ThreeIntsDialog("Subtract", "Default value for empty cell is 0", "Red", "Green", "Blue", true, false);
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                int[] c = await dialog.GetValues();

                AddToUndo(WriteableOutputImage.Clone());
                ImageArithmeticHelper.SubtractConstToImage(WriteableOutputImage, c[0], c[1], c[2]);

                await UpdateOutputImage();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async void MultiplyPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ThreeIntsDialog dialog = new ThreeIntsDialog("Multiply", "Default value for empty cell is 1", "Red", "Green", "Blue", false, false);
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                int[] c = await dialog.GetValues();

                AddToUndo(WriteableOutputImage.Clone());
                ImageArithmeticHelper.MultiplyConstToImage(WriteableOutputImage, c[0], c[1], c[2]);

                await UpdateOutputImage();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async void DividePageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ThreeIntsDialog dialog = new ThreeIntsDialog("Divide", "Default value for empty or equal to 0 cell is 1", "Red", "Green", "Blue", false, true);
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                int[] c = await dialog.GetValues();

                AddToUndo(WriteableOutputImage.Clone());
                ImageArithmeticHelper.DivideConstToImage(WriteableOutputImage, c[0], c[1], c[2]);

                await UpdateOutputImage();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }
    }
}
