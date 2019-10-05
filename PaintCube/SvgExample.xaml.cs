// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Graphics.Canvas.Svg;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ExampleGallery
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


        public SvgExample()
        {
            this.InitializeComponent();
        }

        void canvasControl_CreateResources(CanvasControl sender, CanvasCreateResourcesEventArgs args)
        {
            CreateSomeSimplePlaceholderDocument();
        }

        private void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            Size viewportSize = new Size() { Width = 1000, Height = 1000 };

            args.DrawingSession.DrawSvg(svgDocument, viewportSize);


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
        }

        private void SettingsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canvasControl.Invalidate();
        }

        void CreateSomeSimplePlaceholderDocument()
        {
            svgDocument = new CanvasSvgDocument(canvasControl);

            {
                var rect = svgDocument.Root.CreateAndAppendNamedChildElement("rect");
                UseDefaultStroke(rect);
                rect.SetColorAttribute("fill", Color.FromArgb(0xFF, 0xFF, 0xFF, 0x0));
                rect.SetFloatAttribute("x", 20);
                rect.SetFloatAttribute("y", 20);
                rect.SetFloatAttribute("width", 100);
                rect.SetFloatAttribute("height", 100);
            }
            {
                var circle = svgDocument.Root.CreateAndAppendNamedChildElement("circle");
                UseDefaultStroke(circle);
                circle.SetColorAttribute("fill", Color.FromArgb(0xFF, 0x8B, 0x0, 0x0));
                circle.SetFloatAttribute("cx", 140);
                circle.SetFloatAttribute("cy", 140);
                circle.SetFloatAttribute("r", 70);
            }
            {
                var line = svgDocument.Root.CreateAndAppendNamedChildElement("line");
                UseDefaultStroke(line);
                line.SetFloatAttribute("x1", 20);
                line.SetFloatAttribute("y1", 20);
                line.SetFloatAttribute("x2", 300);
                line.SetFloatAttribute("y2", 180);
            }
        }

        private static void UseDefaultStroke(CanvasSvgNamedElement rect)
        {
            rect.SetColorAttribute("stroke", Colors.Black);
            rect.SetFloatAttribute("stroke-width", 5);
        }

        private void NewDocument_Click(object sender, RoutedEventArgs e)
        {
            CreateSomeSimplePlaceholderDocument();
            canvasControl.Invalidate();
        }

        private void Clear_Clicked(object sender, RoutedEventArgs e)
        {
            // Clears everything.
            svgDocument = new CanvasSvgDocument(canvasControl);
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
                Rect r = pointerDrag.GetRectangle();
                newChild = svgDocument.Root.CreateAndAppendNamedChildElement("rect");
                newChild.SetFloatAttribute("x", (float)r.Left);
                newChild.SetFloatAttribute("y", (float)r.Top);
                newChild.SetFloatAttribute("width", (float)r.Width);
                newChild.SetFloatAttribute("height", (float)r.Height);
            }
            else if (CurrentShapeType == ShapeType.Circle)
            {
                var circle = pointerDrag.GetCircle();
                newChild = svgDocument.Root.CreateAndAppendNamedChildElement("circle");
                newChild.SetFloatAttribute("cx", circle.Center.X);
                newChild.SetFloatAttribute("cy", circle.Center.Y);
                newChild.SetFloatAttribute("r", circle.Radius);
            }
            else if (CurrentShapeType == ShapeType.Line)
            {
                var start = pointerDrag.StartLocation.ToVector2();
                var end = pointerDrag.CurrentLocation.ToVector2();
                newChild = svgDocument.Root.CreateAndAppendNamedChildElement("line");
                newChild.SetFloatAttribute("x1", start.X);
                newChild.SetFloatAttribute("y1", start.Y);
                newChild.SetFloatAttribute("x2", end.X);
                newChild.SetFloatAttribute("y2", end.Y);
            }

            newChild.SetColorAttribute("fill", Colors.Transparent);
            newChild.SetColorAttribute("stroke", Colors.Black);
            newChild.SetFloatAttribute("stroke-width", 4.0f);

            pointerDrag = null;
            canvasControl.Invalidate();
        }
    }
}
