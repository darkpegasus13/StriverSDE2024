using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Graph
    {
        #region BFS
        public List<int> bfsOfGraph(int V, List<int>[] adj)
        {
            List<int> ans = new List<int>();
            Queue<int> q = new Queue<int>();
            q.Enqueue(0);
            int[] visited = new int[adj.Length];
            visited[0] = 1;
            while (q.Count != 0)
            {
                int temp = q.Dequeue();
                ans.Add(temp);
                foreach (int i in adj[temp])
                    if (visited[i] != 1)
                    {
                        q.Enqueue(i);
                        visited[i] = 1;
                    }
            }
            return ans;
        }
        #endregion

        #region DFS

        public int[] dfsOfGraph(int V, List<int>[] adj)
        {
            int[] visited = new int[V];
            return dfsOfGraphHelper(0, adj, new List<int>(), visited).ToArray();
        }

        public List<int> dfsOfGraphHelper(int v, List<int>[] adj, List<int> ans, int[] visited)
        {
            ans.Add(v);
            visited[v] = 1;
            foreach (int i in adj[v])
            {
                if (visited[i] != 1)
                    dfsOfGraphHelper(i, adj, ans, visited);
            }
            return ans;
        }

        #endregion

        #region detect a cycle using bfs

        public bool isCycle(int V, List<int>[] adj)
        {
            int[] visited = new int[V];
            //we are doing this as graph can be disconnected
            for (int i = 0; i < V; i++)
            {
                if (visited[i] != 1)
                    if (isCycleHelper(i, adj, visited))
                        return true;
            }
            return false;
        }
        public bool isCycleHelper(int V, List<int>[] adj, int[] visited)
        {
            Queue<Tuple<int, int>> q = new Queue<Tuple<int, int>>();
            q.Enqueue(new Tuple<int, int>(V, -1));
            visited[V] = 1;
            while (q.Count != 0)
            {
                var temp = q.Dequeue();
                var currNode = temp.Item1;
                var parentNode = temp.Item2;
                foreach (int i in adj[currNode])
                    if (visited[i] != 1)
                    {
                        q.Enqueue(new Tuple<int, int>(i, currNode));
                        visited[i] = 1;
                    }
                    else if (parentNode != i)
                        return true;
            }
            return false;
        }
        #endregion

        #region detect a cycle using dfs
        public bool isCycledfs(int V, List<int>[] adj)
        {
            int[] visited = new int[V];
            //we are doing this as graph can be disconnected
            for (int i = 0; i < V; i++)
            {
                if (visited[i] != 1)
                    if (isCycleHelperdfs(i, adj, visited))
                        return true;
            }
            return false;
        }
        public bool isCycleHelperdfs(int V, List<int>[] adj, int[] visited, int parent = -1)
        {
            if (visited[V] == 1)
                return true;
            visited[V] = 1;
            foreach (int i in adj[V])
            {
                if (visited[i] != 0)
                {
                    if (i != parent)
                        return true;
                    else
                        continue;
                }
                else
                {
                    //returning true as only if true as if it is false there may be other 
                    //nodes where there may be a cycle
                    if(isCycleHelperdfs(i, adj, visited, V))
                        return true;
                }
            }
            return false;
        }


        #endregion

        #region Clone Graph

        private Dictionary<Node, Node> map = new Dictionary<Node, Node>();

        public Node CloneGraph(Node node)
        {
            return node == null ? null : CloneGraphHelper(node);
        }

        public Node CloneGraphHelper(Node node)
        {
            if (map.ContainsKey(node))
                return map[node];
            var copy = new Node(node.val);
            map.Add(node, copy);
            foreach (Node i in node.neighbors)
            {
                copy.neighbors.Add(CloneGraphHelper(i));
            }
            return copy;
        }

        #endregion

        #region Detect a cycle in directed graph

        private bool detectCycleDfsDirected(int node, List<bool> visited, List<bool> dfsVisited, List<int>[] adj)
        {
            visited[node] = true;
            dfsVisited[node] = true;

            foreach (var item in adj[node])
            {
                if (!visited[item])
                {
                    bool isCycle = detectCycleDfsDirected(item, visited, dfsVisited, adj);
                    if (isCycle)
                        return true;
                }
                else if (dfsVisited[item])
                    return true;
            }
            dfsVisited[node] = false;
            return false;
        }

        public bool isCyclicDirected(int V, List<int>[] adj)
        {
            var visited = new List<bool>(new bool[V]);
            var dfsVisited = new List<bool>(new bool[V]);
            for (int i = 0; i < V; i++)
            {
                if (!visited[i])
                    if (detectCycleDfsDirected(i, visited, dfsVisited, adj))
                        return true;
            }
            return false;

        }

        #endregion

        #region detect cycle in directed graph using bfs

        //can do this using toposort exactly same just checking if answer is of 
        //same size as that of number of nodes
        public bool isCyclicDirectedBFS(int V, List<int>[] adj)
        {
            List<int> ans = new List<int>();
            //calculating the degrees array
            int[] degrees = new int[adj.Count()];
            foreach (List<int> lst in adj)
            {
                foreach (var item in lst)
                {
                    degrees[item] += 1;
                }
            }
            //adding nodes with zero indegree to queue
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < degrees.Length; i++)
            {
                if (degrees[i] == 0)
                    q.Enqueue(i);
            }
            //reducing the count to the items to which the removed
            //node was pointing
            while (q.Count != 0)
            {
                var ele = q.Dequeue();
                foreach (var item in adj[ele])
                {
                    degrees[item] -= 1;
                    if (degrees[item] == 0)
                        q.Enqueue(item);
                }
                ans.Add(ele);
            }
            //as if it is equal there is no cycle
            return ans.Count!=adj.Length;
        }

        #endregion

        #region Topological sort BFS

        //Kahn's algorithm valid in only DAGs

        public List<int> topoSort(int V, List<List<int>> adj)
        {
            List<int> ans = new List<int>();
            //calculating the degrees array
            int[] degrees = new int[adj.Count()];
            foreach (List<int> lst in adj)
            {
                foreach (var item in lst)
                {
                    degrees[item] += 1;
                }
            }
            //adding nodes with zero indegree to queue
            Queue<int> q = new Queue<int>();
            for (int i = 0; i < degrees.Length; i++)
            {
                if (degrees[i] == 0)
                    q.Enqueue(i);
            }
           //reducing the count to the items to which the removed
           //node was pointing
            while (q.Count != 0)
            {
                var ele = q.Dequeue();
                foreach (var item in adj[ele])
                {
                    degrees[item] -= 1;
                    if (degrees[item] == 0)
                        q.Enqueue(item);
                }
                ans.Add(ele);
            }
            return ans;
        }

        #endregion

        #region Topological sorting DFS

        public List<int> topoSortDFS(int V, List<List<int>> adj)
        {
            Stack<int> st = new Stack<int>();
            int[] visited = new int[adj.Count];
            for (int i = 0; i < adj.Count; i++)
            {
                if (visited[i] == 0)
                    topoSortDFSHelper(i, adj, visited, ref st);
            }
            return st.ToList();
        }

        public void topoSortDFSHelper(int V, List<List<int>> adj, int[] visited, ref Stack<int> st)
        {
            visited[V] = 1;
            foreach (int i in adj[V])
            {
                if (visited[i] != 1)
                {
                    topoSortDFSHelper(i, adj, visited, ref st);
                }
            }
            st.Push(V);
        }

        #endregion
    }
    public class Node
    {
        public int val;
        public IList<Node> neighbors;

        public Node()
        {
            val = 0;
            neighbors = new List<Node>();
        }

        public Node(int _val)
        {
            val = _val;
            neighbors = new List<Node>();
        }

        public Node(int _val, List<Node> _neighbors)
        {
            val = _val;
            neighbors = _neighbors;
        }
    }
}
