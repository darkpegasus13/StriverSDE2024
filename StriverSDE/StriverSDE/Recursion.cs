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
    }
}
