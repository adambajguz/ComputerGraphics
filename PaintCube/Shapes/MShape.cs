using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;

namespace PaintCube.Shapes
{
    public abstract class MShape
    {
        public virtual Point StartLocation { get; set; }
        public virtual Point EndLocation { get; set; }

        public bool IsInEditMode { get; set; }

        protected MShape()
        {

        }

        protected MShape(Point startLocation, Point endLocation)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
        }

        public void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (IsInEditMode)
                DrawGhost(sender, args);
            else
                DrawNormal(sender, args);
        }

        protected abstract void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args);
        protected abstract void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args);

        public override string ToString()
        {
            return $"{StartLocation} - {EndLocation}";
        }
    }
}