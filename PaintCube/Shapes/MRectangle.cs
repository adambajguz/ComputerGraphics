using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

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

        public override void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawRectangle(Rectangle, Colors.Black, 2);
        }

        public override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawRectangle(Rectangle, Colors.Magenta, 1);
        }
    }
}
