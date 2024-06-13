using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class DynamicProgramming
    {
        #region Maximum Product SubArray

        //Naive Solution S=>O(1) and T=>O(N^3)
        //using 3 nested loops 

        //Better Solution S=>O(1) and T=>O(N^2)
        //using 2 loops 

        //Optimal Approach 1
        //by taking product of whole array and handle -ve and zeros
        //as the greatest sum is obtained by multiply every element in array if we can handle 
        //-ve  and zeros either the prefix will be the answer of the suffix when we exclude a -ve/0

        public int MaxProduct(int[] nums)
        {
            int n = nums.Length;
            int prefix = 1;
            int suffix = 1;
            int max = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                //prefix/suffix has become zero by encountering zero so excluding it
                if (prefix == 0)
                    prefix = 1;
                if (suffix == 0)
                    suffix = 1;
                prefix = prefix * nums[i];
                suffix = suffix * nums[n - i - 1];
                max = Math.Max(max, Math.Max(prefix, suffix));
            }
            return max;
        }
        #endregion

        #region Maximum Sum increasing sequence
        public int maxSumIS(int[] arr, int n)
        {
            return MaxSumHelper(arr, n);
        }

        //Naive Solution S=>O(N) and T=>O(2^N)
        //using recursion
        public int MaxSumHelper(int[] arr, int n, int sum = 0, int indx = 0, int prev = int.MinValue)
        {
            if (indx == n)
                return sum;
            //considering indx
            if (arr[indx] > prev)
                return Math.Max(MaxSumHelper(arr, n, sum + arr[indx], indx + 1, arr[indx]),
                    MaxSumHelper(arr, n, sum, indx + 1, prev));
            //not considering indx
            else
                return MaxSumHelper(arr, n, sum, indx + 1, prev);
        }

        //Better using DP 
        //Memoization refer aditya verma videos



        #endregion

        #region Word Break

        public bool WordBreak(string s, IList<string> wordDict)
        {
            int n = s.Length;
            int[][] tab = new int[n][];
            for (int i = 0; i < n; i++)
            {
                tab[i] = Enumerable.Repeat(-1, n).ToArray();
            }
            MCMRecur(s, 0, s.Length - 1, wordDict, tab);
            return tab[0][n - 1] == 0 ? false : true;
        }
        public int MCMRecur(string s, int i, int j, IList<string> words, int[][] tab)
        {
            if (i > j)
                return 0;
            if (tab[i][j] != -1)
                return tab[i][j];
            var temp = s.Substring(i, j - i + 1);
            if (words.Contains(temp))
                return tab[i][j] = 1;
            for (int k = i; k <= j - 1; k++)
            {
                int left = MCMRecur(s, i, k, words, tab);
                int right = MCMRecur(s, k + 1, j, words, tab);
                int tempAns = left + right == 2 ? 1 : 0;
                if (tempAns == 1)
                    return tab[i][j] = 1;
            }
            return tab[i][j] = 0;
        }

        #endregion
    }
}
