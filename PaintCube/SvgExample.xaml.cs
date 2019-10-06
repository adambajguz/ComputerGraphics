// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas.Svg;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using PaintCube.Shapes;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaintCube
{
    public sealed partial class SvgExample : UserControl
    {
        CanvasSvgDocument svgDocument;

        class PointerDrag
        {
            Point startLocation;
            Point currentLocation;

            public PointerDrag(Point location)
            {
                startLocation = location;
                currentLocation = location;
            }

            public void UpdateDraggedLocation(Point location)
            {
                currentLocation = location;
            }

            public Point StartLocation { get { return startLocation; } }

            public Point CurrentLocation { get { return currentLocation; } }

            public Rect GetRectangle()
            {
                return new Rect(startLocation, currentLocation);
            }

            public struct Circle
            {
                public Vector2 Center;
                public float Radius;
            }

            public Circle GetCircle()
            {
                Circle result = new Circle();
                result.Center = startLocation.ToVector2();

                var dx = currentLocation.X - startLocation.X;
                var dy = currentLocation.Y - startLocation.Y;
                result.Radius = (float)Math.Sqrt((dx * dx) + (dy * dy));

                return result;
            }
        }

        PointerDrag pointerDrag { get; set; } // Null if not currently being used

        public enum ShapeType
        {
            Rectangle,
            Circle,
            Line,
        }
        public List<ShapeType> Shapes { get { return Utils.GetEnumAsList<ShapeType>(); } }
        public ShapeType CurrentShapeType { get; set; }

        public List<MShape> DrawnShapes { get; } = new List<MShape>();

        public SvgExample()
        {
            this.InitializeComponent();
        }

        void canvasControl_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            // Clears everything.
            svgDocument = new CanvasSvgDocument(canvasControl);
            canvasControl.Invalidate();
        }

        private void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            Size viewportSize = new Size() { Width = 1000, Height = 1000 };

            //args.DrawingSession.DrawSvg(svgDocument, viewportSize);


            if (pointerDrag != null)
            {
                if (CurrentShapeType == ShapeType.Rectangle)
                {
                    // Show ghost
                    args.DrawingSession.DrawRectangle(pointerDrag.GetRectangle(), Colors.Magenta);
                }
                else if (CurrentShapeType == ShapeType.Circle)
                {
                    var circle = pointerDrag.GetCircle();
                    args.DrawingSession.DrawCircle(circle.Center, circle.Radius, Colors.Magenta);
                    args.DrawingSession.DrawLine(circle.Center, pointerDrag.CurrentLocation.ToVector2(), Colors.Magenta);
                }
                else if (CurrentShapeType == ShapeType.Line)
                {
                    args.DrawingSession.DrawLine(pointerDrag.StartLocation.ToVector2(), pointerDrag.CurrentLocation.ToVector2(), Colors.Magenta);
                }
            }

            foreach (MShape shape in DrawnShapes)
            {
                shape.Draw(sender, args);
            }
        }

        private void SettingsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvasControl.Invalidate();
        }

        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            // Clears everything.
            svgDocument = new CanvasSvgDocument(canvasControl);
            DrawnShapes.Clear();
            canvasControl.Invalidate();
        }

        private void Undo_Clicked(object sender, RoutedEventArgs e)
        {
            DrawnShapes.RemoveAt(DrawnShapes.Count - 1);
            canvasControl.Invalidate();
        }

        private void control_Unloaded(object sender, RoutedEventArgs e)
        {
            // Explicitly remove references to allow the Win2D controls to get garbage collected
            canvasControl.RemoveFromVisualTree();
            canvasControl = null;
        }

        private void canvasControl_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            pointerDrag = new PointerDrag(e.GetCurrentPoint(canvasControl).Position);
            canvasControl.Invalidate();
        }

        private void canvasControl_PointerMoved(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (pointerDrag == null)
                return; // Nothing to do

            pointerDrag.UpdateDraggedLocation(e.GetCurrentPoint(canvasControl).Position);
            canvasControl.Invalidate();
        }

        private void canvasControl_PointerReleased(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (pointerDrag == null)
                return; // Nothing to do


            // Commit the shape into the document
            CanvasSvgNamedElement newChild = null;

            if (CurrentShapeType == ShapeType.Rectangle)
            {
                DrawnShapes.Add(new MRectangle(pointerDrag.StartLocation, pointerDrag.CurrentLocation));

                Rect r = pointerDrag.GetRectangle();
                newChild = svgDocument.Root.CreateAndAppendNamedChildElement("rect");
                newChild.SetFloatAttribute("x", (float)r.Left);
                newChild.SetFloatAttribute("y", (float)r.Top);
                newChild.SetFloatAttribute("width", (float)r.Width);
                newChild.SetFloatAttribute("height", (float)r.Height);
            }
            else if (CurrentShapeType == ShapeType.Circle)
            {
                DrawnShapes.Add(new MCircle(pointerDrag.StartLocation, pointerDrag.CurrentLocation));

                var circle = pointerDrag.GetCircle();
                newChild = svgDocument.Root.CreateAndAppendNamedChildElement("circle");
                newChild.SetFloatAttribute("cx", circle.Center.X);
                newChild.SetFloatAttribute("cy", circle.Center.Y);
                newChild.SetFloatAttribute("r", circle.Radius);
            }
            else if (CurrentShapeType == ShapeType.Line)
            {
                DrawnShapes.Add(new MLine(pointerDrag.StartLocation, pointerDrag.CurrentLocation));
            }

            pointerDrag = null;
            canvasControl.Invalidate();
        }
    }
}
