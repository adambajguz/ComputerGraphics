using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaintCube.Shapes;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private async void SaveAs_Clicked(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add(".json", new List<string>() { ".json" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until we finish making changes and call CompleteUpdatesAsync.
                CachedFileManager.DeferUpdates(file);
                // write to file

                string serialized = JsonConvert.SerializeObject(DrawnShapes);
                await FileIO.WriteBytesAsync(file, Encoding.UTF8.GetBytes(serialized));

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

        public abstract class JsonCreationConverter<T> : JsonConverter
        {
            protected abstract T Create(Type objectType, JObject jObject);

            public override bool CanConvert(Type objectType)
            {
                return typeof(T) == objectType;
            }

            public override object ReadJson(JsonReader reader, Type objectType,
                object existingValue, JsonSerializer serializer)
            {
                try
                {
                    var jObject = JObject.Load(reader);
                    var target = Create(objectType, jObject);
                    serializer.Populate(jObject.CreateReader(), target);
                    return target;
                }
                catch (JsonReaderException)
                {
                    return null;
                }
            }

            public override void WriteJson(JsonWriter writer, object value,
                JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public class ShapeConverter : JsonCreationConverter<MShape>
        {
            protected override MShape Create(Type objectType, JObject jObject)
            {
                switch ((ShapeType)jObject["Type"].Value<int>())
                {
                    case ShapeType.Line:
                        return new MLine();
                    case ShapeType.Circle:
                        return new MCircle();
                    case ShapeType.Rectangle:
                        return new MRectangle();
                    case ShapeType.Polygon:
                        return new MPolygon();
                }
                return null;
            }
        }

        private async void Open_Clicked(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };

            picker.FileTypeFilter.Add(".json");

            StorageFile inputFile = await picker.PickSingleFileAsync();
            if (inputFile != null)
            {
                IBuffer buffer = await FileIO.ReadBufferAsync(inputFile);
                byte[] bytes = buffer.ToArray();

                DrawnShapes = JsonConvert.DeserializeObject<List<MShape>>(Encoding.UTF8.GetString(bytes), new ShapeConverter());
                canvasControl.Invalidate();
            }
            else
            {
                //Operation cancelled
                return;
            }
        }
    }
}
