namespace ImageProcessor.Pages
{
    using System;
    using Microsoft.Graphics.Canvas;
    using Windows.Foundation;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    {
        private IRandomAccessStream InputImageStream;
        private CanvasVirtualBitmap InputVirtualBitmap;
        private IRandomAccessStream OutputImageStream;
        private CanvasVirtualBitmap OutputVirtualBitmap;
        public WriteableBitmap WriteableOutputImage;
        public WriteableBitmap WriteableOutputImageCopy;

        private void InputImageCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasVirtualControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            if (InputImageStream != null)
            {
                args.TrackAsyncAction(LoadInputVirtualBitmap().AsAsyncAction());
            }
        }

        private void InputImageCanvas_RegionsInvalidated(Microsoft.Graphics.Canvas.UI.Xaml.CanvasVirtualControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasRegionsInvalidatedEventArgs args)
        {
            foreach (var region in args.InvalidatedRegions)
            {
                using (var ds = InputImageCanvas.CreateDrawingSession(region))
                {
                    if (InputVirtualBitmap != null)
                        ds.DrawImage(InputVirtualBitmap, new Rect(0, 0, region.Width * Zoom, region.Height * Zoom), new Rect(0, 0, region.Width, region.Height), 1.0f, CanvasImageInterpolation.NearestNeighbor);
                }
            }
        }

        private void OutputImageCanvas_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasVirtualControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            if (InputImageStream != null)
            {
                args.TrackAsyncAction(LoadOutputVirtualBitmap().AsAsyncAction());
            }
        }

        private void OutputImageCanvas_RegionsInvalidated(Microsoft.Graphics.Canvas.UI.Xaml.CanvasVirtualControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasRegionsInvalidatedEventArgs args)
        {
            foreach (var region in args.InvalidatedRegions)
            {
                using (var ds = OutputImageCanvas.CreateDrawingSession(region))
                {
                    if (OutputVirtualBitmap != null)
                        ds.DrawImage(OutputVirtualBitmap, new Rect(0, 0, region.Width * Zoom, region.Height * Zoom), new Rect(0, 0, region.Width, region.Height), 1.0f, CanvasImageInterpolation.NearestNeighbor);
                }
            }
        }
    }
}
