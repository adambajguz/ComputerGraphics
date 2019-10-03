using System;
using ImageProcessor.Data;
using ImageProcessor.Dialogs;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Pages
{
    public partial class MainPage
    {
        private static readonly int[,] PrewittX = {
                                                    {1, 0, -1},
                                                    {1, 0, -1},
                                                    {1, 0, -1}
                                                  };
        private static readonly int[,] PrewittY = {
                                                     { 1,  1,  1},
                                                     { 0,  0,  0},
                                                     {-1, -1, -1}
                                                  };

        private static readonly int[,] SobelX = {
                                                   {-1, 0, 1},
                                                   {-2, 0, 2},
                                                   {-1, 0, 1}
                                                };
        private static readonly int[,] SobelY = {
                                                    {-1, -2, -1},
                                                    { 0,  0,  0},
                                                    { 1,  2,  1}
                                                };
        //https://homepages.inf.ed.ac.uk/rbf/HIPR2/log.htm
        private static readonly int[,] Laplace1 = {
                                                    { 0, -1,  0},
                                                    {-1,  4, -1},
                                                    { 0, -1,  0}
                                                };

        private static readonly int[,] Laplace2 = {
                                                    {-1, -1, -1},
                                                    {-1,  8, -1},
                                                    {-1, -1, -1}
                                                };


        private async void CustomPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            CustomConvolutionFilterDialog dialog = new CustomConvolutionFilterDialog();
            ContentDialogResult result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Secondary)
            {
                WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, await dialog.GetKernel());

                AddToUndo(WriteableOutputImage.Clone());
                await UpdateOutputImage();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async void CustomConvolutionPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            AnySizeCustomConvolutionFilterDialog dialog = new AnySizeCustomConvolutionFilterDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                if (dialog.Kernel != null)
                {
                    WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, dialog.Kernel);

                    AddToUndo(WriteableOutputImage.Clone());
                    await UpdateOutputImage();
                }
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async void GaussianBlurPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            GaussianBlurDialog dialog = new GaussianBlurDialog();
            ContentDialogResult result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Secondary)
            {
                var kernel = GaussianBlureHelper.CalculateKernel(dialog.SValue, dialog.SDValue);

                WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, kernel);

                AddToUndo(WriteableOutputImage.Clone());
                await UpdateOutputImage();
            }
            else
            {
                // The user clicked the CLoseButton, pressed ESC, Gamepad B, or the system back button.
                // Do nothing.
            }
        }

        private async void PrewittXPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, PrewittX);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void PrewittYPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, PrewittY);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void SobelXPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, SobelX);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void SobelYPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, SobelY);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void Laplace1PageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, Laplace1);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void Laplace2PageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            WriteableOutputImage = WriteableBitmapCovolute.Convolute(WriteableOutputImage, Laplace2);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void KuwaharaPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MedianFilterHelper.KuwaharaFilter(WriteableOutputImage, 5);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void Median3x3PageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MedianFilterHelper.MedianFilter(WriteableOutputImage, 3);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void Median5x5PageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MedianFilterHelper.MedianFilter(WriteableOutputImage, 5);

            AddToUndo(WriteableOutputImage.Clone());
            await UpdateOutputImage();
        }

        private async void MedianCustomPageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MedianFilterDialog dialog = new MedianFilterDialog();
            ContentDialogResult result = await dialog.ShowAsync();


            if (result == ContentDialogResult.Secondary)
            {
                MedianFilterHelper.MedianFilter(WriteableOutputImage, dialog.MaskSize);

                AddToUndo(WriteableOutputImage.Clone());
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
