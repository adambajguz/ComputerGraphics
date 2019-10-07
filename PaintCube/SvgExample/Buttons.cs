using PaintCube.Shapes;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            DrawnShapes.Clear();

            HideEditPanel();

            DrawnShapesCombo.ItemsSource = null;
            DrawnShapesCombo.ItemsSource = DrawnShapes;

            if (ShapeToEdit != null)
            {
                ShapeToEdit.Mode = ShapeModes.Drawn;
                ShapeToEdit = null;
            }
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

        private void Rotate_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (MShape shape in DrawnShapes)
            {
                shape.Rotate(new Point(0, 0), 27);
            }

            canvasControl.Invalidate();
        }

        private void Scale_Clicked(object sender, RoutedEventArgs e)
        {
            foreach (MShape shape in DrawnShapes)
            {
                shape.Scale(new Point(0, 0), 1.5, 1.5);
            }

            canvasControl.Invalidate();
        }

        private void ClearShapesComboSelection()
        {
            HideEditPanel();

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
