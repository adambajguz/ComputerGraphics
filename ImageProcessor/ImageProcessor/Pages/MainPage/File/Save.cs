namespace ImageProcessor.Pages
{
    using System;
    using System.Collections.Generic;
    using ImageProcessor.Dialogs;
    using Windows.Graphics.Imaging;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Provider;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage
    {
        private async void SaveOutputAsImageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {

            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary
            };
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add(".bmp", new List<string>() { ".bmp" });
            savePicker.FileTypeChoices.Add(".heif", new List<string>() { ".heif" });
            savePicker.FileTypeChoices.Add(".heic", new List<string>() { ".heic" });
            savePicker.FileTypeChoices.Add(".jpg", new List<string>() { ".jpg" });
            savePicker.FileTypeChoices.Add(".jpeg", new List<string>() { ".jpeg" });
            savePicker.FileTypeChoices.Add(".jpeg-xr", new List<string>() { ".jpeg" });
            savePicker.FileTypeChoices.Add(".png", new List<string>() { ".png" });
            savePicker.FileTypeChoices.Add(".gif", new List<string>() { ".gif" });
            savePicker.FileTypeChoices.Add(".tiff", new List<string>() { ".tiff" });
            savePicker.FileTypeChoices.Add(".tif", new List<string>() { ".tif" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file

                SoftwareBitmap softWriteableOutputImage = SoftwareBitmap.CreateCopyFromBuffer(WriteableOutputImage.PixelBuffer, BitmapPixelFormat.Bgra8, WriteableOutputImage.PixelWidth, WriteableOutputImage.PixelHeight);

                SaveSoftwareBitmapToFile(softWriteableOutputImage, file);



                // Let Windows know that we're finished changing the file so the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                {
                    //OutputTextBlock.Text = "File " + file.Name + " was saved.";
                }
                else
                {
                    //OutputTextBlock.Text = "File " + file.Name + " couldn't be saved.";
                }
            }
            else
            {
                //OutputTextBlock.Text = "Operation cancelled.";
            }

        }

        private async void SaveSoftwareBitmapToFile(SoftwareBitmap softwareBitmap, StorageFile outputFile)
        {
            Guid? encoderType = FileTypeExtensionToBitmapEncoder(outputFile.FileType);

            if (encoderType == null)
                return;

            using (IRandomAccessStream stream = await outputFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapEncoder encoder = null;

                bool? jpegx = false;
                if (encoderType != BitmapEncoder.JpegEncoderId && encoderType != BitmapEncoder.JpegXREncoderId)
                {
                    jpegx = null;
                }
                else if (encoderType == BitmapEncoder.JpegEncoderId)
                {
                    jpegx = false;
                }
                else if (encoderType == BitmapEncoder.JpegXREncoderId)
                {
                    jpegx = true;
                }

                SaveDialog dialog = new SaveDialog(jpegx);
                ContentDialogResult result = await dialog.ShowAsync();

                if (encoderType == BitmapEncoder.JpegEncoderId || encoderType == BitmapEncoder.JpegXREncoderId)
                {
                    if (result == ContentDialogResult.Secondary)
                    {
                        var propertySet = new BitmapPropertySet();
                        propertySet.Add("ImageQuality", new BitmapTypedValue(dialog.Quality, Windows.Foundation.PropertyType.Single));

                        if (encoderType == BitmapEncoder.JpegXREncoderId)
                        {
                            propertySet.Add("Lossless", new BitmapTypedValue(dialog.Lossless, Windows.Foundation.PropertyType.Boolean));
                        }

                        encoder = await BitmapEncoder.CreateAsync((Guid)encoderType, stream, propertySet);
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    // Create an encoder with the desired format
                    encoder = await BitmapEncoder.CreateAsync((Guid)encoderType, stream);
                }

                // Set the software bitmap
                encoder.SetSoftwareBitmap(softwareBitmap);
                // Set additional encoding parameters, if needed
                //encoder.BitmapTransform.ScaledWidth = 320;
                //encoder.BitmapTransform.ScaledHeight = 240;
                //encoder.BitmapTransform.Rotation = Windows.Graphics.Imaging.BitmapRotation.Clockwise90Degrees;
                encoder.BitmapTransform.InterpolationMode = dialog.Interpolation;
                encoder.IsThumbnailGenerated = true;


                try
                {
                    await encoder.FlushAsync();
                }
                catch (Exception err)
                {
                    const int WINCODEC_ERR_UNSUPPORTEDOPERATION = unchecked((int)0x88982F81);
                    switch (err.HResult)
                    {
                        case WINCODEC_ERR_UNSUPPORTEDOPERATION:
                            // If the encoder does not support writing a thumbnail, then try again
                            // but disable thumbnail generation.
                            encoder.IsThumbnailGenerated = false;
                            break;
                        default:
                            throw;
                    }
                }

                if (encoder.IsThumbnailGenerated == false)
                {
                    await encoder.FlushAsync();
                }


            }
        }

        private Guid? FileTypeExtensionToBitmapEncoder(string extension)
        {
            switch (extension)
            {
                case ".bmp":
                    return BitmapEncoder.BmpEncoderId;

                case ".heic":
                case ".heif":
                    return BitmapEncoder.HeifEncoderId;

                case ".jpg":
                case ".jpeg":
                    return BitmapEncoder.JpegEncoderId;

                case ".jpeg-xr":
                    return BitmapEncoder.JpegXREncoderId;

                case ".png":
                    return BitmapEncoder.PngEncoderId;

                case ".gif":
                    return BitmapEncoder.GifEncoderId;

                case ".tiff":
                case ".tif":
                    return BitmapEncoder.TiffEncoderId;
            }

            return null;
        }
    }
}
