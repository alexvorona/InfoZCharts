using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;

namespace ChartWPF.Model
{
    public class Graph
    {     
        public Graph(int id, string name, Sensor sensor)
        {
            Id = id;
            Name = name;
            Sensor = sensor;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Sensor Sensor { get; set; }
    }
}
