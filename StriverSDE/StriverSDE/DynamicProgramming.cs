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

        public int GetAns(int[] arr, int n, int ind, int prev_indx, int[][] dp)
        {
            if (ind == n)
                return 0;
            if (dp[ind][prev_indx + 1] != -1)
                return dp[ind][prev_indx + 1];
            int notTake = 0 + GetAns(arr, n, ind + 1, prev_indx, dp);
            int take = 0;
            if (prev_indx == -1 || arr[ind] > arr[prev_indx])
                take = 1 + GetAns(arr, n, ind + 1, ind, dp);
            dp[ind][prev_indx + 1] = Math.Max(notTake, take);
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
        //public int LongestCommonSubsequence(string text1, string text2)
        //{

        //}

        public int LongestCommonSubsequenceHelper(string text1, string text2, int indx1, int indx2)
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

        #region 0-1 Knapsack

        //Naive Approach S=>O(1) and T=>O(NlogN+N)
        //take greedy approach but wont work for every case

        //Better Approach
        //using recursion
        public int KnapsackHelper(int ind, int W, int n, int[] weightArr, int[] valueArr, int V)
        {
            if (ind == n)
                return V;
            if (weightArr[ind] <= W)
                return Math.Max(KnapsackHelper(ind + 1, W - weightArr[ind], n, weightArr, valueArr, V + valueArr[ind]),
                KnapsackHelper(ind + 1, W, n, weightArr, valueArr, V));
            else
                return KnapsackHelper(ind + 1, W, n, weightArr, valueArr, V);
        }

        //Optimal Approach
        //using DP memoization
        //please check solution as DP is not working recursive solution 
        //worked

        public int knapSackDP(int W, int[] wt, int[] val, int n)
        {
            //Your code here
            var memoizationMatrix = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                memoizationMatrix[i] = Enumerable.Repeat(-1, W + 1).ToArray();
            }
            return KnapsackHelperDP(0, W, n, wt, val, 0, memoizationMatrix);
        }
        public int KnapsackHelperDP(int ind, int W, int n, int[] weightArr, int[] valueArr, int V, int[][] matrix)
        {
            if (ind == n)
                return matrix[ind][W] = V;
            if (weightArr[ind] <= W)
            {
                return Math.Max(matrix[ind + 1][W] == -1 ?
                KnapsackHelperDP(ind + 1, W - weightArr[ind], n, weightArr, valueArr, V + valueArr[ind], matrix)
                : matrix[ind + 1][W - weightArr[ind]],
                matrix[ind + 1][W] == -1 ? KnapsackHelperDP(ind + 1, W, n, weightArr, valueArr, V, matrix) : matrix[ind + 1][W]);
            }
            else
            {
                if (matrix[ind + 1][W] != -1)
                    return matrix[ind + 1][W];
                return KnapsackHelperDP(ind + 1, W, n, weightArr, valueArr, V, matrix);
            }
        }
        #endregion

        #region Edit Distance
        public int MinDistance(string word1, string word2)
        {
            //return EditDistance(word1,word2,word1.Length-1,word2.Length-1);
            int n = word1.Length - 1;
            int m = word2.Length - 1;
            var memoizationMatrix = new int[n + 1][];
            for (int i = 0; i <= n; i++)
            {
                memoizationMatrix[i] = Enumerable.Repeat(-1, m + 1).ToArray();
            }
            return EditDistanceDP(word1, word2, n, m, memoizationMatrix);
        }
        //Naive Solution S=>O(N) and T=>O(3^N)
        //Try out all possible ways using recursion
        public int EditDistance(string s1, string s2, int ind1, int ind2)
        {
            if (ind1 < 0)
                return ind2 + 1;
            if (ind2 < 0)
                return ind1 + 1;
            if (s1[ind1] == s2[ind2])
                return (0 + EditDistance(s1, s2, ind1 - 1, ind2 - 1));
            else
            {
                return 1 + Math.Min(
                    //insert
                    EditDistance(s1, s2, ind1, ind2 - 1),
                    Math.Min(
                    //replace
                    EditDistance(s1, s2, ind1 - 1, ind2 - 1),
                    //delete
                    EditDistance(s1, s2, ind1 - 1, ind2)));
            }
        }

        //Better Solution S=>O(M*N)+O(M+N) and T=>O(M*N)
        //using Memoization

        public int EditDistanceDP(string s1, string s2, int ind1, int ind2, int[][] matrix)
        {
            if (ind1 < 0)
                return ind2 + 1;
            if (ind2 < 0)
                return ind1 + 1;
            if (matrix[ind1][ind2] != -1)
                return matrix[ind1][ind2];
            if (s1[ind1] == s2[ind2])
                return matrix[ind1][ind2] = (0 + EditDistanceDP(s1, s2, ind1 - 1, ind2 - 1, matrix));
            else
            {
                return matrix[ind1][ind2] = 1 + Math.Min(
                    //insert
                    EditDistanceDP(s1, s2, ind1, ind2 - 1, matrix),
                    Math.Min(
                    //replace
                    EditDistanceDP(s1, s2, ind1 - 1, ind2 - 1, matrix),
                    //delete
                    EditDistanceDP(s1, s2, ind1 - 1, ind2, matrix)));
            }
        }
        //Better Solution S=>O(M*N) and T=>O(M*N)
        //using Tabulation to make positive index we are going to take 1 based indexing

        public int EditDistanceTab(string s1, string s2, int ind1, int ind2, int[][] matrix)
        {
            for (int i = 0; i <= ind1; i++)
                matrix[i][0] = i;
            for (int j = 0; j <= ind2; j++)
                matrix[0][j] = j;
            for (int i = 1; i <= ind1; i++)
            {
                for (int j = 1; j <= ind2; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                        matrix[i][j] = matrix[i - 1][j - 1];
                    else
                        matrix[i][j] = 1 + Math.Min(
                    //insert
                    matrix[i][j - 1],
                    Math.Min(
                    //replace
                    matrix[i - 1][j - 1],
                    //delete
                    matrix[i - 1][j]));
                }
            }

            return matrix[ind1][ind2];
        }

        //Optimal S=>O(M) and T=>O(M*N)
        //Space Optimizing

        public int EditDistanceTabOptimised(string s1, string s2, int ind1, int ind2)
        {
            int[] prev = Enumerable.Repeat(0, ind2 + 1).ToArray();
            int[] curr = Enumerable.Repeat(0, ind2 + 1).ToArray();
            for (int j = 0; j <= ind2; j++)
                prev[j] = j;
            for (int i = 1; i <= ind1; i++)
            {
                curr[0] = i;
                for (int j = 1; j <= ind2; j++)
                {
                    if (s1[i - 1] == s2[j - 1])
                        curr[j] = prev[j - 1];
                    else
                        curr[j] = 1 + Math.Min(
                    //insert
                    curr[j - 1],
                    Math.Min(
                    //replace
                    prev[j - 1],
                    //delete
                    prev[j]));
                }
                Array.Copy(curr,prev,ind2+1);
            }

            return prev[ind2];
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
    }
}
