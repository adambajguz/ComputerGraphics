using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafika2
{
    public partial class Przeksztalcenia2D : Form
    {
        private int x0, y0;
        private Bitmap clearBitmap;
        private bool drawingMode = true;
        private List<Point> pointsList;

        public Przeksztalcenia2D()
        {
            InitializeComponent();
            pointsList = new List<Point>();
            clearBitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics formGraphics = Graphics.FromImage(clearBitmap))
            {
                Pen pen = new Pen(Color.Black);
                x0 = Convert.ToInt32(pictureBox.Width / 2);
                y0 = Convert.ToInt32(pictureBox.Height / 2);
                formGraphics.DrawLine(pen, new Point(x0, 0), new Point(x0, pictureBox.Height));
                formGraphics.DrawLine(pen, new Point(0, y0), new Point(pictureBox.Width, y0));
                pen.Dispose();
                formGraphics.Dispose();
            }
            pictureBox.Image = clearBitmap;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pointsList.Clear();
            pictureBox.Image = clearBitmap;
            drawingMode = true;
        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (drawingMode)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    pointsList.Add(e.Location);
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    pictureBox.Image = clearBitmap;
                    drawingMode = false;
                }
                pictureBox.Refresh();
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (drawingMode)
            {
                foreach (var pt in pointsList)
                {
                    e.Graphics.FillEllipse(Brushes.Green, pt.X - 4, pt.Y - 4, 8, 8);
                }
            }
            else
            {
                GraphicsPath path = new GraphicsPath();
                path.AddLines(pointsList.ToArray());
                e.Graphics.FillPath(Brushes.Red, path);
                e.Graphics.DrawPath(new Pen(Brushes.Red), path);
            }
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            int newWidth = pictureBox.Width;
            int newHeight = pictureBox.Height;

        }

        private void btnPrzesuniecieOK_Click(object sender, EventArgs e)
        {
            try
            {
                int h = Convert.ToInt32(tbPrzesuniecieH.Text);
                int v = Convert.ToInt32(tbPrzesuniecieV.Text);
                
                for (int i = 0; i < pointsList.Count; i++)
                {
                    pointsList[i] = new Point(pointsList[i].X + h, pointsList[i].Y + v);
                }
                pictureBox.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnObrotOK_Click(object sender, EventArgs e)
        {
            try
            {
                int alfa = Convert.ToInt32(tbObrotAlfa.Text);
                for (int i = 0; i < pointsList.Count; i++)
                {
                    double newX = x0 + ((pointsList[i].X - x0) * Math.Cos(ConvertToRadians(alfa))) - ((pointsList[i].Y - y0) * Math.Sin(ConvertToRadians(alfa)));
                    double newY = y0 + ((pointsList[i].X - x0) * Math.Sin(ConvertToRadians(alfa))) + ((pointsList[i].Y - y0) * Math.Cos(ConvertToRadians(alfa)));
                    pointsList[i] = new Point(Convert.ToInt32(newX), Convert.ToInt32(newY));
                }
                pictureBox.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSkalowanieOK_Click(object sender, EventArgs e)
        {
            int Xs = Convert.ToInt32(tbSkalowanieX.Text);
            int Ys = Convert.ToInt32(tbSkalowanieY.Text);
            double k = Convert.ToDouble(tbSkalowanieK.Text);

            for (int i = 0; i < pointsList.Count; i++)
            {
                double newX = pointsList[i].X * k + (1 - k) * Xs;
                double newY = pointsList[i].Y * k + (1 - k) * Ys;
                pointsList[i] = new Point(Convert.ToInt32(newX), Convert.ToInt32(newY));
            }
            pictureBox.Refresh();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            lblMousePos.Text = "Aktualna pozycja myszy: X = " + e.X + "  Y = " + e.Y;
        }

        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private void pictureBox_MouseLeave_1(object sender, EventArgs e)
        {
            lblMousePos.Text = "";
        }
    }
}
