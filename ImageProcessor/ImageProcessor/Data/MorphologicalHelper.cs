using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public enum MorphologicalOperation
    {
        Dilation,
        Erosion,
        Opening,
        Closing,
        HitOrMiss
    }

    public static class MorphologicalHelper
    {
        public static WriteableBitmap Make(WriteableBitmap Input, MorphologicalOperation op, int baseElementWidth = 3)
        {
            if (op == MorphologicalOperation.Dilation)
                return DilateAndErode(Input, 3, MorphologicalOperation.Dilation);
            else if (op == MorphologicalOperation.Erosion)
                return DilateAndErode(Input, 3, MorphologicalOperation.Erosion);
            else if (op == MorphologicalOperation.Opening)
            {
                WriteableBitmap newImage = DilateAndErode(Input, 3, MorphologicalOperation.Erosion);
                return DilateAndErode(newImage, 3, MorphologicalOperation.Dilation);
            }
            else if (op == MorphologicalOperation.Closing)
            {
                WriteableBitmap newImage = DilateAndErode(Input, 3, MorphologicalOperation.Dilation);
                return DilateAndErode(newImage, 3, MorphologicalOperation.Erosion);
            }
            //else if (op == MorphologicalOperation.HitOrMiss)
            //    HitOrIsImage(null, baseElementWidth);

            return Input;
        }


        public static WriteableBitmap DilateAndErode(WriteableBitmap writeableBitmap,
                                               int matrixSize,
                                               MorphologicalOperation morphType,
                                               bool applyBlue = true,
                                               bool applyGreen = true,
                                               bool applyRed = true)
        {
            WriteableBitmap newImage = writeableBitmap.Clone();
            using (BitmapContext newcontext = newImage.GetBitmapContext())
            {
                using (BitmapContext context = writeableBitmap.GetBitmapContext())
                {
                    byte morphResetValue = 0;
                    if (morphType == MorphologicalOperation.Dilation)
                    {
                        morphResetValue = 255;
                    }


                    int filterOffset = (matrixSize - 1) / 2;
                    for (int offsetY = filterOffset; offsetY < context.Height - filterOffset; offsetY++)
                    {
                        for (int offsetX = filterOffset; offsetX < context.Width - filterOffset; offsetX++)
                        {
                            int byteOffset = offsetY * offsetX * 4;

                            byte blue = morphResetValue;
                            byte green = morphResetValue;
                            byte red = morphResetValue;

                            int calcOffset = 0;
                            for (int filterY = -filterOffset; filterY <= filterOffset; filterY++)
                            {
                                for (int filterX = -filterOffset; filterX <= filterOffset; filterX++)
                                {
                                    calcOffset = byteOffset + (filterX * 4) + (filterY);

                                    Color pixel = PixelHelper.GetPixel(context, calcOffset);
                                    if (morphType == MorphologicalOperation.Erosion)
                                    {
                                        if (pixel.B > blue)
                                        {
                                            blue = pixel.B;
                                        }

                                        if (pixel.G > green)
                                        {
                                            green = pixel.G;
                                        }

                                        if (pixel.R > red)
                                        {
                                            red = pixel.R;
                                        }
                                    }
                                    else if (morphType == MorphologicalOperation.Dilation)
                                    {
                                        if (pixel.B < blue)
                                        {
                                            blue = pixel.B;
                                        }

                                        if (pixel.G < green)
                                        {
                                            green = pixel.G;
                                        }

                                        if (pixel.R < red)
                                        {
                                            red = pixel.R;
                                        }
                                    }
                                }
                            }


                            Color pixel2 = PixelHelper.GetPixel(context, calcOffset);

                            if (applyBlue == false)
                            {
                                blue = pixel2.B;
                            }

                            if (applyGreen == false)
                            {
                                green = pixel2.G;
                            }

                            if (applyRed == false)
                            {
                                red = pixel2.R;
                            }
                            PixelHelper.SetPixel(newcontext, byteOffset, red, green, blue);
                        }
                    }

                }
            }

            return newImage;
        }



        private static void HitOrIsImage(BitmapContext context, int baseElementWidth)
        {

        }
    }
}
