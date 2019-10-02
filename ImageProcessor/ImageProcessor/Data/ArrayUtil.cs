using System;

namespace ImageProcessor.Data
{
    public class ArrayUtil
    {
        public static double[,] SumNeighborhoods(double[,] sum, double[,] arr, int neighborhood)
        {
            int n = (neighborhood - 1) / 2;
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            double[] circularBuffer = new double[neighborhood];
            int circularIndex = 0;

            //sums in x direction
            for (int row = 0; row < rows; ++row)
            {
                //start sum
                double sumVal = 0.0;
                for (int i = -n; i <= n; ++i)
                {
                    double val = 0.0;
                    if (i >= 0) val = arr[row, i];
                    sumVal += val;
                    circularBuffer[circularIndex] = val;
                    circularIndex = (circularIndex + 1) % neighborhood;
                }

                for (int col = 0; col < cols; ++col)
                {
                    double val = 0.0;
                    if (col + n + 1 < cols) val = arr[row, col + n + 1];
                    sum[row, col] = sumVal;
                    sumVal -= circularBuffer[circularIndex];
                    sumVal += val;
                    circularBuffer[circularIndex] = val;
                    circularIndex = (circularIndex + 1) % neighborhood;
                }
            }

            //sums of sums in y direction
            for (int col = 0; col < cols; ++col)
            {
                //start sum
                double sumVal = 0.0;
                for (int i = -n; i <= n; ++i)
                {
                    double val = 0.0;
                    if (i >= 0) val = sum[i, col];
                    sumVal += val;
                    circularBuffer[circularIndex] = val;
                    circularIndex = (circularIndex + 1) % neighborhood;
                }

                for (int row = 0; row < rows; ++row)
                {
                    double val = 0.0;
                    if (row + n + 1 < rows) val = sum[row + n + 1, col];
                    sum[row, col] = sumVal;
                    sumVal -= circularBuffer[circularIndex];
                    sumVal += val;
                    circularBuffer[circularIndex] = val;
                    circularIndex = (circularIndex + 1) % neighborhood;
                }
            }
            return sum;
        }

        public static double[,] SquareEach(double[,] squares, double[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    double val = arr[row, col];
                    squares[row, col] = val * val;
                }
            }
            return squares;
        }


        public static double[,] SquareRootEach(double[,] squareRoots, double[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    squareRoots[row, col] = Math.Sqrt(arr[row, col]);
                }
            }
            return squareRoots;
        }

        public static double[,] MultiplyEach(double[,] products, double[,] arr, double val)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    products[row, col] = arr[row, col] * val;
                }
            }
            return products;
        }

        public static double[,] Add(double[,] a, double[,] b)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);
            double[,] target = new double[rows, cols];
            return Add(target, a, b);
        }

        public static double[,] Add(double[,] sum, double[,] a, double[,] b)
        {
            int rows = a.GetLength(0);
            int cols = b.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    sum[row, col] = a[row, col] + b[row, col];
                }
            }
            return sum;
        }

        public static double[,] Subtract(double[,] difference, double[,] a, double[,] b)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);

            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    difference[row, col] = a[row, col] - b[row, col];
                }
            }
            return difference;
        }

        public static double[,] MeanNeighborhood(double[,] arr, int neighborhood)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            double[,] target = new double[rows, cols];
            return MeanNeighborhood(target, arr, neighborhood);
        }

        public static double[,] MeanNeighborhood(double[,] target, double[,] arr, int neighborhood)
        {
            int count = neighborhood * neighborhood;
            double[,] sums = SumNeighborhoods(target, arr, neighborhood);
            return MultiplyEach(target, sums, 1.0 / count);
        }

        public static double[,] StdevNeighborhood(double[,] arr, int neighborhood)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            double[,] target = new double[rows, cols]; //new array
            return StdevNeighborhood(target, arr, neighborhood);
        }

        public static double[,] StdevNeighborhood(double[,] target, double[,] arr, int neighborhood)
        {
            //See the 'Rapid calculation methods' in the 'Standard Deviation' 
            //Wikipedia entry for this formula.
            int count = neighborhood * neighborhood;

            double[,] meansSquared = MeanNeighborhood(arr, neighborhood); //new array
            SquareEach(meansSquared, meansSquared);

            double[,] squaresMean = SquareEach(target, arr);
            squaresMean = MeanNeighborhood(target, squaresMean, neighborhood);

            Subtract(target, squaresMean, meansSquared);
            SquareRootEach(target, target);
            return target;
        }

        public static double[,] ToDoubleArray(short[,] arr)
        {
            int rows = arr.GetLength(0);
            int cols = arr.GetLength(1);

            double[,] outArr = new double[rows, cols];
            for (int row = 0; row < rows; ++row)
            {
                for (int col = 0; col < cols; ++col)
                {
                    outArr[row, col] = (double)arr[row, col];
                }
            }
            return outArr;
        }
    }
}
