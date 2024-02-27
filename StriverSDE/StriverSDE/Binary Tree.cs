using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
    class Binary_Tree
    {
        #region PreOrder Traversal
        //same for inorder, postorder traversal just change the positioning
        //of root.val
        public IList<int> PreorderTraversal(TreeNode root)
        {
            var ans = new List<int>();
            PreorderHelper(root, ans);
            return ans;
        }
        //Recursive S=>O(N) and T=>O(N)
        public void PreorderHelper(TreeNode root, IList<int> ans)
        {
            if (root == null)
                return;
            ans.Add(root.val);
            PreorderHelper(root.left, ans);
            PreorderHelper(root.right, ans);
        }

        //Iterative S=>O(N) if it has left children only and T=>O(N)
        //Using a stack
        public IList<int> PreOrderIter(TreeNode node, IList<int> ans)
        {
            Stack<TreeNode> st = new Stack<TreeNode>();
            while (true)
            {
                if (node != null)
                {
                    st.Push(node);
                    ans.Add(node.val);
                    node = node.left;
                }
                else
                {
                    if (st.Count == 0)
                        break;
                    node = st.Pop();
                    node = node.right;
                }
            }
            return ans;
        }

        #endregion

        #region Morris Traversal
        //Morris Traversal S=>O(1) and T=>O(N)
        //we consider that the last node points to parent node
        public IList<int> MorrisInorder(TreeNode root)
        {
            var ans = new List<int>();
            while (root != null)
            {
                if (root.left == null)
                {
                    //dont touch this logic for other orders 
                    ans.Add(root.val);
                    root = root.right;
                }
                else
                {
                    var prev = root.left;
                    while (prev.right != null && prev.right != root)
                        prev = prev.right;
                    if (root.right == null)
                    {
                        prev.right = root;
                        //ans.Add(root.val) for preorder
                        root = root.left;
                    }
                    else
                    {
                        prev.right = null;
                        ans.Add(root.val);
                        root = root.right;
                    }
                }
            }
            return ans;
        }
        #endregion

        #region Right/Left View of Binary tree
        //right left require a minor change
        //Iterative Approach S=>O(N) and T=>O(N) 
        // using level order traversal

        public IList<int> RightSideView(TreeNode root)
        {
            var ans = new List<int>();
            if (root == null)
                return ans;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            int prev = root.val;
            while (q.Count != 0)
            {
                int curr_count = q.Count;
                //setting count till what position 
                //our level order elements are present
                for (int i = 0; i < curr_count; i++)
                {
                    if (i == 0)
                        ans.Add(prev);
                    var curr = q.Dequeue();
                    if (curr?.left != null)
                    {
                        prev = curr.left.val;
                        q.Enqueue(curr.left);
                    }
                    if (curr?.right != null)
                    {
                        prev = curr.right.val;
                        q.Enqueue(curr.right);
                    }

                }
            }
            return ans;
        }

        //Recursive Approach S=>O(H) and T=>O(N) better as it need H space not N
        //we do a reveres Preorder root->right->left and check length of ans so as to fill only at
        //each level
        public void RightSideViewRecur(TreeNode root, List<int> ans,int lvl=1)
        {
            if (root == null)
                return;
            if(ans.Count<lvl)
                ans.Add(root.val);
            RightSideViewRecur(root.right,ans,lvl+1);
            RightSideViewRecur(root.left,ans,lvl+1);
        }

        #endregion

        #region Bottom/Top View Of Binary Tree

        //Naive S=>O(N) and T=>O(N)
        //using vertical order traversal
        public List<int> bottomView(TreeNode root)
        {
            Queue<Tuple<TreeNode, int>> q = new Queue<Tuple<TreeNode, int>>();
            SortedDictionary<int, int> hash = new SortedDictionary<int, int>();
            q.Enqueue(new Tuple<TreeNode, int>(root, 0));
            while (q.Count != 0)
            {
                Tuple<TreeNode, int> currPair = q.Dequeue();
                TreeNode currNode = currPair.Item1;
                int currDist = currPair.Item2;
                //only change for top view would be is we are adding item only when 
                //given distance not present in hash
                if (!hash.ContainsKey(currDist))
                    hash.Add(currDist, currNode.val);
                else
                    hash[currDist] = (int)currNode.val;
                if (currNode.left != null)
                    q.Enqueue(new Tuple<TreeNode, int>
                    (currNode.left, currDist - 1));
                if (currNode.right != null)
                    q.Enqueue(new Tuple<TreeNode, int>
                    (currNode.right, currDist + 1));
            }
            return hash.Values.ToList();
        }
        #endregion

        #region Post, in, pre traversal in single traversal

        //Optimal Solution S=>O(3N) and T=>O(4N)
        //by using custom logic as defined below
        public void PreInPostTraversal(TreeNode root)
        {
            List<int> PreList = new List<int>();
            List<int> InList = new List<int>();
            List<int> PosList = new List<int>();
            Stack<Tuple<TreeNode, int>> st = new Stack<Tuple<TreeNode, int>>();
            st.Push(new Tuple<TreeNode,int>(root,1));
            while (st.Count != 0)
            {
                var temp = st.Pop();
                if (temp.Item2 == 1)
                {
                    PreList.Add(temp.Item1.val);
                    st.Push(new Tuple<TreeNode, int>(temp.Item1, temp.Item2 + 1));
                    // If left child is not null
                    if (temp.Item1.left != null)
                    {

                        // Insert the left subtree
                        // with status code 1
                        st.Push(new Tuple<TreeNode, int>(temp.Item1.left, 1));
                    }
                }
                else if (temp.Item2 == 2)
                {
                    InList.Add(temp.Item1.val);
                    st.Push(new Tuple<TreeNode, int>(temp.Item1, temp.Item2 + 1));
                    if (temp.Item1.right != null)
                    {

                        // Insert the right subtree into
                        // the stack with status code 1
                        st.Push(new Tuple<TreeNode, int>(temp.Item1.right, 1));
                    }
                }
                else
                    PosList.Add(temp.Item1.val);
            }
        }
        #endregion

        #region vertical level order traversal

        //S=>O(N) and T=>O(N*logN*logN*LogN)
        //by assuming coordinates for given node (0,1,1,-2 etc.)
        public IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            var ans = new List<IList<int>>();
            Queue<Tuple<TreeNode, int, int>> q = new Queue<Tuple<TreeNode, int, int>>();
            q.Enqueue(new Tuple<TreeNode, int, int>(root, 0, 0));
            SortedDictionary<int, List<Tuple<TreeNode, int>>> map = new SortedDictionary<int, List<Tuple<TreeNode, int>>>();
            while (q.Count != 0)
            {
                var temp = q.Dequeue();
                if (!map.ContainsKey(temp.Item2))
                    map.Add(temp.Item2, new List<Tuple<TreeNode, int>> { new Tuple<TreeNode, int>(temp.Item1, temp.Item3) });
                else
                    map[temp.Item2].Add(new Tuple<TreeNode, int>(temp.Item1, temp.Item3));
                if (temp.Item1.left != null)
                    q.Enqueue(new Tuple<TreeNode, int, int>(temp.Item1.left, temp.Item2 - 1, temp.Item3 + 1));
                if (temp.Item1.right != null)
                    q.Enqueue(new Tuple<TreeNode, int, int>(temp.Item1.right, temp.Item2 + 1, temp.Item3 + 1));
            }
            foreach (KeyValuePair<int, List<Tuple<TreeNode, int>>> kp in map)
            {
                //Linq has an advantage to sort element first and then sort to 2nd criteria
                ans.Add(kp.Value.OrderBy(i => i.Item2).ThenBy(i => i.Item1.val).Select(x => x.Item1.val).ToList());
            }
            return ans;
        }
        #endregion

        #region Path from root to node in a BT

        //SOlution S=>O(N) and T=>O(N)
        //travesing the tree in any DFS and storing ans in list
        public bool PathFromRoot(int ele,TreeNode curr, List<int> ans)
        {
            if (curr == null)
                return false;
            ans.Add(curr.val);
            if (curr.val != ele)
                return true;
            if (PathFromRoot(ele, curr.left,ans) || PathFromRoot(ele, curr.right,ans))
                return true;
            ans.RemoveAt(ans.Count - 1);
            return false;
        }
        #endregion

        #region Max width of a BTree

        //Solution S=>O(N) and T=>O(N)
        //By using Level Order traversal and assigning indexes to nodes

        public int WidthOfBinaryTree(TreeNode root)
        {
            if (root == null)
                return 0;
            int ans = 0;
            Queue<Tuple<TreeNode, int>> q = new Queue<Tuple<TreeNode, int>>();
            q.Enqueue(new Tuple<TreeNode,int>(root,0));
            while (q.Count != 0)
            {
                int size = q.Count;
                int mmin = q.Peek().Item2;
                int first=0, last = 0;
                for(int i = 0; i < size; i++)
                {
                    //to avoid overflow
                    int cur_indx = q.Peek().Item2-mmin;
                    var currNode = q.Peek().Item1;
                    q.Dequeue();
                    if (i == 0)
                        first = cur_indx;
                    if (i == size - 1)
                        last = cur_indx;
                    if (currNode.left != null)
                        q.Enqueue(new Tuple<TreeNode,int>(currNode.left,cur_indx*2+1));
                    if (currNode.right != null)
                        q.Enqueue(new Tuple<TreeNode,int>(currNode.right, cur_indx * 2 + 2));
                }
                ans = Math.Max(ans,last-first+1);
            }
            return ans;
        }

        #endregion
    }
}
