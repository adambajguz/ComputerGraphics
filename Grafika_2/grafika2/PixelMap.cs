using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;
using SysColor = System.Drawing.Color;
using SysRectangle = System.Drawing.Rectangle;

namespace grafika2
{
    public class PPMReader
    {
        private byte[] imageData;

        #region Properties
        public string FileType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Depth { get; set; }
        public List<Pixel> Pixels { get; set; }
        public Bitmap BitMap { get; set; }
        #endregion //end Properties

        public PPMReader(string filename)
        {
            if (File.Exists(filename))
            {
                Pixels = new List<Pixel>();
                //LoadImage(filename);
                FileStream stream = new FileStream(filename, FileMode.Open);
                this.LoadImgBinary(stream);
                stream.Close();
            }
            else throw new Exception("Plik nie istnieje.");
        }

        

        private void LoadImage(string filename)
        {
            string line = null;
            using (StreamReader sr = new StreamReader(filename))
            {
                FileType = sr.ReadLine();

                //zczytaj wszystkie komentarze
                line = sr.ReadLine();
                while (line.StartsWith("#"))
                {
                    line = sr.ReadLine();
                }

                //rozmiary obrazka
                var size = line.Split(' ');
                Width = int.Parse(size[0]);
                Height = int.Parse(size[1]);

                //glebia w bitach
                Depth = int.Parse(sr.ReadLine());

                if (FileType == "P3")
                {
                    LoadP3(sr);
                }
                else if (FileType == "P6")
                {
                    Bitmap result = new Bitmap(Width, Height);
                    long pos = sr.BaseStream.Position;
                    sr.Close();
                    FileStream stream = new FileStream(filename, FileMode.Open);
                    BinaryReader br = new BinaryReader(stream);
                    br.BaseStream.Position = pos;
                    var pixel = br.ReadBytes(3);
                    do
                    {
                        Pixels.Add(new Pixel(pixel[0], pixel[1], pixel[2]));
                        pixel = br.ReadBytes(3);
                    } while ((br.BaseStream.Length - br.BaseStream.Position) > 0);

                    for (int i = 0; i < Height; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            Color color = Color.FromArgb(255, Pixels[i * j].R, Pixels[i * j].G, Pixels[i * j].B);
                            result.SetPixel(j, i, color);
                        }
                    }
                    BitMap = result;
                }
            }
        }

        private Bitmap LoadP3(StreamReader sr)
        {
            Bitmap result = new Bitmap(Width, Height);
            string line = null;
            Color color = new Color();

            for (int i = 0; i < Height; i++)
            {
                line = sr.ReadLine();
                int[] rowPixels = line.Split(' ').Where(o => !String.IsNullOrEmpty(o)).Select(int.Parse).ToArray();
                if (Depth != 255) rowPixels = NormalizeLine(rowPixels);

                for (int j = 0; j < rowPixels.Length; j += 3)
                {
                    Pixels.Add(new Pixel(rowPixels[j], rowPixels[j + 1], rowPixels[j + 2]));
                }
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    color = Color.FromArgb(255, Pixels[i * j].R, Pixels[i * j].G, Pixels[i * j].B);
                    result.SetPixel(j, i, color);
                }
            }

            BitMap = result;
            return result;
        }

        private int[] NormalizeLine(int[] src)
        {
            int[] result = new int[src.Length];

            for (int i = 0; i < src.Length; i++)
            {
                decimal diff = src[i] / Convert.ToDecimal(Depth);
                decimal val = diff * 255M;

                result[i] = Convert.ToInt32(val);
            }

            return result;
        }

        private void LoadImgBinary(FileStream stream)
        {
            BinaryReader binReader = new BinaryReader(stream);

            int headerItemCount = 0;
            while (headerItemCount < 4)
            {
                char nextChar = (char)binReader.PeekChar();
                if (nextChar == '#')
                {
                    while (binReader.ReadChar() != '\n') ;
                }
                else if (Char.IsWhiteSpace(nextChar))
                {
                    binReader.ReadChar();
                }
                else
                {
                    switch (headerItemCount)
                    {
                        case 0: //zczytaj typ pliku
                            char[] chars = binReader.ReadChars(2);
                            this.FileType = chars[0].ToString() + chars[1].ToString();
                            headerItemCount++;
                            break;
                        case 1: //liczba kolumn
                            this.Width = ReadValue(binReader);
                            headerItemCount++;
                            break;
                        case 2: //liczba wierszy
                            this.Height = ReadValue(binReader);
                            headerItemCount++;
                            break;
                        case 3: //max wartosc
                            this.Depth = ReadValue(binReader);
                            headerItemCount++;
                            break;
                        default:
                            throw new Exception("[EX_1] Podczas wykonywania operacji wystąpił błąd.");
                    }
                }
            }

            if (FileType == "P3")
            {
                binReader.ReadChar();
                int charsLeft = (int)(binReader.BaseStream.Length - binReader.BaseStream.Position);
                char[] charData = binReader.ReadChars(charsLeft);
                string data = new string(charData);
                data = data.Replace(System.Environment.NewLine, " ");

                int[] pixels = data.Split(' ').Where(o => !String.IsNullOrEmpty(o)).Select(int.Parse).ToArray();

                if (Depth != 255) pixels = NormalizeLine(pixels);

                for (int j = 0; j < pixels.Length; j += 3)
                {
                    Pixels.Add(new Pixel(pixels[j], pixels[j + 1], pixels[j + 2]));
                }

                BitMap = new Bitmap(Width, Height);

                int pIndex = 0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        Color color = Color.FromArgb(255, Pixels[pIndex].R, Pixels[pIndex].G, Pixels[pIndex].B);
                        BitMap.SetPixel(x, y, color);
                        pIndex++;
                    }
                }
                
            }
            else if(FileType == "P6")
            {
                int bytesLeft = (int)(binReader.BaseStream.Length - binReader.BaseStream.Position);
                this.imageData = binReader.ReadBytes(bytesLeft);
                ConvertBGRToRGB();
                if (Depth != 255)
                {
                    imageData = NormalizeByteArray(imageData);
                }


                Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format24bppRgb);
                SysColor color = new SysColor();
                int red, green, blue;
                int index;

                for (int x = 0; x < this.Width; x++)
                {
                    for (int y = 0; y < this.Height; y++)
                    {
                        index = x + y * this.Width;
                        index = 3 * index;
                        blue = (int)this.imageData[index];
                        green = (int)this.imageData[index + 1];
                        red = (int)this.imageData[index + 2];
                        color = SysColor.FromArgb(red, green, blue);
                        bitmap.SetPixel(x, y, color);
                    }
                }
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
                BitMap = bitmap;
            }
            
        }

        private int ReadValue(BinaryReader binReader)
        {
            string value = string.Empty;
            while (!Char.IsWhiteSpace((char)binReader.PeekChar()))
            {
                value += binReader.ReadChar().ToString();
            }
            binReader.ReadByte();
            return int.Parse(value);
        }

        private void ConvertBGRToRGB()
        {
            byte[] tempData = new byte[this.imageData.Length];
            for (int i = 0; i < this.imageData.Length; i++)
            {
                tempData[i] = this.imageData[this.imageData.Length - 1 - i];
            }
            this.imageData = tempData;
        }

        private byte[] NormalizeByteArray(byte[] src)
        {
            byte[] result = new byte[src.Length];

            for (int i = 0; i < src.Length; i++)
            {
                decimal diff = src[i] / Convert.ToDecimal(Depth);
                decimal val = diff * 255M;

                result[i] = Convert.ToByte(val);
            }

            return result;
        }
    }
}
