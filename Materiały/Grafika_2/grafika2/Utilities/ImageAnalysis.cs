using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2.Utilities
{
    public static class ImageAnalysis
    {
        public static bool IsColorGreen(Color color)
        {
            //if (color.R <= color.G && color.B <= color.G / 2)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

            if (color.R <= color.G && color.B <= color.G)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Bitmap DetectGreenTerrain(Bitmap src)
        {
            Bitmap resultBmp = new Bitmap(src.Width, src.Height);
            int totalPixels = src.Width * src.Height;
            int greenPixels = 0;

            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    if (IsColorGreen(src.GetPixel(x, y)))
                    {
                        greenPixels++;
                        resultBmp.SetPixel(x, y, Color.Green);
                    }
                    else
                    {
                        resultBmp.SetPixel(x, y, Color.White);
                    }
                }
            }

            return resultBmp;
        }

        public static decimal CalculateGreenTerrain(Bitmap src)
        {
            Color green = Color.Green;
            int greenPixels = 0;
            int totalPixels = src.Width * src.Height;
            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    var color = src.GetPixel(x, y);
                    if (color.R == green.R && color.G == green.G && color.B == green.B)
                    {
                        greenPixels++;
                    }
                }
            }

            decimal result = (greenPixels * 100) / totalPixels;
            return result;
        }
    }
}
