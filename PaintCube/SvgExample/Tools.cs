using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        public enum Tools
        {
            Select,
            Move,
            Resize,
            Draw,
            DrawClick,
            DrawTextTool
        }

        public Tools SelectedTool { get; set; }

        private void SelectTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Select;

            DrawTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
            DrawTextTool.IsChecked = false;
            ResizeTool.IsChecked = false;
        }

        private void MoveTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Move;

            SelectTool.IsChecked = false;
            DrawTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
            DrawTextTool.IsChecked = false;
            ResizeTool.IsChecked = false;
        }

        private void ResizeTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Resize;

            SelectTool.IsChecked = false;
            DrawTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
            DrawTextTool.IsChecked = false;
            MoveTool.IsChecked = false;

            canvasControl.Invalidate();
        }

        private void DrawTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.Draw;

            SelectTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
            DrawTextTool.IsChecked = false;
            ResizeTool.IsChecked = false;
        }

        private void DrawClickTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.DrawClick;

            SelectTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawTool.IsChecked = false;
            DrawTextTool.IsChecked = false;
            ResizeTool.IsChecked = false;
        }

        private void DrawTextTool_Checked(object sender, RoutedEventArgs e)
        {
            SelectedTool = Tools.DrawTextTool;

            SelectTool.IsChecked = false;
            MoveTool.IsChecked = false;
            DrawTool.IsChecked = false;
            DrawClickTool.IsChecked = false;
            ResizeTool.IsChecked = false;

            optionsPanelAddShape.Visibility = Visibility.Visible;
        }

        private void Tool_Unchecked(object sender, RoutedEventArgs e)
        {
            canvasControl.Invalidate();

            if (PendingShape != null)
                CancelDraw();

            optionsPanelAddShape.Visibility = Visibility.Collapsed;

            if (DrawTool.IsChecked == true ||
                MoveTool.IsChecked == true ||
                SelectTool.IsChecked == true ||
                DrawTextTool.IsChecked == true ||
                ResizeTool.IsChecked == true ||
                DrawClickTool.IsChecked == true)
                return;

            (sender as AppBarToggleButton).IsChecked = true;
        }
    }
}
