namespace ImageProcessor.Pages
{
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ReopenFlyoutItem.IsEnabled = false;
            EditMenuBarItem.IsEnabled = false;
            SaveMenuFlyoutItem.IsEnabled = false;
            AdvancedToolsMenuBarItem.IsEnabled = false;
            ToolsMenuBarItem.IsEnabled = false;
            ZoomCommandBar.IsEnabled = false;

            ContentFrame_Reset();
            ContentFrameCollapse();
        }
    }
}
