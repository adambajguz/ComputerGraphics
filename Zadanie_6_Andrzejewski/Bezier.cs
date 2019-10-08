using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyBezier
{
    class Bezier
    {
        private static List<int> Newton(int n)
        {
            int[] temp = new int[n];
            temp[0] = 1;
            for (int j = 0; j < n - 1; j++)
                for (int i = n - 1; i > 0; i--)
                {
                    temp[i] = temp[i] + (((i - 1) >= 0) ? temp[i - 1] : 0);
                }

            return temp.ToList<int>();
        }

        private static float NextCoord(float t, List<float> x)
        {
            List<int> newton = Newton(x.Count());
            float s = 0;

            for (int i = 0; i < x.Count(); i++)
            {
                float a1 = (x.Count - i + 1) == 0 ? 1 : (float)Math.Pow(1 - t, x.Count() - (i + 1));
                float a2 = i == 0 ? 1 : (float)Math.Pow(t, i);

                s += newton[i] * x[i] * a1 * a2;
            }

            return s;
        }

        public static void DrawBezier(Graphics gr, Pen the_pen, float dt, params PointF[] points)
        {
            List<float> tempX = points.Select(e => e.X).ToList<float>();
            List<float> tempY = points.Select(e => e.Y).ToList<float>();

            List<PointF> p = new List<PointF>();

            for (float t = 0.0f; t < 1; t += dt)
            {
                p.Add(new PointF(
                    NextCoord(t, tempX),
                    NextCoord(t, tempY)));
            }

            p.Add(new PointF(
               NextCoord(1.0f, tempX),
               NextCoord(1.0f, tempY)));

            gr.DrawLines(the_pen, p.ToArray());
        }
    }
}
