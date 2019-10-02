﻿using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data
{
    public class NiblackThreshold
    {

        private readonly int neighborhoodSize;
        private readonly double k;

        /**
         * neighborhood - the size of the box to use for the local threshold. Must be odd.
         * k - coefficient for the standard deviation.
         */
        public NiblackThreshold(int neighborhoodSize, double k)
        {
            this.neighborhoodSize = neighborhoodSize;
            this.k = k;
        }

        public WriteableBitmap Threshold(WriteableBitmap input)
        {
            int rows = input.PixelHeight;
            int cols = input.PixelWidth;
            short[,] data = new short[cols, rows];


            using (var context = input.GetBitmapContext(ReadWriteMode.ReadOnly))
            {
                int[] pixels = context.Pixels;

                for (int x = 0; x < input.PixelWidth; ++x)
                    for (int y = 0; y < input.PixelHeight; ++y)
                    {

                        var c = pixels[y * context.Width + x];
                        var a = (byte)(c >> 24);

                        // Prevent division by zero
                        int ai = a;
                        if (ai == 0)
                        {
                            ai = 1;
                        }

                        // Scale inverse alpha to use cheap integer mul bit shift
                        ai = ((255 << 8) / ai);
                        //Color tmp = Color.FromArgb(a, (byte)((((c >> 16) & 0xFF) * ai) >> 8),
                        //                              (byte)((((c >> 8) & 0xFF) * ai) >> 8),
                        //                              (byte)((((c & 0xFF) * ai) >> 8)));


                        data[x, y] = (byte)((((c >> 16) & 0xFF) * ai) >> 8); // sets from red
                    }
            }


            double[,] dataFloat = ArrayUtil.toDoubleArray(data);
            double[,] stdev = ArrayUtil.stdevNeighborhood(dataFloat, neighborhoodSize);
            double[,] mean = ArrayUtil.meanNeighborhood(dataFloat, neighborhoodSize);
            double[,] threshold = ArrayUtil.add(dataFloat, mean, ArrayUtil.multiplyEach(dataFloat, stdev, k));

            input.ForEach((x, y, curColor) =>
            {
                if (curColor.R > threshold[x,y])
                    return Color.FromArgb(255, 255, 255, 255);

                return Color.FromArgb(255, 0, 0, 0);
            });
        
            return input;
        }
    }
}
