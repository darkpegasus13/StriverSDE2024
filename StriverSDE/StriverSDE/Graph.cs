using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StriverSDE
{
    class Graph
    {
        private List<Tuple<int, int>>[] adj;
        class Edge
        {
            public int src, dest, weight;
            public Edge() { src = dest = weight = 0; }
        };

        int V, E;
        Edge[] edge;

        // Creates a graph with V vertices and E edges
        Graph(int v, int e)
        {
            V = v;
            E = e;
            edge = new Edge[e];
            for (int i = 0; i < e; ++i)
                edge[i] = new Edge();
        }
        public void addEdge(int u, int v, int w)
        {
            adj[u].Add(Tuple.Create(v, w));
            adj[v].Add(Tuple.Create(u, w));
        }

        #region Dijkstra

        // prints shortest path from s
        public void shortestPath(int s)
        {
            // Create a priority queue to store vertices that
            // are being preprocessed.
            var pq = new PriorityQueue<Tuple<int, int>>();

            // Create a vector for distances and initialize all
            // distances as infinite (INF)
            var dist = new int[V];
            for (int i = 0; i < V; i++)
                dist[i] = int.MaxValue;

            // Insert source itself in priority queue and
            // initialize its distance as 0.
            pq.Enqueue(Tuple.Create(0, s));
            dist[s] = 0;

            /* Looping till priority queue becomes empty (or all
            distances are not finalized) */
            while (pq.Count != 0)
            {
                // The first vertex in pair is the minimum
                // distance vertex, extract it from priority
                // queue. vertex label is stored in second of
                // pair (it has to be done this way to keep the
                // vertices sorted distance (distance must be
                // first item in pair)
                var u = pq.Dequeue().Item2;

                // 'i' is used to get all adjacent vertices of a
                // vertex
                foreach (var i in adj[u])
                {
                    // Get vertex label and weight of current
                    // adjacent of u.
                    int v = i.Item1;
                    int weight = i.Item2;

                    //  If there is shorted path to v through u.
                    if (dist[v] > dist[u] + weight)
                    {
                        // Updating distance of v
                        dist[v] = dist[u] + weight;
                        pq.Enqueue(Tuple.Create(dist[v], v));
                    }
                }
            }

            // Print shortest distances stored in dist[]
            Console.WriteLine("Vertex Distance from Source");
            for (int i = 0; i < V; ++i)
                Console.WriteLine("{0}\t\t{1}", i, dist[i]);
        }

        #endregion 

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
                    if (isCycleHelperdfs(i, adj, visited, V))
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
            return ans.Count != adj.Length;
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

        #region Count no. of unique islands using bfs

        public int numIslands(char[][] grid)
        {
            HashSet<List<Tuple<int, int>>> hs = new HashSet<List<Tuple<int, int>>>();
            int R = grid.Length;
            int C = grid[0].Length;
            bool[,] vis = new bool[R, C];

            // Call BFS for every unvisited vertex 
            // Whenever we see an univisted vertex, 
            // we increment res (number of islands) 
            // also. 
            int res = 0;
            for (int i = 0; i < R; i++)
            {
                for (int j = 0; j < C; j++)
                {
                    if (grid[i][j] == '1' && !vis[i, j])
                    {
                        BFS(grid, vis, i, j, ref hs);
                        res++;
                    }
                }
            }
            //returning set count as it will contain only distinct islands
            //return res if want to find total number of islands
            return hs.Count;
        }

        public void BFS(char[][] mat, bool[,] vis,
                    int si, int sj, ref HashSet<List<Tuple<int, int>>> hs)
        {

            // These arrays are used to get row and 
            // column numbers of 8 neighbours of 
            // a given cell 
            int[] row = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] col = { -1, 0, 1, -1, 1, -1, 0, 1 };

            // Simple BFS first step, we enqueue 
            // source and mark it as visited 
            List<Tuple<int, int>> q = new List<Tuple<int, int>>();
            q.Add(new Tuple<int, int>(si, sj));
            vis[si, sj] = true;
            List<Tuple<int, int>> temp = new List<Tuple<int, int>>();
            temp.Add(new Tuple<int, int>(si, sj));
            // Next step of BFS. We take out 
            // items one by one from queue and 
            // enqueue their unvisited adjacent 
            while (q.Count != 0)
            {
                int i = q[0].Item1;
                int j = q[0].Item2;
                q.RemoveAt(0);

                // Go through all 8 adjacent 
                for (int k = 0; k < 8; k++)
                {
                    if (isSafe(mat, i + row[k],
                            j + col[k], vis))
                    {
                        vis[i + row[k], j + col[k]] = true;
                        //just added this extra logic to identify same islands
                        var newTup = new Tuple<int, int>(i + row[k] - si, j + col[k] - sj);
                        q.Add(newTup);
                        temp.Add(newTup);
                    }
                }
                //hashmap will only add distinct if same it wont add
                hs.Add(temp);
            }
        }

        public bool isSafe(char[][] mat, int i, int j,
                            bool[,] vis)
        {
            return (i >= 0) && (i < mat.Length) &&
                (j >= 0) && (j < mat[0].Length) &&
                (mat[i][j] == '1' && !vis[i, j]);
        }

        #endregion

        #region Bipartite Graph BFS

        public bool isBipartite(int V, List<int>[] adj)
        {
            //considering visited to contain coloring info
            //-1 not colored and 0 and 1 are two color
            int[] visited = Enumerable.Repeat(-1, adj.Length).ToArray();
            for (int i = 0; i < adj.Length; i++)
            {
                if (visited[i] == -1)
                    if (!BipartiteBFSHelper(i, adj, visited))
                        return false;
            }
            return true;
        }

        public bool BipartiteBFSHelper(int V, List<int>[] adj, int[] visited)
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(V);
            visited[V] = 0;
            while (q.Count != 0)
            {
                int temp = q.Dequeue();
                foreach (int i in adj[temp])
                    if (visited[i] == -1)
                    {
                        q.Enqueue(i);
                        visited[i] = visited[temp] == 0 ? 1 : 0;
                    }
                    else if (visited[temp] == visited[i])
                        return false;
            }
            return true;
        }

        #endregion

        #region Bipartite Graph DFS
        public bool IsBipartiteDFS(int V, List<int>[] adj)
        {
            //considering visited to contain coloring info
            //-1 not colored and 0 and 1 are two color
            int[] visited = Enumerable.Repeat(-1, adj.Length).ToArray();
            for (int i = 0; i < V; i++)
            {
                if (visited[i] == -1)
                    if (!BipartiteDFSHelper(i, adj, visited))
                        return false;
            }
            return true;
        }

        public bool BipartiteDFSHelper(int V, List<int>[] adj, int[] visited,
        int color = 0)
        {

            visited[V] = color;
            foreach (int i in adj[V])
            {
                if (visited[i] == -1)
                {
                    if (!BipartiteDFSHelper(i, adj, visited, visited[V] == 0 ? 1 : 0))
                        return false;
                }
                else if (visited[i] == visited[V])
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        #region Dijkshtra's algo 

        //using priority queue
        //this is a custom PQ we created <dist,node>
        SortedSet<Tuple<int, int>> pq = new SortedSet<Tuple<int, int>>
            (Comparer<Tuple<int, int>>.Create((a, b) =>
        {
            int cmp = a.Item1.CompareTo(b.Item1);
            return cmp == 0 ? a.Item2.CompareTo(b.Item2) : cmp;
        }));


        public List<int> dijkstra(int V, List<List<int>>[] adj, int S)
        {
            StringBuilder sb = new StringBuilder(S.ToString());
            int[] dist = Enumerable.Repeat(int.MaxValue,V).ToArray();
            dist[S] = 0;
            pq.Add(new Tuple<int, int>(0, S));
            while (pq.Count!=0)
            {
                var temp = pq.Min.Item2;
                pq.Remove(pq.Min);

                foreach(var cur in adj[temp])
                {
                    int v = cur[0];
                    int weight = cur[1];

                    if (dist[v] > dist[temp] + weight)
                    {
                        //for getting the path
                        sb.Append(v);
                        dist[v] = dist[temp] + weight;
                        pq.Add(new Tuple<int, int>(weight, v));
                    }
                }
            }
            return dist.ToList();
        }

        #endregion

        #region KosaRajus Algo(strongly connected comps(node can reach every other node))

        //it has three steps
        //sort according to finish time (topo sort)
        //reverse the graph(transpose)
        //apply dfs according to topo sort
        public int kosaraju(int V, List<List<int>> adj)
        {
            //Your code here
            //step 1 
            Stack<int> st = new Stack<int>();
            int[] visited = new int[V];
            for (int i = 0; i < V; i++)
            {
                if (visited[i] == 0)
                    topoSortDFSHelper(i, adj, visited, ref st);
            }
            //step 2 
            List<List<int>> rev_Adj = new List<List<int>>();
            for (int i = 0; i < V; i++)
            {
                rev_Adj.Add(new List<int>());
            }
            for (int i = 0; i < V; i++)
                foreach (int j in adj[i])
                    rev_Adj[j].Add(i);
            //step 3
            visited = new int[adj.Count];
            var cnt = 0;
            while (st.Count != 0)
            {
                var temp = st.Pop();
                if (visited[temp] != 1)
                {
                    cnt++;
                    dfsOfGraphHelper(temp, rev_Adj, visited);
                }
            }
            return cnt;
        }

        public void dfsOfGraphHelper(int v, List<List<int>> adj, int[] visited)
        {
            visited[v] = 1;
            foreach (int i in adj[v])
            {
                if (visited[i] != 1)
                    dfsOfGraphHelper(i, adj, visited);
            }
        }

        #endregion

        #region Floyd Warshal Algo

        //the bellman and dijkshtras are for single source shortest path
        //but this is a multisource we try to go from all the vertex

        //if any of the diagnol becomes less than 0 it has a negative
        //cycle

        public void shortest_distance(List<List<int>> matrix)
        {
            int n = matrix.Count();
            //setting the matrix according to our implementation
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == -1)
                        matrix[i][j] = int.MaxValue;
                    if (i == j)
                        matrix[i][j] = 0;
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        int curr;
                        if (matrix[j][i] == int.MaxValue || matrix[i][k] == int.MaxValue)
                            curr = int.MaxValue;
                        else
                            curr = matrix[j][i] + matrix[i][k];
                        matrix[j][k] = Math.Min(curr, matrix[j][k]);
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j] == int.MaxValue)
                        matrix[i][j] = -1;
                }
            }
        }

        #endregion

        #region bellman ford algo works for negative weights as well

        //but it works for directed graph only 

        void BellmanFord(Graph graph, int src)
        {
            int V = graph.V, E = graph.E;
            int[] dist = new int[V];

            // Step 1: Initialize distances from src to all
            // other vertices as INFINITE
            for (int i = 0; i < V; ++i)
                dist[i] = int.MaxValue;
            dist[src] = 0;

            // Step 2: Relax all edges |V| - 1 times. A simple
            // shortest path from src to any other vertex can
            // have at-most |V| - 1 edges
            for (int i = 1; i < V; ++i)
            {
                for (int j = 0; j < E; ++j)
                {
                    int u = graph.edge[j].src;
                    int v = graph.edge[j].dest;
                    int weight = graph.edge[j].weight;
                    if (dist[u] != int.MaxValue
                        && dist[u] + weight < dist[v])
                        dist[v] = dist[u] + weight;
                }
            }

            // Step 3: check for negative-weight cycles. The
            // above step guarantees shortest distances if graph
            // doesn't contain negative weight cycle. If we get
            // a shorter path, then there is a cycle.
            for (int j = 0; j < E; ++j)
            {
                int u = graph.edge[j].src;
                int v = graph.edge[j].dest;
                int weight = graph.edge[j].weight;
                if (dist[u] != int.MaxValue
                    && dist[u] + weight < dist[v])
                {
                    Console.WriteLine(
                        "Graph contains negative weight cycle");
                    return;
                }
            }
            printArr(dist, V);
        }

        void printArr(int[] dist, int V)
        {
            Console.WriteLine("Vertex Distance from Source");
            for (int i = 0; i < V; ++i)
                Console.WriteLine(i + "\t\t" + dist[i]);
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

    public class PriorityQueue<T>
    {
        private readonly List<T> _data;
        private readonly Comparison<T> _compare;

        public PriorityQueue()
            : this(Comparer<T>.Default)
        {
        }

        public PriorityQueue(IComparer<T> comparer)
            : this(comparer.Compare)
        {
        }

        public PriorityQueue(Comparison<T> comparison)
        {
            _data = new List<T>();
            _compare = comparison;
        }

        public void Enqueue(T item)
        {
            _data.Add(item);
            var childIndex = _data.Count - 1;

            while (childIndex > 0)
            {
                var parentIndex = (childIndex - 1) / 2;
                if (_compare(_data[childIndex],
                             _data[parentIndex])
                    >= 0)
                    break;

                T tmp = _data[childIndex];
                _data[childIndex] = _data[parentIndex];
                _data[parentIndex] = tmp;

                childIndex = parentIndex;
            }
        }

        public T Dequeue()
        {
            // assumes pq is not empty; up to calling code
            var lastElement = _data.Count - 1;

            var frontItem = _data[0];
            _data[0] = _data[lastElement];
            _data.RemoveAt(lastElement);

            --lastElement;

            var parentIndex = 0;
            while (true)
            {
                var childIndex = parentIndex * 2 + 1;
                if (childIndex > lastElement)
                    break; // End of tree

                var rightChild = childIndex + 1;
                if (rightChild <= lastElement
                    && _compare(_data[rightChild],
                                _data[childIndex])
                           < 0)
                    childIndex = rightChild;

                if (_compare(_data[parentIndex],
                             _data[childIndex])
                    <= 0)
                    break; // Correct position

                T tmp = _data[parentIndex];
                _data[parentIndex] = _data[childIndex];
                _data[childIndex] = tmp;

                parentIndex = childIndex;
            }

            return frontItem;
        }

        public T Peek()
        {
            T frontItem = _data[0];
            return frontItem;
        }

        public int Count
        {
            get { return _data.Count; }
        }
    }
}