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
            Polygon,
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

            HideEditPanel();

            optionsPanelAddShape.Visibility = Visibility.Collapsed;
        }

        private void ShapeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvasControl.Invalidate();
            if (CurrentShapeType == ShapeType.Polygon)
                SelectedTool = Tools.DrawClick;
        }
    }
}
