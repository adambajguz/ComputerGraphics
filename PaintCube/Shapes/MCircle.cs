using System;
using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

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
                Center = _startLocation;

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
            Radius = (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        public MCircle(Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
        }

        public override void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawCircle(Center.ToVector2(), Radius, Colors.Black, 2);
        }

        public override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawCircle(Center.ToVector2(), Radius, Colors.Magenta, 1);
        }

        public override string ToString()
        {
            return $"Circle: {base.ToString()}; c={Center} r={Radius}";
        }
    }
}
