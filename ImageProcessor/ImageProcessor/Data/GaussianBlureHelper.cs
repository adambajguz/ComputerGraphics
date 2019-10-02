using System;

namespace ImageProcessor.Data
{
    public static class GaussianBlureHelper
    {
        public static double[,] CalculateKernel(int kernelSize, double sd)
        {
            if ((kernelSize & 1) == 0)
            {
                throw new InvalidOperationException("Kernel size must be odd!");
            }

            int foff = (kernelSize - 1) >> 1;

            double const1 = 2 * sd * sd;
            double const2 = 1d / (const1 * Math.PI);

            double[,] kernel = new double[kernelSize, kernelSize];
            double kernelSum = 0;
            for (int y = -foff; y <= foff; ++y)
            {
                for (int x = -foff; x <= foff; ++x)
                {
                    double distance = ((y * y) + (x * x)) / const1;

                    kernel[y + foff, x + foff] = const2 * Math.Exp(-distance);
                    kernelSum += kernel[y + foff, x + foff];
                }
            }

            ArrayUtil.MultiplyEach(kernel, kernel, 1d / kernelSum);

            return kernel;
        }
    }
}
