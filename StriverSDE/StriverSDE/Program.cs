using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StriverSDE.Heap;

namespace StriverSDE
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Array
            ArrayComp arr = new ArrayComp();
            //arr.MaxSubArrayPrintRange(new int [] { 1, 2, 3, -2, 5 });
            arr.SubarraySum(new int[]{ 9,-3,3,-1,6,-5},0);
            #endregion

            #region LL
            //LinkedList ll = new LinkedList(0);
            //ll.next = new LinkedList(1);
            //ll.next.next = new LinkedList(2);
            //ll.RotateByK(ll,4);
            //ll.IsPalindrome();
            #endregion

            #region greedy
            //Greedy gr = new Greedy();
            //gr.findPlatform(new int[] { 0900, 0940, 0950, 1100, 1500, 1800 },new int[] { 0910, 1200, 1120, 1130, 1900, 2000 },6);
            #endregion

            #region Stack and Queue

            //StackandQueue stq = new StackandQueue();
            //Stack<int> st = new Stack<int>();
            //st.Push(11);
            //st.Push(9);
            //st.Push(97);
            //stq.sortStack(st);

            #endregion

            #region Recursion

            Recursion recur = new Recursion();
            recur.Permute(new int[]{ 1,2,3});
            //var res = recur.SubsetsWithDup(new int[] {1,2,2});
            //foreach(var i in res)
            //{
            //    foreach(var j in i)
            //    {
            //        Console.Write("{0},",j);
            //    }
            //    Console.WriteLine();
            //}

            #endregion

            #region Trie

            //Trie t = new Trie();
            //var ans=t.PowerSet("abc"); 
            //foreach(var i in ans)
            //{
            //    Console.WriteLine(i);
            //}
            #endregion

            #region String
            //Strings s = new Strings();
            //s.LongestPalindromeSubstring("babad");
            #endregion

            #region Heap
            //Heap hp = new Heap();
            //hp.FindKthLargestOptimal(new int[] { 3, 2, 1, 5, 6, 4 }, 2);
            // MedianFinder mf = new MedianFinder();

            // mf.AddNum(-1);
            // var a = mf.FindMedian();
            // mf.AddNum(-2);
            // var b = mf.FindMedian();
            // mf.AddNum(-3);
            // var c = mf.FindMedian();
            #endregion

            #region DP
            //DynamicProgramming dp = new DynamicProgramming();
            //dp.knapSackDP(4,new int[]{1,2,3},new int[]{4,5,1},3);
            //dp.maxSumIS(new int[] { 1, 101, 2, 3, 100 },5);
            #endregion
            Console.ReadLine();
        }


    }
}
