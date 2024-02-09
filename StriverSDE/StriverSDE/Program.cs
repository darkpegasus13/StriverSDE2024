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
            LinkedList ll = new LinkedList(1);
            ll.next = new LinkedList(3);
            ll.next.next = new LinkedList(4);
            ll.RemoveNthFromEndNaive(ll,3);
            #endregion
            Console.ReadLine();
        }
    }
}
