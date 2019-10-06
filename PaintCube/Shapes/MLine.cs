using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    public class MLine : MShape
    {
        public MLine(Point startLocation, Point endLocation) : base(startLocation, endLocation)
        {

        }

        protected override void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawLine(StartLocation.ToVector2(), EndLocation.ToVector2(), ShapeColor, 2);
        }

        protected override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawLine(StartLocation.ToVector2(), EndLocation.ToVector2(), ShapeColor, 1);
        }

        public override bool OnMouseOver(Point mousePosition)
        {
            //const double tol = 2;

            double x0 = StartLocation.X;
            double y0 = StartLocation.Y;
            double x1 = EndLocation.X;
            double y1 = EndLocation.Y;

            return false;
        }
        public override string ToString()
        {
            return $"Line: {base.ToString()}";
        }
    }
}
