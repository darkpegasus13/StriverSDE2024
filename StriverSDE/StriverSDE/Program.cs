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
            #region Array
            //Console.WriteLine("df");
            //ArrayComp arr = new ArrayComp();
            //arr.MaxSubArrayPrintRange(new int [] { 1, 2, 3, -2, 5 });
            #endregion

            #region LL
            LinkedList ll = new LinkedList(0);
            ll.next = new LinkedList(1);
            ll.next.next = new LinkedList(2);
            ll.RotateByK(ll,4);
            #endregion

            #region greedy
            Greedy gr = new Greedy();
            gr.findPlatform(new int[] { 0900, 0940, 0950, 1100, 1500, 1800 },new int[] { 0910, 1200, 1120, 1130, 1900, 2000 },6);
            #endregion
            Console.ReadLine();
        }
    }
}
