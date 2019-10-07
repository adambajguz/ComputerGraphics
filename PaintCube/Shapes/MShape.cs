using System;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;

namespace PaintCube.Shapes
{
    [Serializable]
    public abstract class MShape
    {
        public abstract ShapeType Type { get; }

        public const int CharacteristicPointsSize = 8;
        public const int CharacteristicPointsSizeHalf = CharacteristicPointsSize / 2;
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

        public virtual void DrawResize(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.FillRectangle((float)StartLocation.X - CharacteristicPointsSizeHalf,
                                              (float)StartLocation.Y - CharacteristicPointsSizeHalf,
                                              CharacteristicPointsSize,
                                              CharacteristicPointsSize,
                                              Colors.Magenta);
            args.DrawingSession.FillRectangle((float)EndLocation.X - CharacteristicPointsSizeHalf,
                                              (float)EndLocation.Y - CharacteristicPointsSizeHalf,
                                              CharacteristicPointsSize,
                                              CharacteristicPointsSize,
                                              Colors.Magenta);
        }

        public virtual int OnPointOver(Point mousePosition)
        {
            Rect start = new Rect(StartLocation.X - CharacteristicPointsSizeHalf,
                                  StartLocation.Y - CharacteristicPointsSizeHalf,
                                  CharacteristicPointsSize,
                                  CharacteristicPointsSize);

            Rect end = new Rect(EndLocation.X - CharacteristicPointsSizeHalf,
                                EndLocation.Y - CharacteristicPointsSizeHalf,
                                CharacteristicPointsSize,
                                CharacteristicPointsSize);

            if (start.Contains(mousePosition))
                return 0;
            else if (end.Contains(mousePosition))
                return 1;

            return -1;
        }

        public virtual void Resize(int point, Point coord)
        {
            if (point == 0)
                StartLocation = coord;
            else if (point == 1)
                EndLocation = coord;
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