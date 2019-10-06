// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System.Collections.Generic;
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
        public object ShapeToEdit { get; set; }

        public MShape PendingShape { get; set; }
        public bool IsMouseDragDrawOn { get; set; }

        public SvgExample()
        {
            this.InitializeComponent();
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
                PendingShape.DrawGhost(sender, args);
            }
        }

        private void SettingsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvasControl.Invalidate();
        }

        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            DrawnShapes.Clear();
            canvasControl.Invalidate();
        }

        private void Undo_Clicked(object sender, RoutedEventArgs e)
        {
            if (DrawnShapes.Count > 0)
            {
                DrawnShapes.RemoveAt(DrawnShapes.Count - 1);
                canvasControl.Invalidate();
            }
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

            canvasControl.Invalidate();
        }

        private void canvasControl_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (PendingShape == null)
                return; // Nothing to do

            PendingShape.EndLocation = e.GetCurrentPoint(canvasControl).Position;

            canvasControl.Invalidate();
        }

        private void canvasControl_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (PendingShape == null || IsMouseDragDrawOn)
                return; // Nothing to do

            AddShape();
        }

        private void AddShape()
        {
            DrawnShapes.Add(PendingShape);
            DrawnShapesCombo.ItemsSource = null;
            DrawnShapesCombo.ItemsSource = DrawnShapes;
            PendingShape = null;
            canvasControl.Invalidate();
        }
    }
}
