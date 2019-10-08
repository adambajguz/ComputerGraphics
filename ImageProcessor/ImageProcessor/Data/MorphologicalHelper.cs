using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public enum MorphologicalOperation
    {
        Dilation,
        Erosion,
        Opening,
        Closing,
        HitOrMiss,
    }

    public static class MorphologicalHelper
    {
        public static readonly bool?[,] DilateMatrix = new bool?[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, true }
        };

        public static readonly bool?[,] ErosionMatrix = new bool?[,]
        {
            { false, false, false },
            { false, false, false },
            { false, false, false }
        };

        public static WriteableBitmap Make(WriteableBitmap Input, MorphologicalOperation op, bool?[,] matrix3x3 = null)
        {
            if (op == MorphologicalOperation.Dilation)
                return DilateAndErode(Input, DilateMatrix);
            else if (op == MorphologicalOperation.Erosion)
            {
                var ret = DilateAndErode(Input, ErosionMatrix);
                return ret.Invert();
            }
            else if (op == MorphologicalOperation.Opening)
            {
                WriteableBitmap newImage = DilateAndErode(Input, ErosionMatrix);
                newImage = newImage.Invert();
                return DilateAndErode(newImage, DilateMatrix);
            }
            else if (op == MorphologicalOperation.Closing)
            {
                WriteableBitmap newImage = DilateAndErode(Input, DilateMatrix);
                newImage = DilateAndErode(newImage, ErosionMatrix);
                return newImage.Invert();
            }
            else if (op == MorphologicalOperation.HitOrMiss)
                return DilateAndErode(Input, matrix3x3);

            return Input;
        }

        public static WriteableBitmap DilateAndErode(WriteableBitmap writeableBitmap, bool?[,] matrix3x3)
        {
            int width = writeableBitmap.PixelWidth;
            int height = writeableBitmap.PixelHeight;

            WriteableBitmap newImage = writeableBitmap.Clone();
            using (BitmapContext newcontext = newImage.GetBitmapContext())
            {
                using (BitmapContext context = writeableBitmap.GetBitmapContext())
                {
                    for (int x = 0; x < width; ++x)
                        for (int y = 0; y < height; ++y)
                        {

                            bool exaclyMatch = true;

                            for (int i = 0; i < 3; ++i)
                                for (int j = 0; j < 3; ++j)
                                {
                                    bool? matVal = matrix3x3[i, j];
                                    int cX = x - 1 + i;
                                    int cY = y - 1 + j;

                                    if (cX > 0 && cY > 0 && cX < width && cY < height && matVal != null)
                                    {
                                        if (!(PixelHelper.IsBlack(context, cX, cY) == matVal))
                                        {
                                            exaclyMatch = false;
                                            goto ExitLoop;
                                        }
                                    }
                                }
                            ExitLoop:

                            if (exaclyMatch)
                                PixelHelper.SetBlack(newcontext, x, y);
                            else
                                PixelHelper.SetWhite(newcontext, x, y);

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
