namespace ImageProcessor.Pages
{
    using System;
    using System.Collections.Generic;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    {
        public Stack<WriteableBitmap> PrevOutputs = new Stack<WriteableBitmap>();

        public void AddToUndo(WriteableBitmap bitmap)
        {
            PrevOutputs.Push(bitmap);
            UndoFlyoutItem.IsEnabled = true;
        }

        private async void UndoMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            PrevOutputs.Pop();

            if (PrevOutputs.Count <= 1)
                UndoFlyoutItem.IsEnabled = false;

            WriteableOutputImage = PrevOutputs.Peek();

            await UpdateOutputImage();

            if (navigated)
            {
                string tmp = navigatedTo;
                ContentFrame_Reset(false);
                NavView_Navigate(tmp, WriteableOutputImage);
            }
        }
    }
}
