using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace MyBezier
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private List<PointF> Points = new List<PointF>();
        private List<PointF> PointsCopy;
        private bool helpLines = true;
        private bool pickedUp = false;
        private bool refreshed = false;
        private PointF PickUpLocation;
        private string[] TestButtonTexts = { "Rysuj testowe", "Ukryj testowe" };
        private string[] DrawHelpLineButtonTexts = { "Ukryj linie pomocnicze", "Pokaż linie pomocnicze" };

        private void picCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !pickedUp)
            {
                PointF p = new PointF(e.X, e.Y);
                Points.Add(p);
                labelPoints.Text = Points.Count().ToString();
                picCanvas.Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!pickedUp)
                    PointsCopy = new List<PointF>(Points);
                else
                    PointsCopy.Clear();
                pickedUp = !pickedUp;
                PickUpLocation = e.Location;
            }
        }
        
        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(picCanvas.BackColor);
            int r = Points.Count();

            if (helpLines)
            {
                foreach (PointF p in Points)
                    e.Graphics.FillRectangle(Brushes.Red, p.X, p.Y, 5, 5);

                for (int i = 0; i < r - 1; i++)
                    e.Graphics.DrawLine(Pens.Red, Points[i], Points[i + 1]);
            }

            if (r>2)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Bezier.DrawBezier(e.Graphics, Pens.Black, 0.01f, Points.ToArray<PointF>());
            }
        }

        private void buttonHideHelpLines_Click(object sender, EventArgs e)
        {
            helpLines = !helpLines;
            buttonHideHelpLines.Text = DrawHelpLineButtonTexts[helpLines ? 0 : 1];
            picCanvas.Refresh();
        }

        private void buttonClearPoints_Click(object sender, EventArgs e)
        {
            Points.Clear();
            labelPoints.Text = Points.Count().ToString();
            picCanvas.Refresh();
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if(pickedUp)
            {
                for (int i = 0; i < Points.Count(); i++)
                    Points[i] = new PointF(
                        PointsCopy[i].X + e.X - PickUpLocation.X,
                        PointsCopy[i].Y + e.Y - PickUpLocation.Y);

                picCanvas.Refresh();
            }
        }

        private void buttonAddPoint_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(textBoxPointX.Text, out int x) &&
                Int32.TryParse(textBoxPointY.Text, out int y))
            {
                Points.Add(new PointF(x, y));
                picCanvas.Refresh();
            }
        }
    }
}
