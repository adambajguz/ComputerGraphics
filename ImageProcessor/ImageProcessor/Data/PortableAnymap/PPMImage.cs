using System;
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

        public Color[] Bitmap { get; }

        public PPMImage(bool isBinary, int width, int height, int maximumColorValue, Color[] bitmap)
        {
            IsBinary = isBinary;
            Width = width;
            Height = height;
            MaximumColorValue = maximumColorValue;
            Bitmap = bitmap;
        }

        public WriteableBitmap ConvertToWriteableBitmap()
        {
            return new WriteableBitmap(Width, Height);
        }

        public static async Task<PPMImage> Open(StorageFile storageFile)
        {
            if (storageFile is null)
                throw new ArgumentNullException(nameof(storageFile));

            await LoadFile(storageFile);

            return new PPMImage(false, 0, 0, 0, null);
        }

        private static async Task LoadFile(StorageFile storageFile)
        {
            using (IRandomAccessStream stream = await storageFile.OpenAsync(FileAccessMode.Read))
            {
                string[] lines = await GetFileContentAsString(stream);

                throw new Exception(); //File damaged 
            }
        }

        private static async Task<string[]> GetFileContentAsString(IRandomAccessStream stream)
        {
            // This is where the byteArray to be stored.
            var bytes = new byte[stream.Size];

            // This returns IAsyncOperationWithProgess, so you can add additional progress handling
            await stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, InputStreamOptions.ReadAhead);

            string content = Encoding.UTF8.GetString(bytes);
            string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            return lines;
        }
    }
}
