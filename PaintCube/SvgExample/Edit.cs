using System;
using System.Threading.Tasks;
using PaintCube.Shapes;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaintCube
{
    public sealed partial class SvgExample
    {
        private void DrawnShapesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateEditPanel();
        }

        private void UpdateEditPanel()
        {
            if (ShapeToEdit != null)
            {
                ShapeToEdit.IsInEditMode = false;
            }

            if (DrawnShapesCombo.SelectedIndex < 0)
                return;

            ShapeToEdit = DrawnShapesCombo.SelectedItem as MShape;
            ShapeToEdit.IsInEditMode = true;

            ShapeOptionsCommon.Visibility = Visibility.Visible;
            ShapeOptionsCommonLabel.Visibility = Visibility.Visible;

            StartLocationXEdit.Text = ShapeToEdit.StartLocation.X.ToString();
            StartLocationYEdit.Text = ShapeToEdit.StartLocation.Y.ToString();
            EndLocationXEdit.Text = ShapeToEdit.EndLocation.X.ToString();
            EndLocationYEdit.Text = ShapeToEdit.EndLocation.Y.ToString();

            if (ShapeToEdit is MCircle circle)
            {
                ShapeOptionsCircle.Visibility = Visibility.Visible;
                ShapeOptionsCircleLabel.Visibility = Visibility.Visible;

                CircleCenterXEdit.Text = circle.Center.X.ToString();
                CircleCenterYEdit.Text = circle.Center.X.ToString();
                CircleRadiusEdit.Text = circle.Radius.ToString();
            }
            else if (ShapeToEdit is MRectangle rect)
            {
                ShapeOptionsRectangle.Visibility = Visibility.Visible;
                ShapeOptionsRectangleLabel.Visibility = Visibility.Visible;

                RectangleWidthEdit.Text = rect.Rectangle.Width.ToString();
                RectangleHeightEdit.Text = rect.Rectangle.Height.ToString();
            }

            canvasControl.Invalidate();
        }

        private void ShapeUpdate_Clicked(object sender, RoutedEventArgs e)
        {
            UpdateShapeFromEditPanel();
        }

        private void ShapeCancelUpdate_Clicked(object sender, RoutedEventArgs e)
        {
            ClearShapesComboSelection();
        }

        private void UpdateShapeFromEditPanel()
        {
            if (ShapeToEdit is MShape shape)
            {
                UpdateShape(shape);

                if (ShapeToEdit is MCircle circle)
                {
                    UpdateCircle(circle);
                }
                else if (ShapeToEdit is MRectangle rect)
                {
                    UpdateRect(rect);
                }

                canvasControl.Invalidate();
            }
        }

        private void UpdateRect(MRectangle rect)
        {
            ShapeOptionsRectangle.Visibility = Visibility.Visible;
            ShapeOptionsRectangleLabel.Visibility = Visibility.Visible;

            RectangleWidthEdit.Text = rect.Rectangle.Width.ToString();
            RectangleHeightEdit.Text = rect.Rectangle.Height.ToString();
        }

        private void UpdateCircle(MCircle circle)
        {
            ShapeOptionsCircle.Visibility = Visibility.Visible;
            ShapeOptionsCircleLabel.Visibility = Visibility.Visible;

            CircleCenterXEdit.Text = circle.Center.X.ToString();
            CircleCenterYEdit.Text = circle.Center.X.ToString();
            CircleRadiusEdit.Text = circle.Radius.ToString();
        }

        private async void UpdateShape(MShape shape)
        {
            ShapeOptionsCommon.Visibility = Visibility.Visible;
            ShapeOptionsCommonLabel.Visibility = Visibility.Visible;

            double sx, sy, ex, ey;
            try
            {
                sx = double.Parse(StartLocationXEdit.Text);
                sy = double.Parse(StartLocationYEdit.Text);
                ex = double.Parse(EndLocationXEdit.Text);
                ey = double.Parse(EndLocationYEdit.Text);
            }
            catch (Exception)
            {
                await InvalidValuesFormatDialog();

                return;
            }

            shape.StartLocation = new Point(sx, sy);
            shape.EndLocation = new Point(ex, ey);

            StartLocationXEdit.Text = shape.StartLocation.X.ToString();
            StartLocationYEdit.Text = shape.StartLocation.Y.ToString();
            EndLocationXEdit.Text = shape.EndLocation.X.ToString();
            EndLocationYEdit.Text = shape.EndLocation.Y.ToString();
        }

        private static async Task InvalidValuesFormatDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "Error",
                Content = "Invalid values passed into fields",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }
    }
}
