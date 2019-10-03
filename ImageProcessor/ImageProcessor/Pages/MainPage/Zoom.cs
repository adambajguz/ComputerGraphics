namespace ImageProcessor.Pages
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage
    {
        private double zoom = 1;

        private object ContentFrameContent;

        public double Zoom
        {
            get => zoom;
            set
            {
                if (value <= 10 && value >= 0.25)
                    zoom = value;
                else
                    return;

                ZoomFactorTextBlock.Text = (zoom * 100) + "%";

                OutputCanvasScrollViewer.ChangeView(InputCanvasScrollViewer.HorizontalOffset, InputCanvasScrollViewer.VerticalOffset, null);
                InputImageCanvas.Invalidate();
                OutputImageCanvas.Invalidate();

                //var size = InputVirtualBitmap.Size;
                //InputImageCanvas.Width = size.Width * (zoom + 1);
                //InputImageCanvas.Height = size.Height * (zoom + 1);
                //OutputImageCanvas.Width = size.Width * (zoom + 1);
                //OutputImageCanvas.Height = size.Height * (zoom + 1);

                ContentFrameContent = ContentFrame.Content;
            }
        }

        private void ZoomOutButton_Click(object sender, RoutedEventArgs e) => Zoom -= 0.25;

        private void ZoomInButton_Click(object sender, RoutedEventArgs e) => Zoom += 0.25;

        private void ZoomPresetMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            MenuFlyoutItem menuFlyoutItem = (MenuFlyoutItem)sender;

            if (Int32.TryParse(menuFlyoutItem.Tag.ToString(), out int x))
            {
                Zoom = (double)x / 100;
            }
        }

        private void SplitButton_Click(SplitButton sender, SplitButtonClickEventArgs args) => Zoom = 1;
    }
}
