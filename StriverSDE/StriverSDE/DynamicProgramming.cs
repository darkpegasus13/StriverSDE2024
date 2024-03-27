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
            for(int i = 0; i < n; i++)
            {
                //prefix/suffix has become zero by encountering zero so excluding it
                if (prefix == 0)
                    prefix = 1;
                if (suffix == 0)
                    suffix = 1;
                prefix = prefix * nums[i];
                suffix = suffix * nums[n - i - 1];
                max = Math.Max(max,Math.Max(prefix,suffix));
            }
            return max;
        }
        #endregion

        #region Longest Increasing Subsequence

        //Naive Solution S=>O(2^n) and T=>O(2^n)
        //generate all subsequences and then check the 
        //LIS

        //Optimal Solution S=>O(N*N)+O(N) and T=>O(N*N) 
        //Using DP
        public int LengthOfLIS(int[] nums)
        {
            int n = nums.Length;
            int[][] dp = new int[n][];

            for (int i = 0; i < n; i++)
            {
                dp[i] = Enumerable.Repeat(-1, n + 1).ToArray();
            }
            return GetAns(nums, n, 0, -1, dp);
        }

        public int GetAns(int[] arr,int n,int ind,int prev_indx,int[][] dp)
        {
            if (ind == n)
                return 0;
            if (dp[ind][prev_indx + 1] != -1)
                return dp[ind][prev_indx + 1];
            int notTake = 0 + GetAns(arr, n, ind + 1, prev_indx, dp);
            int take = 0;
            if (prev_indx == -1 || arr[ind] > arr[prev_indx])
                take = 1 + GetAns(arr,n,ind+1,ind,dp);
            dp[ind][prev_indx + 1] = Math.Max(notTake,take);
            return dp[ind][prev_indx + 1];
        }

        //A good recursive solution written by my intuition but for DP 
        //maybe not compatible
        public int LengthOfLISHelper(int[] nums, int indx, List<int> curr)
        {
            if (indx >= nums.Length)
                return curr.Count;
            if (curr.Count == 0 || curr.Last() < nums[indx])
            {
                curr.Add(nums[indx]);
                var take = LengthOfLISHelper(nums, indx + 1, curr);
                curr.RemoveAt(curr.Count - 1);
                var notTake = LengthOfLISHelper(nums, indx + 1, curr);
                return Math.Max(take, notTake);
            }
            else
                return LengthOfLISHelper(nums, indx + 1, curr);
        }

        #endregion

        #region Longest Common Subsequence

        //Naive Approach S=>O(2^N) and T=>O(2^N)
        //generate all substring for both and then find the matching
        //largest substring

        //Better Approach S=>O(N) and T=>O(2^(n+m))
        //using recursion
        public int LongestCommonSubsequence(string text1, string text2)
        {

        }

        public int LongestCommonSubsequenceHelper(string text1, string text2, int indx1,int indx2)
        {
            if (indx1 < 0 || indx2 < 0)
                return 0;
            if (text1[indx1] == text2[indx2])
                return 1 + LongestCommonSubsequenceHelper(text1, text2, indx1 - 1, indx2 - 1);
            else
            {
                return Math.Max(LongestCommonSubsequenceHelper(text1, text2, indx1, indx2 - 1),
                    LongestCommonSubsequenceHelper(text1, text2, indx1 - 1, indx2));
            }
        }

        //Better Approach S=>O(N)+O(N*M) and T=>O(N*M)
        //using dp memoization

        public int lcs(int n, int m, string s1, string s2)
        {
            //Your code here
            var memoizationMatrix = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                memoizationMatrix[i] = Enumerable.Repeat(-1, m + 1).ToArray();
            }
            return this.LCSDP(s1, s2, n, m, memoizationMatrix);
        }

        public int LCSDP(string a, string b, int m, int n, int[][] memo)
        {
            if (m == 0 || n == 0)
                return 0;
            if (memo[m - 1][n - 1] == -1)
            {
                int res;
                if (a[m - 1] == b[n - 1])
                    res = 1 + LCSDP(a, b, m - 1, n - 1, memo);
                else
                    res = Math.Max(this.LCSDP(a, b, m - 1, n, memo), this.LCSDP(a, b, m, n - 1, memo));
                memo[m - 1][n - 1] = res;
            }
            return memo[m - 1][n - 1];
        }

        //Better Approach S=>O(N)+O(N*M) and T=>O(N*M)
        //using dp tabulation

        public int LCSDPTab(string text1, string text2)
        {
            int n = text1.Length;
            int m = text2.Length;
            var memoizationMatrix = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                memoizationMatrix[i] = Enumerable.Repeat(-1, m + 1).ToArray();
            }
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (text1[i - 1] == text2[j - 1])
                        memoizationMatrix[i][j] = 1 + memoizationMatrix[i - 1][j - 1];
                    else
                        memoizationMatrix[i][j] = Math.Max(memoizationMatrix[i - 1][j], memoizationMatrix[i][j - 1]);
                }
            }
            return memoizationMatrix[n][m];

        }
        //Better Approach S=>O(N)+O(N+M) and T=>O(N*M)
        //using dp tabulation and doing some tabulation
        //we need only two rows curr and prev

        #endregion
    }
}
