namespace ImageProcessor.Data
{
    using Windows.UI;
    using Windows.UI.Xaml.Media.Imaging;

    public static class ImageArithmeticHelper
    {
        public static void AddConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                return Color.FromArgb(color.A,
                                     (byte)(color.R + cR),
                                     (byte)(color.G + cG),
                                     (byte)(color.B + cB));
            });
        }

        public static void SubtractConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                return Color.FromArgb(color.A,
                                     (byte)(color.R - cR),
                                     (byte)(color.G - cG),
                                     (byte)(color.B - cB));
            });
        }

        public static void MultiplyConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                return Color.FromArgb(color.A,
                                     (byte)(color.R - cR),
                                     (byte)(color.G - cG),
                                     (byte)(color.B - cB));
            });
        }

        public static void DivideConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                return Color.FromArgb(color.A,
                                     (byte)(color.R - cR),
                                     (byte)(color.G - cG),
                                     (byte)(color.B - cB));
            });
        }
    }
}
