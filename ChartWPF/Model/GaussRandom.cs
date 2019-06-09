using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace ChartWPF.Model
{
    public class GaussRandom
    {
        private double _mean = 100;
        private double _stdDev = 10;
        private Normal _normalDist;
        public GaussRandom()
        {
            _normalDist = new Normal(_mean, _stdDev);
        }
        public GaussRandom(double mean, double stdDev)
        {
            double _mean = mean;
            double _stdDev = stdDev;
            _normalDist = new Normal(_mean, _stdDev);
        }

        /// <summary>
        /// Box-Muller algorithm
        /// </summary>
        /// <returns></returns>
        public double Next()
        {
            return _normalDist.Sample();
        }       
    }

    public static class Gauss
    {
        public static IEnumerable<double> NormalSequence(double mean, double standardDeviation)
        {
            return MathNet.Numerics.Distributions.Normal.Samples(SystemRandomSource.Default, mean, standardDeviation);
        }
        public static double[] Normal(int length, double mean, double standardDeviation)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            var samples = new double[length];
            MathNet.Numerics.Distributions.Normal.Samples(SystemRandomSource.Default, samples, mean, standardDeviation);
            return samples;
        }
    }
}
