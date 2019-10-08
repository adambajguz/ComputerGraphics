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
        private Point MovingShapeBeginMouse { get; set; }

        private int ResizingPoint { get; set; } = -1;

        #region PointerPressed
        private void canvasControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point startPosition = e.GetCurrentPoint(canvasControl).Position;

            if (MouseClickAction != MouseAction.Normal)
            {
                switch (MouseClickAction)
                {
                    case MouseAction.GetRotate:
                        RotateOrginXEdit.Text = startPosition.X.ToString();
                        RotateOrginYEdit.Text = startPosition.Y.ToString();
                        break;
                    case MouseAction.GetScale:
                        ScaleOrginXEdit.Text = startPosition.X.ToString();
                        ScaleOrginYEdit.Text = startPosition.Y.ToString();
                        break;
                }
                MouseClickAction = MouseAction.Normal;
            }

            bool rightButtonPressed = false;
            // Check for input device
            if (e.Pointer.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                var properties = e.GetCurrentPoint(this).Properties;
                if (properties.IsRightButtonPressed)
                    rightButtonPressed = true;
            }


            if (SelectedTool == Tools.Select || SelectedTool == Tools.Move)
                PointerPressedSelectOrMove(startPosition);
            else if (SelectedTool == Tools.Resize)
                PointerPressedResize(startPosition);
            else if (SelectedTool == Tools.Draw || SelectedTool == Tools.DrawClick)
                PointerPressedDraw(rightButtonPressed, startPosition);
        }

        private void PointerPressedDraw(bool rightButtonPressed, Point startPosition)
        {
            if (CurrentShapeType == ShapeType.Polygon || CurrentShapeType == ShapeType.Bezier)
            {
                DrawClickTool.IsChecked = true;
                SelectedTool = Tools.DrawClick;
            }

            if (SelectedTool == Tools.DrawClick)
            {
                if (CurrentShapeType == ShapeType.Polygon || CurrentShapeType == ShapeType.Bezier)
                {
                    if (ClickedTimes++ >= 1)
                    {
                        if (rightButtonPressed)
                        {
                            if (ClickedTimes >= 4)
                            {
                                ClickedTimes = 0;

                                var poly = PendingShape as MPolygon;
                                if (CurrentShapeType == ShapeType.Polygon)
                                    poly.AddSegment(poly.StartLocation);
                                poly.StartLocation = new Point(0, 0);
                                poly.EndLocation = new Point(0, 0);
                                poly.Closed = true;

                                AddShape();

                                return;
                            }
                            else
                                --ClickedTimes;
                        }
                        else
                        {
                            if (CurrentShapeType == ShapeType.Polygon || CurrentShapeType == ShapeType.Bezier)
                                (PendingShape as MPolygon).AddSegment(startPosition);
                        }
                        this.canvasControl.Invalidate();

                        return;
                    }
                }
                else
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
            else if (CurrentShapeType == ShapeType.Bezier)
            {
                PendingShape = new MBezier(startPosition, startPosition);
            }

            PendingShape.Mode = ShapeModes.Drawing;

            this.canvasControl.Invalidate();
        }

        private void PointerPressedResize(Point startPosition)
        {
            foreach (MShape shape in DrawnShapes)
            {
                int tmpResizingPoint = shape.OnPointOver(startPosition);
                if (tmpResizingPoint >= 0)
                {
                    ResizingPoint = tmpResizingPoint;

                    MovingShape = shape;
                    MovingShape.Mode = ShapeModes.Drawing;
                    MovingShapeBeginMouse = startPosition;

                    canvasControl.Invalidate();

                    break;
                }
            }
        }

        private void PointerPressedSelectOrMove(Point startPosition)
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
                        MovingShapeBeginMouse = startPosition;

                        canvasControl.Invalidate();

                        break;
                    }
                }
            }
        }

        private void AddShape()
        {
            if (PendingShape != null)
            {
                PendingShape.Mode = ShapeModes.Drawn;
                DrawnShapes.Add(PendingShape);

                int idx = DrawnShapesCombo.SelectedIndex;
                DrawnShapesCombo.ItemsSource = null;
                DrawnShapesCombo.ItemsSource = DrawnShapes;
                DrawnShapesCombo.SelectedIndex = idx;

                PendingShape = null;
            }
            canvasControl.Invalidate();
        }
        #endregion

        private void canvasControl_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Point pos = e.GetCurrentPoint(canvasControl).Position;
            MousePositionTextBlock.Text = $"{pos.X.ToString("N4")} x {pos.Y.ToString("N4")}";

            if (SelectedTool == Tools.Move)
            {
                if (MovingShape != null)
                {
                    double mx = MovingShapeBeginMouse.X;
                    double my = MovingShapeBeginMouse.Y;

                    double shiftX = mx - pos.X;
                    double shiftY = my - pos.Y;


                    MovingShape.MoveBy(new Point(-shiftX, -shiftY));
                    MovingShapeBeginMouse = pos;

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
