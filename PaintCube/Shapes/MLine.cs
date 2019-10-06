using System.Numerics;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

namespace PaintCube.Shapes
{
    public class MLine : MShape
    {
        public MLine(Point startLocation, Point endLocation) : base(startLocation, endLocation)
        {

        }

        public override void Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawLine(StartLocation.ToVector2(), EndLocation.ToVector2(), Colors.Black, 2);
        }

        public override void DrawGhost(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawLine(StartLocation.ToVector2(), EndLocation.ToVector2(), Colors.Magenta, 1);
        }

        public override string ToString()
        {
            return $"Line: {base.ToString()}";
        }
    }
}
