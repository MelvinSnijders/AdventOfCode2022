using AoCHelper;

namespace AdventOfCode.Days._12;

public sealed class Day12 : BaseDay
{
    private Dictionary<(int, int), Vertex> _vertices = new();

    public Day12()
    {
        string unparsedGraph = File.ReadAllText(InputFilePath);
        char[][] unparsed = unparsedGraph.Split("\n").Select(line => line.ToCharArray()).ToArray();
        ParseGraph(unparsed);
    }

    private void ParseGraph(char[][] unparsed)
    {
        for (int y = 0; y < unparsed.Length; y++)
        {
            for (int x = 0; x < unparsed[y].Length; x++)
            {
                char c = unparsed[y][x];
                Vertex vertex = _vertices.GetValueOrDefault((y, x), new Vertex(c));
                _vertices[(y, x)] = vertex;
                
                // check left char
                if (x > 0)
                {
                    char tChar = unparsed[y][x - 1];
                    if (Math.Abs(tChar - c) <= 1 || (tChar < c && tChar != 'E' && tChar != 'z') || (tChar == 'E' && c == 'z') || (c == 'S' && tChar == 'a'))
                    {
                     
                        Vertex targetVertex = _vertices.GetValueOrDefault((y, x - 1), new Vertex(tChar));
                        _vertices[(y, x - 1)] = targetVertex;
                        Edge edge = new Edge(targetVertex);
                        vertex.Adjacent.AddLast(edge);
                    }
                }
                
                // check right char
                if (x < unparsed[y].Length - 1)
                {
                    char tChar = unparsed[y][x + 1];
                    if (Math.Abs(tChar - c) <= 1 || (tChar < c && tChar != 'E' && tChar != 'z') || (tChar == 'E' && c == 'z') || (c == 'S' && tChar == 'a'))
                    {
                        Vertex targetVertex = _vertices.GetValueOrDefault((y, x + 1), new Vertex(tChar));
                        _vertices[(y, x + 1)] = targetVertex;
                        Edge edge = new Edge(targetVertex);
                        vertex.Adjacent.AddLast(edge);
                    }
                }
                
                // check top char
                if (y > 0)
                {
                    char tChar = unparsed[y - 1][x];
                    if (Math.Abs(tChar - c) <= 1  || (tChar < c && tChar != 'E' && tChar != 'z') || (tChar == 'E' && c == 'z') || (c == 'S' && tChar == 'a'))
                    {
                        Vertex targetVertex = _vertices.GetValueOrDefault((y - 1, x), new Vertex(tChar));
                        _vertices[(y - 1, x)] = targetVertex;
                        Edge edge = new Edge(targetVertex);
                        vertex.Adjacent.AddLast(edge);
                    }
                }
                
                // check bottom char
                if (y < unparsed.Length - 1)
                {
                    char tChar = unparsed[y + 1][x];
                    if (Math.Abs(tChar - c) <= 1 || (tChar < c && tChar != 'E' && tChar != 'z') || (tChar == 'E' && c == 'z') || (c == 'S' && tChar == 'a'))
                    {
                        Vertex targetVertex = _vertices.GetValueOrDefault((y + 1, x), new Vertex(tChar));
                        _vertices[(y + 1, x)] = targetVertex;
                        Edge edge = new Edge(targetVertex);
                        vertex.Adjacent.AddLast(edge);
                    }
                }
                
            }
            
        }

      
        
    }
    
    public override ValueTask<string> Solve_1()
    {
        Vertex start = _vertices.First((pair) => pair.Value.Name == 'S').Value;
        Search(start);

        return new ValueTask<string>(_vertices.First((pair) => pair.Value.Name == 'E').Value.Distance.ToString());
    }

    // Solution can be improved by searching from the end to all A's instead of performing a lot of BFS's.
    public override ValueTask<string> Solve_2()
    {
        List<int> distances = new();
        foreach (Vertex vertex in _vertices.Where(pair => pair.Value.Name == 'a').Select(pair => pair.Value))
        {
            Search(vertex);
            distances.Add(_vertices.First(pair => pair.Value.Name == 'E').Value.Distance);
        }

        return new ValueTask<string>(distances.Min().ToString());
    }

    private void Search(Vertex start)
    {
        _vertices.Values.ToList().ForEach(v => v.Reset());

        Queue<Vertex> queue = new Queue<Vertex>();
        queue.Enqueue(start);
        start.Distance = 0;

        while (queue.Count > 0)
        {
            Vertex vertex = queue.Dequeue();

            foreach (Edge edge in vertex.Adjacent)
            {
                Vertex w = edge.Destination;
                if (w.Distance == int.MaxValue)
                {
                    w.Distance = vertex.Distance + 1;
                    w.Previous = vertex;
                    queue.Enqueue(w);
                }
            }
        }
    }

}