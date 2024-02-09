using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class LinkedList
    {
        public int data { get; set; }
        public LinkedList next { get; set; }
        public LinkedList(int data)
        {
            this.data = data;
            next = null;
        }

        #region Reverse A LL

        //Naive Solution S=>O(N) and T=>O(2N)
        //Use a stack to store the node and then pop them and
        //append values from the stack 

        //Better SOlution S=>O(N) and T=>O(N)
        //using recursion
        LinkedList ReverseLLOptimalRecur(LinkedList head)
        {
            //for one node or zero node
            if (head == null || head.next==null)
                return head;
            var newHead = ReverseLLOptimalRecur(head.next);
            var next = head.next;
            next.next = head;
            head.next = null;
            return newHead;
        }

        //Optimal S=>O(1) and T=>O(N)
        //Swap in place while traversing
        LinkedList ReverseLLOptimalIter(LinkedList head)
        {
            LinkedList prev = null;
            LinkedList curr = head;
            while (curr!=null)
            {
                var next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            return prev;
        }

        #endregion

        #region Midlle Of Linked List

        //Naive Solution S=>O(1) and T=>O(N)
        //traverse the linked list and get the count and find middle
        public LinkedList MiddleOfLinkedList(LinkedList head)
        {
            int cnt = 0;
            var temp = head;
            while (temp != null)
            {
                temp = temp.next;
                cnt += 1;
            }
            temp = head;
            if (cnt % 2 == 0)
                cnt = cnt / 2;
            else
                cnt = (int)Math.Floor((double)cnt/2);
            for (int i = 0; i < cnt; i++)
                    temp = temp.next;
            return temp;
        }

        //Optimal Solution S=>O(1) && T=>O(N)
        //Rabbit and Hare method

        public LinkedList MiddleOfLinkedListOptimal(LinkedList head)
        {
            LinkedList slowPtr = head;
            LinkedList fastPtr = head;
            while (fastPtr!=null && fastPtr.next!=null)
            {
                slowPtr = slowPtr.next;
                fastPtr = fastPtr.next.next;
            }
            return slowPtr;
        }
        #endregion

        #region Merge Two Sorted LL

        //Naive Solution S=>O(N+M) and T=>O(N+M)
        //Use a new LL and two pointers at the two LL's

        public LinkedList MergeTwoSortedLL(LinkedList l1,LinkedList l2)
        {
            LinkedList newLL = new LinkedList(-1);
            var temp = newLL;
            var ptr1 = l1;
            var ptr2 = l2;
            while (ptr1!=null && ptr2!=null)
            {
                var newNode = new LinkedList(-1);
                if (ptr1.data <= ptr2.data)
                    newNode.data =ptr1.data;
                else
                    newNode.data=ptr2.data;
                temp.next = newNode;
                temp = newNode;
            }
            if (ptr1 != null)
                temp.next = ptr1;
            if (ptr2 != null)
                temp.next = ptr2;
            return newLL.next;
        }

        //Optimal Solution S=>O(1) and T=>O(N+M)
        //Use swap inplace by breaking linkage and joining to other linked list

        public LinkedList MergeTwoLists(LinkedList list1, LinkedList list2)
        {
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            if (list1.data > list2.data)
            {
                //to keep l1 as smaller list always
                LinkedList temp = list1;
                list1 = list2;
                list2 = temp;
            }

            LinkedList res = list1;
            while (list1 != null && list2 != null)
            {
                LinkedList temp = null;
                while (list1 != null && list1.data <= list2.data)
                {
                    temp = list1;
                    list1 = list1.next;
                }
                //swapping to point list1 to smaller element
                temp.next = list2;
                LinkedList tmp = list1;
                list1 = list2;
                list2 = tmp;
            }
            return res;
        }
        #endregion

        #region Remove Nth Node

        //Naive S=>O(1) and T=>O(N)+O(L-N)
        //calculate length by traversing and then subtract
        public LinkedList RemoveNthFromEndNaive(LinkedList head, int n)
        {
            int len = 0;
            var curr = head;
            while (curr != null)
            {
                curr = curr.next;
                len++;
            }
            if (len == n)
                return head.next;
            curr = head;
            var res = len-n;
            //for loop doesn't work here remember to use it
            while (curr != null)
            {
                res--;
                if (res == 0)
                    break;
                curr = curr.next;
            }
            curr.next = curr.next.next;
            return head;
        }

        //Optimal S=>O(1) and T=>O(N)
        //slow and fast pointer move fast till N and then move them together
        //when reaching null slow would be pointing to delete
        public LinkedList RemoveNthFromEnd(LinkedList head, int n)
        {
            var slow = head;
            var fast = head;
            for (int i = 0; i < n; i++)
                fast = fast.next;
            if (fast == null)
                return head.next;
            while (fast.next != null)
            {
                fast = fast.next;
                slow = slow.next;
            }
            slow.next = slow.next.next;
            return head;
        }
        #endregion

    }
}
