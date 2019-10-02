namespace ImageProcessor.Pages
{
    using Windows.UI.Xaml;

    public sealed partial class MainPage
    {
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            InputImageCanvas.RemoveFromVisualTree();
            OutputImageCanvas.RemoveFromVisualTree();

            InputImageCanvas = null;
            OutputImageCanvas = null;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            InputImageCanvas.Invalidate();
            OutputImageCanvas.Invalidate();
            InputCanvasScrollViewer.MaxWidth = double.MaxValue;
            InputCanvasScrollViewer.MaxHeight = double.MaxValue;

            OutputCanvasScrollViewer.MaxWidth = double.MaxValue;
            OutputCanvasScrollViewer.MaxHeight = double.MaxValue;
        }
    }
}
