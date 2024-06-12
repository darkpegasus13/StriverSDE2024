using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class DP_AdityaVerma
    {
        #region 0-1knapsack patterns

        #region 0-1 Knapsack

        //recursive
        public int knapSack(int W, int[] wt, int[] val, int n)
        {
            if (n == 0 || W == 0)
                return 0;
            if (wt[n - 1] <= W)
            {
                return Math.Max(val[n - 1] + knapSack(W - wt[n - 1], wt, val, n - 1), knapSack(
                    W, wt, val, n - 1));
            }
            else
            {
                return knapSack(W, wt, val, n - 1);
            }
        }

        //DP memoization
        public int knapSackMem(int W, int[] wt, int[] val, int n, int[][] tab)
        {
            if (n == 0 || W == 0)
                return 0;
            if (tab[n][W] != -1)
                return tab[n][W];
            if (wt[n - 1] <= W)
            {
                return tab[n][W] = Math.Max(val[n - 1] + knapSackMem(W - wt[n - 1], wt, val, n - 1, tab),
                    knapSackMem(
                    W, wt, val, n - 1, tab));
            }
            else
            {
                return tab[n][W] = knapSackMem(W, wt, val, n - 1, tab);
            }
        }

        //DP top down
        public int knapSackTD(int W, int[] wt, int[] val, int n, int[][] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                for (int j = 0; j < tab[0].Length; j++)
                    if (i == 0 || j == 0)
                        tab[i][j] = 0;

            for (int i = 1; i < tab.Length; i++)
                for (int j = 1; j < tab[0].Length; j++)
                {
                    if (wt[i - 1] <= j)
                        tab[i][j] = Math.Max(val[i - 1] + tab[i - 1][j - wt[i - 1]],
                            tab[i - 1][j]);
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[n][W];
        }
        #endregion

        #region Subset Sum
        //treat W as sum and if as one array is present treat as weight array

        //DP top down
        public bool SubSetSumTD(int W, int[] arr, int n, bool[][] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                for (int j = 0; j < tab[0].Length; j++)
                    if (i == 0 && j == 0)
                        tab[i][j] = true;
                    else if (i == 0)
                        tab[i][j] = false;
                    else
                        tab[i][j] = true;

            for (int i = 1; i < tab.Length; i++)
                for (int j = 1; j < tab[0].Length; j++)
                {
                    if (arr[i - 1] <= j)
                        tab[i][j] = tab[i - 1][j - Math.Abs(arr[i - 1])] || tab[i - 1][j];
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[n][W];
        }

        #endregion

        #region Equal Sum Partition

        public bool CanPartition(int[] nums)
        {
            int sum = nums.Sum();
            if (sum % 2 != 0)
                return false;
            bool[][] tab = new bool[nums.Length + 1][];
            for (int i = 0; i <= nums.Length; i++)
            {
                tab[i] = Enumerable.Repeat(false, sum + 1).ToArray();
            }
            //as you can see only we passed sum/2 and resused the Subsetsum
            return SubSetSumTD(sum / 2, nums, nums.Length, tab);
        }

        #endregion

        #region count number of subset with a given sum
        //changed the false to 0 and true to 1 and || to +
        public int CountSubsetWithGivenSumTD(int W, int[] arr, int n, int[][] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                for (int j = 0; j < tab[0].Length; j++)
                    if (i == 0 && j == 0)
                        tab[i][j] = 1;
                    else if (i == 0)
                        tab[i][j] = 0;
                    else
                        tab[i][j] = 1;

            for (int i = 1; i < tab.Length; i++)
                for (int j = 1; j < tab[0].Length; j++)
                {
                    if (arr[i - 1] <= j)
                        tab[i][j] = tab[i - 1][j - arr[i - 1]] + tab[i - 1][j];
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[n][W];
        }

        #endregion

        #region Minimum diff b/w subsets
        //derived from equal sum partition
        //deals with only positive integers in array
        public int MinimumDifference(int[] nums)
        {
            int sum = nums.Sum();
            bool[][] tab = new bool[nums.Length + 1][];
            for (int i = 0; i <= nums.Length; i++)
            {
                tab[i] = Enumerable.Repeat(false, sum + 1).ToArray();
            }
            SubSetSumTD(sum, nums, nums.Length, tab);
            //traversing till sum/2 and getting min from the last row
            int ans = int.MaxValue;
            for (int i = 0; i < sum / 2 + 1; i++)
            {
                if (tab[nums.Length][i])
                    ans = Math.Min(ans, sum - Math.Abs(2 * i));
            }
            return ans;
        }

        #endregion

        #region count the # of subset with given diff
        //slightly modified version of the function
        public int CountSubsetWithGivenSumTDMod(int W, int[] arr, int n, int[][] tab)
        {
            //only one position will be 1 others are zero
            tab[0][0] = 1;
            for (int i = 1; i < tab.Length; i++)
                for (int j = 0; j < tab[0].Length; j++)
                {
                    if (arr[i - 1] <= j)
                        tab[i][j] = (tab[i - 1][j - arr[i - 1]] + tab[i - 1][j])
                        % 1000000007;
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[n][W];
        }
        //please check afterwards it is not working
        //aditya verma code not working

        //this problem would be reduced to count # of subset sum
        //as mathematically s1 = (diff+sum)/2

        public int countPartitions(int n, int d, int[] arr)
        {
            int[][] tab = new int[n + 1][];
            int sum = arr.Sum();
            //if sum in fractions it is not possible
            if ((d + sum % 2) != 0)
                return 0;
            int s1 = (d + sum) / 2;
            for (int i = 0; i <= n; i++)
            {
                tab[i] = Enumerable.Repeat(0, s1 + 1).ToArray();
            }
            return CountSubsetWithGivenSumTDMod(s1, arr, n, tab);
        }

        #endregion

        #region Target Sum

        //will be reusing the exact same code for count the # of subset with given diff
        //treating target as diff

        //need to check some test cases failling may be need to modify the
        //for this as well CountSubsetWithGivenSumTDMod
        public int FindTargetSumWays(int[] nums, int target)
        {
            if (nums.Length == 1)
                return nums[0] == Math.Abs(target) ? 1 : 0;
            int n = nums.Length;
            int[][] tab = new int[n + 1][];
            int sum = nums.Sum();
            //if sum in fractions it is not possible
            if ((target + sum) % 2 != 0)
                return 0;
            int s1 = Math.Abs((target + sum)) / 2;
            for (int i = 0; i <= n; i++)
            {
                tab[i] = Enumerable.Repeat(0, s1 + 1).ToArray();
            }
            return CountSubsetWithGivenSumTDMod(s1, nums, n, tab);
        }

        #endregion

        #endregion

        #region unbounded knapsack pattern

        #region unbounded knapsack

        //exact same code of 0-1 knapsack 
        //DP top down
        public int UnBoundedknapSackTD(int W, int[] wt, int[] val, int n, int[][] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                for (int j = 0; j < tab[0].Length; j++)
                    if (i == 0 || j == 0)
                        tab[i][j] = 0;

            for (int i = 1; i < tab.Length; i++)
                for (int j = 1; j < tab[0].Length; j++)
                {
                    if (wt[i - 1] <= j)
                        tab[i][j] = Math.Max(val[i - 1] + tab[i][j - wt[i - 1]], //just changed tab[i-1] to i 
                                                                                 // as in unbounded we can take again the same element
                            tab[i - 1][j]);
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[n][W];
        }

        #endregion

        #region rod cutting problem

        //just pass length arr as weight arr and price as value
        public int RodCutting(int n, int[] price)
        {
            int n1 = price.Length;
            int[] lengthArr = new int[n1];
            //creating the weight/length array
            for (int i = 0; i < n1; i++)
            {
                lengthArr[i] = i + 1;
            }
            var tab = GenerateDPTable(n1, n);
            //initialising first row and column with zero
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0)
                        tab[i][j] = 0;
                }
            }
            return UnBoundedknapSackTD(n, lengthArr, price, n, tab);
        }

        #endregion

        #region Coin Change problem max no. of ways
        public long count(int[] coins, int N, int sum)
        {
            //Your code here
            long[][] tab = new long[N + 1][];
            for (int i = 0; i <= N; i++)
            {
                tab[i] = new long[sum + 1];
            }
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= sum; j++)
                {
                    if (j == 0 && i != 0)
                        tab[i][j] = 1;
                }
            }
            return CoinChange(sum, coins, N, tab);
        }
        //using subset sum code 
        //DP top down
        public long CoinChange(int W, int[] arr, int n, long[][] tab)
        {
            for (int i = 0; i < tab.Length; i++)
                for (int j = 0; j < tab[0].Length; j++)
                    if (i == 0 && j == 0)
                        tab[i][j] = 1;
                    else if (i == 0)
                        tab[i][j] = 0;
                    else
                        tab[i][j] = 1;

            for (int i = 1; i < tab.Length; i++)
                for (int j = 1; j < tab[0].Length; j++)
                {
                    if (arr[i - 1] <= j)
                        tab[i][j] = tab[i][j - Math.Abs(arr[i - 1])] + tab[i - 1][j];
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[n][W];
        }
        #endregion

        #region coin change min no. of coins

        //initialisation is a little different 
        //we will initialize second row as well

        public int minCoins(int[] coins, int M, int V)
        {
            //creating
            int[][] tab = GenerateDPTable(M, V);
            //initialising
            for (int i = 0; i <= M; i++)
            {
                for (int j = 0; j <= V; j++)
                {
                    if (j == 0)
                        tab[i][j] = 0;
                    else
                        tab[i][j] = int.MaxValue - 1;
                }
            }
            for (int j = 0; j <= V; j++)
            {
                tab[1][j] = j % coins[0] == 0 ? j / coins[0] : int.MaxValue - 1;
            }
            //same code as unbounded knapsack with some variations
            for (int i = 1; i < tab.Length; i++)
                for (int j = 1; j < tab[0].Length; j++)
                {
                    if (coins[i - 1] <= j)
                        tab[i][j] = Math.Min(tab[i][j - coins[i - 1]] + 1,
                            tab[i - 1][j]);
                    else
                        tab[i][j] = tab[i - 1][j];
                }
            return tab[M][V] == int.MaxValue - 1 ? -1 : tab[M][V];
        }




        #endregion

        #endregion

        #region LCS PAtterns

        #region LCS

        //Recursion
        public int LongestCommonSubsequenceRecur(string text1, string text2, int ind1, int ind2, int ans = 0)
        {
            if (ind1 == 0 || ind2 == 0)
                return 0;
            if (text1[ind1 - 1] == text2[ind2 - 1])
                return 1 + LongestCommonSubsequenceRecur(text1, text2, ind1 - 1, ind2 - 1, ans);
            else
                return Math.Max(LongestCommonSubsequenceRecur(text1, text2, ind1 - 1, ind2, ans),
                    LongestCommonSubsequenceRecur(text1, text2, ind1, ind2 - 1, ans));
        }

        //memoization
        public int LongestCommonSubsequenceMemo(string text1, string text2, int ind1, int ind2, int[][] tab)
        {
            //tab will be initialized -1
            if (ind1 == 0 || ind2 == 0)
                return 0;
            //remember check will be at ind not ind-1
            if (tab[ind1][ind2] != -1)
                return tab[ind1][ind2];
            if (text1[ind1 - 1] == text2[ind2 - 1])
                return tab[ind1][ind2] = 1 + LongestCommonSubsequenceMemo(text1, text2, ind1 - 1, ind2 - 1, tab);
            else
                return tab[ind1][ind2] = Math.Max(LongestCommonSubsequenceMemo(text1, text2, ind1 - 1, ind2, tab),
                    LongestCommonSubsequenceMemo(text1, text2, ind1, ind2 - 1, tab));
        }

        //tabulation
        public int LongestCommonSubsequenceTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
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
        #endregion

        #region longest common substring

        public int LongestCommonSubstringTab(string text1, string text2, int ind1, int ind2, int[][] tab)
        {
            //storing in the variable
            int ans = int.MinValue;
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (text1[i - 1] == text2[j - 1])
                        memoizationMatrix[i][j] = 1 + memoizationMatrix[i - 1][j - 1];
                    else
                        memoizationMatrix[i][j] = 0;
                    //checking here itself so we dont traverse again
                    ans = Math.Max(ans, memoizationMatrix[i][j]);
                }
            }
            //this time not return =ing the last slot instead max of the 
            //whole jagged array
            return ans;
        }

        #endregion

        #region print LCS

        public string PrintLongestCommonSubsequenceTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
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
            string ans = "";
            while (m > 0 && n > 0)
            {
                if (text1[n - 1] == text2[m - 1])
                {
                    ans = text1[n - 1] + ans;
                    m--; n--;
                }
                else
                if (memoizationMatrix[n - 1][m] > memoizationMatrix[n][m - 1])
                    n--;
                else
                    m--;
            }
                
            //we can reverse it later also using the below but will result in higher
            //time complexity 
            //char[] stringArray = originalString.ToCharArray();
            //Array.Reverse(stringArray);
            return ans;
        }
        #endregion

        #region Shortest common supersequence

        public int ShortestCommonSupersequenceTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
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
            return n + m - memoizationMatrix[n][m];
        }

        #endregion

        #region Print Shortest common SUpersequence
        //not workinf please check afterwards.
        public string PrintshortestCommonSupersequenceTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
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
            var ans = "";
            while (m > 0 && n > 0)
            {
                if (text1[n-1] == text2[m-1])
                {
                    ans = text1[n-1] + ans;
                    n--; m--;
                }
                else
                {
                    if (memoizationMatrix[n - 1][m] > memoizationMatrix[n][m - 1])
                    {
                        ans = text1[n - 1] + ans;
                        n--;
                    }
                    else 
                    {
                        ans = text2[m - 1] + ans;
                        m--;
                    }
                        
                }
            }
            while (m > 0)
            {
                ans = text2[m]+ans;
                m--;
            }
            while (n > 0)
            {
                ans = text1[n]+ans;
                n--;
            }
            return ans;
        }

        #endregion

        #region Minimum number of insertion/deletion

        public int MinInsertDeleteTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
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
            //deletions                //insertions
            return (m - memoizationMatrix[n][m]) + n - memoizationMatrix[n][m];
        }

        #endregion

        #endregion

        #region Longest Palindromic Subsequence pattern

        #region LPS
        //same as code from LCS just pass reversed string in second string
        public int LongestPalindromicSubsequenceTab(string s)
        {
            char[] stringArray = s.ToCharArray();
            Array.Reverse(stringArray);
            string reverseString = new string(stringArray);
            return LongestCommonSubsequenceTab(s, reverseString, s.Length, s.Length);
        }
        #endregion

        #region min no. of insertion/deletion to make a string palindrome

        //just subtract the length of s with LPS of s
        //number of deletion and insertion will be same
        public int MinNUmberOfDeletionTab(string s)
        {
            return s.Length - LongestPalindromicSubsequenceTab(s);
        }

        #endregion

        #region Longest repeating subsequence

        //using LCS code
        public int LongestrepeatingsubsequenceTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    //just added an extra condition i!=j
                    if (text1[i - 1] == text2[j - 1] && i!=j)
                        memoizationMatrix[i][j] = 1 + memoizationMatrix[i - 1][j - 1];
                    else
                        memoizationMatrix[i][j] = Math.Max(memoizationMatrix[i - 1][j], memoizationMatrix[i][j - 1]);
                }
            }
            return memoizationMatrix[n][m];
        }
        #endregion

        #region Sequence pattern matching

        public bool SequencePatternMatchinTab(string text1, string text2, int ind1, int ind2)
        {
            int n = ind1;
            int m = ind2;
            var memoizationMatrix = GenerateDPTable(n, m);
            //initialise 1st row and coulumn with 0
            for (int i = 0; i <= n; i++)
                memoizationMatrix[i][0] = 0;
            for (int j = 0; j <= m; j++)
                memoizationMatrix[0][j] = 0;
            //calculating the answer matrix
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    //just added an extra condition i!=j
                    if (text1[i - 1] == text2[j - 1] && i != j)
                        memoizationMatrix[i][j] = 1 + memoizationMatrix[i - 1][j - 1];
                    else
                        memoizationMatrix[i][j] = Math.Max(memoizationMatrix[i - 1][j], memoizationMatrix[i][j - 1]);
                }
            }
            //comparing length is enough as LCS if equal to length of one 
            //of the strings it means it will be contained fully
            return memoizationMatrix[n][m]==text2.Length;
        }


        #endregion


        #endregion

        #region MCM Pattern
        //we need minimum cost in multiplying matrix

        //Recursive
        public int MCMRecur(int[] arr, int i, int j)
        {
            if (i >= j)
                return 0;
            int min = int.MaxValue;
            for(int k = i; k <= j - 1; k++)
            {
                int tempAns = MCMRecur(arr, i, k) + MCMRecur(arr, k + 1, j)
                    + arr[i - 1] * arr[k] * arr[j];
                min = Math.Min(tempAns, min);
            }
            return min;
        }

        //Memoization
        public int MCMMemo(int[] arr, int i, int j, int[][] tab)
        {
            if (i >= j)
                return 0;
            int min = int.MaxValue;
            if (tab[i][j] != -1)
                return tab[i][j];
            for (int k = i; k <= j - 1; k++)
            {
                int tempAns = MCMMemo(arr, i, k,tab) + MCMMemo(arr, k + 1, j,tab)
                    + arr[i - 1] * arr[k] * arr[j];
                min = Math.Min(tempAns, min);
            }
            return tab[i][j] = min;
        }

        //Tabular

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

        #region Palindrome Partitioning

        //memoized
        public int palindromicPartition(string str)
        {
            //Your code here
            int[][] tab = new int[str.Length][];
            for (int i = 0; i < str.Length; i++)
            {
                tab[i] = Enumerable.Repeat(-1, str.Length).ToArray();
            }
            return helper(str, 0, str.Length - 1, tab);
        }

        public int helper(string str, int i, int j, int[][] tab)
        {
            if (i >= j)
                return 0;
            //placing it above Ispalindrome check further optimises it
            if (tab[i][j] != -1)
                return tab[i][j];
            if (IsPalindrome(str, i, j))
                return 0;
            int minm = int.MaxValue;
            for (int k = i; k <= j - 1; k++)
            {
                //here we are not checking for helper(str, i, k, tab) + helper(str, k + 1, j, tab)
                //are calculated already or not so refer helper optimised
                int temp = 1 + helper(str, i, k, tab) + helper(str, k + 1, j, tab);
                minm = Math.Min(minm, temp);
            }
            return tab[i][j] = minm;
        }

        public bool IsPalindrome(string src, int start, int end)
        {
            while (start <= end)
            {
                if (src[start] != src[end])
                    return false;
                start++; end--;
            }
            return true;
        }

        public int helperOptimised(string str, int i, int j, int[][] tab)
        {
            if (i >= j)
                return 0;
            if (tab[i][j] != -1)
                return tab[i][j];
            if (IsPalindrome(str, i, j))
                return 0;
            int minm = int.MaxValue;
            for (int k = i; k <= j - 1; k++)
            {
                if (tab[i][k] == -1)
                    tab[i][k] = helper(str, i, k, tab);
                if (tab[k + 1][j] == -1)
                    tab[k + 1][j] = helper(str, k + 1, j, tab);
                int temp = 1 + tab[i][k] + tab[k + 1][j];
                minm = Math.Min(minm, temp);
            }
            return tab[i][j] = minm;
        }

        #endregion

        #region Evaluate Expression to True

        //doesnt works please check afterwards
        public int countWays(int N, string S)
        {
            //Your code here
            return helper(S, 0, S.Length, 'T');
        }
        public int helper(string s, int i, int j, char isTrue)
        {
            if (i > j)
                return 0;
            if (i == j)
            {
                if (isTrue == 'T')
                    return s[i] == 'T' ? 1 : 0;
                else
                    return s[i] == 'F' ? 1 : 0;
            }
            int ans = 0;
            for (int k = i + 1; k <= j - 1; k = k + 2)
            {
                int leftTrue = helper(s, i, k - 1, 'T');
                int leftFalse = helper(s, i, k - 1, 'F');
                int rightTrue = helper(s, k + 1, j, 'T');
                int rightFalse = helper(s, k + 1, j, 'F');
                if (s[k] == '&')
                {
                    if (isTrue == 'T')
                        ans += leftTrue * rightTrue;
                    else
                        ans += (leftTrue * rightFalse) + (leftFalse * rightTrue) +
                        (leftFalse * rightFalse);
                }
                else if (s[k] == '|')
                {
                    if (isTrue == 'T')
                        ans += (leftTrue * rightFalse) + (leftFalse * rightTrue) +
                            (leftTrue * rightTrue);
                    else
                        ans += leftFalse * rightFalse;
                }
                else if (s[k] == '^')
                {
                    if (isTrue == 'T')
                        ans += (leftTrue * rightFalse) + (leftFalse * rightTrue);
                    else
                        ans += (leftTrue * rightTrue) + (leftFalse * rightFalse);
                }
            }
            return ans;
        }
        #endregion

        #region Scramble Strings

        //please see afterwards

        #endregion

        #region Egg Dropping Problem

        public int SuperEggDrop(int k, int n)
        {
            int[][] tab = new int[k + 1][];
            for (int i = 0; i <= k; i++)
            {
                tab[i] = Enumerable.Repeat(-1, n + 1).ToArray();
            }
            return SuperEggDropHelper(k, n, tab);
        }

        public int SuperEggDropHelper(int e, int f, int[][] tab)
        {
            if (f == 0 || f == 1 || e == 1)
                return f;
            int mn = int.MaxValue;
            if (tab[e][f] != -1)
                return tab[e][f];
            for (int k = 1; k <= f; k++)
            {

                int temp = 1 + Math.Max(tab[e - 1][k - 1] == -1 ? SuperEggDropHelper(e - 1, k - 1, tab) : tab[e - 1][k - 1],
                    tab[e][f - k] == -1 ? SuperEggDropHelper(e, f - k, tab) : tab[e][f - k]);
                mn = Math.Min(mn, temp);
            }
            return tab[e][f] = mn;
        }

        #endregion

        #endregion

        #region DP on trees

        #region Diameter of BT

        public int DiameterOfBinaryTree(TreeNode root)
        {
            int res = int.MinValue;
            DiameterOfBinaryTreeHelper(root, ref res);
            //if question asks edeges otherwise return res
            return res - 1;
        }

        public int DiameterOfBinaryTreeHelper(TreeNode root, ref int res)
        {
            if (root == null)
                return 0;
            int l = DiameterOfBinaryTreeHelper(root.left, ref res);
            int r = DiameterOfBinaryTreeHelper(root.right, ref res);
            int temp = Math.Max(l, r) + 1;
            int ans = Math.Max(temp, l + r + 1);
            res = Math.Max(res, ans);
            return temp;
        }

        #endregion

        #region Maximum Path Sum
        public int MaxPathSum(TreeNode root)
        {
            int Maxi = int.MinValue;
            MaximumPathSumHelper(root, ref Maxi);
            return Maxi;
        }

        public int MaximumPathSumHelper(TreeNode root, ref int res)
        {
            if (root == null)
                return 0;
            int l = MaximumPathSumHelper(root.left, ref res);
            int r = MaximumPathSumHelper(root.right, ref res);
            int temp = Math.Max(Math.Max(l, r) + root.val, root.val);
            int ans = Math.Max(temp, l + r + root.val);
            res = Math.Max(res, ans);
            return temp;
        }

        #endregion

        #region Maximum path sum from leaf to leaf

        public int findMaxSum(TreeNode root)
        {
            //Your code here
            int ans = int.MinValue;
            findMaxSumHelper(root, ref ans);
            return ans;

        }
        public int findMaxSumHelper(TreeNode root, ref int res)
        {
            //Your code here
            if (root == null)
                return 0;
            int l = findMaxSumHelper(root.left, ref res);
            int r = findMaxSumHelper(root.right, ref res);
            int temp = Math.Max(Math.Max(l, r) + root.val, root.val);
            int ans = Math.Max(temp, l + r + root.val);
            res = Math.Max(res, ans);
            return temp;
        }


        #endregion


        #endregion

        #region helper
        public int[][] GenerateDPTable(int row, int col)
        {
            //generating table for DP
            int[][] tab = new int[row + 1][];
            for (int i = 0; i <= row; i++)
            {
                tab[i] = new int[col + 1];
            }
            return tab;
        }
        #endregion 
    }
}
