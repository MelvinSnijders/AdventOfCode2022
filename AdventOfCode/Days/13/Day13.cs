using System.Text.Json.Nodes;
using AoCHelper;

namespace AdventOfCode.Days._13;

public sealed class Day13 : BaseDay
{

    private readonly List<string[]> _pairs;
    private readonly List<string> _packets;
    
    public Day13()
    {
        _pairs = File.ReadAllText(InputFilePath)
            .Split("\n\n")
            .Select(pair => pair.Split("\n").ToArray())
            .ToList();
        _packets = File.ReadAllText(InputFilePath).Split("\n").Where(line => !line.Equals(string.Empty)).ToList();
    }
    
    public override ValueTask<string> Solve_1()
    {

        List<int> indices = new();
        
        for (int i = 1; i <= _pairs.Count; i++)
        {

            JsonNode? nodeLeft = JsonNode.Parse(_pairs[i - 1][0]);
            JsonNode? nodeRight = JsonNode.Parse(_pairs[i - 1][1]);

            if(Compare(nodeLeft, nodeRight) < 0) indices.Add(i);

        }

        return new ValueTask<string>(indices.Sum().ToString());

    }

    public override ValueTask<string> Solve_2()
    {
        _packets.Add("[[2]]");
        _packets.Add("[[6]]");
        _packets.Sort((first, second) => Compare(JsonNode.Parse(first), JsonNode.Parse(second)));

        int firstIndex = _packets.IndexOf("[[2]]") + 1;
        int lastIndex = _packets.IndexOf("[[6]]") + 1;
        
        return new ValueTask<string>(firstIndex * lastIndex + "");
    }

    private int Compare(JsonNode? left, JsonNode? right)
    {

        if (left is JsonValue && right is JsonValue) return (int)left - (int)right;

        JsonArray leftArray = left as JsonArray ?? new JsonArray((int)(left ?? 0));
        JsonArray rightArray = right as JsonArray ?? new JsonArray((int)(right ?? 0));

        for (int i = 0; i < Math.Min(leftArray.Count, rightArray.Count); i++)
        {
            int comparison = Compare(leftArray[i], rightArray[i]);
            if(comparison != 0) return comparison;
        }

        return leftArray.Count - rightArray.Count;

    }

}