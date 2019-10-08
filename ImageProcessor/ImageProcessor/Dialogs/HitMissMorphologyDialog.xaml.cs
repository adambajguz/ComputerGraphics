using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageProcessor.Dialogs
{

    public sealed partial class HitMissMorphologyDialog : ContentDialog
    {
        public HitMissMorphologyDialog()
        {
            this.InitializeComponent();
        }

        public async Task<bool?[,]> GetMatrix()
        {
            bool?[,] matrix = new bool?[3, 3];

            try
            {
                matrix[0, 0] = string.IsNullOrWhiteSpace(V1.Text) ? (bool?)null : bool.Parse(V1.Text);
                matrix[0, 1] = string.IsNullOrWhiteSpace(V2.Text) ? (bool?)null : bool.Parse(V2.Text);
                matrix[0, 2] = string.IsNullOrWhiteSpace(V3.Text) ? (bool?)null : bool.Parse(V3.Text);

                matrix[1, 0] = string.IsNullOrWhiteSpace(V4.Text) ? (bool?)null : bool.Parse(V4.Text);
                matrix[1, 1] = string.IsNullOrWhiteSpace(V5.Text) ? (bool?)null : bool.Parse(V5.Text);
                matrix[1, 2] = string.IsNullOrWhiteSpace(V6.Text) ? (bool?)null : bool.Parse(V6.Text);

                matrix[2, 0] = string.IsNullOrWhiteSpace(V7.Text) ? (bool?)null : bool.Parse(V7.Text);
                matrix[2, 1] = string.IsNullOrWhiteSpace(V8.Text) ? (bool?)null : bool.Parse(V8.Text);
                matrix[2, 2] = string.IsNullOrWhiteSpace(V9.Text) ? (bool?)null : bool.Parse(V9.Text);
            }
            catch (FormatException)
            {
                await ShowError("One or more values is not an integer!");

            }
            catch (OverflowException)
            {
                await ShowError("One or more values value is out of range of the Int32 type!");
            }

            return matrix;
        }

        private async Task ShowError(string text)
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = "Error",
                Content = text,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await dialog.ShowAsync();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (((ComboBox)sender).SelectedIndex)
            {
                //sharp
                case 0:
                    SetMatrix(false, true, false, true, true, true, false, true, false);
                    break;

                //corner points
                case 1:
                    SetMatrix(null, true, null, false, true, true, false, false, null);
                    break;

                default:
                    SetMatrix(false, false, false, false, false, false, false, false, false);
                    break;
            }
        }

        private void SetMatrix(bool? v1, bool? v2, bool? v3, bool? v4, bool? v5, bool? v6, bool? v7, bool? v8, bool? v9)
        {
            V1.Text = v1 == null ? "" : v1.ToString();
            V2.Text = v2 == null ? "" : v2.ToString();
            V3.Text = v3 == null ? "" : v3.ToString();

            V4.Text = v4 == null ? "" : v4.ToString();
            V5.Text = v5 == null ? "" : v5.ToString();
            V6.Text = v6 == null ? "" : v6.ToString();

            V7.Text = v7 == null ? "" : v7.ToString();
            V8.Text = v8 == null ? "" : v8.ToString();
            V9.Text = v9 == null ? "" : v9.ToString();
        }
    }
}
