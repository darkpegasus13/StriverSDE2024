using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class StackandQueue
    {

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
    }
    #endregion
}
