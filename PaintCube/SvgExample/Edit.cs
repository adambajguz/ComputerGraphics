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
            ShapeOptionsMoveByVectorLabel.Visibility = Visibility.Visible;
            ShapeOptionsMoveByVector.Visibility = Visibility.Visible;
            ShapeOptionsScaleVectorLabel.Visibility = Visibility.Visible;
            ShapeOptionsScaleVector.Visibility = Visibility.Visible;
            ShapeOptionsRotateVectorLabel.Visibility = Visibility.Visible;
            ShapeOptionsRotateVector.Visibility = Visibility.Visible;

            EditPanelButtonsUpdate.Visibility = Visibility.Visible;

            if (ShapeToEdit != null)
            {
                ShapeToEdit.Mode = ShapeModes.Drawn;
            }

            if (DrawnShapesCombo.SelectedIndex < 0)
                return;

            ShapeToEdit = DrawnShapesCombo.SelectedItem as MShape;
            ShapeToEdit.Mode = ShapeModes.Editing;

            if (ShapeToEdit is MLine line)
            {
                ShapeOptionsLine.Visibility = Visibility.Visible;
                ShapeOptionsLineLabel.Visibility = Visibility.Visible;

                StartLocationXEdit.Text = line.StartLocation.X.ToString();
                StartLocationYEdit.Text = line.StartLocation.Y.ToString();
                EndLocationXEdit.Text = line.EndLocation.X.ToString();
                EndLocationYEdit.Text = line.EndLocation.Y.ToString();
            }
            else if (ShapeToEdit is MCircle circle)
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

                RectXEdit.Text = rect.Rectangle.X.ToString();
                RectYEdit.Text = rect.Rectangle.Y.ToString();
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
            HideEditPanel();
            ClearShapesComboSelection();
        }

        private void UpdateShapeFromEditPanel()
        {
            if (ShapeToEdit is MLine shape)
            {
                UpdateLine(shape);
            }
            else if (ShapeToEdit is MCircle circle)
            {
                UpdateCircle(circle);
            }
            else if (ShapeToEdit is MRectangle rect)
            {
                UpdateRect(rect);
            }
            else if (ShapeToEdit is MPolygon poly)
            {
                UpdatePoly(poly);
            }

            UpdateEditPanel();

            canvasControl.Invalidate();
        }

        private async void UpdateLine(MLine line)
        {
            ShapeOptionsLine.Visibility = Visibility.Visible;
            ShapeOptionsLineLabel.Visibility = Visibility.Visible;

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

            line.StartLocation = new Point(sx, sy);
            line.EndLocation = new Point(ex, ey);
        }

        private async void UpdateCircle(MCircle circle)
        {
            ShapeOptionsCircle.Visibility = Visibility.Visible;
            ShapeOptionsCircleLabel.Visibility = Visibility.Visible;

            double x, y;
            float r;
            try
            {
                x = double.Parse(CircleCenterXEdit.Text);
                y = double.Parse(CircleCenterYEdit.Text);
                r = float.Parse(CircleRadiusEdit.Text);
            }
            catch (Exception)
            {
                await InvalidValuesFormatDialog();

                return;
            }

            circle.Center = new Point(x, y);
            circle.Radius = r;
        }

        private async void UpdateRect(MRectangle rect)
        {
            ShapeOptionsRectangle.Visibility = Visibility.Visible;
            ShapeOptionsRectangleLabel.Visibility = Visibility.Visible;

            double x, y;
            float w, h;
            try
            {
                x = double.Parse(RectXEdit.Text);
                y = double.Parse(RectYEdit.Text);
                w = float.Parse(RectangleWidthEdit.Text);
                h = float.Parse(RectangleHeightEdit.Text);
            }
            catch (Exception)
            {
                await InvalidValuesFormatDialog();

                return;
            }

            rect.Rectangle = new Rect(x, y, w, h);
        }

        private void UpdatePoly(MPolygon poly)
        {

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

        private void HideEditPanel()
        {
            ShapeOptionsLine.Visibility = Visibility.Collapsed;
            ShapeOptionsLineLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsRectangle.Visibility = Visibility.Collapsed;
            ShapeOptionsRectangleLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsCircle.Visibility = Visibility.Collapsed;
            ShapeOptionsCircleLabel.Visibility = Visibility.Collapsed;
            EditPanelButtonsUpdate.Visibility = Visibility.Collapsed;
            ShapeOptionsMoveByVectorLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsMoveByVector.Visibility = Visibility.Collapsed;
            ShapeOptionsScaleVectorLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsScaleVector.Visibility = Visibility.Collapsed;
            ShapeOptionsRotateVectorLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsRotateVector.Visibility = Visibility.Collapsed;
        }

        private void MoveByVector_Clicked(object sender, RoutedEventArgs e)
        {
            MoveByVectorFromEditPanel();
        }

        private async void MoveByVectorFromEditPanel()
        {
            double x, y;
            try
            {
                x = double.Parse(MoveByvectorXEdit.Text);
                y = double.Parse(MoveByvectorYEdit.Text);
            }
            catch (Exception)
            {
                await InvalidValuesFormatDialog();

                return;
            }

            ShapeToEdit.MoveBy(new Point(x, y));

            UpdateEditPanel();

            canvasControl.Invalidate();
        }
    }
}
