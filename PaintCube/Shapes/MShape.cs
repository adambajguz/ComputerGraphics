using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

namespace PaintCube.Shapes
{
    public abstract class MShape
    {
        public virtual Point StartLocation { get; set; }
        public virtual Point EndLocation { get; set; }

        public ShapeModes Mode { get; set; } = ShapeModes.Drawn;

        public Color ShapeColor
        {
            get
            {
                switch (Mode)
                {
                    case ShapeModes.Drawing:
                        return Colors.Blue;
                    case ShapeModes.Drawn:
                        return Colors.Black;
                    case ShapeModes.Editing:
                        return Colors.Magenta;

                    default:
                        return Colors.Green;
                }
            }
        }

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
            if (Mode == ShapeModes.Drawn)
                DrawNormal(sender, args);
            else
                DrawGhost(sender, args);
        }

        public abstract bool OnMouseOver(Point mousePosition);

        protected abstract void DrawNormal(CanvasControl sender, CanvasDrawEventArgs args);
        protected abstract void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args);

        public override string ToString()
        {
            return $"{StartLocation} - {EndLocation}";
        }
    }
}