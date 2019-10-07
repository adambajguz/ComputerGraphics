using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    public class MRectangle : MShape
    {
        private Rect _rectangle;
        public Rect Rectangle
        {
            get => _rectangle;
            set
            {
                _rectangle = value;
                _startLocation = new Point(_rectangle.X, _rectangle.Y);
                _endLocation = new Point(_rectangle.X + _rectangle.Width, _rectangle.Y + _rectangle.Height);
            }
        }

        private Point _startLocation;
        public override Point StartLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                Rectangle = new Rect(StartLocation, EndLocation);
            }
        }

        private Point _endLocation;
        public override Point EndLocation
        {
            get => _endLocation;
            set
            {
                _endLocation = value;
                Rectangle = new Rect(StartLocation, EndLocation);
            }
        }

        public MRectangle(Point startLocation, Point endLocation)
        {
            Rectangle = new Rect(startLocation, endLocation);
        }

        protected override void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawRectangle(Rectangle, ShapeColor, 2);
        }

        protected override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawRectangle(Rectangle, ShapeColor, 1);
        }

        public override bool OnMouseOver(Point mousePosition)
        {
            const double tol = 2.5;

            double x0 = StartLocation.X;
            double y0 = StartLocation.Y;
            double x1 = EndLocation.X;
            double y1 = EndLocation.Y;

            var rect0 = new Rect(new Point(x0 - tol, y0 - tol), new Point(x1 + tol, y1 + tol));
            var rect1 = new Rect(new Point(x0 + tol, y0 + tol), new Point(x1 - tol, y1 - tol));

            if (rect0.Contains(mousePosition) && !rect1.Contains(mousePosition))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"Rect: {base.ToString()}";
        }
    }
}
