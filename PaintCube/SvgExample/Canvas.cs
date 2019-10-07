using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using PaintCube.Shapes;
using Windows.UI.Xaml;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private void control_Unloaded(object sender, RoutedEventArgs e)
        {
            // Explicitly remove references to allow the Win2D controls to get garbage collected
            canvasControl.RemoveFromVisualTree();
            canvasControl = null;
        }
        void canvasControl_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            // Clears everything.
            canvasControl.Invalidate();
        }

        private void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            foreach (MShape shape in DrawnShapes)
            {
                shape.Draw(sender, args);
                if (SelectedTool == Tools.Resize)
                    shape.DrawResize(sender, args);
            }

            if (PendingShape != null)
            {
                PendingShape.Draw(sender, args);
            }
        }
    }
}
