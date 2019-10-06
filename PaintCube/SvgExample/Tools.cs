using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        public enum Tools
        {
            Pointer,
            Move,
            Draw,
            DrawClick
        }

        public Tools SelectedTool { get; set; }

        private void PointerTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Pointer;

            DrawTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
        }

        private void MoveTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Move;

            PointerTool.IsChecked = false;
            DrawTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
        }

        private void DrawTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Draw;

            PointerTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
        }

        private void DrawClickTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.DrawClick;

            PointerTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawTool.IsChecked = false;
        }

        private void Tool_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DrawTool.IsChecked == true ||
                MoveTool.IsChecked == true ||
                PointerTool.IsChecked == true ||
                DrawClickTool.IsChecked == true)
                return;

            (sender as AppBarToggleButton).IsChecked = true;
        }
    }
}
