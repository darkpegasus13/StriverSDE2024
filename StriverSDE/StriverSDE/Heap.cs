using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Heap
    {
        //Array representation of heap
        public void buildMaxHeap(List<int> arr, int N)
        {
            // Index of last non-leaf node
            int startIdx = (N / 2) - 1;

            // Perform reverse level order traversal
            // from last non-leaf node and heapify
            // each node
            for (int i = startIdx; i >= 0; i--)
            {
                MaxHeapify(arr, i, N);
            }
        }

        public void buildMinHeap(List<int> arr, int N)
        {
            // Index of last non-leaf node
            int startIdx = (N / 2) - 1;

            // Perform reverse level order traversal
            // from last non-leaf node and heapify
            // each node
            for (int i = startIdx; i >= 0; i--)
            {
                MinHeapify(arr, i, N);
            }
        }
        private List<int> heap = new List<int>();
        static void MinHeapify(List<int> arr, int i, int n)
        {
            int smallest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            // Compare the left child with the current smallest node.
            if (left < n && arr[left] < arr[smallest])
                smallest = left;
            // Compare the right child with the current smallest node.
            if (right < n && arr[right] < arr[smallest])
                smallest = right;
            // If the current node is not the smallest
            // swap it with the smallest child.
            if (smallest != i)
            {
                int temp = arr[i];
                arr[i] = arr[smallest];
                arr[smallest] = temp;
                // Recursively call minHeapify on the affected subtree.
                MinHeapify(arr, smallest, n);
            }
        }

        static void MaxHeapify(List<int> arr, int i, int n)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            // Compare the left child with the current smallest node.
            if (left < n && arr[left] > arr[largest])
                largest = left;
            // Compare the right child with the current smallest node.
            if (right < n && arr[right] > arr[largest])
                largest = right;
            // If the current node is not the smallest
            // swap it with the smallest child.
            if (largest != i)
            {
                int temp = arr[i];
                arr[i] = arr[largest];
                arr[largest] = temp;
                // Recursively call minHeapify on the affected subtree.
                MaxHeapify(arr, largest, n);
            }
        }
        private void Swap(int i, int j)
        {
            int temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
        public void InsertMinHeap(int value)
        {
            heap.Add(value);
            int index = heap.Count - 1;
            while (index > 0
                   && heap[(index - 1) / 2] > heap[index])
            {
                Swap(index, (index - 1) / 2);
                index = (index - 1) / 2;
            }
        }

        public void DeleteMaxHeap(int value, List<int> heap)
        {
            int index = heap.IndexOf(value);
            if (index == -1)
            {
                return;
            }
            heap[index] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            while (true)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int largest = index;
                if (leftChild < heap.Count
                    && heap[leftChild] > heap[largest])
                {
                    largest = leftChild;
                }
                if (rightChild < heap.Count
                    && heap[rightChild] > heap[largest])
                {
                    largest = rightChild;
                }
                if (largest != index)
                {
                    Swap(index, largest);
                    index = largest;
                }
                else
                {
                    break;
                }
            }
        }

        #region Kth Largest/smallest element

        //Naive Solution S=>O(1) and T=>O(NlogN)
        //Sorting and then return the Kth element

        //Better SOlution S=>O(k) and T=>O(n+klogk)
        //Using Heapify function and returning the first 
        //element in array representation of min/max heap

        public int FindKthLargest(int[] nums, int k)
        {
            var ans = nums.ToList<int>();
            //    MinHeapify(ans, 0, nums.Length);
            //    heap = ans;
            //    List<int> copy=new List<int>(); 
            //    ans.CopyTo(nums);
            //    for (int i = 0; i < k - 1; i++)
            //        Delete(nums[i]);
            //    return ans[k];
            int n = nums.Length;
            buildMaxHeap(ans, ans.Count);
            for (int i = 0; i < k - 1; i++)
                DeleteMaxHeap(heap[0], ans);
            return heap[0];
        }

        //Optimal Solution S=>O(1) and T=>O(N)- avg O(n^2)
        //Using QuickSelect Algo
        public int FindKthLargestOptimal(int[] nums, int k)
        {
            k = nums.Length - k;
            return QuickSelect(0, nums.Length - 1, k, nums);
        }
        public int QuickSelect(int left, int right, int k, int[] nums)
        {
            int pivot = nums[right], p = left;
            for (int i = left; i < right; i++)
            {
                if (nums[i] <= pivot)
                {
                    (nums[p], nums[i]) = (nums[i], nums[p]);
                    p++;
                }
            }
            (nums[p], nums[right]) = (nums[right], nums[p]);
            if (p > k)
                return QuickSelect(left, p - 1, k, nums);
            else if (p < k)
                return QuickSelect(p + 1, right, k, nums);
            else
                return nums[p];
        }

        #endregion

        #region Maximum Sum Combination

        //Optimal Solution S=>O(1) and T=>O(Clog(min(A,B)))
        //Using extract max function from both the heaps after heapifying the array 

        public List<int> MaxCSumCombo(List<int> A, List<int> B, int C)
        {
            var ans = new List<int>();
            buildMaxHeap(A, A.Count);
            buildMaxHeap(B, B.Count);
            for (int i = 0; i < C; i++)
            {
                ans.Add(A[0] + B[0]);
                DeleteMaxHeap(A[0], A);
                DeleteMaxHeap(B[0], B);
            }
            return ans;
        }
        #endregion

        #region Top K elements

        //Naive Solution S=>O(1) and T=>O(NLogN)
        //sort the array and then only print each element once
        //till k count is matched

        //Better Solution S=>(N) and T=>O(KlogN)
        //using a MaxHeap and storing the counts as nodes in it
        //and getting the mapped keys from hash aur we can use a modified heap

        //Optimal Solution S=>O(N) and T=>O(N)
        //using modified bucket sort keeping count as the index
        //and storing key which have that count in that particular index
        public int[] TopKFrequent(int[] nums, int k)
        {
            var ans = new List<int>();
            Dictionary<int, int> sd = new Dictionary<int, int>();
            List<List<int>> frequencyArray = new List<List<int>>();
            for (int j = 0; j <= nums.Length; j++)
                frequencyArray.Add(new List<int>());
            for (int j = 0; j < nums.Length; j++)
            {
                if (sd.ContainsKey(nums[j]))
                    sd[nums[j]] += 1;
                else
                    sd.Add(nums[j], 1);
            }
            foreach (var curr in sd)
            {
                if (frequencyArray[curr.Value].Count == 0)
                    frequencyArray[curr.Value] = new List<int> { curr.Key };
                else
                    frequencyArray[curr.Value].Add(curr.Key);
            }
            int i = nums.Length;
            while (k != 0)
            {
                if (frequencyArray[i].Count != 0)
                {
                    int cntr = 0;
                    while (k != 0 && cntr < frequencyArray[i].Count)
                    {
                        ans.Add(frequencyArray[i][cntr]);
                        cntr++;
                        k--;
                    }
                }
                i--;
            }
            return ans.ToArray();
        }
        #endregion

        #region Find Median from data stream

        //Naive solution S=>O(1) and T=>O(N*NlogN)
        //By maintaining sorted order

        //please try afterwards not able to pass testcases

        //Better Solution S=>O(n) and T=>O(Nlogn)
        //using a min heap and max heap and the custom logic
        //using sorted set here but better to use as heaps later 
        //on implement it

        public class MedianFinder
        {

            SortedSet<int> g;
            SortedSet<int> s;
            public MedianFinder()
            {
                // Declaring two min heap and treating one as max heap
                g = new SortedSet<int>();
                s = new SortedSet<int>();
            }

            public void InsertMinHeap(int value)
            {
                // Negation for treating it as max heap
                s.Add(value);
                g.Add(s.Max);
                s.Remove(s.Max);
                if (g.Count > s.Count)
                {
                    s.Add(g.Min);
                    g.Remove(g.Min);
                }
            }

            public void AddNum(int num)
            {
                InsertMinHeap(num);
            }

            public double FindMedian()
            {
                if (s.Count < g.Count)
                    return g.Min;
                else if (s.Count > g.Count)
                    return s.Max;
                else
                    return((g.Min + s.Max) / 2.0);
            }
        }
        #endregion

        #region Merge K sorted arrays

        //Naive Solution S=>O(K.length sum of all arrays length) and T=>O(K*NlogN)
        //merge two arrays and applysorting

        //Better Solution S=>O(K.length sum of all arrays length) and T=>O(KlogN)
        //merge two arrays using merge sort

        //Optimal Solution
        //using a min heap and using merge sort logic
        public class MinHeapNode
        {
            public int element; // The element to be stored 

            // index of the array from 
            // which the element is taken 
            public int i;

            // index of the next element 
            // to be picked from array 
            public int j;

            public MinHeapNode(int element, int i, int j)
            {
                this.element = element;
                this.i = i;
                this.j = j;
            }
        };

        // A class for Min Heap 
        public class MinHeap
        {
            MinHeapNode[] harr; // Array of elements in heap 
            int heap_size; // Current number of elements in min heap 

            // Constructor: Builds a heap from 
            // a given array a[] of given size 
            public MinHeap(MinHeapNode[] a, int size)
            {
                heap_size = size;
                harr = a;
                int i = (heap_size - 1) / 2;
                while (i >= 0)
                {
                    MinHeapify(i);
                    i--;
                }
            }

            // A recursive method to heapify a subtree 
            // with the root at given index This method 
            // assumes that the subtrees are already heapified 
            void MinHeapify(int i)
            {
                int l = left(i);
                int r = right(i);
                int smallest = i;

                if (l < heap_size
                    && harr[l].element < harr[i].element)
                    smallest = l;

                if (r < heap_size
                    && harr[r].element < harr[smallest].element)
                    smallest = r;

                if (smallest != i)
                {
                    swap(harr, i, smallest);
                    MinHeapify(smallest);
                }
            }

            // to get index of left child of node at index i 
            int left(int i) { return (2 * i + 1); }

            // to get index of right child of node at index i 
            int right(int i) { return (2 * i + 2); }

            // to get the root 
            MinHeapNode getMin()
            {
                if (heap_size <= 0)
                {
                    Console.WriteLine("Heap underflow");
                    return null;
                }
                return harr[0];
            }

            // to replace root with new node 
            // "root" and heapify() new root 
            void replaceMin(MinHeapNode root)
            {
                harr[0] = root;
                MinHeapify(0);
            }

            // A utility function to swap two min heap nodes 
            void swap(MinHeapNode[] arr, int i, int j)
            {
                MinHeapNode temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }

            // A utility function to print array elements 
            static void printArray(int[] arr)
            {
                foreach (int i in arr) Console.Write(i + " ");
                Console.WriteLine();
            }

            // This function takes an array of 
            // arrays as an argument and All 
            // arrays are assumed to be sorted. 
            // It merges them together and 
            // prints the final sorted output. 
            static void mergeKSortedArrays(int[,] arr, int K)
            {
                MinHeapNode[] hArr = new MinHeapNode[K];
                int resultSize = 0;
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    MinHeapNode node
                        = new MinHeapNode(arr[i, 0], i, 1);
                    hArr[i] = node;
                    resultSize += arr.GetLength(1);
                }

                // Create a min heap with k heap nodes. 
                // Every heap node has first element of an array 
                MinHeap mh = new MinHeap(hArr, K);

                int[] result
                    = new int[resultSize]; // To store output array 

                // Now one by one get the minimum element 
                // from min heap and replace it with 
                // next element of its array 
                for (int i = 0; i < resultSize; i++)
                {

                    // Get the minimum element and 
                    // store it in result 
                    MinHeapNode root = mh.getMin();
                    result[i] = root.element;

                    // Find the next element that will 
                    // replace current root of heap. 
                    // The next element belongs to same 
                    // array as the current root. 
                    if (root.j < arr.GetLength(1))
                        root.element = arr[root.i, root.j++];

                    // If root was the last element of its array 
                    else
                        root.element = int.MaxValue;

                    // Replace root with next element of array 
                    mh.replaceMin(root);
                }
                printArray(result);
            }
        }
        #endregion

        #region Kth largest in stream

        //Naive SOlution S=>O(1) and T=>O(N*NlogN+K)
        //when adding sort them and return the 
        //kth element

        public class KthLargest
        {
            List<int> arr;
            int k1;
            public KthLargest(int k, int[] nums)
            {
                k1 = k;
                Array.Sort(nums);
                Array.Reverse(nums);
                arr = nums.ToList();
            }

            public int Add(int val)
            {
                var n = k1 - 1;
                arr.Add(val);
                arr = arr.OrderByDescending(x => x).ToList();
                return arr[n];
            }
        }

        //Optimal Solution S=>O(1) and T=>O(n*Logn)
        //using heaps implement extrtact min and top in heap to get this working

        //public class KthLargestOptimal
        //{
        //    Heap arr;
        //    int k1;
        //    public KthLargestOptimal(int k, int[] nums)
        //    {
        //        arr = new Heap();
        //        k1 = k;
        //        arr.buildMinHeap(nums.ToList(), nums.Length);
        //        //for(int i=0;i<nums.Length-k;i++)
        //            //arr.extractmin();
        //    }

        //    public int Add(int val)
        //    {
        //        var n = k1 - 1;
        //        arr.InsertMinHeap(val);
        //        arr.extractmin();
        //        return //arr.Top();
        //    }
        //}


        #endregion
    }
}
