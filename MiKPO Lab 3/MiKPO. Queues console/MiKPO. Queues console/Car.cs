using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiKPO.Queues_console
{
    class Car
    {
        public string Name;
        public double tIn;
        public double tOut;
        public Car(string name, double tIn, double tOut = 0)
        {
            Name = name;
            this.tIn = tIn;
            this.tOut = tOut;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
