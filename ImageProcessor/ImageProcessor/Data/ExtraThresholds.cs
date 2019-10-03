using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public static class ExtraThresholds
    {
        public static WriteableBitmap PercentageBlackSelectionCalculate(WriteableBitmap writeableBitmap, double percentage)
        {
            int? threshold = PercentageBlackSelectionThreshold(writeableBitmap, percentage);
            if (threshold == null)
                return null;

            return BinaryzationHelper.ManualBinaryzation((int)threshold, writeableBitmap);
        }

        public static int? PercentageBlackSelectionThreshold(WriteableBitmap writeableBitmap, double percentage)
        {
            ImageHistogramData bitmapHistogramData = new ImageHistogramData(writeableBitmap);
            if (bitmapHistogramData.IsGrayscale)
            {
                var grayscaleHistogram = bitmapHistogramData.GrayscaleHistogram;
                int desiredPixelsCount = (int)Math.Floor(writeableBitmap.PixelHeight * writeableBitmap.PixelWidth * percentage);

                for (int count = 0, i = 0; i < 256; i++)
                {
                    count += grayscaleHistogram[i];

                    if (count > desiredPixelsCount)
                        return Math.Min(255, i);
                }
            }

            return null;
        }

        public static WriteableBitmap MinimumErrorCalculate(WriteableBitmap writeableBitmap, out int? threshold)
        {
            threshold = MinimumErrorThreshold(writeableBitmap);
            if (threshold == null)
                return null;

            return BinaryzationHelper.ManualBinaryzation((int)threshold, writeableBitmap);
        }

        public static int? MinimumErrorThreshold(WriteableBitmap writeableBitmap)
        {
            ImageHistogramData bitmapHistogramData = new ImageHistogramData(writeableBitmap);
            if (bitmapHistogramData.IsGrayscale)
            {
                var grayscaleHistogram = bitmapHistogramData.GrayscaleHistogram;

            }

            return null;
        }
    }
}
