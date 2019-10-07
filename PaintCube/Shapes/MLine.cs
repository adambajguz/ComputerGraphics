using System;
using System.Linq;
using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    [Serializable]
    public class MLine : MShape
    {
        public override ShapeType Type
        {
            get { return ShapeType.Line; }
        }

        public MLine()
        {

        }

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
            const double tol = 4;

            double x0 = StartLocation.X;
            double y0 = StartLocation.Y;
            double x1 = EndLocation.X;
            double y1 = EndLocation.Y;

            Point[] pts = new Point[] { new Point { X = x0 - tol, Y = y0 - tol },
                                        new Point { X = x1 - tol, Y = y1 - tol },
                                        new Point { X = x1 + tol, Y = y1 + tol },
                                        new Point { X = x0 + tol, Y = y0 + tol } };

            if (IsInPolygon(pts, mousePosition))
                return true;

            return false;
        }

        public static bool IsInPolygon(Point[] poly, Point point)
        {
            var coef = poly.Skip(1).Select((p, i) =>
                                            (point.Y - poly[i].Y) * (p.X - poly[i].X)
                                          - (point.X - poly[i].X) * (p.Y - poly[i].Y))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"Line: {base.ToString()}";
        }
    }
}
