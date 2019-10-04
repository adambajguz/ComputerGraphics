namespace ImageProcessor.Pages
{
    using System;
    using System.Linq;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using ImageProcessor.Data.PortableAnymap;
    using Microsoft.Graphics.Canvas;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Imaging;

    public sealed partial class MainPage
    {
        private async Task<bool> ShowFileTryOpenDialog()
        {
            ContentDialog deleteFileDialog = new ContentDialog
            {
                Title = "Error: unsupported file",
                Content = "An error occured during file opening.\n\nDo you want to try opening anyway?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();

            // Delete the file if the user clicked the primary button.
            /// Otherwise, do nothing.
            if (result == ContentDialogResult.Primary)
                return true;

            return false;
        }

        private async void OpenImageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };

            picker.FileTypeFilter.Add(".bmp");
            picker.FileTypeFilter.Add(".heic");
            picker.FileTypeFilter.Add(".heif");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");
            picker.FileTypeFilter.Add(".tif");
            picker.FileTypeFilter.Add(".tiff");
            picker.FileTypeFilter.Add(".ppm");
            picker.FileTypeFilter.Add("*");

            StorageFile inputFile = await picker.PickSingleFileAsync();
            if (inputFile != null)
            {
                if (!(IsKnownFile(inputFile.FileType) || IsKnownFileMime(inputFile.ContentType)))
                {
                    if (!(await ShowFileTryOpenDialog()))
                        return;
                }
                ContentFrame_Reset();

                ImageFileTextBox.Text = inputFile.Path;

                if (InputImageStream != null)
                {
                    InputImageStream.Dispose();
                    InputImageStream = null;
                }

                if (inputFile.FileType == ".ppm")
                {
                    try
                    {
                        PPMImage ppmImage = await PPMImage.Open(inputFile);
                        WriteableOutputImage = ppmImage.ConvertToWriteableBitmap();
                    }
                    catch (Exception)
                    {
                        await ShowErrorDialog("An error occured during file open. Damaged file!");
                        return;
                    }



                    SoftwareBitmap softWriteableOutputImage = SoftwareBitmap.CreateCopyFromBuffer(WriteableOutputImage.PixelBuffer, BitmapPixelFormat.Bgra8, WriteableOutputImage.PixelWidth, WriteableOutputImage.PixelHeight);
                    InputImageStream = await GetRandomAccessStreamFromSoftwareBitmap(softWriteableOutputImage, BitmapEncoder.PngEncoderId);
                    await LoadInputVirtualBitmap();

                    await UpdateOutputImage();
                }
                else
                {
                    try
                    {
                        using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
                        {
                            WriteableBitmap writeableInputImage;

                            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                            SoftwareBitmap softwareBitmap1 = await decoder.GetSoftwareBitmapAsync();
                            ImageResolution.Text = softwareBitmap1.PixelWidth + " x " + softwareBitmap1.PixelHeight;

                            writeableInputImage = new WriteableBitmap(softwareBitmap1.PixelWidth, softwareBitmap1.PixelHeight);
                            writeableInputImage.SetSource(stream);

                            InputImageStream = stream;

                            await LoadInputVirtualBitmap();
                        }

                        //  inputImage.Source = writeableInputImage;


                        using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
                        {
                            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                            SoftwareBitmap softwareBitmap1 = await decoder.GetSoftwareBitmapAsync();
                            WriteableOutputImage = new WriteableBitmap(softwareBitmap1.PixelWidth, softwareBitmap1.PixelHeight);
                            WriteableOutputImage.SetSource(stream);
                            WriteableOutputImageCopy = WriteableOutputImage.Clone();

                            PrevOutputs.Push(WriteableOutputImage.Clone());
                            UndoFlyoutItem.IsEnabled = false;

                            await UpdateOutputImage();
                        }
                    }
                    catch (Exception)
                    {
                        await ShowErrorDialog("An error occured during file open. Damaged file!");
                        return;
                    }
                }

                //outputImage.Source = writableOutputImage;
            }
            else
            {
                //Operation cancelled
                return;
            }

            ReopenFlyoutItem.IsEnabled = true;
            SaveMenuFlyoutItem.IsEnabled = true;
            EditMenuBarItem.IsEnabled = true;
            AdvancedToolsMenuBarItem.IsEnabled = true;
            ToolsMenuBarItem.IsEnabled = true;
            ZoomCommandBar.IsEnabled = true;

            //await Open();
        }

        private bool IsKnownFileMime(string mime)
        {
            string[] mimes = new string[] { MediaTypeNames.Image.Gif,
                                            MediaTypeNames.Image.Jpeg,
                                            MediaTypeNames.Image.Tiff,
                                            "image/bmp",
                                            "image/tiff",
                                            "image/png"
                                          };

            return mimes.Contains(mime);
        }

        private bool IsKnownFile(string type)
        {
            string[] types = new string[] { ".bmp",
                                            ".heic",
                                            ".heif",
                                            ".jpg",
                                            ".jpeg",
                                            ".png",
                                            ".gif",
                                            ".tif",
                                            ".tiff",
                                            ".ppm"
                                          };

            return types.Contains(type);
        }

        private async void ReOpenImageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame_Reset();

            WriteableOutputImage = WriteableOutputImageCopy.Clone();
            PrevOutputs.Clear();
            PrevOutputs.Push(WriteableOutputImage.Clone());

            await UpdateOutputImage();
        }

        public async Task UpdateOutputImage()
        {
            SoftwareBitmap softWriteableOutputImage = SoftwareBitmap.CreateCopyFromBuffer(WriteableOutputImage.PixelBuffer, BitmapPixelFormat.Bgra8, WriteableOutputImage.PixelWidth, WriteableOutputImage.PixelHeight);

            OutputImageStream = await GetRandomAccessStreamFromSoftwareBitmap(softWriteableOutputImage, BitmapEncoder.PngEncoderId);

            await LoadOutputVirtualBitmap();
        }

        private async Task<IRandomAccessStream> GetRandomAccessStreamFromSoftwareBitmap(SoftwareBitmap soft, Guid encoderId)
        {
            // Use an encoder to copy from SoftwareBitmap to an in-mem stream (FlushAsync)

            InMemoryRandomAccessStream inMemoryStream = new InMemoryRandomAccessStream();

            BitmapEncoder encoder = await BitmapEncoder.CreateAsync(encoderId, inMemoryStream);
            encoder.SetSoftwareBitmap(soft);

            await encoder.FlushAsync();

            return inMemoryStream;
        }

        private async Task LoadInputVirtualBitmap()
        {
            ContentFrame_Reset();
            ContentFrameCollapse();

            if (InputVirtualBitmap != null)
            {
                InputVirtualBitmap.Dispose();
                InputVirtualBitmap = null;
            }

            //LoadedImageInfo = "";

            InputVirtualBitmap = await CanvasVirtualBitmap.LoadAsync(InputImageCanvas.Device, InputImageStream, CanvasVirtualBitmapOptions.None);

            if (InputImageCanvas == null)
            {
                // This can happen if the page is unloaded while LoadAsync is running
                return;
            }

            var size = InputVirtualBitmap.Size;
            InputImageCanvas.Width = size.Width * (Zoom + 1);
            InputImageCanvas.Height = size.Height * (Zoom + 1);
            InputImageCanvas.Invalidate();

            //LoadedImageInfo = string.Format("{0}x{1} image, is {2}CachedOnDemand", size.Width, size.Height, virtualBitmap.IsCachedOnDemand ? "" : "not ");
        }

        private async Task LoadOutputVirtualBitmap()
        {
            if (OutputVirtualBitmap != null)
            {
                OutputVirtualBitmap.Dispose();
                OutputVirtualBitmap = null;
            }

            //LoadedImageInfo = "";

            OutputVirtualBitmap = await CanvasVirtualBitmap.LoadAsync(OutputImageCanvas.Device, OutputImageStream, CanvasVirtualBitmapOptions.None);

            if (OutputImageCanvas == null)
            {
                // This can happen if the page is unloaded while LoadAsync is running
                return;
            }

            ImageResolution.Text = InputVirtualBitmap.Bounds.Width + " x " + InputVirtualBitmap.Bounds.Height + " | " + OutputVirtualBitmap.Bounds.Width + " x " + OutputVirtualBitmap.Bounds.Height;

            var size = OutputVirtualBitmap.Size;
            OutputImageCanvas.Width = size.Width * (Zoom + 1);
            OutputImageCanvas.Height = size.Height * (Zoom + 1);
            OutputImageCanvas.Invalidate();

            //LoadedImageInfo = string.Format("{0}x{1} image, is {2}CachedOnDemand", size.Width, size.Height, virtualBitmap.IsCachedOnDemand ? "" : "not ");
        }
    }
}
