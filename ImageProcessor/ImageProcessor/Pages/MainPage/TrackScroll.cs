namespace ImageProcessor.Pages
{
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage : Page
    {
        private void InputCanvasScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            InputImageCanvas.Invalidate();
            OutputCanvasScrollViewer.ChangeView(InputCanvasScrollViewer.HorizontalOffset, InputCanvasScrollViewer.VerticalOffset, null);
        }

        private void InputCanvasScrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e)
        {
            InputImageCanvas.Invalidate();
            OutputCanvasScrollViewer.ChangeView(InputCanvasScrollViewer.HorizontalOffset, InputCanvasScrollViewer.VerticalOffset, null);
        }

        private void OutputCanvasScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => OutputImageCanvas.Invalidate();

        private void OutputCanvasScrollViewer_ViewChanging(object sender, ScrollViewerViewChangingEventArgs e) => OutputImageCanvas.Invalidate();

    }
}
