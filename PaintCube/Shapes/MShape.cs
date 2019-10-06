using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    public abstract class MShape
    {
        public virtual Point StartLocation { get; set; }
        public virtual Point EndLocation { get; set; }

        protected MShape(Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
        }

        public abstract void Draw(CanvasControl sender, CanvasDrawEventArgs args);
    }
}