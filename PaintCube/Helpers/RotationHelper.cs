using System;
using Windows.Foundation;

namespace PaintCube.Helpers
{
    public static class RotationHelper
    {
        public static double DegreesToRadians(double angleInDegrees)
        {
            return angleInDegrees * (Math.PI / 180);
        }

        public static Point RotatePoint(Point pointToRotate, Point centerPoint, double angleInDegrees)
        {
            double angleInRadians = DegreesToRadians(angleInDegrees);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);

            double cx = pointToRotate.X - centerPoint.X;
            double cy = pointToRotate.Y - centerPoint.Y;

            double X = (int)(cosTheta * cx - sinTheta * cy + centerPoint.X);
            double Y = (int)(sinTheta * cx + cosTheta * cy + centerPoint.Y);

            return new Point(X, Y);
        }
    }
}
