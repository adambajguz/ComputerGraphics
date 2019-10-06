// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using PaintCube.Shapes;
using Windows.Foundation;
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
        public bool IsMouseDragDrawOn { get; set; }
        public bool IsSelectionMode { get; set; }

        public SvgExample()
        {
            this.InitializeComponent();

            ShapeOptionsCommon.Visibility = Visibility.Collapsed;
            ShapeOptionsCommonLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsRectangle.Visibility = Visibility.Collapsed;
            ShapeOptionsRectangleLabel.Visibility = Visibility.Collapsed;
            ShapeOptionsCircle.Visibility = Visibility.Collapsed;
            ShapeOptionsCircleLabel.Visibility = Visibility.Collapsed;
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
            }

            if (PendingShape != null)
            {
                PendingShape.Draw(sender, args);
            }
        }

        private void SettingsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvasControl.Invalidate();
        }

        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            DrawnShapes.Clear();
            ClearShapesComboSelection();

            canvasControl.Invalidate();
        }

        private void Undo_Clicked(object sender, RoutedEventArgs e)
        {
            if (DrawnShapes.Count > 0)
            {
                DrawnShapes.RemoveAt(DrawnShapes.Count - 1);
                ClearShapesComboSelection();

                canvasControl.Invalidate();
            }
        }

        private void ClearShapesComboSelection()
        {
            DrawnShapesCombo.ItemsSource = null;
            DrawnShapesCombo.ItemsSource = DrawnShapes;
            ShapeToEdit.IsInEditMode = false;

            ShapeToEdit = null;
            UpdateEditPanel();
            canvasControl.Invalidate();
        }

        private void control_Unloaded(object sender, RoutedEventArgs e)
        {
            // Explicitly remove references to allow the Win2D controls to get garbage collected
            canvasControl.RemoveFromVisualTree();
            canvasControl = null;
        }

        private bool ClickedOnce { get; set; }

        private void canvasControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (IsSelectionMode)
            {

                return;
            }

            if (IsMouseDragDrawOn)
            {
                if (!ClickedOnce)
                    ClickedOnce = true;
                else
                {
                    ClickedOnce = false;

                    AddShape();

                    return;
                }
            }

            Point startPosition = e.GetCurrentPoint(canvasControl).Position;
            if (CurrentShapeType == ShapeType.Rectangle)
            {
                PendingShape = new MRectangle(startPosition, startPosition);
            }
            else if (CurrentShapeType == ShapeType.Circle)
            {
                PendingShape = new MCircle(startPosition, startPosition);
            }
            else if (CurrentShapeType == ShapeType.Line)
            {
                PendingShape = new MLine(startPosition, startPosition);
            }

            PendingShape.IsInEditMode = true;

            canvasControl.Invalidate();
        }

        private void canvasControl_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (IsSelectionMode)
            {
                return;
            }

            if (PendingShape == null)
                return; // Nothing to do

            PendingShape.EndLocation = e.GetCurrentPoint(canvasControl).Position;

            canvasControl.Invalidate();
        }

        private void canvasControl_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (IsSelectionMode)
            {
                return;
            }

            if (PendingShape == null || IsMouseDragDrawOn)
                return; // Nothing to do

            AddShape();
        }

        private void AddShape()
        {
            PendingShape.IsInEditMode = false;
            DrawnShapes.Add(PendingShape);

            int idx = DrawnShapesCombo.SelectedIndex;
            DrawnShapesCombo.ItemsSource = null;
            DrawnShapesCombo.ItemsSource = DrawnShapes;
            DrawnShapesCombo.SelectedIndex = idx;

            PendingShape = null;
            canvasControl.Invalidate();
        }

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

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            // IsSelectionMode = (sender as ToggleSwitch).IsOn;
        }

        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            // IsMouseDragDrawOn = (sender as ToggleSwitch).IsOn;
        }
    }
}
