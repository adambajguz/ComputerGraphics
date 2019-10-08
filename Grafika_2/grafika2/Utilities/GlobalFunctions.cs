using grafika2.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grafika2.Utilities
{
    public static class GlobalFunctions
    {
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static int[] CreateRValuesTable(Bitmap imgSrc)
        {
            int[] values = new int[256];
            Color color;

            for (int i = 0; i < 256; i++)
            {
                values[i] = 0;
            }

            for (int i = 0; i < imgSrc.Width; i++)
            {
                for (int j = 0; j < imgSrc.Height; j++)
                {
                    color = imgSrc.GetPixel(i, j);
                    values[color.R] += 1;
                }
            }

            return values;
        }

        public static int[] CreateGValuesTable(Bitmap imgSrc)
        {
            int[] values = new int[256];
            Color color;

            for (int i = 0; i < 256; i++)
            {
                values[i] = 0;
            }

            for (int i = 0; i < imgSrc.Width; i++)
            {
                for (int j = 0; j < imgSrc.Height; j++)
                {
                    color = imgSrc.GetPixel(i, j);
                    values[color.G] += 1;
                }
            }

            return values;
        }

        public static int[] CreateBValuesTable(Bitmap imgSrc)
        {
            int[] values = new int[256];
            Color color;

            for (int i = 0; i < 256; i++)
            {
                values[i] = 0;
            }

            for (int i = 0; i < imgSrc.Width; i++)
            {
                for (int j = 0; j < imgSrc.Height; j++)
                {
                    color = imgSrc.GetPixel(i, j);
                    values[color.B] += 1;
                }
            }

            return values;
        }

        public static void CreateRGBTables(Bitmap imgSrc, out int[] redTable, out int[] greenTable, out int[] blueTable)
        {
            Color color;
            redTable = new int[256];
            greenTable = new int[256];
            blueTable = new int[256];

            for (int i = 0; i < 256; i++)
            {
                redTable[i] = 0;
                greenTable[i] = 0;
                blueTable[i] = 0;
            }

            for (int i = 0; i < imgSrc.Width; i++)
            {
                for (int j = 0; j < imgSrc.Height; j++)
                {
                    color = imgSrc.GetPixel(i, j);
                    redTable[color.R] += 1;
                    greenTable[color.G] += 1;
                    blueTable[color.B] += 1;
                }
            }
        }

        public static int[] CreateLUTWithIndexValues()
        {
            int[] result = new int[256];
            for (int i = 0; i < 256; i++)
            {
                result[i] = i;
            }
            return result;
        }

        public static long[] CreateLUTWithIndexValuesLong()
        {
            long[] result = new long[256];
            for (long i = 0; i < 256; i++)
            {
                result[i] = i;
            }
            return result;
        }

        public static decimal[] CreateLUTWithIndexValuesDecimal()
        {
            decimal[] result = new decimal[256];
            for (int i = 0; i < 256; i++)
            {
                result[i] = i;
            }
            return result;
        }

        /// <summary>
        /// Przycina tablice do wartosci z przedzialu <0; 255>
        /// </summary>
        /// <param name="input">Tablica wejsciowa</param>
        /// <returns></returns>
        public static int[] TrimTableToSection(int[] input)
        {
            decimal onePoint = input.Max() / 255.0M;
            for (int i = 0; i < 256; i++)
            {
                input[i] = Decimal.ToInt32((decimal)input[i] / onePoint);
            }
            return input;
        }

        public static int[] TrimTableToSectionLong(long[] input)
        {
            decimal onePoint = input.Max() / 255.0M;
            for (int i = 0; i < 256; i++)
            {
                input[i] = Decimal.ToInt32((decimal)input[i] / onePoint);
            }

            int[] result = new int[256];
            for (int i = 0; i < 256; i++)
            {
                result[i] = Convert.ToInt32(input[i]);
            }
            return result;
        }

        public static int[] TrimTableToSectionDecimal(decimal[] input)
        {
            decimal onePoint = input.Max() / 255.0M;
            for (int i = 0; i < 256; i++)
            {
                input[i] = Decimal.ToInt32(input[i] / onePoint);
            }

            int[] result = new int[256];
            for (int i = 0; i < 256; i++)
            {
                result[i] = Convert.ToInt32(input[i]);
            }
            return result;
        }

        public static Bitmap ApplyLUTToImage(Bitmap imgSrc, int[] LUT)
        {
            Color color, newColor;
            for (int i = 0; i < imgSrc.Width; i++)
            {
                for (int j = 0; j < imgSrc.Height; j++)
                {
                    color = imgSrc.GetPixel(i, j);
                    newColor = Color.FromArgb(255, LUT[color.R], LUT[color.G], LUT[color.B]);
                    imgSrc.SetPixel(i, j, newColor);
                }
            }
            return imgSrc;
        }

        public static Bitmap ConvertBitmapToGrayScale(Bitmap src)
        {
            //create a blank bitmap the same size as original
            Bitmap result = new Bitmap(src.Width, src.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(result);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][] 
                {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
                });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(src, new Rectangle(0, 0, src.Width, src.Height),
               0, 0, src.Width, src.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return result;
        }

        public static Bitmap BinarizeImage(Bitmap imgSrc, int prog)
        {
            var gray = GlobalFunctions.ConvertBitmapToGrayScale(imgSrc);
            for (int i = 0; i < gray.Width; i++)
            {
                for (int j = 0; j < gray.Height; j++)
                {
                    var pixel = (int)((gray.GetPixel(i, j).GetBrightness()) * 260);
                    if (pixel < prog)
                        gray.SetPixel(i, j, Color.Black);
                    else
                        gray.SetPixel(i, j, Color.White);
                }
            }
            return gray;
        }

        #region prog OTSU
        //http://www.codeproject.com/Articles/38319/Famous-Otsu-Thresholding-in-C
        public static int GetOTSUThreshold(Bitmap imgSrc)
        {
            byte prog = 0;
            float[] vet = new float[256];
            int[] hist = new int[256];
            vet.Initialize();

            float p1, p2, p12;
            int k;

            BitmapData bitmapData = imgSrc.LockBits(new Rectangle(0, 0, imgSrc.Width, imgSrc.Height),
            ImageLockMode.ReadOnly, imgSrc.PixelFormat);
            unsafe
            {
                byte* p = (byte*)(void*)bitmapData.Scan0.ToPointer();


                //tworzenie histogramu
                hist.Initialize();
                for (int i = 0; i < imgSrc.Height; i++)
                {
                    for (int j = 0; j < imgSrc.Width * 3; j += 3)
                    {
                        int index = i * bitmapData.Stride + j;
                        hist[p[index]]++;
                    }
                }


                for (k = 1; k != 255; k++)
                {
                    p1 = Px(0, k, hist);
                    p2 = Px(k + 1, 255, hist);
                    p12 = p1 * p2;
                    if (p12 == 0)
                        p12 = 1;
                    float diff = (Mx(0, k, hist) * p2) - (Mx(k + 1, 255, hist) * p1);
                    vet[k] = (float)diff * diff / p12;

                }
            }
            imgSrc.UnlockBits(bitmapData);

            prog = (byte)FindMax(vet, 256);

            return prog;
        }

        // function is used to compute the q values in the equation
        public static float Px(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += hist[i];

            return (float)sum;
        }

        // function is used to compute the mean values in the equation (mu)
        public static float Mx(int init, int end, int[] hist)
        {
            int sum = 0;
            int i;
            for (i = init; i <= end; i++)
                sum += i * hist[i];

            return (float)sum;
        }

        public static int FindMax(float[] vec, int n)
        {
            float maxVec = 0;
            int idx = 0;
            int i;

            for (i = 1; i < n - 1; i++)
            {
                if (vec[i] > maxVec)
                {
                    maxVec = vec[i];
                    idx = i;
                }
            }
            return idx;
        }
        #endregion //prog OTSU

        /// <summary>
        /// Binaryzacja obrazu z progiem wyznaczanym metodą Niblacka
        /// </summary>
        /// <param name="imgSrc">Obraz źródłowy</param>
        /// <param name="m">Rozmiar okna</param>
        /// <param name="k">Parametr progowania</param>
        /// <returns></returns>
        public static Bitmap BinarizeNiblack(Bitmap imgSrc, int m, double k)
        {
            Bitmap img = (Bitmap)imgSrc.Clone();
            var gray = GlobalFunctions.ConvertBitmapToGrayScale(img);

            int diff = (m - 1) / 2;
            List<double> values = new List<double>();
            List<int> progi = new List<int>();

            for (int x = 0; x < gray.Width; x++)
            {
                for (int y = 0; y < gray.Height; y++)
                {
                    values.Clear();
                    for (int i = x - diff; i <= x + diff; i++)
                    {
                        for (int j = y - diff; j <= y + diff; j++)
                        {
                            if (i < 0 || j < 0 || i >= gray.Width || j >= gray.Height || (i == x && j == y)) continue;
                            values.Add(gray.GetPixel(i, j).R);
                        }
                    }
                    double srednia = values.Average();
                    double suma = 0;
                    foreach (var item in values)
                    {
                        suma += Math.Pow(item - srednia, 2);
                    }
                    double odchylenieStd = Math.Sqrt(suma / values.Count());
                    int prog = Convert.ToInt32(srednia + (k * odchylenieStd));

                    var pixel = (int)((gray.GetPixel(x, y).GetBrightness()) * 260);
                    if (pixel < prog) gray.SetPixel(x, y, Color.Black);
                    else gray.SetPixel(x, y, Color.White);
                }
            }

            return gray;
        }

        public static Bitmap ConvolutionFiltering(Bitmap src, double[,] mask)
        {
            List<Pixel> pixels = new List<Pixel>();
            Bitmap result = (Bitmap)src.Clone();
            double m = Math.Sqrt((double)mask.Length);
            int edge = Convert.ToInt32(m);
            m = (m - 1) / 2;
            int diff = Convert.ToInt32(m);
            double filterFactor = 0;
            foreach (var val in mask)
            {
                filterFactor += val;
            }
            filterFactor = filterFactor == 0 ? 1 : 1 / filterFactor;

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    int sumR = 0;
                    int sumG = 0;
                    int sumB = 0;

                    int maskI = 0;
                    int maskJ = 0;
                    for (int j = y - diff; j <= y + diff; j++)
                    {
                        for (int i = x - diff; i <= x + diff; i++)
                        {
                            if (i < 0 || j < 0 || i >= result.Width || j >= result.Height)
                            {
                                maskI++;
                                if (maskI == edge)
                                {
                                    maskI = 0;
                                    maskJ++;
                                }
                                continue;
                            }
                            sumR += (int)(src.GetPixel(i, j).R * mask[maskI, maskJ]);
                            sumG += (int)(src.GetPixel(i, j).G * mask[maskI, maskJ]);
                            sumB += (int)(src.GetPixel(i, j).B * mask[maskI, maskJ]);

                            maskI++;
                            if (maskI == edge)
                            {
                                maskI = 0;
                                maskJ++;
                            }
                        }
                    }

                    sumR = Convert.ToInt32(sumR * filterFactor);
                    sumG = Convert.ToInt32(sumG * filterFactor);
                    sumB = Convert.ToInt32(sumB * filterFactor);

                    if (sumR > 255) sumR = 255;
                    else if (sumR < 0) sumR = 0;
                    if (sumG > 255) sumG = 255;
                    else if (sumG < 0) sumG = 0;
                    if (sumB > 255) sumB = 255;
                    else if (sumB < 0) sumB = 0;
                    pixels.Add(new Pixel(sumR, sumG, sumB));
                }
            }

            int pIndex = 0;
            for (int i = 0; i < src.Height; i++)
            {
                for (int j = 0; j < src.Width; j++)
                {
                    Color newColor = Color.FromArgb(255, pixels[pIndex].R, pixels[pIndex].G, pixels[pIndex].B);
                    result.SetPixel(j, i, newColor);
                    pIndex++;
                }
            }

            return result;
        }

        private static List<Pixel> NormalizePixels(List<Pixel> pixels)
        {
            List<Pixel> result = new List<Pixel>(pixels);

            int maxValR = pixels.Max(o => o.R);
            int maxValG = pixels.Max(o => o.G);
            int maxValB = pixels.Max(o => o.B);
            int minValR = pixels.Min(o => o.R);
            int minValG = pixels.Min(o => o.G);
            int minValB = pixels.Min(o => o.B);

            if (minValR < 0 || maxValR > 255)
            {
                int min = minValR < 0 ? minValR : 0;
                int max = maxValR > 255 ? maxValR : 0;
                int range = max - min;
                for (int i = 0; i < pixels.Count; i++)
                {
                    decimal scale = pixels[i].R / range;
                    result[i].R = (int)scale * 255;
                }
            }

            if (minValG < 0 || maxValG > 255)
            {
                int min = minValG < 0 ? minValG : 0;
                int max = maxValG > 255 ? maxValG : 0;
                int range = max - min;
                for (int i = 0; i < pixels.Count; i++)
                {
                    decimal scale = pixels[i].G / range;
                    result[i].G = (int)scale * 255;
                }
            }

            if (minValB < 0 || maxValB > 255)
            {
                int min = minValB < 0 ? minValB : 0;
                int max = maxValB > 255 ? maxValB : 0;
                int range = max - min;
                for (int i = 0; i < pixels.Count; i++)
                {
                    decimal scale = pixels[i].B / range;
                    result[i].B = (int)scale * 255;
                }
            }

            return result;
        }

        public static Bitmap KuwaharaFilter(Bitmap src)
        {
            Bitmap result = (Bitmap)src.Clone();

            int diff = 2;
            List<int> valuesR = new List<int>();
            List<int> valuesG = new List<int>();
            List<int> valuesB = new List<int>();
            List<VariationAndAverage> variationsR = new List<VariationAndAverage>();
            List<VariationAndAverage> variationsG = new List<VariationAndAverage>();
            List<VariationAndAverage> variationsB = new List<VariationAndAverage>();

            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    variationsR.Clear();
                    variationsG.Clear();
                    variationsB.Clear();

                    //Obszar 1 - lewy gorny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y - diff; j <= y; j++)
                    {
                        for (int i = x - diff; i <= x; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndAverage_Kuwahara(valuesR));
                    variationsG.Add(CalculateVariationAndAverage_Kuwahara(valuesG));
                    variationsB.Add(CalculateVariationAndAverage_Kuwahara(valuesB));

                    //Obszar 2 - prawy gorny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y - diff; j <= y; j++)
                    {
                        for (int i = x; i <= x + diff; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndAverage_Kuwahara(valuesR));
                    variationsG.Add(CalculateVariationAndAverage_Kuwahara(valuesG));
                    variationsB.Add(CalculateVariationAndAverage_Kuwahara(valuesB));

                    //Obszar 3 - lewy dolny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y; j <= y + diff; j++)
                    {
                        for (int i = x - diff; i <= x; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndAverage_Kuwahara(valuesR));
                    variationsG.Add(CalculateVariationAndAverage_Kuwahara(valuesG));
                    variationsB.Add(CalculateVariationAndAverage_Kuwahara(valuesB));

                    //Obszar 4 - prawy dolny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y; j <= y + diff; j++)
                    {
                        for (int i = x; i <= x + diff; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndAverage_Kuwahara(valuesR));
                    variationsG.Add(CalculateVariationAndAverage_Kuwahara(valuesG));
                    variationsB.Add(CalculateVariationAndAverage_Kuwahara(valuesB));

                    double minR = variationsR.Min(o => o.Variation);
                    double minG = variationsG.Min(o => o.Variation);
                    double minB = variationsB.Min(o => o.Variation);
                    double resultValueR = variationsR.Where(o => o.Variation == minR).Select(o => o.Average).First();
                    double resultValueG = variationsG.Where(o => o.Variation == minG).Select(o => o.Average).First();
                    double resultValueB = variationsB.Where(o => o.Variation == minB).Select(o => o.Average).First();

                    Color newColor = Color.FromArgb(255, (int)resultValueR, (int)resultValueG, (int)resultValueB);
                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }

        private static VariationAndAverage CalculateVariationAndAverage_Kuwahara(List<int> values)
        {
            VariationAndAverage result = new VariationAndAverage();
            double average, variation = 0;
            average = values.Average();
            foreach (var item in values)
            {
                variation += Math.Pow((item - average), 2);
            }
            variation = variation / values.Count;

            result.Average = average;
            result.Variation = variation;
            return result;
        }

        public static Bitmap KuwaharaMedianFilter(Bitmap src, int windowLength)
        {
            Bitmap result = (Bitmap)src.Clone();

            int diff = (windowLength - 1) / 2;
            List<int> valuesR = new List<int>();
            List<int> valuesG = new List<int>();
            List<int> valuesB = new List<int>();
            List<VariationAndAverage> variationsR = new List<VariationAndAverage>();
            List<VariationAndAverage> variationsG = new List<VariationAndAverage>();
            List<VariationAndAverage> variationsB = new List<VariationAndAverage>();

            for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    variationsR.Clear();
                    variationsG.Clear();
                    variationsB.Clear();

                    //Obszar 1 - lewy gorny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y - diff; j <= y; j++)
                    {
                        for (int i = x - diff; i <= x; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndAverage_Kuwahara(valuesR));
                    variationsG.Add(CalculateVariationAndAverage_Kuwahara(valuesG));
                    variationsB.Add(CalculateVariationAndAverage_Kuwahara(valuesB));

                    //Obszar 2 - prawy gorny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y - diff; j <= y; j++)
                    {
                        for (int i = x; i <= x + diff; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndMedian(valuesR));
                    variationsG.Add(CalculateVariationAndMedian(valuesG));
                    variationsB.Add(CalculateVariationAndMedian(valuesB));

                    //Obszar 3 - lewy dolny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y; j <= y + diff; j++)
                    {
                        for (int i = x - diff; i <= x; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndMedian(valuesR));
                    variationsG.Add(CalculateVariationAndMedian(valuesG));
                    variationsB.Add(CalculateVariationAndMedian(valuesB));

                    //Obszar 4 - prawy dolny rog
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y; j <= y + diff; j++)
                    {
                        for (int i = x; i <= x + diff; i++)
                        {
                            if (i < 0 || j < 0 || j >= src.Height || i >= src.Width) continue;
                            Color color = src.GetPixel(i, j);
                            valuesR.Add(color.R);
                            valuesG.Add(color.G);
                            valuesB.Add(color.B);
                        }
                    }
                    variationsR.Add(CalculateVariationAndMedian(valuesR));
                    variationsG.Add(CalculateVariationAndMedian(valuesG));
                    variationsB.Add(CalculateVariationAndMedian(valuesB));

                    double minR = variationsR.Min(o => o.Variation);
                    double minG = variationsG.Min(o => o.Variation);
                    double minB = variationsB.Min(o => o.Variation);
                    double resultValueR = variationsR.Where(o => o.Variation == minR).Select(o => o.Average).First();
                    double resultValueG = variationsG.Where(o => o.Variation == minG).Select(o => o.Average).First();
                    double resultValueB = variationsB.Where(o => o.Variation == minB).Select(o => o.Average).First();

                    Color newColor = Color.FromArgb(255, (int)resultValueR, (int)resultValueG, (int)resultValueB);
                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }

        public static Bitmap MedianFilter(Bitmap src, int windowLength)
        {
            Bitmap result = (Bitmap)src.Clone();

            int diff = (windowLength - 1) / 2;
            List<int> valuesR = new List<int>();
            List<int> valuesG = new List<int>();
            List<int> valuesB = new List<int>();
            List<VariationAndAverage> variationsR = new List<VariationAndAverage>();
            List<VariationAndAverage> variationsG = new List<VariationAndAverage>();
            List<VariationAndAverage> variationsB = new List<VariationAndAverage>();

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    int sumR = 0;
                    int sumG = 0;
                    int sumB = 0;
                    valuesR.Clear();
                    valuesG.Clear();
                    valuesB.Clear();
                    for (int j = y - diff; j <= y + diff; j++)
                    {
                        for (int i = x - diff; i <= x + diff; i++)
                        {
                            if (i < 0 || j < 0 || i >= result.Width || j >= result.Height)
                            {
                                continue;
                            }
                            Color currentColor = src.GetPixel(i, j);
                            valuesR.Add((int)(currentColor.R));
                            valuesG.Add((int)(currentColor.G));
                            valuesB.Add((int)(currentColor.B));
                        }
                    }

                    sumR = Convert.ToInt32(GetMedian(valuesR));
                    sumG = Convert.ToInt32(GetMedian(valuesG));
                    sumB = Convert.ToInt32(GetMedian(valuesB));

                    if (sumR > 255) sumR = 255;
                    else if (sumR < 0) sumR = 0;
                    if (sumG > 255) sumG = 255;
                    else if (sumG < 0) sumG = 0;
                    if (sumB > 255) sumB = 255;
                    else if (sumB < 0) sumB = 0;
                    Color newColor = Color.FromArgb(255, sumR, sumG, sumB);
                    result.SetPixel(x, y, newColor);
                }
            }

            return result;
        }

        private static VariationAndAverage CalculateVariationAndMedian(List<int> values)
        {
            VariationAndAverage result = new VariationAndAverage();
            double average, variation = 0;
            average = values.Average();
            foreach (var item in values)
            {
                variation += Math.Pow((item - average), 2);
            }
            variation = variation / values.Count;

            result.Average = Convert.ToDouble(GetMedian(values));
            result.Variation = variation;
            return result;
        }

        public static decimal GetMedian(this IEnumerable<int> source)
        {
            // Create a copy of the input, and sort the copy
            int[] temp = source.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                int a = temp[count / 2 - 1];
                int b = temp[count / 2];
                return (a + b) / 2m;
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }

        public static void DrawBezierSpline(Graphics grfx, Pen pen, Point[] aptDefine)
        {
            Point[] apt = new Point[100];
            for (int i = 0; i < apt.Length; i++)
            {
                float t = (float)i / (apt.Length - 1);
                float x = (1 - t) * (1 - t) * (1 - t) * aptDefine[0].X +
                3 * t * (1 - t) * (1 - t) * aptDefine[1].X +
                3 * t * t * (1 - t) * aptDefine[2].X +
                t * t * t * aptDefine[3].X;
                float y = (1 - t) * (1 - t) * (1 - t) * aptDefine[0].Y +
                3 * t * (1 - t) * (1 - t) * aptDefine[1].Y +
                3 * t * t * (1 - t) * aptDefine[2].Y +
                t * t * t * aptDefine[3].Y;
                apt[i] = new Point((int)Math.Round(x), (int)Math.Round(y));
            }
            grfx.DrawLines(pen, apt);
        }
    }
}
