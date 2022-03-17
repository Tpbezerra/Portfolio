using System.Collections.Generic;

public class Graph
{
    HashSet<int>[] adj;
    int v, e;

    int source = -1;
    bool[] marked;
    int[] edgeTo;

    public Graph(int v)
    {
        this.v = v;
        e = 0;

        adj = new HashSet<int>[v];
        for (int i = 0; i < v; i++)
        {
            adj[i] = new HashSet<int>();
        }
    }

    public int V() { return v; }
    public int E() { return e; }

    public void AddEdge(int v, int w)
    {
        adj[v].Add(w);
        adj[w].Add(v);
        e++;
    }

    public void RemoveEdge(int v, int w)
    {
        adj[v].Remove(w);
        adj[w].Remove(v);
        e--;
    }

    public void Bfp(int source)
    {
        this.source = source;

        marked = new bool[v];
        edgeTo = new int[v];

        Queue<int> toCheck = new Queue<int>();

        toCheck.Enqueue(source);
        marked[source] = true;

        int current;
        while (toCheck.Count > 0)
        {
            current = toCheck.Dequeue();

            foreach (int adjacent in Adj(current))
            {
                if (!marked[adjacent])
                {
                    edgeTo[adjacent] = current;
                    marked[adjacent] = true;

                    toCheck.Enqueue(adjacent);
                }
            }
        }

    }

    public Stack<int> PathTo(int destination)
    {
        if (marked != null && !marked[destination] || source == -1)
            return null;

        Stack<int> path = new Stack<int>();

        for (int v = destination; v != source; v = edgeTo[v])
        {
            path.Push(v);
        }
        path.Push(source);

        return path;
    }

    public HashSet<int> Adj(int v)
    {
        return adj[v];
    }
}