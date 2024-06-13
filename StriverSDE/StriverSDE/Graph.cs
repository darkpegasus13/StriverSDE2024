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
            return dfsOfGraphHelper(0,adj,new List<int>(),visited).ToArray();
        }

        public List<int> dfsOfGraphHelper(int v, List<int>[] adj, List<int> ans,int [] visited)
        {
            ans.Add(v);
            visited[v] = 1;
            foreach(int i in adj[v])
            {
                if(visited[i]!=1)
                    dfsOfGraphHelper(i,adj,ans,visited);
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
    }
}
