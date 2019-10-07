using PaintCube.Shapes;
using Windows.Foundation;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private int ClickedTimes { get; set; }

        private void CancelDraw()
        {
            ClickedTimes = 0;
            PendingShape = null;
            canvasControl.Invalidate();

            if (MovingShape != null)
            {
                MovingShape.Mode = ShapeModes.Drawn;
                MovingShape = null;
            }
            ResizingPoint = -1;
        }

        private MShape MovingShape { get; set; }
        private Point MovingShapeBeginStart { get; set; }
        private Point MovingShapeBeginEnd { get; set; }
        private Point MovingShapeBeginMouse { get; set; }

        private int ResizingPoint { get; set; } = -1;

        private void canvasControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point startPosition = e.GetCurrentPoint(canvasControl).Position;

            if (SelectedTool == Tools.Select || SelectedTool == Tools.Move)
            {
                foreach (MShape shape in DrawnShapes)
                {
                    if (shape.OnMouseOver(startPosition))
                    {
                        if (SelectedTool == Tools.Select)
                        {
                            DrawnShapesCombo.SelectedItem = shape;
                            break;
                        }
                        else if (SelectedTool == Tools.Move)
                        {
                            MovingShape = shape;
                            MovingShape.Mode = ShapeModes.Drawing;
                            MovingShapeBeginStart = shape.StartLocation;
                            MovingShapeBeginEnd = shape.EndLocation;
                            MovingShapeBeginMouse = startPosition;

                            canvasControl.Invalidate();

                            break;
                        }
                    }
                }
            }
            else if (SelectedTool == Tools.Resize)
            {
                foreach (MShape shape in DrawnShapes)
                {
                    int tmpResizingPoint = shape.OnPointOver(startPosition);
                    if (tmpResizingPoint >= 0)
                    {
                        ResizingPoint = tmpResizingPoint;

                        MovingShape = shape;
                        MovingShape.Mode = ShapeModes.Drawing;
                        MovingShapeBeginStart = shape.StartLocation;
                        MovingShapeBeginEnd = shape.EndLocation;
                        MovingShapeBeginMouse = startPosition;

                        canvasControl.Invalidate();

                        break;
                    }
                }
            }
            else if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
            {
                if (SelectedTool == Tools.DrawClick)
                {
                    if (ClickedTimes == 0)
                        ClickedTimes = 1;
                    else
                    {
                        ClickedTimes = 0;

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
                else if (CurrentShapeType == ShapeType.Polygon)
                {
                    PendingShape = new MPolygon(startPosition, startPosition);
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
            if (SelectedTool == Tools.Move)
            {
                if (MovingShape != null)
                {
                    double x0 = MovingShapeBeginStart.X;
                    double y0 = MovingShapeBeginStart.Y;
                    double x1 = MovingShapeBeginEnd.X;
                    double y1 = MovingShapeBeginEnd.Y;

                    double mx = MovingShapeBeginMouse.X;
                    double my = MovingShapeBeginMouse.Y;

                    double shiftX = mx - pos.X;
                    double shiftY = my - pos.Y;

                    MovingShape.StartLocation = new Point(x0 - shiftX, y0 - shiftY);
                    MovingShape.EndLocation = new Point(x1 - shiftX, y1 - shiftY);
                    canvasControl.Invalidate();
                }
            }
            else if (SelectedTool == Tools.Resize)
            {
                if (MovingShape != null)
                {
                    MovingShape.Resize(ResizingPoint, pos);
                    canvasControl.Invalidate();
                }
            }
            else if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
            {
                if (PendingShape == null)
                    return; // Nothing to do

                PendingShape.EndLocation = pos;

                canvasControl.Invalidate();
            }
        }

        private void canvasControl_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (SelectedTool == Tools.Move || SelectedTool == Tools.Resize)
            {
                if (MovingShape != null)
                {
                    MovingShape.Mode = ShapeModes.Drawn;
                    MovingShape = null;
                    ResizingPoint = -1;
                    canvasControl.Invalidate();
                }
            }
            else if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
            {
                if (PendingShape == null || SelectedTool == Tools.DrawClick)
                    return; // Nothing to do

                AddShape();
            }
        }
    }
}
