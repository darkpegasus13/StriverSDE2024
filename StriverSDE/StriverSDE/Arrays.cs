using System;
using System.Collections.Generic;

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
}