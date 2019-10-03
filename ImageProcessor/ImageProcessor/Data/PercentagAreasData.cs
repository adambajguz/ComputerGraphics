namespace ImageProcessor.Data
{
    public class PercentagAreasData
    {
        public int DetectedPixels { get; }
        public int TotalPixels { get; }

        public double Percentage { get; }

        public PercentagAreasData(int detectedPixels, int totalPixels)
        {
            DetectedPixels = detectedPixels;
            TotalPixels = totalPixels;
            Percentage = (double)detectedPixels / totalPixels;
        }
    }
}
