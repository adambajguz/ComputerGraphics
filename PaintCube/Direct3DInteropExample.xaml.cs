// Copyright (c) Microsoft Corporation. All rights reserved.
//
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

using System;
using System.Numerics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;
using PaintCube.Direct3DInterop;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PaintCube
{
    public sealed partial class Direct3DInteropExample : Page
    {
        public bool SpinEnabled { get; set; }

        private float SpinTheTeapot { get; set; }


        // The TeapotRenderer class is provided by the ExampleGallery.Direct3DInterop project,
        // which is written in C++/CX. It uses interop to combine Direct3D rendering with Win2D.
        private CubeRenderer Cube { get; set; }

        private float TextRenderTargetSize { get; } = 256;

        private CanvasRenderTarget TextRenderTarget { get; set; }

        public Direct3DInteropExample()
        {
            SpinEnabled = true;

            this.InitializeComponent();
        }

        private void canvas_CreateResources(CanvasAnimatedControl sender, CanvasCreateResourcesEventArgs args)
        {
            // Create the Direct3D teapot model.
            Cube = new CubeRenderer(sender);

            TextRenderTarget = new CanvasRenderTarget(sender, TextRenderTargetSize, TextRenderTargetSize);

            // Set the scrolling text rendertarget (a Win2D object) as
            // source texture for our 3D teapot model (which uses Direct3D).
            Cube.SetTexture(TextRenderTarget);
        }

        private async void canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if (SpinEnabled)
            {
                SpinTheTeapot += (float)args.Timing.ElapsedTime.TotalSeconds / 2;

                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    Yaw.Value = yawVal = (float)(SpinTheTeapot % 6.21);
                    Pitch.Value = pitchVal = (float)((SpinTheTeapot / 23) % 6.21);
                    Roll.Value = rollVal = (float)((SpinTheTeapot / 42) % 6.21);
                });
            }
            else
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    yawVal = (float)Yaw.Value;
                    pitchVal = (float)Pitch.Value;
                    rollVal = (float)Roll.Value;
                });
            }
        }

        private void canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            DrawWalls(args.Timing);

            DrawTeapot(sender, args.DrawingSession);
        }

        private float yawVal, pitchVal, rollVal;
        private void DrawTeapot(ICanvasAnimatedControl sender, CanvasDrawingSession drawingSession)
        {
            Vector2 size = sender.Size.ToVector2();

            // Draw the teapot (using Direct3D).
            //Cube.SetWorld(Matrix4x4.CreateFromYawPitchRoll(-SpinTheTeapot, SpinTheTeapot / 23, SpinTheTeapot / 42));


            Cube.SetWorld(Matrix4x4.CreateFromYawPitchRoll(-yawVal, pitchVal, rollVal));
            Cube.SetView(Matrix4x4.CreateLookAt(new Vector3(1.5f, 1, 0), Vector3.Zero, Vector3.UnitY));
            Cube.SetProjection(Matrix4x4.CreatePerspectiveFieldOfView(1, size.X / size.Y, 0.1f, 10f));

            Cube.Draw(drawingSession);
        }

        private void DrawWalls(CanvasTimingInformation timing)
        {
            // We draw the text into a rendertarget, and update this every frame to make it scroll.
            // The resulting rendertarget is then mapped as a texture onto the Direct3D teapot model.
            using (var drawingSession = TextRenderTarget.CreateDrawingSession())
            {
                drawingSession.Clear(Colors.White);
                drawingSession.DrawRectangle(0, 0, TextRenderTargetSize - 1, TextRenderTargetSize - 1, Colors.Red);
            }
        }

        private void control_Unloaded(object sender, RoutedEventArgs e)
        {
            // Explicitly remove references to allow the Win2D controls to get garbage collected
            canvas.RemoveFromVisualTree();
            canvas = null;
        }
    }
}
