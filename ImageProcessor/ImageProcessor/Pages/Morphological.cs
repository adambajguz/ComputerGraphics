using System;
using System.Threading.Tasks;
using ImageProcessor.Data;
using ImageProcessor.Dialogs;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Pages
{
    public sealed partial class MainPage
    {
        private async Task RunMorphological(MorphologicalOperation op, bool?[,] matrix3x3 = null)
        {
            OtsuBinaryzationPageMenuFlyoutItem_Click(null, null);
            AddToUndo(WriteableOutputImage.Clone());
            WriteableOutputImage = MorphologicalHelper.Make(WriteableOutputImage, op, matrix3x3);

            await UpdateOutputImage();
        }

        private async void DilationPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await RunMorphological(MorphologicalOperation.Dilation);
        }

        private async void ErosionPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await RunMorphological(MorphologicalOperation.Erosion);
        }

        private async void OpeningPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await RunMorphological(MorphologicalOperation.Opening);
        }

        private async void ClosingPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await RunMorphological(MorphologicalOperation.Closing);
        }

        private async void HitOrMissPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HitMissMorphologyDialog dialog = new HitMissMorphologyDialog();
            ContentDialogResult result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Secondary)
            {
                var matrix = await dialog.GetMatrix();

                await RunMorphological(MorphologicalOperation.HitOrMiss, matrix);
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }

        }
    }
}
