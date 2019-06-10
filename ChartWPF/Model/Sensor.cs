using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Geared;

namespace ChartWPF.Model
{
    public class Sensor
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        const int valuePerSecond = 100; 
        const int keepSecond = 60;
        const int keepRecords = keepSecond* valuePerSecond;

        private GaussRandom _r;

        public GearedValues<double> Values { get; set; }        
        public Sensor()
        {
            Values = new GearedValues<double>();
            _r = new GaussRandom();
           
            Task.Factory.StartNew(GetNewValue);
        }

        public void GetNewValue()
        {
            while (true)
            {
                try
                {
                    double trend = _r.Next();
                    Thread.Sleep(1000 / valuePerSecond);
                    var first = Values.DefaultIfEmpty(0).FirstOrDefault();
                    if (Values.Count > keepRecords - 1) Values.Remove(first);
                    if (Values.Count < keepRecords) Values.Add(trend);
                    //Console.WriteLine(trend);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Sensor");
                }
              
            }
        }        

    }
}
