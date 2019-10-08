using System.Threading.Tasks;
using ImageProcessor.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Pages
{
    public sealed partial class MainPage
    {
        private async Task RunMorphological(MorphologicalOperation op)
        {
            AddToUndo(WriteableOutputImage.Clone());
            WriteableOutputImage = MorphologicalHelper.Make(WriteableOutputImage, op);

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
            await RunMorphological(MorphologicalOperation.HitOrMiss);
        }
    }
}
