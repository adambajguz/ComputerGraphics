using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

namespace PaintCube.Shapes
{
    public class MRectangle : MShape
    {
        private Rect rectangle;

        public Rect Rectangle
        {
            get => rectangle;
            set
            {
                rectangle = value;
                StartLocation = new Point(rectangle.X, rectangle.Y);
                EndLocation = new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height);
            }
        }

        public override Point StartLocation
        {
            get => base.StartLocation;
            set
            {
                base.StartLocation = value;
                Rectangle = new Rect(StartLocation, EndLocation);
            }
        }

        public override Point EndLocation
        {
            get => base.EndLocation;
            set
            {
                base.EndLocation = value;
                Rectangle = new Rect(StartLocation, EndLocation);
            }
        }

        public MRectangle(Point startLocation, Point endLocation) : base(startLocation, endLocation)
        {
            Rectangle = new Rect(startLocation, endLocation);
        }

        public override void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawRectangle(Rectangle, Colors.Black, 2);
        }
    }
}
