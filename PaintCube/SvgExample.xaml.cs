using System.Collections.Generic;
using PaintCube.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaintCube
{
    public sealed partial class SvgExample : UserControl
    {
        public enum ShapeType
        {
            Rectangle,
            Circle,
            Line,
        }

        public List<ShapeType> Shapes { get { return Utils.GetEnumAsList<ShapeType>(); } }
        public ShapeType CurrentShapeType { get; set; }

        public List<MShape> DrawnShapes { get; } = new List<MShape>();
        public MShape ShapeToEdit { get; set; }

        public MShape PendingShape { get; set; }

        public SvgExample()
        {
            this.InitializeComponent();
            SelectTool.IsChecked = true;

            ShapeOptionsLine.Visibility = Visibility.Collapsed;
            ShapeOptionsLineLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsRectangle.Visibility = Visibility.Collapsed;
            ShapeOptionsRectangleLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsCircle.Visibility = Visibility.Collapsed;
            ShapeOptionsCircleLabel.Visibility = Visibility.Collapsed;

            optionsPanelAddShape.Visibility = Visibility.Collapsed;
        }

        private void ShapeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvasControl.Invalidate();
        }
    }
}
