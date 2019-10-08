using grafika2.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace grafika2
{
    public enum DrawingModes
    {
        DrawShapes,
        Bezier,
        None
    }

    public enum Shape
    {
        Linia,
        Prostokat,
        Okrag
    }

    public partial class PPMForm : Form
    {
        private PPMReader image;
        public Bitmap Img { get; set; }

        #region rysowanie
        public Image ImgTmp { get; set; }
        public SolidBrush Brush { get; set; }
        public bool IsMouseDown { get; set; }
        public Shape PickedShape { get; set; }
        public float Grubosc { get; set; }
        public Point MouseDownPoint { get; set; }
        #endregion
        private Bitmap OriginalImage { get; set; }
        private DrawingModes DrawingMode { get; set; }

        private int[] R_Values { get; set; }
        private int[] G_Values { get; set; }
        private int[] B_Values { get; set; }
        private PixelFormat PixelFormat { get; set; }

        #region Bezier
        protected Point[] apt = new Point[4];
        bool startPoint_Set = false;
        bool endPoint_Set = false;
        bool controlPoint1_Set = false;
        bool controlPoint2_Set = false;
        bool all_Set = false;
        Brush startPointBrush = Brushes.Black;
        Brush endPointBrush = Brushes.Magenta;
        Brush ctrlPt1Brush = Brushes.Green;
        Brush ctrlPt2Brush = Brushes.Yellow;
        int MovingPoint = -1;
        #endregion

        public PPMForm()
        {
            DrawingMode = DrawingModes.None;
            InitializeComponent();
            Brush = new SolidBrush(Color.Black);
            colorPanel.BackColor = Color.Black;
            IsMouseDown = false;
            PickedShape = Shape.Linia;
            Grubosc = 1;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        public void WriteBitmapToPPMP6(string file, Bitmap img)
        {
            Bitmap bitmap = img;
            //Use a streamwriter to write the text part of the encoding
            var writer = new StreamWriter(file);
            writer.Write("P6" + Environment.NewLine);
            writer.Write(bitmap.Width + " " + bitmap.Height + Environment.NewLine);
            writer.Write("255" + Environment.NewLine);
            writer.Close();
            //Switch to a binary writer to write the data
            var writerB = new BinaryWriter(new FileStream(file, FileMode.Append));
            for (int x = 0; x < bitmap.Height; x++)
                for (int y = 0; y < bitmap.Width; y++)
                {
                    Color color = bitmap.GetPixel(y, x);
                    writerB.Write(color.R);
                    writerB.Write(color.G);
                    writerB.Write(color.B);
                }
            writerB.Close();
        }

        public void WriteBitmapToPPMP3(string file, Bitmap img)
        {
            Bitmap bitmap = img;
            //Use a streamwriter to write the text part of the encoding
            var writer = new StreamWriter(file);
            writer.Write("P3" + Environment.NewLine);
            writer.Write(bitmap.Width + " " + bitmap.Height + Environment.NewLine);
            writer.Write("255" + Environment.NewLine);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    writer.Write(color.R + " " + color.G + " " + color.B + "    ");
                }
                writer.Write(Environment.NewLine);
            }

            writer.Close();
            
        }

        private void LoadCharts()
        {
            Color color;
            R_Values = new int[256];
            G_Values = new int[256];
            B_Values = new int[256];

            histogramRGBSrednia.Series["RGB"].Points.Clear();
            histogramR.Series["R"].Points.Clear();
            histogramG.Series["G"].Points.Clear();
            histogramB.Series["B"].Points.Clear();
            histogramRGB.Series["R"].Points.Clear();
            histogramRGB.Series["G"].Points.Clear();
            histogramRGB.Series["B"].Points.Clear();

            histogramRGBSredniaMod.Series["RGB"].Points.Clear();
            histogramRMod.Series["R"].Points.Clear();
            histogramGMod.Series["G"].Points.Clear();
            histogramBMod.Series["B"].Points.Clear();
            histogramRGBMod.Series["R"].Points.Clear();
            histogramRGBMod.Series["G"].Points.Clear();
            histogramRGBMod.Series["B"].Points.Clear();

            //inicjalizacja tablic
            for (int i = 0; i < 256; i++)
            {
                R_Values[i] = 0;
                G_Values[i] = 0;
                B_Values[i] = 0;
            }

            for (int i = 0; i < OriginalImage.Width; i++)
            {
                for (int j = 0; j < OriginalImage.Height; j++)
                {
                    color = OriginalImage.GetPixel(i, j);
                    R_Values[color.R] += 1;
                    G_Values[color.G] += 1;
                    B_Values[color.B] += 1;
                }
            }

            for (int i = 0; i < 256; i++)
            {
                histogramRGBSrednia.Series["RGB"].Points.AddXY(i, (int)((R_Values[i] + G_Values[i] + B_Values[i]) / 3));
                histogramR.Series["R"].Points.AddXY(i, R_Values[i]);
                histogramG.Series["G"].Points.AddXY(i, G_Values[i]);
                histogramB.Series["B"].Points.AddXY(i, B_Values[i]);

                histogramRGB.Series["R"].Points.AddXY(i, R_Values[i]);
                histogramRGB.Series["G"].Points.AddXY(i, G_Values[i]);
                histogramRGB.Series["B"].Points.AddXY(i, B_Values[i]);
            }

            #region setting min and max values
            histogramRGBSrednia.ChartAreas[0].AxisX.Maximum = 255;
            histogramRGBSrednia.ChartAreas[0].AxisX.Minimum = 0;
            histogramR.ChartAreas[0].AxisX.Maximum = 255;
            histogramR.ChartAreas[0].AxisX.Minimum = 0;
            histogramG.ChartAreas[0].AxisX.Maximum = 255;
            histogramG.ChartAreas[0].AxisX.Minimum = 0;
            histogramB.ChartAreas[0].AxisX.Maximum = 255;
            histogramB.ChartAreas[0].AxisX.Minimum = 0;

            histogramRGB.ChartAreas[0].AxisX.Maximum = 255;
            histogramRGB.ChartAreas[0].AxisX.Minimum = 0;
            #endregion

            LoadModifiedCharts();
        }

        private void LoadModifiedCharts()
        {
            Color color;
            R_Values = new int[256];
            G_Values = new int[256];
            B_Values = new int[256];

            histogramRGBSredniaMod.Series["RGB"].Points.Clear();
            histogramRMod.Series["R"].Points.Clear();
            histogramGMod.Series["G"].Points.Clear();
            histogramBMod.Series["B"].Points.Clear();
            histogramRGBMod.Series["R"].Points.Clear();
            histogramRGBMod.Series["G"].Points.Clear();
            histogramRGBMod.Series["B"].Points.Clear();

            //inicjalizacja tablic
            for (int i = 0; i < 256; i++)
            {
                R_Values[i] = 0;
                G_Values[i] = 0;
                B_Values[i] = 0;
            }

            for (int i = 0; i < Img.Width; i++)
            {
                for (int j = 0; j < Img.Height; j++)
                {
                    color = Img.GetPixel(i, j);
                    R_Values[color.R] += 1;
                    G_Values[color.G] += 1;
                    B_Values[color.B] += 1;
                }
            }

            for (int i = 0; i < 256; i++)
            {
                histogramRGBSredniaMod.Series["RGB"].Points.AddXY(i, (int)((R_Values[i] + G_Values[i] + B_Values[i]) / 3));
                histogramRMod.Series["R"].Points.AddXY(i, R_Values[i]);
                histogramGMod.Series["G"].Points.AddXY(i, G_Values[i]);
                histogramBMod.Series["B"].Points.AddXY(i, B_Values[i]);

                histogramRGBMod.Series["R"].Points.AddXY(i, R_Values[i]);
                histogramRGBMod.Series["G"].Points.AddXY(i, G_Values[i]);
                histogramRGBMod.Series["B"].Points.AddXY(i, B_Values[i]);
            }

            histogramRGBSredniaMod.ChartAreas[0].AxisX.Maximum = 255;
            histogramRGBSredniaMod.ChartAreas[0].AxisX.Minimum = 0;
            histogramRMod.ChartAreas[0].AxisX.Maximum = 255;
            histogramRMod.ChartAreas[0].AxisX.Minimum = 0;
            histogramGMod.ChartAreas[0].AxisX.Maximum = 255;
            histogramGMod.ChartAreas[0].AxisX.Minimum = 0;
            histogramBMod.ChartAreas[0].AxisX.Maximum = 255;
            histogramBMod.ChartAreas[0].AxisX.Minimum = 0;

            histogramRGBMod.ChartAreas[0].AxisX.Maximum = 255;
            histogramRGBMod.ChartAreas[0].AxisX.Minimum = 0;
        }

        private void rozciagnieciePrzeliczLUT(int a, int b, int[] lut)
        {
            for (int i = 0; i < 256; i++)
            {
                if ((a * (i + b)) > 255)
                    lut[i] = 255;
                else if ((a * (i + b)) < 0)
                    lut[i] = 0;
                else lut[i] = (a * (i + b));
            }
        }

        #region UI Events
        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (pbImage.Image != null) pbImage.Image = null;
                if (pbImage.BackgroundImage != null) pbImage.BackgroundImage = null;

                var dlg = new OpenFileDialog();
                dlg.Filter = "Pliki JPEG|*.jpeg;*.jpg|Pliki PPM|*.ppm";
                dlg.Title = "Wybierz plik";
                dlg.Multiselect = false;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var fileExtension = System.IO.Path.GetExtension(dlg.FileName);
                    if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg")
                    {
                        Img = new Bitmap(dlg.FileName);
                    }
                    else
                    {
                        image = new PPMReader(dlg.FileName);
                        Img = image.BitMap;
                    }
                    pbImage.Width = Img.Width;
                    pbImage.Height = Img.Height;
                    pbImage.Image = Img;
                    OriginalImage = (Bitmap)Img.Clone();
                    PixelFormat = Img.PixelFormat;
                    LoadCharts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var dlg = new SaveFileDialog();
                dlg.Filter = "Plik JPEG|*.jpeg|Pliki PPM|*.ppm";
                dlg.Title = "Zapisz plik";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (System.IO.Path.GetExtension(dlg.FileName.ToLower()) == ".ppm")
                    {
                        WriteBitmapToPPMP6(dlg.FileName, Img);
                    }
                    else
                    {
                        Img.Save(dlg.FileName);
                    }

                    MessageBox.Show("Plik został poprawnie zapisany");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void wartośćŚredniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null) return;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color currentColor = Img.GetPixel(x, y);
                    int sum = currentColor.R + currentColor.G + currentColor.B;
                    int average = Convert.ToInt32(Math.Round((double)sum / (double)3, 0));
                    Color newColor = Color.FromArgb(255, average, average, average);
                    Img.SetPixel(x, y, newColor);
                }
            }
            pbImage.Image = Img;
            LoadModifiedCharts();
            MessageBox.Show("Wykonano operację");
        }

        private void zWagamiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null) return;
            for (int y = 0; y < Img.Height; y++)
            {
                for (int x = 0; x < Img.Width; x++)
                {
                    Color currentColor = Img.GetPixel(x, y);
                    int average = Convert.ToInt32(Math.Round((0.299 * (double)currentColor.R + 0.587 * (double)currentColor.G + 0.114 * (double)currentColor.B), 0));
                    Color newColor = Color.FromArgb(255, average, average, average);
                    Img.SetPixel(x, y, newColor);
                }
            }
            pbImage.Image = Img;
            LoadModifiedCharts();
            MessageBox.Show("Wykonano operację");
        }

        private void dodawanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValuePopup dlg = new ValuePopup();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color color = Img.GetPixel(x, y);
                        int r, g, b;
                        r = color.R + (int)dlg.ValueR;
                        if (r > 255) r = 255;
                        if (r < 0) r = 0;
                        g = color.G + (int)dlg.ValueG;
                        if (g > 255) g = 255;
                        if (g < 0) g = 0;
                        b = color.B + (int)dlg.ValueB;
                        if (b > 255) b = 255;
                        if (b < 0) b = 0;
                        Color newColor = Color.FromArgb(255, r, g, b);
                        Img.SetPixel(x, y, newColor);
                    }
                }
                pbImage.Image = Img;
                LoadModifiedCharts();
                MessageBox.Show("Wykonano operację");
            }
        }

        private void mnożenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValuePopup dlg = new ValuePopup();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color color = Img.GetPixel(x, y);
                        int r, g, b;
                        r = Convert.ToInt32((double)color.R * dlg.ValueR);
                        if (r > 255) r = 255;
                        if (r < 0) r = 0;
                        g = Convert.ToInt32((double)color.G * dlg.ValueG);
                        if (g > 255) g = 255;
                        if (g < 0) g = 0;
                        b = Convert.ToInt32((double)color.B * dlg.ValueB);
                        if (b > 255) b = 255;
                        if (b < 0) b = 0;
                        Color newColor = Color.FromArgb(255, r, g, b);
                        Img.SetPixel(x, y, newColor);
                    }
                }
                pbImage.Image = Img;
                LoadModifiedCharts();
                MessageBox.Show("Wykonano operację");
            }
        }

        private void dzielenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValuePopup dlg = new ValuePopup();
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                for (int y = 0; y < Img.Height; y++)
                {
                    for (int x = 0; x < Img.Width; x++)
                    {
                        Color color = Img.GetPixel(x, y);
                        int r, g, b;
                        r = Convert.ToInt32((double)color.R / dlg.ValueR);
                        if (r > 255) r = 255;
                        if (r < 0) r = 0;
                        g = Convert.ToInt32((double)color.G / dlg.ValueG);
                        if (g > 255) g = 255;
                        if (g < 0) g = 0;
                        b = Convert.ToInt32((double)color.B / dlg.ValueB);
                        if (b > 255) b = 255;
                        if (b < 0) b = 0;
                        Color newColor = Color.FromArgb(255, r, g, b);
                        Img.SetPixel(x, y, newColor);
                    }
                }
                pbImage.Image = Img;
                LoadModifiedCharts();
                MessageBox.Show("Wykonano operację");
            }
        }

        private void filtryMorfologiczneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorphologicalOperationsForm dlg = new MorphologicalOperationsForm(Img);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Img = dlg.Image;
                pbImage.Image = Img;
                LoadModifiedCharts();
            }
        }

        private void zresetujObrazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OriginalImage != null)
            {
                Img = (Bitmap)OriginalImage.Clone();
                pbImage.Image = Img;
                LoadModifiedCharts();
            }
        }

        private void obliczTerenyZieloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img != null)
            {
                var result = ImageAnalysis.DetectGreenTerrain(Img);
                Img = result;
                Img = MorphologicalOperations.Close(Img, 7, true, true, true);
                pbImage.Image = Img;
                var percentage = ImageAnalysis.CalculateGreenTerrain(Img);
                LoadModifiedCharts();
                MessageBox.Show("Tereny zielone stanowią " + percentage + "% obrazu.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDrawShapes.Checked == true)
            {
                DrawingMode = DrawingModes.DrawShapes;
                cbBezier.Checked = false;
            }
            else if(cbBezier.Checked == false)
            {
                DrawingMode = DrawingModes.None;
            }
        }

        private void cbBezier_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBezier.Checked == true)
            {
                DrawingMode = DrawingModes.Bezier;
                cbDrawShapes.Checked = false;
                MessageBox.Show("Proszę wybrać punkt startowy");
            }
            else if (cbDrawShapes.Checked == false)
            {
                DrawingMode = DrawingModes.None;
            }
        }

        private void btnLinia_Click(object sender, EventArgs e)
        {
            PickedShape = Shape.Linia;
        }

        private void btnOkrag_Click(object sender, EventArgs e)
        {
            PickedShape = Shape.Okrag;
        }

        private void btnProstokat_Click(object sender, EventArgs e)
        {
            PickedShape = Shape.Prostokat;
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.Color = Brush.Color;

            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                Brush = new SolidBrush(MyDialog.Color);
                colorPanel.BackColor = MyDialog.Color;
            }
        }

        private void tbGrubosc_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbGrubosc.Text)) return;

            float gr;
            if (float.TryParse(tbGrubosc.Text, out gr))
            {
                Grubosc = gr;
            }
            else MessageBox.Show("Podana wartość jest niepoprawna");
        }

        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            switch (DrawingMode)
            {
                case DrawingModes.DrawShapes:
                    pbImage_MouseDown_DrawShapes(sender, e);
                    break;
                case DrawingModes.Bezier:
                    pictureBox1_MouseDown_Bezier(sender, e);
                    break;
                case DrawingModes.None:
                    break;
                default:
                    break;
            }
        }

        private void pbImage_MouseMove(object sender, MouseEventArgs e)
        {
            switch (DrawingMode)
            {
                case DrawingModes.DrawShapes:
                    pbImage_MouseMove_DrawShapes(sender, e);
                    break;
                case DrawingModes.Bezier:
                    pictureBox1_MouseMove_Bezier(sender, e);
                    break;
                case DrawingModes.None:
                    break;
                default:
                    break;
            }
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            switch (DrawingMode)
            {
                case DrawingModes.DrawShapes:
                    pbImage_MouseUp_DrawShapes(sender, e);
                    break;
                case DrawingModes.Bezier:
                    break;
                case DrawingModes.None:
                    break;
                default:
                    break;
            }
        }
        #endregion //end UI Events

        private void pbImage_MouseDown_DrawShapes(object sender, MouseEventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            MouseDownPoint = me.Location;
            ImgTmp = (Bitmap)Img.Clone();
            Graphics g = Graphics.FromImage(ImgTmp);
            Pen pen = new Pen(Brush, Grubosc);

            if (PickedShape == Shape.Linia)
            {
                g.DrawLine(pen, MouseDownPoint, MouseDownPoint);
            }
            else if (PickedShape == Shape.Prostokat)
            {
                g.DrawRectangle(pen, MouseDownPoint.X, MouseDownPoint.Y, 0, 0);
            }
            else if (PickedShape == Shape.Okrag)
            {
                g.DrawEllipse(pen, MouseDownPoint.X, MouseDownPoint.Y, 0, 0);
            }

            pbImage.Image = ImgTmp;
            IsMouseDown = true;
        }

        private void pbImage_MouseMove_DrawShapes(object sender, MouseEventArgs e)
        {
            if (!IsMouseDown) return;
            MouseEventArgs me = (MouseEventArgs)e;
            Point coords = me.Location;
            pbImage.Image = Img;
            ImgTmp = (Image)Img.Clone();

            Graphics g = Graphics.FromImage(ImgTmp);
            Pen pen = new Pen(Brush, Grubosc);

            if (PickedShape == Shape.Linia)
            {
                g.DrawLine(pen, MouseDownPoint, coords);
            }
            else if (PickedShape == Shape.Prostokat)
            {
                int x = Math.Min(MouseDownPoint.X, coords.X);
                int y = Math.Min(MouseDownPoint.Y, coords.Y);
                int w = Math.Abs(coords.X - MouseDownPoint.X);
                int h = Math.Abs(coords.Y - MouseDownPoint.Y);
                g.DrawRectangle(pen, x, y, w, h);
            }
            else if (PickedShape == Shape.Okrag)
            {
                g.DrawEllipse(pen, MouseDownPoint.X, MouseDownPoint.Y, coords.X - MouseDownPoint.X, coords.Y - MouseDownPoint.Y);
            }

            pbImage.Image = ImgTmp;
        }

        private void pbImage_MouseUp_DrawShapes(object sender, MouseEventArgs e)
        {
            IsMouseDown = false;
            MouseEventArgs me = (MouseEventArgs)e;
            Point coords = me.Location;
            pbImage.Image = Img;
            ImgTmp = (Image)Img.Clone();

            Graphics g = Graphics.FromImage(ImgTmp);
            Pen pen = new Pen(Brush, Grubosc);

            if (PickedShape == Shape.Linia)
            {
                g.DrawLine(pen, MouseDownPoint, coords);
            }
            else if (PickedShape == Shape.Prostokat)
            {
                int x = Math.Min(MouseDownPoint.X, coords.X);
                int y = Math.Min(MouseDownPoint.Y, coords.Y);
                int w = Math.Abs(coords.X - MouseDownPoint.X);
                int h = Math.Abs(coords.Y - MouseDownPoint.Y);
                g.DrawRectangle(pen, x, y, w, h);
            }
            else if (PickedShape == Shape.Okrag)
            {
                g.DrawEllipse(pen, MouseDownPoint.X, MouseDownPoint.Y, coords.X - MouseDownPoint.X, coords.Y - MouseDownPoint.Y);
            }

            pbImage.Image = ImgTmp;
            Img = (Bitmap)ImgTmp.Clone();
            LoadModifiedCharts();
        }

        private void przestrzenieBarwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrzestrzenieBarwForm dlg = new PrzestrzenieBarwForm();
            dlg.ShowDialog();
        }

        private void wyrównanieHistogramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] rValues = R_Values.Clone() as int[];
            int[] gValues = G_Values.Clone() as int[];
            int[] bValues = B_Values.Clone() as int[];
            decimal rValCount = rValues.Count();
            decimal gValCount = gValues.Count();
            decimal bValCount = bValues.Count();
            decimal[] LUTr = new decimal[256];
            decimal[] LUTg = new decimal[256];
            decimal[] LUTb = new decimal[256];
            decimal[] Dr = new decimal[256];
            decimal[] Dg = new decimal[256];
            decimal[] Db = new decimal[256];

            decimal sumR, sumG, sumB;
            decimal iloscR = rValues.Sum();
            decimal iloscG = gValues.Sum();
            decimal iloscB = bValues.Sum();

            for (int i = 0; i < 256; i++)
            {
                sumR = 0;
                sumG = 0;
                sumB = 0;
                for (int j = 0; j <= i; j++)
                {
                    sumR += rValues[j];
                    sumG += gValues[j];
                    sumB += bValues[j];
                }
                Dr[i] = sumR / iloscR;
                Dg[i] = sumG / iloscG;
                Db[i] = sumB / iloscB;
            }

            decimal drZero = Dr.Where(o => o > 0).First();
            decimal dbZero = Db.Where(o => o > 0).First();
            decimal dgZero = Dg.Where(o => o > 0).First();
            for (int i = 0; i < 256; i++)
            {
                LUTr[i] = ((Dr[i] - drZero) / (1.0M - drZero)) * (rValCount - 1.0M);
                LUTg[i] = ((Dg[i] - dgZero) / (1.0M - dgZero)) * (gValCount - 1.0M);
                LUTb[i] = ((Db[i] - dbZero) / (1.0M - dbZero)) * (bValCount - 1.0M);
            }

            Img = Img.Clone() as Bitmap;
            Color color;
            Color newColor;
            if (PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                ColorPalette palette = Img.Palette;
                for (int i = 0; i < palette.Entries.Length; i++)
                {
                    newColor = Color.FromArgb(255, Decimal.ToInt32(LUTr[i]), Decimal.ToInt32(LUTg[i]), Decimal.ToInt32(LUTb[i]));
                    palette.Entries[i] = newColor;
                }
                Img.Palette = palette;
            }
            else
            {
                for (int i = 0; i < Img.Width; i++)
                {
                    for (int j = 0; j < Img.Height; j++)
                    {
                        color = Img.GetPixel(i, j);

                        newColor = Color.FromArgb(255, Decimal.ToInt32(LUTr[color.R]), Decimal.ToInt32(LUTg[color.G]), Decimal.ToInt32(LUTb[color.B]));
                        Img.SetPixel(i, j, newColor);

                    }
                }
            }
            LoadModifiedCharts();
            pbImage.Image = Img;
        }

        private void rozciagniecieHistogramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RozciagniecieHistogramuForm dlg = new RozciagniecieHistogramuForm();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int AValue = dlg.AValue;
                int BValue = dlg.BValue;
                int[] rValues = R_Values.Clone() as int[];
                int[] gValues = G_Values.Clone() as int[];
                int[] bValues = B_Values.Clone() as int[];
                int[] LUTr = new int[256];
                int[] LUTg = new int[256];
                int[] LUTb = new int[256];
                int iRMin = -1, iRMax = -1, iGMin = -1, iGMax = -1, iBMin = -1, iBMax = -1;

                //znalezienie ekstremow
                for (int i = AValue + 1; i < BValue; i++)
                {
                    if (rValues[i] > 0 && iRMin == -1) iRMin = i;
                    if (gValues[i] > 0 && iGMin == -1) iGMin = i;
                    if (bValues[i] > 0 && iBMin == -1) iBMin = i;
                }
                for (int i = BValue - 1; i > AValue; i--)
                {
                    if (rValues[i] > 0 && iRMax == -1) iRMax = i;
                    if (gValues[i] > 0 && iGMax == -1) iGMax = i;
                    if (bValues[i] > 0 && iBMax == -1) iBMax = i;
                }

                //przelicz tablice LUT, tak by rozciagnac histogram
                rozciagnieciePrzeliczLUT((int)255 / (iRMax - iRMin), -iRMin, LUTr);
                rozciagnieciePrzeliczLUT((int)255 / (iGMax - iGMin), -iGMin, LUTg);
                rozciagnieciePrzeliczLUT((int)255 / (iBMax - iBMin), -iBMin, LUTb);

                Img = Img.Clone() as Bitmap;
                Color color;
                Color newColor;
                int r, g, b;

                if (PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    ColorPalette palette = Img.Palette;
                    for (int i = 0; i < Img.Palette.Entries.Length; i++)
                    {
                        if (palette.Entries[i].R <= AValue || palette.Entries[i].R >= BValue)
                            continue;

                        newColor = Color.FromArgb(255, LUTr[i], LUTg[i], LUTb[i]);
                        palette.Entries[i] = newColor;
                    }
                    Img.Palette = palette;
                }
                else
                {
                    for (int i = 0; i < Img.Width; i++)
                    {
                        for (int j = 0; j < Img.Height; j++)
                        {
                            color = Img.GetPixel(i, j);
                            if (color.R < AValue || color.R > BValue) r = color.R;
                            else r = LUTr[color.R];

                            if (color.G < AValue || color.G > BValue) g = color.G;
                            else g = LUTg[color.G];

                            if (color.B < AValue || color.B > BValue) b = color.B;
                            else b = LUTb[color.B];

                            newColor = Color.FromArgb(255, r, g, b);
                            Img.SetPixel(i, j, newColor);
                        }
                    }
                }

                pbImage.Image = Img;
                LoadModifiedCharts();
            }
        }

        #region Filters
        private void rozmywajacy3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{1, 1, 1,},
                                            {1, 1, 1,},
                                            {1, 1, 1,}, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1  1  1" + Environment.NewLine +
                            "1  1  1" + Environment.NewLine +
                            "1  1  1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void rozmywajacy5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{1, 1, 1, 1, 1,},
                                            {1, 1, 1, 1, 1,},
                                            {1, 1, 1, 1, 1,},
                                            {1, 1, 1, 1, 1,},
                                            {1, 1, 1, 1, 1,}, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1  1  1  1  1" + Environment.NewLine +
                            "1  1  1  1  1" + Environment.NewLine +
                            "1  1  1  1  1" + Environment.NewLine +
                            "1  1  1  1  1" + Environment.NewLine +
                            "1  1  1  1  1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void rozmywajacy3x32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{1, 1, 1,},
                                            {1, 2, 1,},
                                            {1, 1, 1,}, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1  1  1" + Environment.NewLine +
                            "1  2  1" + Environment.NewLine +
                            "1  1  1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void rozmywajacyGaussa3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 1, 2, 1, }, 
                                            { 2, 4, 2, }, 
                                            { 1, 2, 1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1  2  1" + Environment.NewLine +
                            "2  4  2" + Environment.NewLine +
                            "1  2  1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void rozmywajacyGaussa5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 2, 04, 05, 04, 2 }, 
                                            { 4, 09, 12, 09, 4 }, 
                                            { 5, 12, 15, 12, 5 },
                                            { 4, 09, 12, 09, 4 },
                                            { 2, 04, 05, 04, 2 }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "2, 04, 05, 04, 2" + Environment.NewLine +
                            "4, 09, 12, 09, 4" + Environment.NewLine +
                            "5, 12, 15, 12, 5" + Environment.NewLine +
                            "4, 09, 12, 09, 4" + Environment.NewLine +
                            "2, 04, 05, 04, 2");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void lAPL1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 0, -1, 0, }, 
                                            { -1, 4, -1, }, 
                                            { 0, -1, 0, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "0, -1, 0" + Environment.NewLine +
                            "-1, 4, -1" + Environment.NewLine +
                            "0, -1, 0");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void lAPL2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, -1, -1, }, 
                                            { -1, 8, -1, }, 
                                            { -1, -1, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1, -1, -1" + Environment.NewLine +
                            "-1,  8, -1" + Environment.NewLine +
                            "-1, -1, -1,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void lAPL3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 1, -2, 1, }, 
                                            { -2, 4, -2, }, 
                                            { 1, -2, 1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            " 1, -2,  1" + Environment.NewLine +
                            "-2,  4, -2" + Environment.NewLine +
                            " 1, -2,  1,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void laplaceaUkośnyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, 0, -1, }, 
                                            { 0, 4, 0, }, 
                                            { -1, 0, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1, 0, -1" + Environment.NewLine +
                            " 0, 4,  0" + Environment.NewLine +
                            "-1, 0, -1,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void laplaceaPoziomyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 0, -1, 0, }, 
                                            { 0, 2, 0, }, 
                                            { 0, -1, 0, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "0, -1, 0" + Environment.NewLine +
                            "0,  2, 0" + Environment.NewLine +
                            "0, -1, 0,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void laplaceaPionowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 0, -1, 0, }, 
                                            { 0, 2, 0, }, 
                                            { 0, -1, 0, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "0,  0,  0" + Environment.NewLine +
                            "-1, 2, -1" + Environment.NewLine +
                            "0,  0,  0,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void sobelaKrPoziomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 1, 2, 1, }, 
                                            { 0, 0, 0, }, 
                                            { -1, -2, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            " 1,  2,  1" + Environment.NewLine +
                            " 0,  0,  0" + Environment.NewLine +
                            "-1, -2, -1,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void sobelaKrPionoweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 1, 0, -1, }, 
                                            { 2, 0, -2, }, 
                                            { 1, 0, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1, 0, -1" + Environment.NewLine +
                            "2, 0, -2" + Environment.NewLine +
                            "1, 0, -1,");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void prewittaKrPoziomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, -1, -1, }, 
                                            { 0, 0, 0, }, 
                                            { 1, 1, 1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1, -1, -1" + Environment.NewLine +
                            " 0,  0,  0" + Environment.NewLine +
                            " 1,  1,  1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void prewittaKrPionoweToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 1, 0, -1, }, 
                                            { 1, 0, -1, }, 
                                            { 1, 0, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1, 0, -1" + Environment.NewLine +
                            "1, 0, -1" + Environment.NewLine +
                            "1, 0, -1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void wykrNarożniki1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, -1, -1, }, 
                                            { 2, 2, 2, }, 
                                            { -1, -1, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1, -1, -1" + Environment.NewLine +
                            " 2,  2,  2" + Environment.NewLine +
                            "-1, -1, -1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void wykrNarożniki2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, 2, -1, }, 
                                            { -1, 2, -1, }, 
                                            { -1, 2, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1, 2, -1" + Environment.NewLine +
                            "-1, 2, -1" + Environment.NewLine +
                            "-1, 2, -1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void wykrNarożniki3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ 1, 1, 1, }, 
                                            { 1, -2, -1, }, 
                                            { 1, -1, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "1,  1, 1" + Environment.NewLine +
                            "1, -2, -1" + Environment.NewLine +
                            "1, -1, -1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void wykrNarożniki4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, 1, 1, }, 
                                            { -1, -2, 1, }, 
                                            { -1, 1, 1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1,  1, 1" + Environment.NewLine +
                            "-1, -2, 1" + Environment.NewLine +
                            "-1,  1, 1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void filtrKuwaharaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }
            Img = GlobalFunctions.KuwaharaFilter(Img);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void medianowy3x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }
            Img = GlobalFunctions.KuwaharaMedianFilter(Img, 3);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void medianowy5x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }
            Img = GlobalFunctions.KuwaharaMedianFilter(Img, 5);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void meanRemovalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{ -1, -1, -1, }, 
                                            { -1,  9, -1, }, 
                                            { -1, -1, -1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            "-1, -1, -1" + Environment.NewLine +
                            "-1,  9, -1" + Environment.NewLine +
                            "-1, -1, -1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void hP1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{  0, -1,  0, }, 
                                            { -1,  5, -1, }, 
                                            {  0, -1,  0, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            " 0, -1,  0" + Environment.NewLine +
                            "-1,  5, -1" + Environment.NewLine +
                            " 0, -1,  0");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void hP2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{  1, -2,  1, }, 
                                            { -2,  5, -2, }, 
                                            {  1, -2,  1, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            " 1, -2,  1" + Environment.NewLine +
                            "-2,  5, -2" + Environment.NewLine +
                            " 1, -2,  1");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void hP3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            double[,] mask = new double[,] {{  0, -1,  0, }, 
                                            { -1, 20, -1, }, 
                                            {  0, -1,  0, }, };

            MessageBox.Show("maska" + Environment.NewLine +
                            " 0, -1,  0" + Environment.NewLine +
                            "-1, 20, -1" + Environment.NewLine +
                            " 0, -1,  0");

            Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void x3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            Img = GlobalFunctions.MedianFilter(Img, 3);
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void x5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }

            Img = GlobalFunctions.MedianFilter(Img, 5
                );
            pbImage.Image = Img;
            LoadModifiedCharts();
        }

        private void filtrKonwolucyjnyZWłasnaMaskaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Img == null)
            {
                MessageBox.Show("Proszę załadować obraz");
                return;
            }
            FilterMaskCreator dlg = new FilterMaskCreator();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                double[,] mask = new double[3, 3];
                mask[0, 0] = dlg.Val00;
                mask[0, 1] = dlg.Val01;
                mask[0, 2] = dlg.Val02;
                mask[1, 0] = dlg.Val10;
                mask[1, 1] = dlg.Val11;
                mask[1, 2] = dlg.Val12;
                mask[2, 0] = dlg.Val20;
                mask[2, 1] = dlg.Val21;
                mask[2, 2] = dlg.Val22;

                Img = GlobalFunctions.ConvolutionFiltering(Img, mask);
                pbImage.Image = Img;
                LoadModifiedCharts();
            }
        }
        #endregion //end Filters

        #region Binaryzacja
        private void binaryzacjaZRęcznieWyznaczonymProgiemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BinaryzacjaZRecznymProgiem dlg = new BinaryzacjaZRecznymProgiem(Img);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Img = dlg.Image;
                    pbImage.Image = Img;
                    LoadModifiedCharts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void binaryzacjaZProgiemOTSUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int prog = GlobalFunctions.GetOTSUThreshold(Img);
                Img = GlobalFunctions.BinarizeImage(Img, prog);
                pbImage.Image = Img;
                LoadModifiedCharts();
                MessageBox.Show("Wyznaczony próg przy pomocy metody OTSU: " + prog);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void binaryzacjaNiblackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BinaryzacjaNiblack dlg = new BinaryzacjaNiblack(Img);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Img = dlg.Image;
                    pbImage.Image = Img;
                    LoadModifiedCharts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void pbImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (DrawingMode == DrawingModes.Bezier)
            {
                if (!all_Set && e.Button == MouseButtons.Left)
                {
                    if (!startPoint_Set)
                    {
                        apt[0] = new Point(e.X, e.Y);
                        startPoint_Set = true;
                        MessageBox.Show("Proszę wybrać punkt końcowy");
                        pbImage.Refresh();
                    }
                    else if (!endPoint_Set)
                    {
                        apt[3] = new Point(e.X, e.Y);
                        endPoint_Set = true;
                        MessageBox.Show("Proszę wybrać pierwszy punkt kontrolny");
                        pbImage.Refresh();
                    }
                    else if (!controlPoint1_Set)
                    {
                        apt[1] = new Point(e.X, e.Y);
                        controlPoint1_Set = true;
                        MessageBox.Show("Proszę wybrać drugi punkt kontrolny");
                        pbImage.Refresh();
                    }
                    else if (!controlPoint2_Set)
                    {
                        apt[2] = new Point(e.X, e.Y);
                        controlPoint2_Set = true;
                        all_Set = true;
                        MessageBox.Show("Wszystkie punkty ustawione. LPM - modyfikacja 1PK, PPM - modyfikacja 2PK.");
                        pbImage.Refresh();
                    }
                }
            }
        }

        private int isPointClicked(int x, int y)
        {
            for (int i = 0; i < apt.Length; i++)
            {
                if (x >= apt[i].X - 5 && x <= apt[i].X + 5 && y >= apt[i].Y - 5 && y <= apt[i].Y + 5)
                {
                    return i;
                }
            }
            return -1;
        }

        private void pbImage_Paint(object sender, PaintEventArgs e)
        {
            if (DrawingMode == DrawingModes.Bezier)
            {
                if (all_Set)
                {
                    var tmp = new Point[apt.Length];
                    for (int i = 0; i < apt.Length; i++)
                    {
                        tmp[i] = new Point(apt[i].X, apt[i].Y);
                    }
                    GlobalFunctions.DrawBezierSpline(e.Graphics, Pens.Red, tmp);
                }
                if (startPoint_Set) e.Graphics.FillEllipse(startPointBrush, apt[0].X - 4, apt[0].Y - 4, 8, 8);
                if (endPoint_Set) e.Graphics.FillEllipse(endPointBrush, apt[3].X - 4, apt[3].Y - 4, 8, 8);
                if (controlPoint1_Set) e.Graphics.FillEllipse(ctrlPt1Brush, apt[1].X - 4, apt[1].Y - 4, 8, 8);
                if (controlPoint2_Set) e.Graphics.FillEllipse(ctrlPt2Brush, apt[2].X - 4, apt[2].Y - 4, 8, 8);
            }
        }

        private void pictureBox1_MouseMove_Bezier(object sender, MouseEventArgs mea)
        {
            if (all_Set)
            {
                if (mea.Button == MouseButtons.Left && MovingPoint != -1)
                {
                    apt[MovingPoint] = new Point(mea.X, mea.Y);
                    pbImage.Refresh();
                }
            }
        }

        private void pictureBox1_MouseDown_Bezier(object sender, MouseEventArgs mea)
        {
            if (all_Set && mea.Button == MouseButtons.Left)
            {
                int index = isPointClicked(mea.X, mea.Y);
                if (index != -1)
                {
                    MovingPoint = index;
                    Cursor.Position = PointToScreen(apt[index]);
                }
                else MovingPoint = -1;
            }
        }

        private void przekształcenia2DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Przeksztalcenia2D dlg = new Przeksztalcenia2D();
            dlg.ShowDialog();
        }

        #region Bezier

        #endregion
    }
}
