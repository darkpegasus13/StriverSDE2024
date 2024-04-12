using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Recursion
    {
        #region Subset Sum 

        //Subset Sum S=>O(N) and T=>O(2^N)
        //Using recursion
        public void SubsetSum(int[] arr, int indx = 0, int sum = 0)
        {
            if (indx == arr.Length)
            {
                Console.Write("{0},", sum);
                return;
            }
            SubsetSum(arr, indx + 1, sum);
            SubsetSum(arr, indx + 1, sum += arr[indx]);
        }
        #endregion

        #region Subset Sum 2
        //should not contain duplicate

        //Naive Solution


        //Optimal Solution S=>O(2^n*k) and T=>O(k*2^n)
        //using recursion 

        public void SubsetsWithDupRecursive(int[] nums, List<int> subSet, List<IList<int>> result, int indx = 0)
        {
            result.Add(new List<int>(subSet));
            int prev = -11;
            for (int i = indx; i < nums.Length; i++)
            {
                if (prev == nums[i])
                    continue;
                subSet.Add(nums[i]);
                this.SubsetsWithDupRecursive(nums, subSet, result, i + 1);
                prev = nums[i];
                subSet.RemoveAt(subSet.Count - 1);
            }
        }

        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            Array.Sort(nums);
            var ans = new List<IList<int>>();
            this.SubsetsWithDupRecursive(nums, new List<int>(), ans);
            return ans;
        }

        #endregion

        #region Combination Sum

        //Optimal S=>O(k*x) and T=>O(k*(2^n))
        //using Recursion
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            List<IList<int>> ans
                    = new List<IList<int>>();
            List<int> temp = new List<int>();

            // first do hashing since hashset does not always
            // sort
            //  removing the duplicates using HashSet and
            // Sorting the List
            var arr = candidates.ToList();
            HashSet<int> set = new HashSet<int>(arr);
            arr.Clear();
            arr.AddRange(set);
            arr.Sort();

            this.CombinationSumHelper(ans, arr, target, 0, temp);
            return ans;
        }
        public void CombinationSumHelper(List<IList<int>> ans, List<int> arr, int sum, int index, List<int> temp)
        {
            if (sum == 0)
            {

                // Adding deep copy of list to ans

                ans.Add(new List<int>(temp));
                return;
            }

            for (int i = index; i < arr.Count; i++)
            {

                // checking that sum does not become negative

                if ((sum - arr[i]) >= 0)
                {

                    // Adding element which can contribute to
                    // sum

                    temp.Add(arr[i]);

                    CombinationSumHelper(ans, arr, sum - arr[i], i,
                                temp);

                    // removing element from list (backtracking)
                    temp.Remove(arr[i]);
                }
            }
        }
        #endregion

        #region Combination sum print 

        //Optimal S=>O(k*x) and T=>O(2^n*k)
        //using recursion
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            List<IList<int>> ans
                        = new List<IList<int>>();
            List<int> temp = new List<int>();
            // Sorting the List
            var arr = candidates.ToList();
            arr.Sort();

            this.CombinationSumHelper2(ans, arr, target, 0, temp);
            return ans;
        }
        public void CombinationSumHelper2(List<IList<int>> ans, List<int> arr, int sum, int index, List<int> temp)
        {
            if (sum == 0)
            {
                // Adding deep copy of list to ans
                ans.Add(new List<int>(temp));
                return;
            }
            // checking that sum does not become negative
            for (int i = index; i < arr.Count; i++)
            {
                if (i > index && arr[i] == arr[i - 1])
                    continue;
                if (arr[i] > sum)
                    break;
                temp.Add(arr[i]);
                CombinationSumHelper2(ans, arr, sum - arr[i], i + 1, temp);
                temp.RemoveAt(temp.Count - 1);
            }
        }
        #endregion

        #region Palindrome Partitioning

        //Optimal Solution S=>O(k*N) and T=>O(2^N*k*(n/2))
        //using backtracking and recursion
        public IList<IList<string>> Partition(string s)
        {
            var ans = new List<IList<string>>();
            Partitions(0, s, new List<string>(), ans);
            return ans;
        }
        public void Partitions(int index, string s, List<string> path, List<IList<string>> res)
        {
            if (index == s.Length)
            {
                res.Add(new List<string>(path));
                return;
            }
            for (int i = index; i < s.Length; i++)
            {
                if (IsPalindrome(s, index, i))
                {
                    path.Add(s.Substring(index, i - index + 1));
                    Partitions(i + 1, s, path, res);
                    path.RemoveAt(path.Count - 1);
                }
            }
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
        #endregion

        #region Find Kth Permutation

        //Naive Solution S=>O(N) and T=>O(N!*N)+O(N!LogN!) 
        //by generating all the permutation and then sorting and returning the
        //given index

        //Optimal Solution  S=>O(N) an dT=>O(N^2)
        //by using factorial and dividing the cases into n partition
        //and check by the index where it falls

        public string FindKthPermutation(int n, int k)
        {
            int fact = 1;
            List<int> integrs = new List<int>();
            for (int i = 1; i < n; i++)
            {
                fact = fact * i;
                integrs.Add(i);
            }
            integrs.Add(n);
            k = k - 1;
            StringBuilder ans = new StringBuilder();
            while (true)
            {
                ans.Append(integrs[k / fact]);
                integrs.RemoveAt(k / fact);
                if (integrs.Count == 0)
                    break;
                k = k % fact;
                fact = fact / integrs.Count;
            }
            return ans.ToString();
        }
        #endregion

        #region Find all permutation

        //Naive Solution S=>O(N)+O(N) and T=>O(N!XN)
        //using recursion and a map for storing the values already visited

        public IList<IList<int>> Permute(int[] nums)
        {
            var ans = new List<IList<int>>();
            bool[] freq = new bool[nums.Length];
            PermuteHelper(nums,new List<int>(),ans,freq,nums.Length);
            return ans;
        }

        public void PermuteHelper(int[] nums,List<int> curr,List<IList<int>> ans, bool[] freq,int n)
        {
            if (curr.Count == n)
            {
                var temp = new int[n];
                curr.CopyTo(temp);
                ans.Add(temp);
                return;
            }
            for(int i = 0; i < n; i++)
            {
                if (!freq[i])
                {
                    freq[i] = true;
                    curr.Add(nums[i]);
                    PermuteHelper(nums, curr, ans, freq, n);
                    curr.RemoveAt(curr.Count - 1);
                    freq[i] = false;
                }
            }
        }
        //Optimal Solution S=>O(N) and T=>O(N!XN)
        //using backtracking and swap method
        static void swap(int[] nums, int l, int i)
        {
            int temp = nums[l];
            nums[l] = nums[i];
            nums[i] = temp;
        }

        // Function to find the possible permutations 
        static void PermuteHelperOptimal(List<int[]> res, int[] nums, int l, int h)
        {
            // Base case: Add the array to result and return 
            if (l == h)
            {
                res.Add((int[])nums.Clone());
                return;
            }

            // Permutations made 
            for (int i = l; i <= h; i++)
            {
                // Swapping 
                swap(nums, l, i);

                // Calling permutations for next greater value of l 
                PermuteHelperOptimal(res, nums, l + 1, h);

                // Backtracking 
                swap(nums, l, i);
            }
        }

        //Note
        //void Method1(Dictionary<string, string> dict) {
        //        dict["a"] = "b";
        //    dict = new Dictionary<string, string>();
        //}

        //    void Method2(ref Dictionary<string, string> dict)
        //    {
        //        dict["e"] = "f";
        //        dict = new Dictionary<string, string>();
        //    }
        //in second one the dicionary would be replaced

        #endregion
    }
}
