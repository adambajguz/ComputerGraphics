using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public static class MorphologicalHelper
    {
        public static void tewst(WriteableBitmap Input, int maskSize)
        {
            int width = Input.PixelWidth;
            int height = Input.PixelHeight;

            WriteableBitmap originalImage = Input.Clone();

            using (BitmapContext contextOriginal = originalImage.GetBitmapContext())
            {
                using (BitmapContext context = Input.GetBitmapContext())
                {


                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {

                            Color tmp = PixelHelper.GetPixel(contextOriginal, c, r);


                            PixelHelper.SetPixel(context, x, y, tmp);
                        }
                    }

                }
            }

        }



    }
}
