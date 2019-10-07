using System;
using Windows.UI;

namespace PaintCube.Infrastructure
{
    public static class RGBHelper
    {
        public static byte[] RgbToCmyk(Color rgb) => RgbToCmyk(rgb.R, rgb.G, rgb.B);

        public static byte[] RgbToCmyk(byte red, byte green, byte blue)
        {
            double black = Math.Min(1.0 - red / 255.0, Math.Min(1.0 - green / 255.0, 1.0 - blue / 255.0));
            double cyan = (1.0 - (red / 255.0) - black) / (1.0 - black);
            double magenta = (1.0 - (green / 255.0) - black) / (1.0 - black);
            double yellow = (1.0 - (blue / 255.0) - black) / (1.0 - black);

            return new[] { (byte)(cyan * 100), (byte)(magenta * 100), (byte)(yellow * 100), (byte)(black * 100) };
        }

        public static Color CmykToRgb(double cyan, double magenta, double yellow, double black)
        {
            byte[] color = CmykToRgbArray(cyan, magenta, yellow, black);

            return Color.FromArgb(255, color[0], color[1], color[2]);
        }

        public static byte[] CmykToRgbArray(double cyan, double magenta, double yellow, double black)
        {
            byte red = Convert.ToByte(255 * (1 - cyan / 100) * (1 - black / 100));
            byte green = Convert.ToByte(255 * (1 - magenta / 100) * (1 - black / 100));
            byte blue = Convert.ToByte(255 * (1 - yellow / 100) * (1 - black / 100));

            return new[] { red, green, blue };
        }
    }
}
