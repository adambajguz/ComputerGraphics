﻿using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    [Serializable]
    public class MPolygon : MShape
    {
        public override ShapeType Type
        {
            get { return ShapeType.Polygon; }
        }

        public List<MLine> Lines { get; set; } = new List<MLine>();
        public bool Closed { get; set; }

        public override ShapeModes Mode
        {
            get => base.Mode;
            set
            {
                base.Mode = value;

                foreach (var line in Lines)
                {
                    line.Mode = Mode;
                }
            }
        }

        private Point _startLocation;
        public override Point StartLocation
        {
            get => _startLocation;
            set
            {
                if (Closed)
                {
                    return;
                }

                _startLocation = value;
            }
        }

        private Point _endLocation;
        public override Point EndLocation
        {
            get => _endLocation;
            set
            {
                if (Closed)
                {
                    return;
                }

                _endLocation = value;
            }
        }

        public MPolygon()
        {

        }

        public MPolygon(Point startLocation, Point endLocation) : base(startLocation, endLocation)
        {

        }

        protected override void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args)
        {
            foreach (var line in Lines)
            {
                line.Draw(sender, args);
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
                line.Draw(sender, args);
            }
        }

        public override void DrawResize(CanvasControl sender, CanvasDrawEventArgs args)
        {
            foreach (var line in Lines)
            {
                line.DrawResize(sender, args);
            }
        }

        public override bool OnMouseOver(Point mousePosition)
        {
            const double tol = 3;

            if (MouseHelpers.PointIsInCircle(Lines[0].StartLocation, tol, mousePosition) || MouseHelpers.PointIsInCircle(Lines[Lines.Count - 1].EndLocation, tol, mousePosition))
                return true;

            return false;
        }

        public override int OnPointOver(Point mousePosition)
        {
            for (int i = 0; i < Lines.Count; ++i)
            {
                MLine line = Lines[i];
                int result = line.OnPointOver(mousePosition);

                if (result == 1)
                {
                    return i;
                }
            }

            return -1;
        }

        public override void Resize(int point, Point coord)
        {
            Lines[point++].EndLocation = coord;

            if (point >= Lines.Count)
                point = 0;

            Lines[point].StartLocation = coord;
        }

        public override void MoveBy(Point shift)
        {
            double xShift = shift.X;
            double yShift = shift.Y;

            for (int i = 0; i < Lines.Count; ++i)
            {
                MLine line = Lines[i];
                line.StartLocation = new Point(line.StartLocation.X + xShift, line.StartLocation.Y + yShift);
                line.EndLocation = new Point(line.EndLocation.X + xShift, line.EndLocation.Y + yShift);
            }
        }

        public void AddSegment(Point end)
        {
            Point start = Lines.Count == 0 ? StartLocation : Lines[Lines.Count - 1].EndLocation;
            Lines.Add(new MLine(start, end));
        }

        public override void Rotate(Point orgin, double angle)
        {
            foreach (MLine line in Lines)
            {
                line.Rotate(orgin, angle);
            }
        }
        public override void Scale(Point orgin, double scaleX, double scaleY)
        {
            foreach (MLine line in Lines)
            {
                line.Scale(orgin, scaleX, scaleY);
            }
        }

        public override string ToString()
        {
            return $"Polygon: lines={Lines.Count}";
        }
    }
}

