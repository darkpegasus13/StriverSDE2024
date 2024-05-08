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
        #region Maximum Sum Path

        public int MaxPathSum(TreeNode root)
        {
            int Maxi = int.MinValue;
            MaxPathSumHelper(root, ref Maxi);
            return Maxi;
        }
        public int MaxPathSumHelper(TreeNode root, ref int maxi)
        {
            if (root == null)
                return 0;
            var leftSum = Math.Max(0, MaxPathSumHelper(root.left, ref maxi));
            var rightSum = Math.Max(0, MaxPathSumHelper(root.right, ref maxi));
            maxi = Math.Max(maxi, leftSum + rightSum + root.val);
            return root.val + Math.Max(leftSum < 0 ? 0 : leftSum, rightSum < 0 ? 0 : rightSum);
        }
            #endregion
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

        #region level order traversal

        //Optimal Solution S=>O(N) and T=>O(N)
        //using a queue

        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var ansList = new List<IList<int>>();
            if (root == null)
                return ansList;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count() != 0)
            {
                var tempList = new List<int>();
                int c = q.Count();
                for (int i = 0; i < c; i++)
                {
                    var temp = q.Dequeue();
                    if (temp.left != null)
                        q.Enqueue(temp.left);
                    if (temp.right != null)
                        q.Enqueue(temp.right);
                    tempList.Add(temp.val);
                }
                ansList.Add(tempList);
            }
            return ansList;
        }

        #endregion

        #region find the depth of the tree

        //Optimal Approach S=>O(N) and T=>O(N)
        //using the same approach as above

        public int DepthOfTree(TreeNode root)
        {
            if (root == null)
                return 0;
            int cnt=0;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Count() != 0)
            {
                int c = q.Count();
                for (int i = 0; i < c; i++)
                {
                    var temp = q.Dequeue();
                    if (temp.left != null)
                        q.Enqueue(temp.left);
                    if (temp.right != null)
                        q.Enqueue(temp.right);
                }
                cnt++;
            }
            return cnt;
        }

        //Optimal Solution 2
        //using recursion
        public int DepthOfTreeRecur(TreeNode root)
        {
            if (root == null)
                return 0;
            return 1
                + Math.Max(DepthOfTreeRecur(root.left),
                           DepthOfTreeRecur(root.right));
        }
        #endregion

        #region Diameter of a Binary Tree
        
        //Naive Solution S=>O(N) and T=>O(N^2)
        //canculating the depth of trees and calculating
        public int diameterHelper(TreeNode root)
        {
            //Your code here
            if (root == null)
                return 0;

            // get the height of left and right sub-trees
            int lHeight = DepthOfTreeRecur(root.left);
            int rHeight = DepthOfTreeRecur(root.right);

            // get the diameter of left and right sub-trees
            int lDiameter = diameterHelper(root.left);
            int rDiameter = diameterHelper(root.right);

            // Return max of following three
            // 1) Diameter of left subtree
            // 2) Diameter of right subtree
            // 3) Height of left subtree + height of right
            // subtree + 1
            return Math.Max(lHeight + rHeight + 1,
                            Math.Max(lDiameter, rDiameter));
        }

        //Optimal Approach T=>O(N) and S=>O(1)
        //using calculating height in same step
        //see implementation afterwards

        #endregion

        #region Zig Zag traversal

        //Optimal Solution T=>O(N) and S=>O(N)
        //exactly same as level order with a small
        //change

        public IList<IList<int>> ZigZag(TreeNode root)
        {
            var ansList = new List<IList<int>>();
            if (root == null)
                return ansList;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            bool isOdd=false;
            while (q.Count() != 0)
            {
                var tempList = new List<int>();
                int c = q.Count();
                for (int i = 0; i < c; i++)
                {
                    var temp = q.Dequeue();
                        if (temp.left != null)
                            q.Enqueue(temp.left);
                        if (temp.right != null)
                            q.Enqueue(temp.right);
                    tempList.Add(temp.val);
                }
                if (isOdd)
                    tempList.Reverse();
                ansList.Add(tempList);
                isOdd = !isOdd;
            }
            return ansList;
        }

        #endregion

        #region Boundary traversal(anticlockwise)

        //Optimal Solution S=>O(N) and T=>O(N)
        //left boundary leaves node and right boundary

        // A simple function to print leaf
        // nodes of a binary tree
        public virtual void printLeaves(TreeNode node)
        {
            if (node == null)
                return;

            printLeaves(node.left);

            // Print it if it is a leaf node
            if (node.left == null && node.right == null)
            {
                Console.Write(node.val + " ");
            }
            printLeaves(node.right);
        }

        // A function to print all left boundary
        // nodes, except a leaf node. Print the
        // nodes in TOP DOWN manner
        public virtual void printBoundaryLeft(TreeNode node)
        {
            if (node == null)
                return;

            if (node.left != null)
            {

                // to ensure top down order, print the node
                // before calling itself for left subtree
                Console.Write(node.val + " ");
                printBoundaryLeft(node.left);
            }
            else if (node.right != null)
            {
                Console.Write(node.val + " ");
                printBoundaryLeft(node.right);
            }

            // do nothing if it is a leaf node,
            // this way we avoid duplicates in output
        }

        // A function to print all right boundary
        // nodes, except a leaf node. Print the
        // nodes in BOTTOM UP manner
        public virtual void printBoundaryRight(TreeNode node)
        {
            if (node == null)
                return;

            if (node.right != null)
            {
                // to ensure bottom up order,
                // first call for right subtree,
                // then print this node
                printBoundaryRight(node.right);
                Console.Write(node.val + " ");
            }
            else if (node.left != null)
            {
                printBoundaryRight(node.left);
                Console.Write(node.val + " ");
            }
            // do nothing if it is a leaf node,
            // this way we avoid duplicates in output
        }

        // A function to do boundary traversal
        // of a given binary tree
        public virtual void printBoundary(TreeNode node)
        {
            if (node == null)
                return;

            Console.Write(node.val + " ");

            // Print the left boundary in
            // top-down manner.
            printBoundaryLeft(node.left);

            // Print all leaf nodes
            printLeaves(node.left);
            printLeaves(node.right);

            // Print the right boundary in
            // bottom-up manner
            printBoundaryRight(node.right);
        }
        #endregion

        #region Check Two trees are same

        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            if (p == null && q == null)
                return true;
            if ((p == null || q == null) || (p.val != q.val))
                return false;
            return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
        }

        #endregion

        #region Check if BT is a height balanced or not

        //Naive Solution S=>O(1) and T=>O(N^2)
        //calculate the height of right and left 
        //subtree and check the difference

        public int GetHeight(TreeNode root)
        {
            if (root == null)
                return 0;
            return Math.Max(GetHeight(root.left),GetHeight(root.right)) + 1;
        }

        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
                return true;
            int leftHeight = GetHeight(root.left);
            int rightHeight = GetHeight(root.right);

            if (Math.Abs(leftHeight - rightHeight) <= 1 && IsBalanced(root.left)
                && IsBalanced(root.right))
                return true;
            return false;
        }

        //Optimal Approach S=>O(1) and T=>O(N^2)
        //just modifying the getheight function

        public int GetHeightBalancedTree(TreeNode root)
        {
            //if result==-1 return fals ein main function
            if (root == null)
                return 0;
            int lh = GetHeightBalancedTree(root.left);
            if (lh == -1)
                return -1;
            int rh = GetHeightBalancedTree(root.right);
            if (rh == -1)
                return -1;
            if (Math.Abs(lh - rh) > 1)
                return -1;
            return Math.Max(lh,rh) + 1;
        }
        #endregion

        #region Lowest Common Ancestor

        //Naive Solution S=>O(N+M) and T=O(N+M) n and m are path for the 
        //two nodes
        //by storing the path in the array and checking for common answer

        //Better Solution S=>O(N)(auxillary) and T=>O(N) where N is the nodes 
        //using recursion

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null || root == p || root == q)
                return root;
            var left = LowestCommonAncestor(root.left, p, q);
            var right = LowestCommonAncestor(root.right, p, q);
            if (left == null)
                return right;
            else if (right == null)
                return left;
            else
                return root;

        }

        #endregion

        #region Symmetric Binary Tree

        //Optimal solution S=>O(N) and T=>O(N)
        //preorder traversal with one left and reverse preorder with the other tree
        public bool IsSymmetric(TreeNode root)
        {
            var right = root.right;
            var left = root.left;
            return IsSymmetricHelper(right, left);
        }

        public bool IsSymmetricHelper(TreeNode right, TreeNode left)
        {
            if (right == null && left == null)
                return true;
            if (right == null || left == null || right.val != left.val)
                return false;
            return IsSymmetricHelper(right.left, left.right) &&
            IsSymmetricHelper(right.right, left.left);
        }

        #endregion

        #region Convert Binary tree into its mirror


        //Optimal Solution S=>O(H) and T=>O(N)
        //using post order to swap left and right
        public void Mirror(TreeNode root)
        {
            if (root == null)
                return;
            //remember to use post order only in this
            //as the left and right are getting swapped
            Mirror(root.left);
            Mirror(root.right);
            //swapping the left and the right
            var temp = root.left;
            root.left = root.right;
            root.right = temp;
        }

        #endregion

        #region flatten binary tree to linked list in preorder
        //revisit the question leetcode problem has made it complex
        public void Flatten(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            flatten(root);
        }

        private TreeNode flatten(TreeNode root)
        {
            if (root.left == null && root.right == null)
                return root;

            if (root.left == null || root.right == null)
            {
                if (root.right == null)
                {
                    moveLeftToRight(root);
                }

                return flatten(root.right);
            }

            TreeNode lastLeft = flatten(root.left);
            TreeNode lastRight = flatten(root.right);
            TreeNode tempRight = root.right;

            moveLeftToRight(root);
            lastLeft.right = tempRight;

            return lastRight;
        }

        private void moveLeftToRight(TreeNode root)
        {
            root.right = root.left;
            root.left = null;
        }

        //using stack and DFS

        //public void Flatten(TreeNode root)
        //{
        //    if (root == null) return;
        //    FlattenAndGetEndNode(root);
        //}

        //public TreeNode FlattenAndGetEndNode(TreeNode root)
        //{
        //    var leftEndNode = root.left == null ? root : FlattenAndGetEndNode(root.left);
        //    var rightEndNode = root.right == null ? leftEndNode : FlattenAndGetEndNode(root.right);
        //    leftEndNode.right = root.right;
        //    if (root.left != null) root.right = root.left;
        //    root.left = null;
        //    return rightEndNode;
        //}

        //using morris traversal
    }
    #endregion

    }
