using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    [Serializable]
    public class MBezier : MPolygon
    {
        public override ShapeType Type
        {
            get { return ShapeType.Bezier; }
        }

        private const double DRAW_PRECISION = 0.005;

        public MBezier()
        {

        }

        public MBezier(Point startLocation, Point endLocation) : base(startLocation, endLocation)
        {

        }

        protected override void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args)
        {
            foreach (var line in Lines)
            {
                DrawBezier(args, DRAW_PRECISION, ConvertLinesToPoints());
            }
        }
        protected override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (!Closed)
            {
                Point start = Lines.Count == 0 ? StartLocation : Lines[Lines.Count - 1].EndLocation;
                args.DrawingSession.DrawLine(start.ToVector2(), EndLocation.ToVector2(), ShapeColor, 1);
            }

            foreach (var line in Lines)
            {
                DrawBezier(args, DRAW_PRECISION, ConvertLinesToPoints());
                line.Draw(sender, args);
            }
        }

        public override void DrawResize(CanvasControl sender, CanvasDrawEventArgs args)
        {
            foreach (var line in Lines)
            {
                DrawBezier(args, DRAW_PRECISION, ConvertLinesToPoints());
                Mode = ShapeModes.Editing;
                line.Draw(sender, args);
                Mode = ShapeModes.Drawn;
                line.DrawResize(sender, args);
            }
        }

        public override bool OnMouseOver(Point mousePosition)
        {
            foreach (var line in Lines)
            {
                if (line.OnMouseOver(mousePosition))
                    return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Bezier: {base.ToString()}";
        }

        #region Bezier helper
        public List<Point> ConvertLinesToPoints()
        {
            int count = Lines.Count;
            List<Point> points = new List<Point>(count + 1);
            for (int i = 0; i < count; ++i)
                points.Add(Lines[i].StartLocation);

            points.Add(Lines[count - 1].EndLocation);

            return points;
        }

        private void DrawBezier(CanvasDrawEventArgs args, double precision, List<Point> points)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int pointsCount = points.Count;

            double[] coordXArray = new double[pointsCount];
            double[] coordYArray = new double[pointsCount];
            for (int i = 0; i < pointsCount; ++i)
            {
                coordXArray[i] = points[i].X;
                coordYArray[i] = points[i].Y;
            }

            List<Point> p = new List<Point>();

            for (double t = 0; t < 1; t += precision)
                p.Add(new Point(NextCoord(t, coordXArray), NextCoord(t, coordYArray)));

            p.Add(new Point(NextCoord(1.0f, coordXArray), NextCoord(1.0f, coordYArray)));

            int pCountEnd = p.Count - 1;
            for (int i = 0; i < pCountEnd; ++i)
                args.DrawingSession.DrawLine(p[i].ToVector2(), p[i + 1].ToVector2(), ShapeColor, 2);


            sw.Stop();
            Debug.WriteLine("ElapsedTicks={0}", sw.ElapsedTicks);
        }

        //https://pomax.github.io/bezierinfo/   
        private static double NextCoord(double t, double[] x)
        {
            int length = x.Length;
            int[] distribution = BinomialDistribution(length);

            double s = 0;
            for (int i = 0; i < length; ++i)
            {
                int c1 = length - (i + 1);

                double a1 = c1 == 0 ? 1 : Math.Pow(1 - t, c1);
                double a2 = i == 0 ? 1 : Math.Pow(t, i);

                s += distribution[i] * x[i] * a1 * a2;
            }

            return s;
        }

        private static int[] BinomialDistribution(int n)
        {
            int[] result = new int[n];
            result[0] = 1;

            for (int j = 0; j < n - 1; ++j)
                for (int i = n - 1; i > 0; --i)
                    result[i] = result[i] + (((i - 1) >= 0) ? result[i - 1] : 0);

            return result;
        }

        #endregion
    }
}

