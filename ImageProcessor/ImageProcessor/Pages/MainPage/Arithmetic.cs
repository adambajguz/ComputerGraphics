namespace ImageProcessor.Pages
{
    using ImageProcessor.Data;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    {
        //TODO clamp to 255

        private async void AddPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int cR = 200, cG = 201, cB = 201;

            AddToUndo(WriteableOutputImage.Clone());
            ImageArithmeticHelper.AddConstToImage(WriteableOutputImage, cR, cG, cB);

            await UpdateOutputImage();
        }

        private async void SubtractPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int cR = 1, cG = 1, cB = 1;

            AddToUndo(WriteableOutputImage.Clone());
            ImageArithmeticHelper.SubtractConstToImage(WriteableOutputImage, cR, cG, cB);

            await UpdateOutputImage();
        }

        private async void MultiplyPageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int cR = 2, cG = 1, cB = 1;

            AddToUndo(WriteableOutputImage.Clone());
            ImageArithmeticHelper.MultiplyConstToImage(WriteableOutputImage, cR, cG, cB);

            await UpdateOutputImage();
        }

        private async void DividePageMenuFlyoutItem_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int cR = 1, cG = 1, cB = 1;

            AddToUndo(WriteableOutputImage.Clone());
            ImageArithmeticHelper.DivideConstToImage(WriteableOutputImage, cR, cG, cB);

            await UpdateOutputImage();
        }
    }
}
