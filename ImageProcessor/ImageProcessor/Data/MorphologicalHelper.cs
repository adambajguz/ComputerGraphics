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

        public static void Make(WriteableBitmap Input, MorphologicalOperation op)
        {
            using (BitmapContext context = Input.GetBitmapContext())
            {
                if (op == MorphologicalOperation.Dilation)
                    DilatateImage(context);
                else if (op == MorphologicalOperation.Erosion)
                    ErodeImage(context);
                else if (op == MorphologicalOperation.Opening)
                    OpenImage(context);
                else if (op == MorphologicalOperation.Closing)
                    CloseImage(context);
                else if (op == MorphologicalOperation.HitOrMiss)
                    HitOrMiss(context);
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

        private static void ErodeImage(BitmapContext context, int baseElementWidth = 3)
        {
            GetErosion(context, baseElementWidth);
        }

        private static void DilatateImage(BitmapContext context, int baseElementWidth = 3)
        {
            GetDilatation(context, baseElementWidth);
        }

        private static void OpenImage(BitmapContext context, int baseElementWidth = 3)
        {
            ErodeImage(context, baseElementWidth);
            DilatateImage(context, baseElementWidth);
        }

        private static void CloseImage(BitmapContext context, int baseElementWidth = 3)
        {
            DilatateImage(context, baseElementWidth);
            ErodeImage(context, baseElementWidth);
        }

        private static void HitOrMiss(BitmapContext context, int baseElementWidth = 3)
        {

        }
    }
}
