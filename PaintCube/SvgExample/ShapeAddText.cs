using System;
using PaintCube.Shapes;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private async void ShapeAdd_Clicked(object sender, RoutedEventArgs e)
        {
            double sx, sy, ex, ey;
            try
            {
                sx = double.Parse(AddStartLocationX.Text);
                sy = double.Parse(AddStartLocationY.Text);
                ex = double.Parse(AddEndLocationX.Text);
                ey = double.Parse(AddEndLocationY.Text);
            }
            catch (Exception)
            {
                await InvalidValuesFormatDialog();

                return;
            }

            Point start = new Point(sx, sy);
            Point end = new Point(ex, ey);

            if (CurrentShapeType == ShapeType.Rectangle)
            {
                DrawnShapes.Add(new MRectangle(start, end));
            }
            else if (CurrentShapeType == ShapeType.Circle)
            {
                start = new Point((ex - sx) / 2, (ey - sy) / 2);
                DrawnShapes.Add(new MCircle(start, end));
            }
            else if (CurrentShapeType == ShapeType.Line)
            {
                DrawnShapes.Add(new MLine(start, end));
            }

            canvasControl.Invalidate();
        }
    }
}
