using PaintCube.Shapes;
using Windows.UI.Xaml;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            DrawnShapes.Clear();
            ClearShapesComboSelection();

            canvasControl.Invalidate();
        }

        private void Undo_Clicked(object sender, RoutedEventArgs e)
        {
            if (DrawnShapes.Count > 0)
            {
                DrawnShapes.RemoveAt(DrawnShapes.Count - 1);
                ClearShapesComboSelection();

                canvasControl.Invalidate();
            }
        }

        private void ClearShapesComboSelection()
        {
            DrawnShapesCombo.ItemsSource = null;
            DrawnShapesCombo.ItemsSource = DrawnShapes;

            if (ShapeToEdit != null)
            {
                ShapeToEdit.Mode = ShapeModes.Drawn;
                ShapeToEdit = null;
            }

            UpdateEditPanel();
            canvasControl.Invalidate();
        }
    }
}
