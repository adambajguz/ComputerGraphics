using System;
using System.Linq.Expressions;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public static class PercentageAreasDetectorHelper
    {
        public static PercentagAreasData CalculateGreen(WriteableBitmap input)
        {
            return Calculate(input, x => x.H >= 75 && x.H <= 145 && x.S >= 28 && x.L >= 32 && x.L <= 75);
        }

        public static PercentagAreasData Calculate(WriteableBitmap bmp, Expression<Func<HSLColor, bool>> condition)
        {
            int width = bmp.PixelWidth;
            int height = bmp.PixelHeight;
            int totalPixels = width * height;

            Func<HSLColor, bool> f = condition.Compile();

            int sum = 0;
            using (BitmapContext context = bmp.GetBitmapContext())
            {

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        Color tmp = PixelHelper.GetPixel(context, x, y);

                        if (f.Invoke(RGB2HSL(tmp)))
                        {
                            ++sum;
                        }
                    }
                }

            }

            return new PercentagAreasData(sum, totalPixels);
        }

        public static HSLColor RGB2HSL(Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double h = 0; // default to black
            double s = 0;

            double v = Math.Max(r, g);
            v = Math.Max(v, b);

            double m = Math.Min(r, g);
            m = Math.Min(m, b);

            double l = (m + v) / 2.0;

            if (l <= 0.0)
                return new HSLColor(true, h, s, l);

            double vm = v - m;
            s = vm;

            if (s > 0.0)
                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);
            else
                return new HSLColor(true, h, s, l);


            double r2 = (v - r) / vm;
            double g2 = (v - g) / vm;
            double b2 = (v - b) / vm;

            if (r == v)
                h = (g == m ? 5.0 + b2 : 1.0 - g2);
            else if (g == v)
                h = (b == m ? 1.0 + r2 : 3.0 - b2);
            else
                h = (r == m ? 3.0 + g2 : 5.0 - r2);

            h /= 6.0;

            return new HSLColor(true, h, s, l);
        }
    }
}
