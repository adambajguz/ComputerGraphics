using PaintCube.Shapes;
using Windows.Foundation;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private bool ClickedOnce { get; set; }

        private void CancelDraw()
        {
            ClickedOnce = false;
            PendingShape = null;
            canvasControl.Invalidate();
        }

        private void canvasControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point startPosition = e.GetCurrentPoint(canvasControl).Position;

            if (SelectedTool == Tools.Select)
            {
                foreach (MShape shape in DrawnShapes)
                {
                    if (shape.OnMouseOver(startPosition))
                    {
                        DrawnShapesCombo.SelectedItem = shape;

                        break;
                    }
                }
            }
            else if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
            {
                if (SelectedTool == Tools.DrawClick)
                {
                    if (!ClickedOnce)
                        ClickedOnce = true;
                    else
                    {
                        ClickedOnce = false;

                        AddShape();

                        return;
                    }
                }

                if (CurrentShapeType == ShapeType.Rectangle)
                {
                    PendingShape = new MRectangle(startPosition, startPosition);
                }
                else if (CurrentShapeType == ShapeType.Circle)
                {
                    PendingShape = new MCircle(startPosition, startPosition);
                }
                else if (CurrentShapeType == ShapeType.Line)
                {
                    PendingShape = new MLine(startPosition, startPosition);
                }

                PendingShape.Mode = ShapeModes.Drawing;

                canvasControl.Invalidate();
            }
        }

        private void AddShape()
        {
            PendingShape.Mode = ShapeModes.Drawn;
            DrawnShapes.Add(PendingShape);

            int idx = DrawnShapesCombo.SelectedIndex;
            DrawnShapesCombo.ItemsSource = null;
            DrawnShapesCombo.ItemsSource = DrawnShapes;
            DrawnShapesCombo.SelectedIndex = idx;

            PendingShape = null;
            canvasControl.Invalidate();
        }

        private void canvasControl_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point pos = e.GetCurrentPoint(canvasControl).Position;

            if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
            {
                if (PendingShape == null)
                    return; // Nothing to do

                PendingShape.EndLocation = pos;

                canvasControl.Invalidate();
            }
        }

        private void canvasControl_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
            {
                if (PendingShape == null || SelectedTool == Tools.DrawClick)
                    return; // Nothing to do

                AddShape();
            }
        }

    }
}
