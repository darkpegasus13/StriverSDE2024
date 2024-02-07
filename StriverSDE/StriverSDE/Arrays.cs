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
    public IList<IList<int>> Generate(int numRows) {
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
    public void NextPermutation(int[] nums) {
        //We are finding a break point that is a[i]<a[i+1]
        int indx=-1;
        int n = nums.Length;
        for(int i=n-2;i>=0;i--){
            if(nums[i]<nums[i+1]){
                indx=i;
                break;
            }
        }
        //if it is already the largest
        if(indx==-1){
            Array.Reverse(nums);
            return;
        }
        int swapIndx=-1;
        for(int i=n-1;i>indx;i--){
            if(nums[i]>nums[indx]){
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
        (nums[swapIndx],nums[indx]) = (nums[indx],nums[swapIndx]);
        //reverse the array from indx+1 to end
        //as it was a increasing sequence
        Array.Reverse(nums,indx+1,n-indx-1);
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
                ansEnd= i;
            }
            //whenever meh<0 we make it 0
            if (meh < 0)
                meh = 0;
        }
        for(int i = ansStrt; i <= ansEnd; i++)
            Console.WriteLine(nums[i]);
    }
    #endregion
}