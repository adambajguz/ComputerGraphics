namespace ImageProcessor.Data
{
    public struct HSLColor
    {
        public double H { get; }
        public double S { get; }
        public double L { get; }

        public HSLColor(bool normalized, double h, double s, double l)
        {
            H = normalized ? h * 360 : h;
            S = normalized ? s * 100 : s;
            L = normalized ? l * 100 : l;
        }
    }
}
