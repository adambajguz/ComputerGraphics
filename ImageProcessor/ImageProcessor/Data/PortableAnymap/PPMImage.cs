using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageProcessor.Data.PortableAnymap
{
    public class PPMImage
    {
        public bool IsBinary { get; }

        public int Width { get; }
        public int Height { get; }
        public int MaximumColorValue { get; }

        public Color[,] Bitmap { get; }

        public PPMImage(bool isBinary, int maximumColorValue, Color[,] bitmap)
        {
            IsBinary = isBinary;
            Width = bitmap.GetLength(0);
            Height = bitmap.GetLength(1);
            MaximumColorValue = maximumColorValue;
            Bitmap = bitmap;
        }

        public WriteableBitmap ConvertToWriteableBitmap()
        {
            var writeableBitmap = new WriteableBitmap(Width, Height);

            using (BitmapContext context = writeableBitmap.GetBitmapContext())
            {
                for (int x = 0; x < Width; ++x)
                    for (int y = 0; y < Height; ++y)
                    {
                        PixelHelper.SetPixel(context, x, y, Bitmap[x, y]);
                    }
            }

            return writeableBitmap;
        }

        public static async Task<PPMImage> Open(StorageFile storageFile)
        {
            if (storageFile is null)
                throw new ArgumentNullException(nameof(storageFile));

            return await LoadFile(storageFile); ;
        }

        private static async Task<PPMImage> LoadFile(StorageFile storageFile)
        {
            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
                byte[] bytes = await GetFileContent(stream);
                string[] lines = Encoding.UTF8.GetString(bytes).Split(new[] { "\r\n", "\r", "\n", "\t" }, StringSplitOptions.None);

                bool isBinary = IsFileBinary(lines[0]);
                if (lines.Length <= 3)
                    throw new Exception("File header broken!"); //File damaged 

                List<string> lst = new List<string>(lines);
                //CheckLinesLengthLessEqual70(lst);
                RemoveComments(lst);
                if (lst.Count < 3)
                    throw new Exception(); //File damaged 

                lst.RemoveAt(0);
                var dimensions = GetFileDimensions(lst[0]);
                lst.RemoveAt(0);
                int colorDepth = GetFileColorDepth(lst[0]);
                lst.RemoveAt(0);
                if (lst.Count == 0)
                    throw new Exception(); //File damaged 

                Color[,] bitmap = new Color[dimensions.Width, dimensions.Height];
                if (isBinary)
                    BinaryGetBitmap(bytes, colorDepth, bitmap);
                else
                    ASCIIGetBitmap(lst, colorDepth, bitmap);

                return new PPMImage(isBinary, colorDepth, bitmap);
            }

            throw new Exception();
        }

        private static void BinaryGetBitmap(byte[] bytes, int colorDepth, Color[,] bitmap)
        {
            int width = bitmap.GetLength(0);
            int height = bitmap.GetLength(1);

            int imageStartIndex = bytes.Length - bitmap.Length * 3;

            int idx = imageStartIndex;
            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x, idx += 3)
                {
                    byte r = bytes[idx];
                    byte g = bytes[idx + 1];
                    byte b = bytes[idx + 2];

                    if (r > colorDepth || g > colorDepth || b > colorDepth)
                        throw new Exception("Image has color value greater than colorDepth!");

                    byte scaledR = ConvertRange(0, colorDepth, 0, 255, r);
                    byte scaledG = ConvertRange(0, colorDepth, 0, 255, g);
                    byte scaledB = ConvertRange(0, colorDepth, 0, 255, b);

                    bitmap[x, y] = Color.FromArgb(255, scaledR, scaledG, scaledB);
                }
        }

        private static void ASCIIGetBitmap(List<string> lst, int colorDepth, Color[,] bitmap)
        {
            string colors = ConvertListOfStringsToString(lst);

            string[] singleValues = colors.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

            if (singleValues.Length != bitmap.Length * 3)
                throw new Exception("Bitmap content too short!");

            int width = bitmap.GetLength(0);
            int height = bitmap.GetLength(1);

            int idx = 0;
            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x, idx += 3)
                {
                    string rs = singleValues[idx];
                    string gs = singleValues[idx + 1];
                    string bs = singleValues[idx + 2];

                    if (!byte.TryParse(rs, out byte r) || !byte.TryParse(gs, out byte g) || !byte.TryParse(bs, out byte b))
                        throw new Exception("Image has invalid color number!");

                    if (r > colorDepth || g > colorDepth || b > colorDepth)
                        throw new Exception("Image has color value greater than colorDepth!");

                    byte scaledR = ConvertRange(0, colorDepth, 0, 255, r);
                    byte scaledG = ConvertRange(0, colorDepth, 0, 255, g);
                    byte scaledB = ConvertRange(0, colorDepth, 0, 255, b);

                    bitmap[x, y] = Color.FromArgb(255, scaledR, scaledG, scaledB);
                }
        }

        public static byte ConvertRange(int originalStart, int originalEnd, int newStart, int newEnd, byte value)
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (byte)(newStart + ((value - originalStart) * scale));
        }

        private static string ConvertListOfStringsToString(List<string> lst)
        {
            var sb = new StringBuilder();

            foreach (string s in lst)
            {
                sb.Append(s).Append(" ");
            }

            return sb.Remove(sb.Length - 1, 1).ToString(); // Removes last " "
        }

        private static bool IsFileBinary(string line)
        {
            if (line == "P3")
                return false;
            else if (line == "P6")
                return true;
            else
                throw new Exception("Incorrect magic number!"); //File damaged 
        }

        private static (int Width, int Height) GetFileDimensions(string line)
        {
            string[] size = line.Split(null);
            if (size.Length != 2)
                throw new Exception("Image dimensions not specified correctly!");

            if (!int.TryParse(size[0], out int w) || !int.TryParse(size[1], out int h))
                throw new Exception("Image dimensions are not int type!");

            return (Width: w, Height: h);
        }

        private static int GetFileColorDepth(string line)
        {
            if (!int.TryParse(line, out int d))
                throw new Exception("Color depth is not int!");

            if (d > 255)
                throw new Exception("Color depth should be [0-255]!");

            return d;
        }

        private static void CheckLinesLengthLessEqual70(List<string> lst)
        {
            for (int i = 0; i < lst.Count; ++i)
            {
                if (lst[i].Length > 70)
                    throw new Exception("Each line in file should have length less or equal 70!");
            }
        }

        private static void RemoveComments(List<string> lst)
        {
            for (int i = 0; i < lst.Count; ++i)
            {
                if (lst[i].StartsWith('#'))
                {
                    lst.RemoveAt(i);
                    --i;
                }
            }
        }

        private static async Task<byte[]> GetFileContent(IRandomAccessStream stream)
        {
            // This is where the byteArray to be stored.
            var bytes = new byte[stream.Size];

            // This returns IAsyncOperationWithProgess, so you can add additional progress handling
            await stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, InputStreamOptions.ReadAhead);

            return bytes;
        }
    }
}

