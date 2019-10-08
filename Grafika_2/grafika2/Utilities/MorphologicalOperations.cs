using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace grafika2.Utilities
{
    public static class MorphologicalOperations
    {
        public static Bitmap Open(this Bitmap sourceBitmap,
                                                  int matrixSize,
                                                  bool applyBlue = true,
                                                  bool applyGreen = true,
                                                  bool applyRed = true)
        {
            Bitmap resultBitmap = sourceBitmap.DilateAndErode(matrixSize,
                                                        MorphologicalFilterName.Erozja,
                                               applyBlue, applyGreen, applyRed);

            resultBitmap = resultBitmap.DilateAndErode(matrixSize,
                                                MorphologicalFilterName.Dylatacja,
                                               applyBlue, applyGreen, applyRed);

            return resultBitmap;
        }

        public static Bitmap Close(this Bitmap sourceBitmap,
                                                   int matrixSize,
                                                   bool applyBlue = true,
                                                   bool applyGreen = true,
                                                   bool applyRed = true)
        {
            Bitmap resultBitmap = sourceBitmap.DilateAndErode(matrixSize,
                                                        MorphologicalFilterName.Dylatacja,
                                                applyBlue, applyGreen, applyRed);

            resultBitmap = resultBitmap.DilateAndErode(matrixSize,
                                                 MorphologicalFilterName.Erozja,
                                                applyBlue, applyGreen, applyRed);

            return resultBitmap;
        }

        public static Bitmap DilateAndErode(this Bitmap sourceBitmap,
                                                int matrixSize,
                                                MorphologicalFilterName morphType,
                                                bool applyBlue = true,
                                                bool applyGreen = true,
                                                bool applyRed = true)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), 
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride *
                                          sourceData.Height];

            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);

            sourceBitmap.UnlockBits(sourceData);

            int filterOffset = (matrixSize - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            byte blue = 0;
            byte green = 0;
            byte red = 0;

            byte morphResetValue = 0;

            if (morphType == MorphologicalFilterName.Dylatacja)
            {
                morphResetValue = 255;
            }

            for (int offsetY = filterOffset; offsetY < sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX < sourceBitmap.Width - filterOffset; offsetX++)
                {
                    byteOffset = offsetY * sourceData.Stride + offsetX * 4;

                    blue = morphResetValue;
                    green = morphResetValue;
                    red = morphResetValue;

                    if (morphType == MorphologicalFilterName.Erozja)
                    {
                        for (int filterY = -filterOffset;
                            filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                            {
                                calcOffset = byteOffset + (filterX * 4) + (filterY * sourceData.Stride);

                                if (pixelBuffer[calcOffset] > blue)
                                {
                                    blue = pixelBuffer[calcOffset];
                                }

                                if (pixelBuffer[calcOffset + 1] > green)
                                {
                                    green = pixelBuffer[calcOffset + 1];
                                }

                                if (pixelBuffer[calcOffset + 2] > red)
                                {
                                    red = pixelBuffer[calcOffset + 2];
                                }
                            }
                        }
                    }
                    else if (morphType == MorphologicalFilterName.Dylatacja)
                    {
                        for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                        {
                            for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                            {

                                calcOffset = byteOffset + (filterX * 4) +
                                (filterY * sourceData.Stride);

                                if (pixelBuffer[calcOffset] < blue)
                                {
                                    blue = pixelBuffer[calcOffset];
                                }

                                if (pixelBuffer[calcOffset + 1] < green)
                                {
                                    green = pixelBuffer[calcOffset + 1];
                                }

                                if (pixelBuffer[calcOffset + 2] < red)
                                {
                                    red = pixelBuffer[calcOffset + 2];
                                }
                            }
                        }
                    }

                    if (applyBlue == false)
                    {
                        blue = pixelBuffer[byteOffset];
                    }

                    if (applyGreen == false)
                    {
                        green = pixelBuffer[byteOffset + 1];
                    }

                    if (applyRed == false)
                    {
                        red = pixelBuffer[byteOffset + 2];
                    }

                    resultBuffer[byteOffset] = blue;
                    resultBuffer[byteOffset + 1] = green;
                    resultBuffer[byteOffset + 2] = red;
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height), 
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);

            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }
    }
}
