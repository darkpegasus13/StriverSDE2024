using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class BinarySearch
    {
        #region Nth Root Of a Number

        //Naive Solution S=>O(1) and T=>O(M)
        //by going through the number 

        public int NthRoot(int n, int m)
        {
            for(int i = 1; i <= m; i++)
            {
                var temp = (int)Math.Pow((double)i, (double)n);
                if ( temp== m)
                    return m;
                if (temp>m)
                    return -1;
            }
            return -1;
        }

        //Optimal Solution S=>O(1) and T=>O(logM)
        //by going through the number and using binary search

        public int NthOptimalHelper(int mid,int n, int m)
        {
            long ans = 1;
            for(int i=1;i<=n ; i++)
            {
                ans = ans * mid;
                if (ans > m)
                    return 2;
            }
            if (ans == m)
                return 1;
            return 0;
        }
        public int NthRootOptimal(int n, int m)
        {
            int left = 1;
            int right = m;
            while(left<=right)
            {
                int mid = (left + right) / 2;
                var temp = this.NthOptimalHelper(mid, n, m);
                if (temp == 1)
                    return mid;
                else if (temp ==0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }
        #endregion

        #region Median Of Row Wise sorted matrix

        //Brute Force S=>O(row*col) and (row*colLog(col*row))
        //store all the element in linear array then sort
        //and find the middle element

        // Optimal SOlution S=>O(1) and T=>O(row*Log(col))
        //

        public int CountSmallerThanMid(List<int> A,int mid)
        {
            int l = 0, h = A.Count - 1;
            while (l <= h)
            {
                int md = (l + h) / 2;
                if (A[md] <= mid)
                    l = md + 1;
                else
                    h = md - 1;
            }
            return l;
        }

        public int MedianOptimal(List<List<int>> matrix, int R, int C)
        {
            //Your code here
            int low = 1;
            int high = int.MaxValue;
            int n = R;
            int m = C;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                int cnt = 0;
                for (int i = 0; i < n; i++)
                    cnt += CountSmallerThanMid(matrix[i], mid);
                if (cnt <= (n * m) / 2)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return low;
        }

        #endregion

        #region Search Single element in an sorted array
        //these solution works only for sorted array IMPORTANT
        //Naive Solution S=>O(1) and T=>O(N)
        //check the previous and next element if they are not same it is the answer
        //just keep in mind the edge cases start and end and if array is 1 or 2 length long

        //Better Solution S=>O(1) and T=>O(N) 
        //check do the xor of all the elements

        //OPtimal Solution S=>O(1) and T=>O(Nlog(N))
        //by using concept of (even,odd) & (odd,even) and removal of left or right half
        public int SingleNonDuplicate(int[] nums)
        {
            int n = nums.Length;
            if (n == 1)
                return nums[0];
            if (nums[0] != nums[1])
                return nums[0];
            if (nums[n - 1] != nums[n - 2])
                return nums[n - 1];
            int low = 1, high = n - 2;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (nums[mid] != nums[mid - 1] && nums[mid] != nums[mid + 1])
                    return nums[mid];
                //we are in left
                if((mid%2==1 && nums[mid]==nums[mid-1]) || (mid % 2 == 0 && nums[mid] == nums[mid + 1]))
                        low = mid + 1; //eliminated the left half
                else
                    high = mid - 1; //eliminated the right half
            }
            return -1;
        }

        #endregion

        #region search in sorted rotated array

        //Naive Solution S=>O(1) and T=>O(N)
        //using linear solution

        //Optimal solution S=>O(1) and T=>O(LogN)
        //using binary search

        public int SearchRotated(int[] nums, int target)
        {
            int low = 0;
            int high = nums.Length - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (nums[mid] == target)
                    return mid;
                if (nums[low] <= nums[mid])
                {
                    if (target >= nums[low] && target <= nums[mid])
                        high = mid - 1;
                    else
                        low = mid + 1;
                }
                else
                {
                    if (target >= nums[mid] && target <= nums[high])
                        low = mid + 1;
                    else
                        high = mid - 1;
                }
            }
            return -1;
        }
        #endregion

        #region Median Of Two Sorted Arrays

        //Naive Solution S=>O(m+n) and T=>O(m+n)
        //merge(use merge step of merge sort) both arrays and then sort

        //Better Solution S=>O(1) and T=>O(m+n)
        //instead of using auxillary array we will use 

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int n1 = nums1.Length;
            int n2 = nums2.Length;
            int n = n1 + n2;
            int ind2 = n/2 ;
            int ind1 = ind2 - 1;
            int cnt = 0;
            int ind1el = -1, ind2el = -1;

            int i = 0, j = 0;
            while(i<n1 && j < n2)
            {
                if (nums1[i] < nums2[j])
                {
                    if (cnt == ind1)
                        ind1el = nums1[i];
                    if (cnt == ind2)
                        ind2el = nums1[i];
                    i++;
                }
                else
                {
                    if (cnt == ind1)
                        ind1el = nums2[j];
                    if (cnt == ind2)
                        ind2el = nums2[j];
                    j++;
                }
                cnt++;
            }

            while (i < n1)
            {
                if (cnt == ind1)
                    ind1el = nums1[i];
                if (cnt == ind2)
                    ind2el = nums1[i];
                cnt++;
                i++;
            }

            while (j < n2)
            {
                if (cnt == ind1)
                    ind1el = nums2[j];
                if (cnt == ind2)
                    ind2el = nums2[j];
                cnt++;
                j++;
            }

            if (n % 2 == 0)
                return (double)((double)((ind2el + ind1el)) / 2.0);
            else
                return (double)ind2el;
        }

        //Optimal Solution S=>O(1) and T=>O(Log(min(n1,n2)))
        //By applying partition and cross comparing the left and right sides

        public double FindMedianSortedArraysOPtimal(int[] nums1, int[] nums2)
        {
            int n1 = nums1.Length, n2 = nums2.Length;
            if (n1 > n2)
                return FindMedianSortedArraysOPtimal(nums2, nums1);
            int n = n1 + n2;
            int left = (n + 1) / 2;
            int low = 0, high = n1;
            while (low <= high)
            {
                int mid1 = (low + high) / 2;
                int mid2 = left - mid1;
                int l1 = (mid1 > 0) ? nums1[mid1 - 1] : int.MinValue;
                int l2 = (mid2 > 0) ? nums2[mid2 - 1] : int.MinValue;
                int r1 = (mid1 < n1) ? nums1[mid1] : int.MaxValue;
                int r2 = (mid2 < n2) ? nums2[mid2] : int.MaxValue;

                if (l1 <= r2 && l2 <= r1)
                {
                    if (n % 2 == 1)
                        return Math.Max(l1, l2);
                    else
                        return (double)(Math.Max(l1, l2) + Math.Min(r1, r2)) / 2.0;
                }
                else if (l1 > r2)
                    high = mid1 - 1;
                else
                    low = mid1 + 1;
            }
            return 0;
        }
        #endregion

        #region Find Kth element in two sorted arrays
        //Naive Solution S=>O(N+M) an T=>O(N+M+(N+M)log(N+M))
        //Copy the two arrays in aux array and then sort

        //Optimal Solution S=>O(1) and T=>O(Log(min(n1,n2)))
        //just an extension of median of two sorted arrays
        public long kthElement(int[] arr1, int[] arr2, int n, int m, int k)
        {
            if (m > n)
                return kthElement(arr2, arr1,n,m,k);
            int low = Math.Max(0,k-m), high = Math.Min(k,n);
            while (low <= high)
            {
                int mid1 = (low + high) / 2;
                int mid2 = k - mid1;
                int l1 = (mid1 == 0) ? int.MinValue :arr1[mid1 - 1];
                int l2 = (mid2 == 0) ? int.MinValue :arr2[mid2 - 1];
                int r1 = (mid1 == n) ? int.MaxValue :arr1[mid1];
                int r2 = (mid2 == m) ? int.MaxValue :arr2[mid2];

                if (l1 <= r2 && l2 <= r1)
                    return Math.Max(l1, l2);
                else if (l1 > r2)
                    high = mid1 - 1;
                else
                    low = mid1 + 1;
            }
            return -1;
        }
        #endregion

        #region Allocate Books

        //Naive Solution S=>O(1) and T=>O(N*(sum(arr[])=max(arr[])+1))
        //by taking the maximum in array and then compute 
        //how many students will be required for the same 

        public int CountStudent(List<int> arr,int pages)
        {
            int n = arr.Count;
            int students = 1;
            long pagesStudent = 0;
            for(int i = 0; i < n; i++)
            {
                if (pagesStudent + arr[i] <= pages)
                    pagesStudent += arr[i];
                else
                {
                    students++;
                    pagesStudent = arr[i];
                }
            }
            return students;
        }

        public int FindPages(List<int> arr,int n, int m )
        {
            if (m > n)
                return -1;
            int low = arr.Max();
            int high = arr.Sum();
            for(int i = low; i <= high; i++)
            {
                if (CountStudent(arr, i) == m)
                    return i;
            }
            return low;
        }

        //Optimal SOlution S=>O(1) and T=>O(N*log(sum(arr[])-max(arr[])+1))
        //using same logic but using binary search

        public int FindPagesOptimal(List<int> arr, int n, int m)
        {
            if (m > n)
                return -1;
            int low = arr.Max();
            int high = arr.Sum();
            while(low<=high){
                int mid = (low + high) / 2;
                int students = CountStudent(arr,mid);
                if (students > m)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return low;
        }
        #endregion

        #region Aggressive Cows

        //Naive Solution S=>O(1) and T=>O(NLogN)+O(N*(max(stalls)-min(stalls)))
        //ans will always between 1 to max-min of arr
        public bool CanWePlace(int[] stalls, int cows, int currdist)
        {
            int cntCows = 1;
            int last = stalls[0];
            for (int i = 1; i < stalls.Length; i++)
            {
                if (stalls[i] - last >= currdist)
                {
                    cntCows++;
                    last = stalls[i];
                }
                if (cntCows >= cows)
                    return true;
            }
            return false;
        }

        //Optimal Solution
        //using binary search
        public int AggressiveCows(int[] stalls, int k)
        {
            int n = stalls.Length;
            Array.Sort(stalls);
            int high = stalls[n - 1],low= stalls[0];
            while (low <= high) {
                int mid = (low + high) / 2;
                if (this.CanWePlace(stalls, k, mid) == true)
                    low = mid + 1;
                else
                    high = mid - 1;
            }
            return high;
        }

        #endregion
    }
}
