using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ImageProcessor.Dialogs
{
    public sealed partial class ThreeIntsDialog : ContentDialog
    {
        private string DefaultValue { get; }
        private string DefaultValueOposite { get; }
        private int DefaultValueInt { get; }
        private bool DontAllowOpositeToDefualt { get; }

        public ThreeIntsDialog(string title, string desc, string desc1, string desc2, string desc3, bool zeroAsDefulat, bool dontAllowOpositeToDefualt)
        {
            this.InitializeComponent();
            this.Title = title;

            Desc.Text = desc;

            D1.Text = desc1;
            D2.Text = desc2;
            D3.Text = desc3;

            DefaultValue = zeroAsDefulat ? "0" : "1";
            DefaultValueOposite = zeroAsDefulat ? "1" : "0";
            DefaultValueInt = zeroAsDefulat ? 0 : 1;
            V1.Text = DefaultValue;
            V2.Text = DefaultValue;
            V3.Text = DefaultValue;

            DontAllowOpositeToDefualt = dontAllowOpositeToDefualt;
        }

        public async Task<int[]> GetValues()
        {
            int[] kernel = new int[3];

            try
            {
                PopulateValue(kernel, V1.Text, 0);
                PopulateValue(kernel, V2.Text, 1);
                PopulateValue(kernel, V3.Text, 2);
            }
            catch (FormatException)
            {
                await ShowError("One or more values is not an integer!");

            }
            catch (OverflowException)
            {
                await ShowError("One or more values value is out of range of the Int32 type!");
            }

            return kernel;
        }

        private void PopulateValue(int[] kernel, string val, int index)
        {
            if (DontAllowOpositeToDefualt && val == DefaultValueOposite)
                kernel[index] = DefaultValueInt;
            else if (!string.IsNullOrWhiteSpace(val))
                kernel[index] = Int32.Parse(val);
            else
                kernel[index] = DefaultValueInt;
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
    }
}
