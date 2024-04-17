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
        public LinkedList random { get; set; }
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
            if (head == null || head.next == null)
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
            while (curr != null)
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
                cnt = (int)Math.Floor((double)cnt / 2);
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
            while (fastPtr != null && fastPtr.next != null)
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

        public LinkedList MergeTwoSortedLL(LinkedList l1, LinkedList l2)
        {
            LinkedList newLL = new LinkedList(-1);
            var temp = newLL;
            var ptr1 = l1;
            var ptr2 = l2;
            while (ptr1 != null && ptr2 != null)
            {
                var newNode = new LinkedList(-1);
                if (ptr1.data <= ptr2.data)
                    newNode.data = ptr1.data;
                else
                    newNode.data = ptr2.data;
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
            var res = len - n;
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

        #region Add Two Numbers as a LL
        //Naive Solution S=>O(1) and T=>O(N+M)
        //traverse and find length of LL and convert LL to a number
        //then add two number then replace the LL elements by the new number formed

        //Better Solution S=>O(1) and T=>Max(N,M)
        //using elementary math

        public LinkedList AddTwoNumbers(LinkedList l1, LinkedList l2)
        {
            LinkedList ansNode = new LinkedList(-1);
            var temp = ansNode;
            int carry = 0;
            int sum = 0;
            while (l1 != null || l2 != null || carry != 0)
            {
                if (l1 != null)
                {
                    sum += l1.data;
                    l1 = l1.next;
                }
                if (l2 != null)
                {
                    sum += l2.data;
                    l2 = l2.next;
                }
                sum += carry;
                temp.next = new LinkedList(sum % 10);
                carry = sum / 10;
                sum = 0;
                temp = temp.next;
            }
            return ansNode.next;
        }
        #endregion

        #region Delete a Given Node in a LL when node is given

        //Optimal Solution S=>O(1) and T=>O(1)
        //by copying the next integer value and then skipping the next element
        //considering it is never the last node
        public void DeleteNode(LinkedList node)
        {
            node.data = node.next.data;
            node.next = node.next.next;
        }
        #endregion

        #region Rotate a LL by k units

        //Naive Solution S=>O(1) and T=>O(L*k)
        //copy previous LL's value in the node starting from 1st indx at last replace
        //first and last element values

        public LinkedList RotateByK(LinkedList head, int k)
        {
            if (head == null || head.next == null || k == 0)
                return head;
            //more than or equal to 2 nodes
            for (int i = 0; i < k; i++)
            {
                var curr = head.next;
                int prev = head.data;
                while (curr != null)
                {
                    var temp = curr.data;
                    curr.data = prev;
                    prev = temp;
                    curr = curr.next;
                }
                //feeding last elements value to first node
                head.data = prev;
            }
            return head;
        }

        //Better S=>O(1) and T=>O(L*(k%L))
        //using modulo as if we rotate a LL equal to its length 
        //it will become the same
        public LinkedList RotateByKBetter(LinkedList head, int k)
        {
            if (head == null || head.next == null || k == 0)
                return head;
            //finding length of LL
            var lengthCurr = head;
            int len=0;
            while (lengthCurr != null)
            {
                lengthCurr = lengthCurr.next;
                len++;
            }
            //more than or equal to 2 nodes
            for (int i = 0; i < k%len; i++)
            {
                var curr = head.next;
                int prev = head.data;
                while (curr != null)
                {
                    var temp = curr.data;
                    curr.data = prev;
                    prev = temp;
                    curr = curr.next;
                }
                //feeding last elements value to first node
                head.data = prev;
            }
            return head;
        }

        //Optimal S=>O(1) and T=>O(N+K%len)
        //make it a circular ll and break linkage from len-K
        //and return len_k+1 as head
        public LinkedList RotateByKOptimal(LinkedList head, int k)
        {
            if (head == null || head.next == null || k == 0)
                return head;
            //finding length of LL  
            var lengthCurr = head;
            int len = 1;
            while (lengthCurr.next != null)
            {
                lengthCurr = lengthCurr.next;
                len++;
            }
            //Pointing last node to first and making it circular
            lengthCurr.next = head;
            var curr = head;
            var newHead = head;
            for(int i = 1; i < len - k; i++)
            {
                curr = curr.next;
                newHead = curr.next;
            }
            return newHead;
        }

        #endregion

        #region clone a LL with an additional param random pointer
        //Naive SOlution S=>O(N) and T=>O(N)+O(N)
        //maintaing a hashmap with value same as each node and then mapping
        //them within the hashmap
        public LinkedList CopyRandomListNaive(LinkedList head)
        {
            if (head == null)
                return null;
            Dictionary<LinkedList, LinkedList> hashMap = new Dictionary<LinkedList, LinkedList>();
            var curr = head;
            while (curr != null)
            {
                hashMap.Add(curr, new LinkedList(curr.data));
                curr = curr.next;
            }
            curr = head;
            while (curr != null)
            {
                hashMap[curr].next = curr.next == null ? null : hashMap[curr.next];
                hashMap[curr].random = curr.random == null ? null : hashMap[curr.random];
                curr = curr.next;
            }
            return hashMap[head];
        }

        //Optimal Solution S=>O(N) and T=>O(3N)
        //By following the 3 steps mentioned below

        public static LinkedList CloneLinkedList(LinkedList head)
        {
            if (head == null)
            {
                return null;
            }

            // Step 1: Create new nodes and insert them next to the original nodes
            LinkedList curr = head;
            while (curr != null)
            {
                LinkedList newNode = new LinkedList(curr.data);
                newNode.next = curr.next;
                curr.next = newNode;
                curr = newNode.next;
            }

            // Step 2: Set the random pointers of the new nodes
            curr = head;
            while (curr != null)
            {
                if (curr.random != null)
                {
                    curr.next.random = curr.random.next;
                }
                curr = curr.next.next;
            }

            // Step 3: Separate the new nodes from the original nodes
            curr = head;
            LinkedList clonedHead = head.next;
            LinkedList clonedCurr = clonedHead;
            while (clonedCurr.next != null)
            {
                curr.next = curr.next.next;
                clonedCurr.next = clonedCurr.next.next;
                curr = curr.next;
                clonedCurr = clonedCurr.next;
            }
            curr.next = null;
            clonedCurr.next = null;

            return clonedHead;
        }
        #endregion

        #region find intersection of two points

        //Naive Solution S=>O(1) and T=>O(M*N)
        //traverse through one LL and check whether it is equal to 
        //the node of other for every node

        //Better Solution S=>O(N) and T=>O(M+N)
        //store the nodes in a hash map and if already present 
        //return that node


        //Optimal Solution S=>O(1) and T=>O(2(N+M))
        //subtract the length of LLs and move then
        //from longer LL after traversing the difference in length
        public LinkedList FindIntersection(LinkedList headA, LinkedList headB)
        {
            if (headA == null || headB == null)
                return null;
            var a = headA;
            var b = headB;
            var aLen = 0;
            var bLen = 0;
            //calculating Length of LL
            while (a != null)
            {
                a = a.next;
                aLen++;
            }
            //calculating Length of LL
            while (b != null)
            {
                b = b.next;
                bLen++;
            }
            a = headA;
            b = headB;
            //calculating difference of LL
            var diff = Math.Abs(aLen - bLen);
            //traversing till difference in the longer LL
            if (aLen >= bLen)
            {
                for (int i = 0; i < diff; i++)
                    a = a.next;
            }
            else
            {
                for (int i = 0; i < diff; i++)
                    b = b.next;
            }
            //starting both LL and seeing a junction
            while (a != b)
            {
                a = a.next;
                b = b.next;
                //if one of the LL is ended it means
                //there is no junction
                if (a == null || b == null)
                    return null;
            }
            return a;
        }

        #endregion

        #region Detect a cycle in LL

        //Naive Solution S=>O(N) and T=>O(N)
        //store nodes in a hash map and return node if already found
        public bool DetectCycle(LinkedList head)
        {
            if (head == null)
                return false;
            var curr = head;
            Dictionary<LinkedList, int> d = new Dictionary<LinkedList, int>();
            while (curr != null)
            {
                if (d.ContainsKey(curr))
                    return true;
                else
                    d.Add(curr, 1);
                curr = curr.next;
            }
            return false;
        }

        //Optimal Solution
        //using rabbit and hare method

        public bool DetectCycleOptimal(LinkedList head)
        {
            if (head == null)
                return false;
            var slow = head;
            var fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (fast == slow)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Check a Linked List is palindrome

        //Naive Solution S=>O(N) and T=>O(N)
        //put all the element in a stack and then pop out
        //elements by again traversing the LL as stack will store in reverse order
        //if it comes out to be empty it is a palindrome
        public bool IsPalindrome(LinkedList head)
        {
            if (head == null || head.next == null)
                return true;
            Stack<int> st = new Stack<int>();
            var curr = head;
            while (curr != null)
            {
                st.Push(curr.data);
                curr = curr.next;
            }
            curr = head;
            while (curr != null)
            {
                if (st.Peek() == curr.data)
                    st.Pop();
                else
                    return false;
                curr = curr.next;
            }
            return st.Count == 0 ? true : false;
        }

        //Optimal Solution
        //if even number break in two halves and reverse the other half
        //and then start traversing both the halves to get check palindrome

        public bool IsPalindromeOptimal(LinkedList head)
        {
            //single or no element 
            if (head == null || head.next == null)
                return true;
            var i = head;
            var j = head;
            //traversing to middle of LL
            while (j.next != null && j.next.next != null)
            {
                i = i.next;
                j = j.next.next;
            }
            //reversed the second half
            i.next = ReverseLLOptimalIter(i.next);
            i = i.next;
            //will now compare the first half from head
            //and the second half from i
            LinkedList dummy = head;

            while (i != null)
            {
                if (dummy.data != i.data)
                    return false;
                dummy = dummy.next;
                i = i.next;
            }
            //if all nodes match send true
            return true;
        }

        #endregion

        #region Starting Point in a linked list

        //Naive Solution 
        //store values in a hash map and then 
        //check if it is already present

        //Optimal solution
        //turtoise and hare method

        public LinkedList DetectCycle(LinkedList head)
        {
            if (head == null || head.next == null)
                return null;
            var slow = head;
            var fast = head;
            var entry = head;
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if (fast == slow)
                {
                    //loop is found
                    while (entry != slow)
                    {
                        //this is for finding the starting point
                        //of the loop
                        entry = entry.next;
                        slow = slow.next;
                    }
                    return entry;
                }
            }
            return null;
        }
        #endregion
    }
}
