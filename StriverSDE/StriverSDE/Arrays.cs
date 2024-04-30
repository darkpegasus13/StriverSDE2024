using StriverSDE;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using static StriverSDE.BinarySearchTree;

public class ArrayComp
{
    #region Set Zeroes
    //Naive T=>O(N*M(N+M)+N*M) S=>O(1)
    //By setting the rows with -1 in one iteration and setting 0 in another
    //iteration

    //Better T=>O(2(N*M)) and S=>O(N+M)
    //By taking two arrays row and column and then traversing again
    //if any row or coulumn contains 0 then make element 0


    //Optimal T=>O(M*N) and S=>O(1)
    //By Using the oth row and column to store 0 flag
    public void SetZeroesOptimal(int[][] matrix)
    {
        //setting 0,0 as variable aas it overlaps
        int col0 = 1;
        //setting 0 in the first row and column
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[0].Length; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0;
                    if (j == 0)
                        col0 = 0;
                    else
                        matrix[0][j] = 0;
                }
            }
        }

        //filling zero by checking row and column 
        for (int i = 1; i < matrix.Length; i++)
        {
            for (int j = 1; j < matrix[0].Length; j++)
            {
                if (matrix[i][j] != 0)
                {
                    if (matrix[i][0] == 0 || matrix[0][j] == 0)
                        matrix[i][j] = 0;
                }
            }
        }
        //now taking care of first row and column
        //remember to always do for 0 row first
        if (matrix[0][0] == 0)
        {
            for (int j = 0; j < matrix[0].Length; j++)
            {
                matrix[0][j] = 0;
            }
        }
        if (col0 == 0)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i][0] = 0;
            }
        }
    }
    #endregion

    #region GeneratePascalsTriangle

    //Variation 1 T=>O(C) and T=>O(1)
    //find the given element at r,c
    //using maths

    long nCr(int n, int r)
    {
        long res = 1;
        for (int i = 0; i < r; i++)
        {
            res = res * (n - i);
            res = res / (i + 1);

        }
        return res;
    }

    long PascalTriangleVar1(int r, int c)
    {
        return nCr(r - 1, c - 1);
    }

    //Variation 2 T=>O(N*R) && S=>O(1)
    //Print the Nth Row

    void PascalTriangleVar2(int n)
    {
        for (int c = 1; c <= n; c++)
            System.Console.WriteLine("{0}, ", nCr(n - 1, c - 1));
    }

    //Variation 3 T=>O(N^3)
    //print Pascal Triangle

    List<List<int>> PascalTriangleVar3(int n)
    {
        List<List<int>> res = new List<List<int>>();

        for (int row = 1; row <= n; row++)
        {
            List<int> temp_res = new List<int>();
            for (int col = 1; col <= row; col++)
            {
                temp_res.Add((int)nCr(row - 1, col - 1));
            }
            res.Add(temp_res);
        }
        return res;
    }

    //Variation 3 OPtimal T=>O(N^2)
    List<int> GenerateRow(int row)
    {
        int ans = 1;
        List<int> ansRow = new List<int>();
        ansRow.Add(ans);
        for (int col = 1; col < row; col++)
        {
            ans = ans * (row - col);
            ans = ans / col;
            ansRow.Add(ans);
        }
        return ansRow;
    }

    List<List<int>> PascalTriangleOptimalVar3(int n)
    {
        List<List<int>> res = new List<List<int>>();
        for (int row = 1; row <= n; row++)
        {
            res.Add(GenerateRow(row));
        }
        return res;
    }

    //Optimal 2 T=>O(N^2)
    //rememberr when to use list and when ilist
    public IList<IList<int>> Generate(int numRows)
    {
        IList<IList<int>> triangle = new List<IList<int>>();
        if (numRows == 1)
        {
            triangle.Add(new List<int> { 1 });
            return triangle;
        }
        triangle.Add(new List<int> { 1 });
        triangle.Add(new List<int> { 1, 1 });
        if (numRows == 2)
            return triangle;
        else
        {
            for (int i = 1; i < numRows - 1; i++)
            {
                List<int> row = new List<int> { 1 };
                for (int j = 0; j < triangle[i].Count - 1; j++)
                    row.Add(triangle[i][j] + triangle[i][j + 1]);
                row.Add(1);
                triangle.Add(row);
            }
        }
        return triangle;
    }
    #endregion

    #region Next Permutation

    //Naive S=>O(1) and T=>O(N!*N)
    //Find all Permutation and then print the lexicographic next element

    //Optimal S=>O(1) and T=>O(3N)
    public void NextPermutation(int[] nums)
    {
        //We are finding a break point that is a[i]<a[i+1]
        int indx = -1;
        int n = nums.Length;
        for (int i = n - 2; i >= 0; i--)
        {
            if (nums[i] < nums[i + 1])
            {
                indx = i;
                break;
            }
        }
        //if it is already the largest
        if (indx == -1)
        {
            Array.Reverse(nums);
            return;
        }
        int swapIndx = -1;
        for (int i = n - 1; i > indx; i--)
        {
            if (nums[i] > nums[indx])
            {
                if (swapIndx != -1)
                {
                    if (nums[swapIndx] > nums[i])
                        swapIndx = i;
                }
                else
                    swapIndx = i;
            }
        }
        //swapped the breakpoint with the smallest greater
        (nums[swapIndx], nums[indx]) = (nums[indx], nums[swapIndx]);
        //reverse the array from indx+1 to end
        //as it was a increasing sequence
        Array.Reverse(nums, indx + 1, n - indx - 1);
    }
    #endregion

    #region Maximum Subarray Sum

    //Naive S=>O(1) and T=>O(N^2)
    //calculate sum for every subarray

    //Optimal S=>O(1) and T=>O(N)
    //using kadanes Algo

    public int MaxSubArray(int[] nums)
    {
        //maxtillnow
        int meh = 0;
        int max_sum = int.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            meh += nums[i];
            if (meh > max_sum)
                max_sum = meh;
            //whenever meh<0 we make it 0
            if (meh < 0)
                meh = 0;
        }
        return max_sum;
    }

    //follow up question
    //print the subarray
    public void MaxSubArrayPrintRange(int[] nums)
    {
        int[] range = new int[2];
        //maxtillnow
        int meh = 0;
        int max_sum = int.MinValue;
        int start = 0;
        int ansStrt = -1;
        int ansEnd = -1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (meh == 0)
                start = i;
            meh += nums[i];
            if (meh > max_sum)
            {
                max_sum = meh;
                //updating ansStrt only when max is found
                ansStrt = start;
                ansEnd = i;
            }
            //whenever meh<0 we make it 0
            if (meh < 0)
                meh = 0;
        }
        for (int i = ansStrt; i <= ansEnd; i++)
            Console.WriteLine(nums[i]);
    }
    #endregion

    #region Sort 0s,1s,2s

    //Naive S=>O(N) and T=>NlogN
    //using sort function 

    //Better S=>O(1) and T=>O(2N)
    //By maintaining counts of all 3 in a map space will be constant it will be constant
    //also we can maintain three variables as well

    //Optimal S=>O(1) and T=>O(N)
    //Dutch National Flag 0-low-1 are 0 and low-mid-1 are 1 and mid-high are random and high+1-n-1 are 2

    void Sort3elementOptimal(int[] arr)
    {
        int low = 0, mid = 0, high = arr.Length - 1;
        while (mid <= high)
        {
            if (arr[mid] == 0)
            {
                (arr[mid], arr[low]) = (arr[low], arr[mid]);
                low++; mid++;
            }
            else if (arr[mid] == 2)
            {
                (arr[high], arr[mid]) = (arr[mid], arr[high]);
                high--;
            }
            else
                mid++;
        }
    }

    #endregion

    #region Buy Sell Stocks

    //Naive Solution S=>O(1) and T=>O(N^2)
    //Calculate for every posiibility of stocks

    //Optimal Solution S=>O(1) and T=>O(N)
    //Store minimum till current indx and if current indx>min find the diff and store if greater

    public int MaxProfitOptimal(int[] prices)
    {
        int res = 0;
        int minmTillNow = int.MaxValue;
        for (int i = 0; i < prices.Length; i++)
        {
            if (prices[i] > minmTillNow)
                //calculating profit and then saving the largest
                res = Math.Max(res, prices[i] - minmTillNow);
            else
                //setting new minimum
                minmTillNow = prices[i];
        }
        return res;
    }

    #endregion

    #region 3 Sum 
    //In this triplets are not the same

    //remember to sort array as duplicates are now allowed
    //Naive SOlution S=>O(N) for storing triplets and T=>O(N^3*log(unique triplets))
    //using three loops 

    //Better Solution S=>O(N) and T=>O(N^2*log(unique triplets))
    //by maintaining elements in hashmap and running two loops

    //Better Solution S=>O(N) and T=>O(NlogN + O(M*N))
    //using three pointers

    public IList<IList<int>> ThreeSum(int[] nums)
    {
        IList<IList<int>> ans = new List<IList<int>>();
        Array.Sort(nums);
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;
            int j = i + 1;
            int k = n - 1;
            while (j < k)
            {
                var temp = nums[i] + nums[j] + nums[k];
                if (temp > 0)
                    k--;
                else if (temp < 0)
                    j++;
                else
                {
                    ans.Add(new List<int> { nums[i], nums[j], nums[k] });
                    j++;
                    k--;
                    while (j < n && nums[j] == nums[j - 1])
                        j++;
                    while (k >= 0 && nums[k] == nums[k + 1])
                        k--;
                }
            }
        }
        return ans;
    }
    #endregion

    #region Trapping Rain Water

    //Naive solution S=>O(1) and T+>O(N^2)
    //calculate min of the left side and right side and then subtract
    //the height of current ceiling min(left[i],right[i]-rainArr[i])


    //Better solution S=>O(2N) and T=>O(3N)
    //by keeping a prefix and suffix array

    public int TrappingRainWaterBetter(int[] height)
    {
        int n = height.Length;
        int[] suffix = new int[n];
        int[] prefix = new int[n];
        int res = 0;
        //calculating prefix
        int prefixLargest = int.MinValue;
        int suffixLargest = int.MinValue;
        for (int i = 0; i < n; i++)
        {
            prefixLargest = Math.Max(height[i], prefixLargest);
            prefix[i] = prefixLargest;
        }
        //calculating prefix
        for (int i = n - 1; i >= 0; i--)
        {
            suffixLargest = Math.Max(height[i], suffixLargest);
            suffix[i] = suffixLargest;
        }
        for (int i = 0; i < n; i++)
        {
            res += Math.Min(suffix[i], prefix[i]) - height[i];
        }
        return res;
    }


    //Optimal solution
    //two pointer approach
    public int TrappingRainWaterOptimal(int[] rainArr)
    {
        int left = 0;
        int right = rainArr.Length - 1;
        int leftMax = 0;
        int rightMax = 0;
        int res = 0;
        while (left <= right)
        {
            if (rainArr[left] <= rainArr[right])
            {
                if (rainArr[left] < leftMax)
                    res += leftMax - rainArr[left];
                else
                    leftMax = Math.Max(leftMax, rainArr[left]);
                left++;
            }
            else
            {
                if (rainArr[right] < rightMax)
                    res += rightMax - rainArr[right];
                else
                    rightMax = Math.Max(rightMax, rainArr[right]);
                right--;
            }
        }
        return res;
    }
    #endregion

    #region Remove Duplicate from sorted array and array should be in place

    //Naive Solution
    //using hashmap it works for non sorted as well
    public int RemoveDuplicate(int[] nums)
    {
        Dictionary<int, int> dict = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (dict.ContainsKey(nums[i]))
                continue;
            else
                dict.Add(nums[i], 0);
        }
        int pos = 0;
        foreach (KeyValuePair<int, int> pair in dict)
        {
            nums[pos] = pair.Key;
            pos++;
        }
        return dict.Count;
    }

    //Optimal Solution
    //making use of the fact that array is sorted

    public int RemoveDuplicateOptimal(int[] nums)
    {
        //j pointer keeps the indx of last unique element
        int j = 0;
        //i pointer traverse the array and whenever find a different 
        //element we swap i and j and increment both
        int i = 0;
        while (i < nums.Length)
        {
            if (nums[j] == nums[i])
                i += 1;
            else
            {
                j += 1;
                nums[j] = nums[i];
                i += 1;
            }
        }
        return j + 1;
    }
    #endregion

    #region Max Consecutive ones

    //OPtimal Solution
    //breaking the while loop as soon as we encounter a number
    //other than 0 and storing max
    public int FindMaxConsecutiveOnes(int[] nums)
    {
        int mxcont = -1;
        for (int i = 0; i < nums.Length; i++)
        {
            int curr = 0;
            //when encounter 1 start the loop and break when it is non 1
            while (i < nums.Length && nums[i] == 1)
            {
                curr += 1;
                i += 1;
            }
            //we have encountered something other than zero or i>length
            mxcont = Math.Max(mxcont, curr);
        }
        return mxcont;
    }
    #endregion

    #region 2 Sum

    //Naive Approach S=>O(1) and T=>O(N^2)
    //run two loops and check

    //Better S=>O(1)if we only need to prnit yes or no else O(N) not good in this case and T=>O(NLogN) +O(N)
    //sort the array and then use concept of two pointers
    public int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++)
        {
            temp[nums[i]] = i;
        }
        Array.Sort(nums);
        int p1 = 0;
        int p2 = nums.Length - 1;
        while (p1 < p2)
        {
            if (nums[p1] + nums[p2] < target)
                p1++;
            else if (nums[p1] + nums[p2] > target)
                p2--;
            else
                return new int[] { temp[nums[p1]], temp[nums[p2]] };
        }
        return new int[2];
    }

    //Optimal solution S=>O(N) and T+>O(N)
    //store the difference in hashmap and then if the
    //number is present in hashmap return the solution

    #endregion

    #region Longest Consecutive Sequence
    //Naive SOlution S=>O(1) and T=>O(N^2)
    //using two loops and check for the number plus 1

    //Naive solution S=>O(1) and T=>O(NLogn+N)
    //sort it and then using a while loop calculate

    //Optimal Solution S=>O(N) and T=>O(N)
    //store it in a map and then if prev is present of the current skip
    //otherwise you check for curr+1 until not found

    public int LongestConsecutive(int[] nums)
    {
        if (nums.Length == 0)
            return 0;
        Dictionary<int, int> map = new Dictionary<int, int>();
        foreach (int i in nums)
        {
            map[i] = 1;
        };
        int longest = 1;
        foreach (KeyValuePair<int, int> kp in map)
        {
            if (!map.ContainsKey(kp.Key - 1))
            {
                int curr_longest = 1;
                int curr_value = kp.Key + 1;
                while (map.ContainsKey(curr_value))
                {
                    curr_longest++;
                    curr_value += 1;
                }
                longest = Math.Max(longest, curr_longest);
            }
        }
        return longest;
    }
    #endregion

    #region Merge Overlapping sub intervals

    //Naive Solution S=>O(N) and T=>O(NLogN)+O(2N)
    //sort it then using two loops check for every element
    public int[][] Merge(int[][] intervals)
    {
        int n = intervals.Length;
        //using Linq we are sorting
        List<int[]> ls = new List<int[]>();
        intervals = intervals.OrderBy(inner => inner[0]).ToArray();
        int i = 0;
        while (i < n)
        {
            for (int j = i; j < n; j++)
            {
                while (i < n && j < n && intervals[j][0] <= intervals[i][1])
                {
                    intervals[i][1] = Math.Max(intervals[j][1], intervals[i][1]);
                    j++;
                }
                ls.Add(new int[] { intervals[i][0], intervals[i][1] });
                i = j == i ? i : j;
            }
        }
        return ls.ToArray();
    }

    //Optimal Solution S=>O(N) and T=>O(NLogN)+O(N)
    //using one loop only by comparing with ans array

    public int[][] MergeOptimal(int[][] intervals)
    {
        if (intervals.Length == 0 || intervals.Length == 1)
            return intervals;
        List<int[]> mergedintervals = new List<int[]>();
        intervals = intervals.OrderBy(inner => inner[0]).ToArray();
        foreach (int[] interv in intervals)
        {
            if (mergedintervals.Count == 0 || interv[0] > mergedintervals.Last()[1])
                mergedintervals.Add(interv);
            else
                mergedintervals.Last()[1] = Math.Max(mergedintervals.Last()[1], interv[1]);
        }
        return mergedintervals.ToArray();
    }
    #endregion

    #region Length of longest Subarray with zero/k sum

    //Naive Solution S=>O(1) and T=>O(N^2)
    //using two loops

    //Optimal Solution S=>O(N) and T=>O(N)
    //using prefix sum

    public int SubarraySum(int[] nums, int k = 0)
    {
        int sum = 0;
        int longest = 0;
        //the key is sum till i index and value is the index 
        Dictionary<int, int> dict = new Dictionary<int, int>() { { 0, -1 } };
        for (int curr = 0; curr < nums.Length; curr++)
        {
            sum += nums[curr];
            if (dict.ContainsKey(sum - k))
            {
                longest = Math.Max(longest, dict[sum - k] == -1 ? 1 : curr - dict[sum - k]);
            }
            if (!dict.ContainsKey(sum))
                dict.Add(sum, curr);
            //we are storing the first index only once that has the sum as we need the longest
            //length
            //for printing the number of subarray with given sum just replace
            //the index in dictionary with counts
        }
        return longest;
    }

    #endregion

    #region Longest Substring with unique characters

    //Naive Solution S=>O(N) and T=>O(N^2)
    //use two loops and check for every substring and a hash

    //Better Solution S=>O(N) and T=>O(2N) as left and right pointer both will travel the 
    //elements once.
    //using the left and right pointer and a hash

    //Optimal Solution S=>O(N) and T=>O(N)
    //using the left and right pointer and a hash and also storing their indexes in the hash
    //so we can jump left ptr directly to the index

    public int LengthOfLongestSubstring(string s)
    {
        Dictionary<char, int> d = new Dictionary<char, int>();
        int left = 0;
        int len = 0;
        int right = 0;
        while (right < s.Length)
        {
            if (d.ContainsKey(s[right]))
                //because sometimes the d[s[right]] + 1 can be not b/w l and r
                left = Math.Max(left, d[s[right]] + 1);
            if (d.ContainsKey(s[right]))
                d[s[right]] = right;
            else
                d.Add(s[right], right);
            len = Math.Max(len, right - left + 1);
            right += 1;
        }
        return len;
    }

    #endregion

    #region Search in a 2D matrix

    //Naive Solution S=>O(1) and T=>O(N*M)
    //search in entire matrix using two loops

    //Better Solution S=>O(1) and T=>O(n+LogM)
    //using binary search on all rows

    //Better Solution 2 S=>O(M*N) and T=>O(Log(M*N))
    //we can flatten the list by converting to 1D array 
    //and apply binary search

    //Best Solution S=>(1) and T=>O(Log(M*N))
    //using the pointer point to 1 row and last column
    public bool SearchMatrix(int[][] matrix, int target)
    {
        int i = 0;
        int j = matrix[0].Length - 1;
        while (i < matrix.Length && j >= 0)
        {
            int strt = matrix[i][j];
            if (strt < target)
                i += 1;
            else if (strt > target)
                j -= 1;
            else
                return true;
        }
        return false;
    }

    #endregion

    #region Merge Two Sorted Arrays 

    //Naive SOlution S=>O(N+M) and T=>O(N+M)
    //using a third array

    //Better Solution S=>O(1) and T=>O(min(N,M))+O(NLOgN+MLOgN)
    //put all smaller elements in arr1 and bigger in arr2 then sort individiually
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        int left = m - 1;
        int right = 0;
        while (right < n && left >= 0)
        {
            if (nums2[right] > nums1[left])
            {
                int temp = nums1[left];
                nums1[left] = nums2[right];
                nums2[right] = temp;
                left--;
                right++;
            }
            else
                break; //the elements are already correct
        }
        //we will sort now individually as nums1 contains all smaller elements
        //than nums2

        Array.Sort(nums2);
        Array.Sort(nums1);
    }


    //Optimal Solution S=>O(1) and T=>O(N+M+Log(N+M))
    //using gap method

    static int NextGap(int gap)
    {
        if (gap <= 1)
            return 0;
        //another method for getting the ceil withount ceiling function
        //as math.ceiling returns decimal
        return (gap / 2) + (gap % 2);
    }

    private static void MergeOptimal(int[] arr1, int[] arr2, int n,
                              int m)
    {
        int i, j, gap = n + m;
        for (gap = NextGap(gap); gap > 0; gap = NextGap(gap))
        {
            // comparing elements in the first 
            // array. 
            for (i = 0; i + gap < n; i++)
                if (arr1[i] > arr1[i + gap])
                {
                    int temp = arr1[i];
                    arr1[i] = arr1[i + gap];
                    arr1[i + gap] = temp;
                }

            // comparing elements in both arrays. 
            for (j = gap > n ? gap - n : 0; i < n && j < m;
                 i++, j++)
                if (arr1[i] > arr2[j])
                {
                    int temp = arr1[i];
                    arr1[i] = arr2[j];
                    arr2[j] = temp;
                }

            if (j < m)
            {
                // comparing elements in the 
                // second array. 
                for (j = 0; j + gap < m; j++)
                    if (arr2[j] > arr2[j + gap])
                    {
                        int temp = arr2[j];
                        arr2[j] = arr2[j + gap];
                        arr2[j + gap] = temp;
                    }
            }
        }
    }

    #endregion

    #region Majority Elements n/2

    //Naive SOlution S=>O(1) and T=>O(N^2)
    //using two loops and comparing 

    //Better Solution S=>O(N) and T=>O(N)
    //using a map and maintaining the count

    //Optimal Solution S=>O(1) and T=>O(N)
    //using Moores voting algo

    public int MajorityElement(int[] nums)
    {
        int candidate = 0;
        int occurence = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (occurence == 0)
            {
                candidate = nums[i];
            }
            if (candidate == nums[i])
            {
                occurence += 1;
            }
            else
            {
                occurence -= 1;
            }
        }
        //need this code only when there may be
        //case when majority does not exist
        //return occurence > nums.Length / 2 ? candidate : -1;
        return candidate;
    }
    #endregion

    #region Majority element n/3
    //at maximimum 2 integer can be the ans
    //take floor of len/3 and the integer having count > than this are ans also break the loop
    //as soon as your array has 2 integers

    //Naive Solution S=>O(1) and T=>O(N^2)
    //using two loops and comparing 

    //Better Solution S=>O(N) and T=>O(N)
    //using a map and maintaining the count

    //Optimal Solution S=>O(1) and T=>O(2N)
    //using Moores voting algo

    public List<int> MajorityElementn3(int[] nums)
    {
        int candidate1 = int.MinValue, candidate2 = int.MinValue;
        int occurence1 = 0, occurence2 = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (occurence1 == 0 && nums[i] != candidate2)
            {
                candidate1 = nums[i];
                occurence1 = 1;
            }
            else if (occurence2 == 0 && nums[i] != candidate1)
            {
                candidate2 = nums[i];
                occurence2 = 1;
            }
            else if (candidate1 == nums[i])
            {
                occurence1 += 1;
            }
            else if (candidate2 == nums[i])
            {
                occurence2 += 1;
            }
            else
            {
                occurence2--;
                occurence1--;
            }
        }
        //we are not sure whether the ans is definitelt these two
        //so we check in the below step
        List<int> ans = new List<int>();
        int cnt1 = 0, cnt2 = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] == candidate1) cnt1++;
            if (nums[i] == candidate2) cnt2++;
        }
        var mini = (nums.Length / 3) + 1;
        if (cnt1 >= mini) ans.Add(candidate1);
        if (cnt2 >= mini) ans.Add(candidate2);
        return ans;
    }

    #endregion

    #region Reverse Pairs

    //Naive Solution S=>O(1) and T => O(N ^ 2)
    //using two loops and then checking the condition

    static void Merge(int[] arr, int low, int mid, int high)
    {
        // Calculate the lengths of the two subarrays
        int len1 = mid - low + 1;
        int len2 = high - mid;

        // Create temporary arrays to hold the subarrays
        int[] first = new int[len1];
        int[] second = new int[len2];

        // Copy data from the main array to the temporary arrays
        int k = low;
        for (int a = 0; a < len1; a++)
        {
            first[a] = arr[k++];
        }

        k = mid + 1;
        for (int a = 0; a < len2; a++)
        {
            second[a] = arr[k++];
        }

        // Merge the two subarrays back into the main array
        int i = 0;
        int j = 0;
        k = low;
        while (i < len1 && j < len2)
        {
            // Compare and merge elements in sorted order
            if (first[i] <= second[j])
            {
                arr[k++] = first[i++];
            }
            else
            {
                arr[k++] = second[j++];
            }
        }

        // Copy any remaining elements from the temporary arrays
        while (i < len1)
        {
            arr[k++] = first[i++];
        }

        while (j < len2)
        {
            arr[k++] = second[j++];
        }
    }

    // Function to count the reverse pairs using merge sort
    static int CountReversePairs(int[] arr, int low, int mid, int high)
    {
        int cnt = 0;
        int j = mid + 1;
        for (int i = low; i <= mid; i++)
        {
            while (i <= high && j <= high && arr[i] > 2 * arr[j])
            {
                j++;
            }
            cnt += (j - (mid + 1));
        }
        return cnt;
    }

    // Merge Sort function to sort the array and count reverse pairs
    public int MergeSort(int[] arr, int low, int high)
    {
        int cnt = 0;
        if (low >= high)
        {
            return cnt;
        }
        int mid = low + (high - low) / 2;

        // Recursively count reverse pairs in left and right halves
        cnt += MergeSort(arr, low, mid);
        cnt += MergeSort(arr, mid + 1, high);

        // Merge the sorted halves and count reverse pairs
        cnt += CountReversePairs(arr, low, mid, high);

        // Merge the two halves
        Merge(arr, low, mid, high);

            return cnt;
        }

    #endregion

    #region Find the duplicate in an array of N+1 integers

    //Naive Solution S=>(N*N) and T=>O(N*N)
    //by taking a new array and putting the first column in last column and so on

    //Optimal Solution S=>O(1) and T=>O(N*N)
    //by transposing and then reversing the row 
    public void Rotate(int[][] matrix)
    {
        int n = matrix.Length;
        int m = matrix[0].Length;
        Console.WriteLine(m);
        //transpose
        for (int i = 0; i < n; i++)
        {
            //optimised by taking i+1 as 
            //i==j no sense of swapping
            for (int j = i + 1; j < m; j++)
            {
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }
        //reverse
        for (int i = 0; i < n; i++)
        {
            int j = 0;
            while (j < m / 2)
            {
                (matrix[i][j], matrix[i][m - j - 1]) = (matrix[i][m - j - 1], matrix[i][j]);
                j++;
            }
        }
    }
    #endregion

    #region Find the Duplicate Number
    //Naive Solution S=>O(1) and T=>O(NLogN+N)
    //using sorting

    //Naive Solution S=>O(N) and T=>O(N)
    //using the hash map and keeping the counts

    //Optimal Solution  S=>O(1) and T=>O(N)
    //using XOR of every number with zero and then 
    //doing XOR for 1 to n+1

    //Optimal Solution S=>O(1) and T=>O(N)
    //using the Linked List Cycle method
    public int FindDuplicate(int[] nums) {
    int slow = nums[0];
    int fast = nums[0];
    slow=nums[slow];
        fast=nums[nums[fast]];
        while(slow!=fast){
            slow=nums[slow];
            fast = nums[nums[fast]];
        }
slow = nums[0];
while (slow != fast)
{
    slow = nums[slow];
    fast = nums[fast];
}
return slow;
    }
    #endregion

    #region POW(x,n)
    //Naive Solution S=>O(1) and T=>O(N)
    //using for loop 
    public double Pow(int x,int n)
    {
        double ans = 1.0;
        long nn = n;
        if (n < 0)
            nn = nn * -1;
        for (int i = 0; i < nn; i++)
        {
            ans = ans * x;
        }
        return n < 0 ? (1.00 / ans) : ans;
    }
        return cnt;
    }

    //Optimal Solution S=>O(1) and T=>O(logN)
    //using maths logic

    public double PowOptimal(int x, int n)
    {
        double ans = 1.0;
        //taking long to avoid overflow
        long nn = n;
        if (nn < 0) nn = -1 * nn;
        while (nn > 0)
        {
            if (nn % 2 == 1)
            {
                ans = ans * x;
                nn = nn - 1;
            }
            else
            {
                x = x * x;
                nn = nn / 2;
            }
        }
        if (n < 0) ans = (double)(1.0) / (double)(ans);
        return ans;
    }

    #endregion

    #region repeat and missing number

    //Naive Solution S=>O(1) and T=>O(N^2)
    //run a loop to 1 to n both inclusive and see if it 
    //repeat or missing

    //Better Solution S=>O(N) and T=>O(2N)
    //using hashing store number 0 to n from the array in a hash
    //number with value 2 is repeat and if not present in map 
    //it is missing

    //Optimal Solution S=>O(1) and T=>O(N)
    //using maths quadratic equations

    int[] findTwoElement(int[] arr, int n)
    {
        long sn = (n * (n + 1)) / 2;
        long s2n = ((n * (n + 1)) * (2 * n + 1)) / 6;

        long S = 0, S2 = 0;
        for (int i = 0; i < n; i++)
        {
            S += arr[i];
            S2 += (long)arr[i] * (long)arr[i];
        }

        //S-Sn = X-Y
        long val1 = S - sn;

        //S2-S2n = X^2-Y^2
        long val2 = S2 - s2n;

        //S2-S2n=(X-Y)(X+Y)
        val2 = val2 / val1;

        long x = (val1 + val2) / 2;
        long y = x - val1;

        int[] ans = new int[] { (int)x, (int)y };
        return ans;

    }

    //Optimal Solution S=>O(1) and T=>O(N)
    //using XOR we divide the numbers according to one bit where the bit was 1
    //this will create two groups one for 0 and other for 1 the answer would be accuring odd number of times
    //3 for repeating and 1 for missing
    int[] findTwoElement2(int[] arr, int n)
    {
        /* Will hold xor of all elements
        and numbers from 1 to n */
        int xor1;

        /* Will have only single set bit of xor1 */
        int set_bit_no;

        int i;
        int x = 0, y = 0;

        xor1 = arr[0];

        /* Get the xor of all array elements */
        for (i = 1; i < n; i++)
            xor1 = xor1 ^ arr[i];

        /* XOR the previous result with numbers from 
        1 to n*/
        for (i = 1; i <= n; i++)
            xor1 = xor1 ^ i;

        /* Get the rightmost set bit in set_bit_no */
        set_bit_no = xor1 & ~(xor1 - 1);

        /* Now divide elements in two sets by comparing
        rightmost set bit of xor1 with bit at same 
        position in each element. Also, get XORs of two
        sets. The two XORs are the output elements.The 
        following two for loops serve the purpose */
        for (i = 0; i < n; i++)
        {
            if ((arr[i] & set_bit_no) != 0)

                /* arr[i] belongs to first set */
                x = x ^ arr[i];

            else

                /* arr[i] belongs to second set*/
                y = y ^ arr[i];
        }
        for (i = 1; i <= n; i++)
        {
            if ((i & set_bit_no) != 0)

                /* i belongs to first set */
                x = x ^ i;

            else

                /* i belongs to second set*/
                y = y ^ i;
        }

        /* *x and *y hold the desired output elements */
        return new int[] { x, y };
    }
    #endregion

    #region Count inversion

    //Naive Solution S=>O(1) and T=>O(N^2)
    //using two loops

    //Optimal Solution S=>O(1) and T=>O(N+M)
    //using the merge sort 

    public long inversionCount(long[] arr, int N)
    {
        //Your Code Here
        return MergeSort(arr, 0, N - 1);
    }
    static long Merge(long[] arr, int low, int mid, int high)
    {
        List<long> temp = new List<long>();
        // Calculate the lengths of the two subarrays
        int left = low;
        int right = mid + 1;

        long cnt = 0;

        while (left <= mid && right <= high)
        {
            if (arr[left] <= arr[right])
            {
                temp.Add(arr[left]);
                left++;
            }
            else
            {
                temp.Add(arr[right]);
                cnt += (mid - left + 1);
                right++;
            }
        }
        while (left <= mid)
        {
            temp.Add(arr[left]);
            left++;
        }
        while (right <= high)
        {
            temp.Add(arr[right]);
            right++;
        }
        for (int i = low; i <= high; i++)
        {
            arr[i] = temp[i - low];
        }
        return cnt;
    }

    // Merge Sort function to sort the array and count reverse pairs
    public long MergeSort(long[] arr, int low, int high)
    {
        long cnt = 0;
        if (low >= high)
        {
            return cnt;
        }
        int mid = low + (high - low) / 2;

        // Recursively count reverse pairs in left and right halves
        cnt += MergeSort(arr, low, mid);
        cnt += MergeSort(arr, mid + 1, high);

        // Merge the sorted halves and count reverse pairs
        cnt += Merge(arr, low, mid, high);
        return cnt;
    }

    #endregion
    #region 4Sum

    //Naive SOlution S=>O(1) and T=>O(N^4)
    //using 4 loops 

    //Better Solution S=>O(N) and T=>O(N^3)
    //using 3 loops and storing the element in map
    //always remember only to store the elements between j and k in the map

    //Optimal solution S=>O(1) and T=>O(N^3)
    //using 2 loops and two pointer aproach.;

    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        var ans = new List<IList<int>>();
        Array.Sort(nums);
        int n = nums.Length;
        for (int i = 0; i < n; i++)
        {
            //skip duplicates
            if (i > 0 && nums[i] == nums[i - 1])
                continue;
            for (int j = i + 1; j < n; j++)
            {
                if (j != i + 1 && nums[j] == nums[j - 1])
                    continue;
                int k = j + 1;
                int l = n - 1;
                while (k < l)
                {
                    long sum = (long)nums[i] + (long)nums[j] + (long)nums[k] + (long)nums[l];
                    if (sum < target)
                        k++;
                    else if (sum > target)
                        l--;
                    else
                    {
                        ans.Add(new List<int>() { nums[i], nums[j], nums[k], nums[l] });
                        l--;
                        k++;
                        while (k < l && nums[k] == nums[k - 1])
                            k++;
                        while (k < l && nums[l] == nums[l + 1])
                            l--;
                    }
                }
            }
        }
        return ans;
    }

    #endregion

    #region flattening of linked list
    public class NodeSpecial
    {
        public int data;
        public NodeSpecial next;
        public NodeSpecial bottom;
        NodeSpecial(int x)
        {
            this.data = x;
            this.next = null;
            this.bottom = null;
        }
    }

    //Optimal Solution S=>O(N for recursion) and T=>O(N)
    //traversing to right and using mergesort logic

        
    public NodeSpecial MergeTwoList(NodeSpecial l1, NodeSpecial l2)
    {
        var temp = new NodeSpecial(-1);
        var root = temp;

        while (l1 != null && l2 != null)
        {
            if (l1.data >= l2.data)
            {
                temp.bottom = l2;
                l2 = l2.bottom;
            }
            else
            {
                temp.bottom = l1;
                l1 = l1.bottom;
            }
            temp = temp.bottom;
        }
        if (l1 != null)
            temp.bottom = l1;
        if (l2 != null)
            temp.bottom = l2;
        return root.bottom;
    }

    public NodeSpecial flatten(NodeSpecial head)
    {
        //your code here
        if (head == null || head.next == null)
            return head;
        head.next = this.flatten(head.next);
        head = this.MergeTwoList(head, head.next);
        return head;
    }

    #endregion

    #region count number of subarray with xor k
    //Naive Solution S=>O(1) and T=>O(N^3)
    static long subarrayXor(int[] arr, int n, int m)
    {
        int cnt = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = i; j < n; j++)
            {
                var xor = 0;
                for (int k = i; k < j; k++)
                {
                    xor = xor ^ arr[k];
                    if (xor == m)
                        cnt++;
                }
            }
        }
        return cnt;
    }

    //Better Solution S=>O(1) and T=>O(N^2)
    static long subarrayXorBetter(int[] arr,int n, int m)
    {
        int cnt = 0;
        for(int i = 0; i < n; i++)
        {
            var xor = 0;
            for(int j = i; j < n; j++)
            {
                xor = xor ^ arr[j];
                if (xor == m)
                    cnt++;
            }
        }
        return cnt;
    }

    //Optimal Solution S=>O(N) and T=>O(1)
    //using maths 
    static long subarrayXorOptimal(int[] arr, int n, int m)
    {
        Dictionary<int, int> map = new Dictionary<int, int>();
        int cnt = 0;
        map.Add(0, 1);
        var xor = 0;
        for(int i = 0; i < n; i++)
        {
            xor = xor ^ arr[i];
            if (map.ContainsKey(xor ^ m))
                cnt += map[xor ^ m];
            if (map.ContainsKey(arr[i]))
                map[arr[i]] += 1;
            else
                map.Add(arr[i], 1);
        }
        return cnt;
     }

    #endregion

}