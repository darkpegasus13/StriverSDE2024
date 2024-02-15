using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Greedy
    {
        

        //Optimal Solution S=>O(N) and T=>O(N)+O(NlogN)+O(N)
        //by sorting the meets and finding if start time does not lie in the 
        //end time of previous meeting
        #region N Meetings in a room
        public int maxMeetings(int[] start, int[] end, int n)
        {
            Tuple<int, int>[] meetings = new Tuple<int, int>[n];
            for (int i = 0; i < n; i++)
                meetings[i] = (new Tuple<int, int>(start[i], end[i]));
            Array.Sort(meetings, (a, b) =>
            {
                if (a.Item2 > b.Item2)
                    return 1;
                else if (a.Item2 < b.Item2)
                    return -1;
                else
                    return 0;
            });
            int res = 1;
            var prev = meetings[0];
            for (int i = 1; i < n; i++)
            {
                if (meetings[i].Item1 > prev.Item2)
                {
                    //adding one to result and only updating prev when we 
                    //meet the criteria
                    res += 1;
                    prev = meetings[i];
                }
            }
            return res;
        }
        #endregion

        #region min railway platforms needed

        //Naive Solution S=>O(1) and T=>O(N^2) 
        //using nested loops find the number of intervals 
        //that is enclosed in the interval or if it is itself closed in interval

        //Optimal Solution S=>O(1) and T=>O(2NLogN)+O(2N)
        //using 2 pointer approach and sortng the arrival and departure arrays
        //random index after sorting doesn't matter as we are interested in only the arrival and departure
        public int findPlatform(int[] arr, int[] dep, int n)
        {
            // Sort arrival and departure arrays
            Array.Sort(arr);
            Array.Sort(dep);

            // plat_needed indicates number of
            // platforms needed at a time
            int plat_needed = 1, result = 1;
            int i = 1, j = 0;

            // Similar to merge in merge sort
            // to process all events in sorted
            // order
            while (i < n && j < n)
            {

                // If next event in sorted order
                // is arrival, increment count
                // of platforms needed
                if (arr[i] <= dep[j])
                {
                    plat_needed++;
                    i++;
                }

                // Else decrement count of
                // platforms needed
                else if (arr[i] > dep[j])
                {
                    plat_needed--;
                    j++;
                }

                // Update result if needed
                if (plat_needed > result)
                    result = plat_needed;
            }

            return result;

        }
        #endregion

        #region Job Scheduling

        //Better Approach S=>O(N) and T=>O(NlogN)+O(N*M)
        //
        public int[] JobScheduling(Job[] arr, int n)
        {
            //Your code here
            int[] auxArray = new int[n];
            int counter = 0;
            int profit = 0;
            Array.Sort(arr, (a, b) =>
            {
                if (a.profit > b.profit)
                    return -1;
                else if (a.profit < b.profit)
                    return 1;
                else
                    return 0;
            });
            for (int i = 0; i < n; i++)
            {
                var slot = arr[i].dead - 1;
                while (slot >= 0)
                {
                    if (auxArray[slot] == 0)
                    {
                        auxArray[slot] = arr[i].profit;
                        profit += arr[i].profit;
                        counter++;
                        break;
                    }
                    slot--;
                }
            }
            return new int[] { counter, profit };

        }
        #endregion

        #region Fractional Knapsack

        //Optimal Solution S=>O(N) and T=>O(NLogN)+O(N)
        //calculate profit/weight and then start with highest to lowest
        class itemComparator : IComparer<Item>
        {
            public int Compare(Item a, Item b)
            {
                double r1 = (double)(a.value) / (double)(a.weight);
                double r2 = (double)(b.value) / (double)(b.weight);
                if (r1 < r2) return 1;
                else if (r1 > r2) return -1;
                else return 0;
            }
        }
        public double fractionalKnapsack(int W, Item[] arr, int n)
        {
            Array.Sort(arr, new itemComparator());

            int curWeight = 0;
            float finalvalue = 0.0f;

            for (int i = 0; i < n; i++)
            {

                if (curWeight + arr[i].weight <= W)
                {
                    curWeight += arr[i].weight;
                    finalvalue += arr[i].value;
                }

                else
                {
                    int remain = W - curWeight;
                    finalvalue += ((float)arr[i].value / (float)arr[i].weight) * (float)remain;
                    break;
                }
            }

            return (double)finalvalue;
        }
        #endregion

        #region Minimum number of coins

        //Naive Solution S=>O(1) and T=>O(N)
        //works only for given arr fails when other cases
        //arr= new int[] { 1, 2, 5, 10, 20, 50, 100 }
        public int MinCoins(int value, int n, int[] arr)
        {
            int ptr = n-1;
            int coins = 0;
            while (value != 0)
            {
                if (value <= arr[ptr])
                {
                    int temp = value / arr[ptr];
                    value = value % arr[ptr];
                    coins += temp;
                }
                else
                {
                    ptr -= 1;
                }
            }
            return coins;
        }
        #endregion

        #region N Meetings in one room

        //Optimal Solution S=>O(N) and T=>O(NLogN)+O(N)
        public int MaxMeetings(int[] start, int[] end, int n)
        {
            Tuple<int, int>[] meetings = new Tuple<int, int>[n];
            for (int i = 0; i < n; i++)
                meetings[i] = (new Tuple<int, int>(start[i], end[i]));
            Array.Sort(meetings, (a, b) => {
                if (a.Item2 > b.Item2)
                    return 1;
                else if (a.Item2 < b.Item2)
                    return -1;
                else
                    return 0;
            });
            int res = 1;
            var prev = meetings[0];
            for (int i = 1; i < n; i++)
            {
                if (meetings[i].Item1 > prev.Item2)
                {
                    res += 1;
                    prev = meetings[i];
                }
            }
            return res;
        }
        #endregion
    }
    public class Job
    {
        public int id;
        public int dead;
        public int profit;
        public Job(int id, int dead, int profit)
        {
            this.id = id;
            this.dead = dead;
            this.profit = profit;
        }
    }

    public class Item
    {
        public int value;
        public int weight;
    }
}
