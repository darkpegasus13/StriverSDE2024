using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("df");
            ArrayComp arr = new ArrayComp();
            arr.MaxSubArrayPrintRange(new int [] { 1, 2, 3, -2, 5 });
            Console.ReadLine();
        }
    }
}
