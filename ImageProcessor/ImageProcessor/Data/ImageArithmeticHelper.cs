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
                int r = color.R + cR;
                int g = color.G + cG;
                int b = color.B + cB;

                //clamp
                if (r > 255) r = 255;
                if (g > 255) g = 255;
                if (b > 255) b = 255;
                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
            });
        }

        public static void SubtractConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                int r = color.R - cR;
                int g = color.G - cG;
                int b = color.B - cB;

                //clamp
                if (r > 255) r = 255;
                if (g > 255) g = 255;
                if (b > 255) b = 255;
                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
            });
        }

        public static void MultiplyConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                int r = color.R * cR;
                int g = color.G * cG;
                int b = color.B * cB;

                //clamp
                if (r > 255) r = 255;
                if (g > 255) g = 255;
                if (b > 255) b = 255;
                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
            });
        }

        public static void DivideConstToImage(WriteableBitmap bmp, int cR, int cG, int cB)
        {
            bmp.ForEach((x, y, color) =>
            {
                int r = color.R / cR;
                int g = color.G / cG;
                int b = color.B / cB;

                //clamp
                if (r > 255) r = 255;
                if (g > 255) g = 255;
                if (b > 255) b = 255;
                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                return Color.FromArgb(color.A, (byte)r, (byte)g, (byte)b);
            });
        }
    }
}
