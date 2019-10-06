using System;
using System.Numerics;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    public class MCircle : MShape
    {
        private Point _center;
        public Point Center
        {
            get => _center;
            set
            {
                _center = value;
                _startLocation = _center;
            }
        }

        private float _radius;
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;

                _endLocation = new Point(Center.X + (_radius * Math.Cos(90)),
                                         Center.Y + (_radius * Math.Sin(90))
                                        );
            }
        }

        private Point _startLocation;
        public override Point StartLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                _center = _startLocation;

                UpdateRadius();
            }
        }

        private Point _endLocation;
        public override Point EndLocation
        {
            get => _endLocation;
            set
            {
                _endLocation = value;

                UpdateRadius();
            }
        }

        private void UpdateRadius()
        {
            var dx = _endLocation.X - _startLocation.X;
            var dy = _endLocation.Y - _startLocation.Y;
            _radius = (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        public MCircle(Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
        }

        protected override void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawCircle(Center.ToVector2(), Radius, ShapeColor, 2);
        }

        protected override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawCircle(Center.ToVector2(), Radius, ShapeColor, 1);
            args.DrawingSession.DrawLine(Center.ToVector2(), EndLocation.ToVector2(), ShapeColor, 1);
            args.DrawingSession.DrawText($"r={Radius.ToString("N2")}", Center.ToVector2(), ShapeColor, new CanvasTextFormat { FontSize = 10 });
        }

        public override bool OnMouseOver(Point mousePosition)
        {
            const double tol = 2;

            if (PointIsInCircle(Center, Radius + tol, mousePosition) && !PointIsInCircle(Center, Radius - tol, mousePosition))
                return true;

            return false;
        }
        private bool PointIsInCircle(Point center, double radius, Point point)
        {
            var c1 = center.X - point.X;
            var c2 = center.Y - point.Y;

            var D = Math.Sqrt(Math.Pow(c1, 2) + Math.Pow(c2, 2));
            return D <= radius;
        }

        public override string ToString()
        {
            return $"Circle: {base.ToString()}; c={Center} r={Radius}";
        }
    }
}
