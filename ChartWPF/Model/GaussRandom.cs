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
}
