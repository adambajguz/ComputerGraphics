using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public enum InterpolationTypes
    {
        NearestNeighbor,
        Bilinear
    }

    public static class BitmapResizeHelper
    {
        public static WriteableBitmap Resize(WriteableBitmap bmp, int width, int height, InterpolationTypes interpolation)
        {
            using (var srcContext = bmp.GetBitmapContext(ReadWriteMode.ReadOnly))
            {
                WriteableBitmap result = BitmapFactory.New(width, height);

                using (var dstContext = result.GetBitmapContext())
                {
                    int[] pd = new int[width * height];

                    if (interpolation == InterpolationTypes.NearestNeighbor)
                        NearstNeighborResize(srcContext.Pixels, srcContext.Width, srcContext.Height, width, height, pd);
                    else if (interpolation == InterpolationTypes.Bilinear)
                        BilinearResize(srcContext.Pixels, srcContext.Width, srcContext.Height, width, height, pd);

                    BitmapContext.BlockCopy(pd, 0, dstContext, 0, 4 * pd.Length);
                }

                return result;
            }
        }

        private static void NearstNeighborResize(int[] pixels, int widthSource, int heightSource, int width, int height, int[] pd)
        {
            double xs = (double)widthSource / width;
            double ys = (double)heightSource / height;

            for (int y = 0, srcIdx = 0; y < height; ++y)
                for (var x = 0; x < width; x++)
                    pd[srcIdx++] = pixels[(int)(y * ys) * widthSource + (int)(x * xs)];
        }

        private static void BilinearResize(int[] pixels, int widthSource, int heightSource, int width, int height, int[] pd)
        {
            double xs = (double)widthSource / width;
            double ys = (double)heightSource / height;

            for (int y = 0, srcIdx = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    double sx = x * xs;
                    double sy = y * ys;
                    int x0 = (int)sx;
                    int y0 = (int)sy;

                    // Calculate coordinates of the 4 interpolation points
                    double fracx = sx - x0;
                    double fracy = sy - y0;
                    double ifracx = 1f - fracx;
                    double ifracy = 1f - fracy;

                    int x1 = x0 + 1;
                    if (x1 >= widthSource)
                        x1 = x0;

                    int y1 = y0 + 1;
                    if (y1 >= heightSource)
                        y1 = y0;

                    // Read source color
                    int c = pixels[y0 * widthSource + x0];
                    byte c1a = (byte)(c >> 24);
                    byte c1r = (byte)(c >> 16);
                    byte c1g = (byte)(c >> 8);
                    byte c1b = (byte)(c);

                    c = pixels[y0 * widthSource + x1];
                    byte c2a = (byte)(c >> 24);
                    byte c2r = (byte)(c >> 16);
                    byte c2g = (byte)(c >> 8);
                    byte c2b = (byte)(c);

                    c = pixels[y1 * widthSource + x0];
                    byte c3a = (byte)(c >> 24);
                    byte c3r = (byte)(c >> 16);
                    byte c3g = (byte)(c >> 8);
                    byte c3b = (byte)(c);

                    c = pixels[y1 * widthSource + x1];
                    byte c4a = (byte)(c >> 24);
                    byte c4r = (byte)(c >> 16);
                    byte c4g = (byte)(c >> 8);
                    byte c4b = (byte)(c);


                    // Calculate colors
                    double l0 = ifracx * c1a + fracx * c2a;
                    double l1 = ifracx * c3a + fracx * c4a;

                    double l2 = ifracx * c1r + fracx * c2r;
                    double l3 = ifracx * c3r + fracx * c4r;

                    double l4 = ifracx * c1g + fracx * c2g;
                    double l5 = ifracx * c3g + fracx * c4g;

                    double l6 = ifracx * c1b + fracx * c2b;
                    double l7 = ifracx * c3b + fracx * c4b;

                    pd[srcIdx++] = ((byte)(ifracy * l0 + fracy * l1) << 24) |
                                   ((byte)(ifracy * l2 + fracy * l3) << 16) |
                                   ((byte)(ifracy * l4 + fracy * l5) << 8) |
                                    (byte)(ifracy * l6 + fracy * l7);
                }
            }
        }
    }
}
