using System;
using System.Diagnostics;
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

                for (int count = 0, i = 0; i < 256; ++i)
                {
                    count += grayscaleHistogram[i];

                    if (count > desiredPixelsCount)
                        return Math.Min(255, i);
                }
            }

            return null;
        }

        public static WriteableBitmap EntropySelectionCalculate(WriteableBitmap writeableBitmap, out int? threshold)
        {
            threshold = EntroypSelectionThreshold(writeableBitmap);
            if (threshold == null)
                return null;

            return BinaryzationHelper.ManualBinaryzation((int)threshold, writeableBitmap);
        }

        public static int? EntroypSelectionThreshold(WriteableBitmap writeableBitmap)
        {
            ImageHistogramData bitmapHistogramData = new ImageHistogramData(writeableBitmap);
            if (bitmapHistogramData.IsGrayscale)
            {
                var grayscaleHistogram = bitmapHistogramData.GrayscaleHistogram;

                int threshold = 0;
                double max_entrophy = Double.MinValue;
                int length = grayscaleHistogram.Length;

                for (int i = 0; i < length; ++i)
                {
                    double background_entrophy = CalculateBackgroundEntropy(grayscaleHistogram, i);
                    double foreground_entrophy = CalculateForegorundEntropy(grayscaleHistogram, length, i);

                    Debug.WriteLine(background_entrophy + foreground_entrophy + " : " + max_entrophy);
                    if (background_entrophy + foreground_entrophy > max_entrophy)
                    {
                        max_entrophy = background_entrophy + foreground_entrophy;
                        threshold = i;
                    }
                }

                return threshold;
            }

            return null;
        }

        private static double CalculateForegorundEntropy(int[] grayscaleHistogram, int length, int i)
        {
            int sum = 0;
            for (int j = i; j < length; ++j)
                sum += grayscaleHistogram[j];

            double foreground_entrophy = 0;
            for (int j = i; j < length; ++j)
                foreground_entrophy += (double)grayscaleHistogram[j] / sum * Math.Log((grayscaleHistogram[j] != 0) ? (double)grayscaleHistogram[j] / sum : 1);

            return foreground_entrophy * -1;
        }

        private static double CalculateBackgroundEntropy(int[] grayscaleHistogram, int i)
        {
            int sum = 0;
            for (int j = 0; j < i; ++j)
                sum += grayscaleHistogram[j];

            double background_entrophy = 0;
            for (int j = 0; j < i; ++j)
                background_entrophy += (double)grayscaleHistogram[j] / sum * Math.Log((grayscaleHistogram[j] != 0) ? (double)grayscaleHistogram[j] / sum : 1);

            return background_entrophy * -1;
        }
    }
}
