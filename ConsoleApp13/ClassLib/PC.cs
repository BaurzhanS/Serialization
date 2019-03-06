using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    [Serializable]
    public class PC
    {
        public string Mark { get; set; }
        public string SerialNum { get; set; }
        public string Model { get; set; }
        public int Price { get; set; }

        public PC()
        {

        }
        public PC(string mark, string serialNum, string model)
        {
            Mark = mark;
            SerialNum = serialNum;
            Model = model;
        }

        public void TurOn()
        {
            Console.WriteLine("PC {0} turnon", SerialNum);
        }
        public void TurOff()
        {
            Console.WriteLine("PC {0} turnoff", SerialNum);

        }
        public void Refresh()
        {
            Console.WriteLine("PC {0} refresh", SerialNum);

        }




    }
}
