using System;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    public static class MouseHelpers
    {
        public static bool PointIsInCircle(Point center, double radius, Point point)
        {
            var c1 = center.X - point.X;
            var c2 = center.Y - point.Y;

            var D = Math.Sqrt(Math.Pow(c1, 2) + Math.Pow(c2, 2));
            return D <= radius;
        }
    }
}
