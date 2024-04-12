using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class BinarySearchTree
    {
        public class Node
        {
            public int val;
            public Node left;
            public Node right;
            public Node next;

            public Node(int _val)
            {
                val = _val;
            }
        }
        #region calculating next right pointers in each node

        //Naive Approach S=>O(N) and T=>O(N)
        //using level order traversal
        public Node Connect(Node root)
        {
            if (root == null)
                return root;
            Queue<Node> q = new Queue<Node>();
            var ans = root;
            q.Enqueue(root);
            while (q.Count != 0)
            {
                int cnt = q.Count();
                for (int i = 0; i < cnt; i++)
                {
                    var temp = q.Dequeue();
                    if (i == cnt - 1)
                        temp.next = null;
                    else
                    {
                        if (q.Count != 0)
                            temp.next = q.Peek();
                        else
                            temp.next = null;
                    }
                    if (temp.left != null)
                        q.Enqueue(temp.left);
                    if (temp.right != null)
                        q.Enqueue(temp.right);
                }
            }
            return ans;
        }
        #endregion

        #region Search key In a BST

        //Naive Solution S=>O(1) and T=>O(H)
        //Using BST properties

        public TreeNode SearchBST(TreeNode root, int val)
        {
            // Base Cases: root is null or key is present at root
            if (root == null || root.val == val)
                return root;

            // Key is greater than root's key
            if (root.val < val)
                return SearchBST(root.right, val);

            // Key is smaller than root's key
            return SearchBST(root.left, val);
        }
        #endregion

        #region Validate BST

        //Naive Solution S=>O(N) and T=>O(N)
        //By checking inorder traversal it should be in ascending

        public bool IsValidBST(TreeNode root)
        {
            int? l = null;
            Stack<TreeNode> s = new Stack<TreeNode>();
            TreeNode cur = root;
            while (cur != null || s.Count() > 0)
            {
                while (cur != null)
                {
                    s.Push(cur);
                    cur = cur.left;
                }
                cur = s.Pop();
                if (l != null && l >= cur.val) return false;
                l = cur.val;
                cur = cur.right;
            }

            return true;
        }
        #endregion

        #region Convert Sorted Array to BST

        //Optimal Solution S=>O(logN) as it is height balanced tree and T=>O(N)
        //Making use of sorted array property

        public TreeNode SortedArrayToBST(int[] nums)
        {
            return helper(0, nums.Length - 1, nums);
        }
        public TreeNode helper(int l, int r, int[] nums)
        {
            if (l > r)
                return null;
            int m = (l + r) / 2;
            TreeNode root = new TreeNode(nums[m]);
            root.left = helper(l, m - 1, nums);
            root.right = helper(m + 1, r, nums);
            return root;
        }

        #endregion

        #region Construct BST from PreOrder Traversal

        //Brute Force 
        //using preorder property

        //Better S=>O(N) and T=>O(NLogN)+O(N)
        //by inorder and preorder traversal we create a BST

        //Optimal S=>O(N) and T=>O(3N)
        //using the upper bound logic 
        //public static int i = 0; some how we need to do it by passing as an array itself
        public TreeNode BstFromPreorder(int[] preorder)
        {
            int i = 0;
            return this.ConstructBSTPreorder(preorder, int.MaxValue, ref i);
        }

        public TreeNode ConstructBSTPreorder(int[] A, int bound, ref int i)
        {
            Console.WriteLine(i);
            if (i == A.Length || A[i] > bound)
                return null;
            TreeNode root = new TreeNode(A[i++]);
            root.left = ConstructBSTPreorder(A, root.val, ref i);
            root.right = ConstructBSTPreorder(A, bound, ref i);
            return root;
        }
        #endregion

        #region Lowest Common Ancestor in BST

        //Naive S=>O(1) and T=>O(N)
        //search same as in BT

        //Better
        //
        public virtual TreeNode lca(TreeNode node, int n1, int n2)
        {
            if (node == null)
            {
                return null;
            }

            // If both n1 and n2 are smaller than root, then LCA
            // lies in left
            if (node.val > n1 && node.val > n2)
            {
                return lca(node.left, n1, n2);
            }

            // If both n1 and n2 are greater than root, then LCA
            // lies in right
            if (node.val < n1 && node.val < n2)
            {
                return lca(node.right, n1, n2);
            }

            return node;
        }

        //Optimal S=>O(1) and T=>O(LogH) can do recursively as well but will take extra space
        //using BST property and if one is greater and other is smaller then the node is ancestor
        //other wise if both lies in same sub tree and the node is one of the two then it is the answer
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode ans = null;
            var curr = root;
            while (curr != null)
            {
                
                if (curr.val < p.val && curr.val < q.val)
                    curr = curr.right;
                else if (curr.val > p.val && curr.val > q.val)
                    curr = curr.left;
                else
                {
                    ans = curr;
                    break;
                }
            }
            return ans;
    }
        #endregion

        #region Inorder Successor or Predeseccor

        //Naive Solution S=>O(N) and T=>O(H)+(NlogN)
        //by storing the inorder of tree and then using binary search to 
        //find element just greater than node

        //Optimal Solution S=>O(1) and T=>O(H)
        //by traversing in inorder fashion and finding element just greater
        //than given node val

        public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            TreeNode successor = null;
            var curr = root;
            while (curr != null)
            {
                if (curr.val <= p.val)
                    curr = curr.right;
                else
                {
                    successor = curr;
                    curr = curr.left;
                }
            }
            return successor;
        }

        #endregion

        //Naive Solution S=>O(1) and T=>O(LogN)
        //
        #region Ceil/Floor from BST
        public int FindCeil(Node root, int input)
        {
            if (root == null)
                return -1;

            //Your code here
            Node res = null;
            while (root != null)
            {
                if (root.val == input)
                    return root.val;
                else if (root.val > input)
                {
                    //storing the possible answers
                    //and updating the answer to closest ceil
                    res = root;
                    root = root.left;
                }
                else
                    root = root.right;
            }
            if (res == null)
                return -1;
            return res.val;
        }

        public int FindFloor(Node root, int input)
        {
            if (root == null)
                return -1;

            //Your code here
            Node res = null;
            while (root != null)
            {
                if (root.val == input)
                    return root.val;
                else if (root.val > input)
                {
                    root = root.left;
                }
                else
                {
                    res = root;
                    root = root.right;
                }
            }
            if (res == null)
                return -1;
            return res.val;
        }
        #endregion

        #region Kth Largest/smallest element
        //Naive Solution S=>O(N) and T=>O(N)
        //as Inorder traversal of BST is sorted k-1 is smallest and n-k-1 is largest


        //Optimal Solution
        //do inorder traversal and maintain a counter for smaller
        //do reverse inorder traversal(right,node,left) and maintain a counter for larger
        //,int[] counter,int[] kthsmallest
        public void kthSmallest(TreeNode root, int k,int[] answer,int[] counter)
        {
            if (root == null || counter[0] >= k)
                return;
            kthSmallest(root.left, k, answer, counter);
            counter[0]++;
            if (counter[0] == k)
            {
                answer[0] = root.val;
                return;
            }
            kthSmallest(root.right,k,answer,counter);
        }

        public void KthLargest(TreeNode root, int k, int[] answer, int[] counter)
        {
            if (root == null || counter[0] >= k)
                return;
            KthLargest(root.right, k, answer, counter);
            counter[0]++;
            if (counter[0] == k)
            {
                answer[0] = root.val;
                return;
            }
            KthLargest(root.left, k, answer, counter);
        }

        #endregion

        #region Two sum in BST

        //Naive Solution S=>O(N) and T=>O(2N)
        //the inorder traversal gives a sorted array and apply 2 sum on it
        public bool FindTarget(TreeNode root, int k)
        {
            List<int> v = new List<int>();
            //storing result in a list
            InorderInsert(root, v);
            //normal 2 sum problem
            int n = v.Count;
            int i = 0;
            int j = n - 1;
            while (j > i)
            {
                if (v[i] + v[j] == k)
                {
                    return true;
                }
                else if (v[i] + v[j] > k)
                {
                    j--;
                }
                else
                {
                    i++;
                }
            }
            return false;
        }

        public static void InorderInsert(TreeNode root, List<int> v)
        {
            if (root == null)
            {
                return;
            }
            InorderInsert(root.left, v);
            v.Add(root.val);
            InorderInsert(root.right, v);
        }
        //Optimal Solutino S=>O(1) and T=>O()
        //using bst iterator

        #endregion
    }
}
