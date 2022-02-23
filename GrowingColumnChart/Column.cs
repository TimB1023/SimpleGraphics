using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowingColumnChart
{
    class Column
    {
        public double Width { get; set; }
        public int Count { get; set; }
        public double BottomOfBin { get; set; }
        public double TopOfBin { get; set; }
        public int BinNumber { get; set; }


        Random rand = new Random();
    }
}
