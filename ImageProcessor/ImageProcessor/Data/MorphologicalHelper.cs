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

        public static void Make(WriteableBitmap Input, MorphologicalOperation op, int baseElementWidth = 3)
        {
            using (BitmapContext context = Input.GetBitmapContext())
            {
                if (op == MorphologicalOperation.Dilation)
                    GetDilatation(context, baseElementWidth);
                else if (op == MorphologicalOperation.Erosion)
                    GetErosion(context, baseElementWidth);
                else if (op == MorphologicalOperation.Opening)
                {
                    GetErosion(context, baseElementWidth);
                    GetDilatation(context, baseElementWidth);
                }
                else if (op == MorphologicalOperation.Closing)
                {
                    GetDilatation(context, baseElementWidth);
                    GetErosion(context, baseElementWidth);
                }
                else if (op == MorphologicalOperation.HitOrMiss)
                    HitOrIsImage(context, baseElementWidth);
            }
        }

        private static void GetErosion(BitmapContext context, int baseElementWidth)
        {
            var width = context.Width;
            var height = context.Height;

            for (int i = baseElementWidth / 2; i < width - baseElementWidth / 2; ++i)
            {
                for (int j = baseElementWidth / 2; j < height - baseElementWidth / 2; ++j)
                {
                    bool breakInnerLoops = false;
                    for (int u = i - baseElementWidth / 2; u <= i + baseElementWidth / 2 && !breakInnerLoops; ++u)
                    {
                        for (int v = j - baseElementWidth / 2; v <= j + baseElementWidth / 2 && !breakInnerLoops; ++v)
                        {
                            if (PixelHelper.IsWhite(context, u, v))
                            {
                                PixelHelper.SetWhite(context, i, j);
                                breakInnerLoops = true;
                            }
                        }
                    }
                }
            }
        }

        private static void GetDilatation(BitmapContext context, int baseElementWidth)
        {
            var width = context.Width;
            var height = context.Height;

            for (int i = baseElementWidth / 2; i < width - baseElementWidth / 2; ++i)
            {
                for (int j = baseElementWidth / 2; j < height - baseElementWidth / 2; ++j)
                {
                    bool breakInnerLoops = false;
                    for (int u = i - baseElementWidth / 2; u <= i + baseElementWidth / 2 && !breakInnerLoops; ++u)
                    {
                        for (int v = j - baseElementWidth / 2; v <= j + baseElementWidth / 2 && !breakInnerLoops; ++v)
                        {
                            if (PixelHelper.IsBlack(context, u, v))
                            {
                                PixelHelper.SetBlack(context, i, j);
                                breakInnerLoops = true;
                            }
                        }
                    }
                }
            }
        }

        private static void HitOrIsImage(BitmapContext context, int baseElementWidth)
        {

        }
    }
}
