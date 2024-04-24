﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class StackandQueue
    {
        #region Balanced Paranthesis
        public bool IsValid(string s)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>(){
            {'(',')'},
            {'[',']'},
            {'{','}'}
        };
            Stack<char> st = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(' || c == '{' || c == '[')
                    st.Push(c);
                else
                    if (st.Count > 0 && c == dict[st.Peek()])
                    st.Pop();
                else
                    return false;
            }
            return st.Count == 0 ? true : false;
        }
        #endregion

        #region Next Greater Element
        //finding greater element in a circular array
        //Naive Solution S=>O(1) and T=>O(N^2)

        public int[] NGE(int[] arr)
        {
            //first part finds NGE in the right 
            int n = arr.Length;
            int[] ans = new int[n];
            Stack<int> st = new Stack<int>();
            //we can consider appending the same list to right of orignal array
            //but instead we will start from 2n
            for (int i = 2 * n - 1; i >= 0; i--)
            {
                if (st.Count == 0)
                    if (i < n)
                        ans[i] = -1;
                    else
                    {
                        while (st.Count > 0 && st.Peek() < arr[i % n])
                            st.Pop();
                        if (i < n)
                            ans[i] = st.Count == 0 ? -1 : st.Peek();
                    }
                st.Push(arr[i % n]);
            }
            return ans;
        }
        #endregion

        #region Sort A Stack Using Recursion

        public void sortedInsert(Stack<int> s, int x)
        {
            // Base case: Either stack is empty or
            // newly inserted item is greater than top
            // (more than all existing)
            if (s.Count == 0 || x > (int)s.Peek())
            {
                s.Push(x);
                return;
            }

            // If top is greater, remove
            // the top item and recur
            int temp = (int)s.Peek();
            s.Pop();
            sortedInsert(s, x);

            // Put back the top item removed earlier
            s.Push(temp);
        }

        // Method to sort stack
        public void sortStack(Stack<int> s)
        {
            // If stack is not empty
            if (s.Count > 0)
            {
                // Remove the top item
                int x = (int)s.Peek();
                s.Pop();

                // Sort remaining stack
                sortStack(s);

                // Push the top item back in sorted stack
                sortedInsert(s, x);
            }
        }
        #endregion

        #region online stock span
        //Optimal Solution S=>O(N) and T=>O(N)
        //by using a stack and storing the count and price in it
        public class StockSpanner
        {
            private Stack<(int, int)> stack;

            public StockSpanner()
            {
                stack = new Stack<(int, int)>();
            }

            public int Next(int price)
            {
                int span = 1;

                while (stack.Count > 0 && stack.Peek().Item1 <= price)
                    span += stack.Pop().Item2;

                stack.Push((price, span));

                return span;
            }
        }

        #endregion

        #region Next Greater Element

        public int[] NextGreaterElement(int[] nums1, int[] nums2)
        {
            Stack<int> st = new Stack<int>();
            Dictionary<int, int> d = new Dictionary<int, int>();
            int[] ans = Enumerable.Repeat(-1, nums1.Length).ToArray();
            for (int n = 0; n < nums1.Length; n++)
            {
                d.Add(nums1[n], n);
            }
            foreach (int num in nums2)
            {
                if (st.Count > 0)
                {
                    if (st.Count != 0 && num < st.Peek() && nums1.Contains(num))
                    {
                        st.Push(num);
                    }
                    else if (st.Count != 0 && num > st.Peek())
                    {
                        while (st.Count != 0 && st.Peek() < num)
                        {
                            int temp = st.Pop();
                            ans[d[temp]] = num;
                        }
                    }
                }
                if (nums1.Contains(num))
                    st.Push(num);
            }
            return ans;
        }
        #endregion

        #region Largest Area in a Histogram

        //Naive Solution S=>O(1) and T=>O(N*N)
        //by taking a start and end and calculate all possible areas

        public int largestArea(int[] arr, int n)
        {
            int maxArea = int.MinValue;
            for (int i = 0; i < n; i++)
            {

                int minHeight = int.MaxValue;
                for (int j = i; j < n; j++)
                {
                    minHeight = Math.Min(minHeight, arr[j]);
                    maxArea = Math.Max(maxArea, minHeight * (j - i + 1));
                }
            }
            return maxArea;
        }

        //Optimal Solution S=>O(N) and T=>O(N)
        //using concept of next greater element

        public int LargestRectangleArea(int[] heights)
        {
            int maxm = 0;
            Stack<int> st = new Stack<int>();
            int n = heights.Length;
            for (int i = 0; i <= n; i++)
            {
                while (st.Count != 0 && (i == n || heights[st.Peek()] >= heights[i]))
                {
                    int h = heights[st.Peek()];
                    st.Pop();
                    int w;
                    if (st.Count == 0)
                        w = i;
                    else
                        w = i - st.Peek() - 1;
                    maxm = Math.Max(maxm, h * w);
                }
                st.Push(i);
            }
            return maxm;
        }

        #endregion
        
    }
    #region Implement Stack using Array
    class Stack
    {
        private int[] ele;
        private int top;
        private int max;
        public Stack(int size)
        {
            ele = new int[size]; // Maximum size of Stack 
            top = -1;
            max = size;
        }

        public void push(int item)
        {
            if (top == max - 1)
            {
                Console.WriteLine("Stack Overflow");
                return;
            }
            else
            {
                ele[++top] = item;
            }
        }

        public int pop()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack is Empty");
                return -1;
            }
            else
            {
                Console.WriteLine("{0} popped from stack ", ele[top]);
                return ele[top--];
            }
        }

        public int peek()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack is Empty");
                return -1;
            }
            else
            {
                Console.WriteLine("{0} top from stack ", ele[top]);
                return ele[top];
            }
        }

        public void printStack()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack is Empty");
                return;
            }
            else
            {
                for (int i = 0; i <= top; i++)
                {
                    Console.WriteLine("{0} pushed into stack", ele[i]);
                }
            }
        }
    }
    #endregion

    #region Implement Queue using Array
    public class CircularQueue
    {

        // Declaring the class variables.
        private int size, front, rear;

        // Declaring array list of integer type.
        private List<int> queue = new List<int>();

        // Constructor
        CircularQueue(int size)
        {
            this.size = size;
            this.front = this.rear = -1;
        }

        // Method to insert a new element in the queue.
        public void enQueue(int data)
        {

            // Condition if queue is full.
            if ((front == 0 && rear == size - 1) ||
              (rear == (front - 1) % (size - 1)))
            {
                Console.Write("Queue is Full");
            }

            // condition for empty queue.
            else if (front == -1)
            {
                front = 0;
                rear = 0;
                queue.Add(data);
            }

            else if (rear == size - 1 && front != 0)
            {
                rear = 0;
                queue[rear] = data;
            }

            else
            {
                rear = (rear + 1);

                // Adding a new element if
                if (front <= rear)
                {
                    queue.Add(data);
                }

                // Else updating old value
                else
                {
                    queue[rear] = data;
                }
            }
        }

        // Function to dequeue an element
        // form th queue.
        public int deQueue()
        {
            int temp;

            // Condition for empty queue.
            if (front == -1)
            {
                Console.Write("Queue is Empty");

                // Return -1 in case of empty queue
                return -1;
            }

            temp = queue[front];

            // Condition for only one element
            if (front == rear)
            {
                front = -1;
                rear = -1;
            }

            else if (front == size - 1)
            {
                front = 0;
            }
            else
            {
                front = front + 1;
            }

            // Returns the dequeued element
            return temp;
        }

        // Method to display the elements of queue
        public void displayQueue()
        {

            // Condition for empty queue.
            if (front == -1)
            {
                Console.Write("Queue is Empty");
                return;
            }

            // If rear has not crossed the max size
            // or queue rear is still greater then
            // front.
            Console.Write("Elements in the circular queue are: ");

            if (rear >= front)
            {

                // Loop to print elements from
                // front to rear.
                for (int i = front; i <= rear; i++)
                {
                    Console.Write(queue[i]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }

            // If rear crossed the max index and
            // indexing has started in loop
            else
            {

                // Loop for printing elements from
                // front to max size or last index
                for (int i = front; i < size; i++)
                {
                    Console.Write(queue[i]);
                    Console.Write(" ");
                }

                // Loop for printing elements from
                // 0th index till rear position
                for (int i = 0; i <= rear; i++)
                {
                    Console.Write(queue[i]);
                    Console.Write(" ");
                }
                Console.Write("\n");
            }
        }
    }
    #endregion

    #region Implement Stack using two queues
    //Optimal SOlution
    //By using q2 as an auxillary array and in push copy from q1 to q2
    //and then q2 to q1 
    public class MyStack
    {
        private Queue<int> q1;
        private Queue<int> q2;

        public MyStack()
        {
            q1 = new Queue<int>();
            q2 = new Queue<int>();
        }

        public void Push(int x)
        {
            while (q1.Count > 0)
                q2.Enqueue(q1.Dequeue());

            q1.Enqueue(x);

            while (q2.Count > 0)
                q1.Enqueue(q2.Dequeue());
        }

        public int Pop()
        {
            return q1.Count > 0 ? q1.Dequeue() : -1;
        }

        public int Top()
        {
            return q1.Count > 0 ? q1.Peek() : -1;
        }

        public bool Empty()
        {
            return q1.Count == 0;
        }
        #region Rotten oranges

        //Optimal Approach S=>O(N) && T=>O(N*N)*4
        //by creating another visited matrix and keeping count
        //of fresh oranges
        public int OrangesRotting(int[][] grid)
        {
            if (grid == null || grid[0].Length == 0)
                return 0;
            int m = grid.Length, n = grid[0].Length;
            int fresh = 0;

            Queue<(int, int)> queue = new Queue<(int, int)>();

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] == 1)
                        fresh++;
                    else if (grid[i][j] == 2)
                        queue.Enqueue((i, j));
                }
            }
            //if already rotten or empty return true
            if (fresh == 0)
                return 0;

            int time = 0;
            int[,] dir = new int[,] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

            // check fresh before BFS traverse the graph
            while (queue.Count > 0 && fresh > 0)
            {
                time++;
                int size = queue.Count;
                for (int i = 0; i < size; i++)
                {
                    var curr = queue.Dequeue();
                    for (int j = 0; j < 4; j++)
                    {
                        int newRow = curr.Item1 + dir[j, 0];
                        int newCol = curr.Item2 + dir[j, 1];

                        if (newRow >= 0 && newRow < m && newCol >= 0 && newCol < n && grid[newRow][newCol] == 1)
                        {
                            grid[newRow][newCol] = 2;
                            queue.Enqueue((newRow, newCol));
                            fresh--;
                        }
                    }
                }
            }

            return fresh == 0 ? time : -1;
        }

        #endregion
    }
    #endregion

    #region Implement Queue using stack

    //First Solution
    //using two stacks and push is O(N)
    //we pop all element from 1st to 2nd then add new ele to 1
    //and add all elements from 2nd to 1st
    //assuming s1 and s2 are the stacks already created
    //in this case top and pop are O(1)
    //public void PushToStack(int ele)
    //{
    //    while (s1.Count > 0)
    //    {
    //        s2.Push(s1.Pop());
    //    }
    //    s1.Push(ele);
    //    while (s2.Count > 0)
    //    {
    //        s1.Push(s2.Pop());
    //    }
    //}

    //Second Solution
    //using two stacks and push is O(1)
    //we just normaly push to stack pop and top are O(N)

    //public void POPStack()
    //{
    //    while (s1.Count > 0)
    //    {
    //        s2.Push(s1.Pop());
    //    }
    //    s2.Pop();
    //    while (s2.Count > 0)
    //    {
    //        s1.Push(s2.Pop());
    //    }
    //}

    //public void TopStack()
    //{
    //    while (s1.Count > 0)
    //    {
    //        s2.Push(s1.Pop());
    //    }
    //    s2.Top();
    //    while (s2.Count > 0)
    //    {
    //        s1.Push(s2.Pop());
    //    }
    //}

    #endregion

    #region MinStack
    //Naive Solution S=>O(2N) and T=>O(1)
    //pushing the minimal and value in stack itself
    public class MinStack
    {

        Stack<Tuple<int, int>> st;
        public MinStack()
        {
            st = new Stack<Tuple<int, int>>();
        }

        public void Push(int val)
        {
            if (st.Count == 0)
                st.Push(new Tuple<int, int>(val, val));
            else
            {
                var temp = st.Peek();
                if (temp.Item2 > val)
                    st.Push(new Tuple<int, int>(val, val));
                else
                    st.Push(new Tuple<int, int>(val, temp.Item2));
            }
        }

        public void Pop()
        {
            st.Pop();
        }

        public int Top()
        {
            return st.Peek().Item1;
        }

        public int GetMin()
        {
            return st.Peek().Item2;
        }
    }

    //optimal solution S=>O(N) and T=>O(1)
    //by inserting a modified value into the stack 
    //and then perform calculations to get correct result
    //use long as integer overflow is happening

    public class MinStackOptimal
    {
        Stack<long> st;
        long min;
        public MinStackOptimal()
        {
            st = new Stack<long>();
            min = long.MaxValue;
        }

        public void Push(int val)
        {
            if (st.Count == 0)
            {
                st.Push(val);
                min = val;
            }
            else
            {
                if (min > val)
                {
                    st.Push(2 * val - min);
                    min = val;
                }
                else
                    st.Push(val);
            }
        }

        public void Pop()
        {
            if (st.Count == 0)
                return;
            var temp = st.Pop();
            if (temp < min)
                min = 2 * min - temp;
        }

        public int Top()
        {
            var temp = st.Peek();
            if (temp < min)
                return (int)min;
            return (int)temp;
        }

        public int GetMin()
        {
            return (int)min;
        }
    }

    #endregion


}
