using System;
using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

namespace PaintCube.Shapes
{
    public class MCircle : MShape
    {
        public Point Center;
        public float Radius;

        public MCircle(Point startLocation, Point endLocation) : base(startLocation, endLocation)
        {
            Center = startLocation;

            var dx = endLocation.X - startLocation.X;
            var dy = endLocation.Y - startLocation.Y;
            Radius = (float)Math.Sqrt((dx * dx) + (dy * dy));
        }

        public override void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawCircle(Center.ToVector2(), Radius, Colors.Black, 2);
        }
    }
}
