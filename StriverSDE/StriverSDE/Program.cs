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
            //arr.pow(2,-2);
            //arr.SubarraySum(new int[]{ 9,-3,3,-1,6,-5},0);
            //arr.Merge(new int[] { 5,6,4,0,0,0},3,new int[] {3,2,4 },3);
            //arr.FourSum(new int[] {1, 0, -1, 0, -2, 2}, 0);
            arr.countDistinct(new int[] { 1, 2, 1, 3, 4, 2, 3 }, 7, 4);
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

            StackandQueue stq = new StackandQueue();
            //Stack<int> st = new Stack<int>();
            //st.Push(11);
            //st.Push(9);
            //st.Push(97);
            //stq.sortStack(st);
            //LFUCache lf = new LFUCache(2);
            //lf.Put(1,10);
            //lf.Put(2, 11);
            //lf.Get(1);
            //lf.Put(3, 12);
            //lf.Get(2);
            //stq.celebrityOptimal(new int[3, 3] { {0, 1 ,0 }, {0, 0 ,0 }, {0, 1, 0 } }, 3);
            //stq.MaxSlidingWindow(new int[] {1, 3, -1, -3, 5, 3, 6, 7 },3);

            #endregion

            #region Recursion

            Recursion recur = new Recursion();
            //recur.Permute(new int[]{ 1,2,3});
            //foreach(string s in recur.WordBreak(new List<string>() {"god","is","now","no","where","here" },
            //    "godisnowherenowhere"))
            //{
            //    Console.WriteLine(s);
            //}
            //var res = recur.SubsetsWithDup(new int[] {1,2,2});
            //foreach(var i in res)
            //{
            //    foreach(var j in i)
            //    {
            //        Console.Write("{0},",j);
            //    }
            //    Console.WriteLine();
            //}
            //recur.Solve(new char[][] { new char[]{ '3', '.','6', '5', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //       new char[]{ '3', '0','6', '.', '0','8', '4', '0', '0' },
            //});
            recur.FloodFill(new int[][]{new int[]{ 0,0, 0 },
            new int[]{ 0, 0, 0 } },0,0,0);

            #endregion

            #region Trie

            Trie t = new Trie();
            //var ans=t.PowerSet("abc"); 
            //foreach(var i in ans)
            //{
            //    Console.WriteLine(i);
            //}
            t.FindMaximumXOR(new int[] { 3, 10, 5, 25, 2, 8 });
            #endregion

            #region String
            Strings s = new Strings();
            //s.LongestPalindromeSubstring("babad");
            s.CompareVersionOptimal("1.01","1.1");
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

            #region Binary Tree

            //KthLargest k = new KthLargest(3, new int[] { 4, 5, 8, 2 });
            //var n = new int[] {3, 5, 10, 9, 4};
            //foreach(int i in n)
            //{
            //    Console.WriteLine(k.Add(i));
            //}
            //Binary_Tree bt = new Binary_Tree();
            //TreeNode tree = new TreeNode(1);
            //tree.left = new TreeNode(2);
            //tree.right = new TreeNode(3);
            //tree.right.left = new TreeNode(4);
            //tree.right.right = new TreeNode(5);
            ////bt.Flatten(tree);
            //var ans = bt.serialize(tree);
            //bt.deserialize(ans);
            #endregion

            #region dp

            DP_AdityaVerma dp = new DP_AdityaVerma();
            //dp.FindTargetSumWays(new int[] {100,100},-400);
            dp.PrintshortestCommonSupersequenceTab("cab", "abac",3,4);
            dp.SuperEggDrop(1,3);
            #endregion
            Console.ReadLine();
        }


    }
}
