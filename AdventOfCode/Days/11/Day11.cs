using AoCHelper;

namespace AdventOfCode.Days._11;

public sealed class Day11 : BaseDay
{
    private readonly Dictionary<int, Monkey> _monkeysPart1;
    private readonly Dictionary<int, Monkey> _monkeysPart2;

    public Day11()
    {
        string[] unparsedMonkeys = File.ReadAllText(InputFilePath)
            .Split("\n\n");

        _monkeysPart1 = ParseInput(unparsedMonkeys);
        _monkeysPart2 = ParseInput(unparsedMonkeys);
    }

    private Dictionary<int, Monkey> ParseInput(string[] unparsed)
    {
        return unparsed.Select(ParseMonkey)
            .ToDictionary(keySelector: m => m.Id, elementSelector: m => m);
    }

    private Monkey ParseMonkey(string unparsed)
    {
        string[] lines = unparsed.Split("\n");

        int monkeyId = int.Parse(lines[0].Replace(":", "").Split(" ")[1]);
        Queue<long> items = new(lines[1][18..].Split(",").Select(long.Parse));
        string operation = lines[2][19..];
        int divisor = int.Parse(lines[3][21..]);
        int testTrueMonkey = int.Parse(lines[4][29..]);
        int testFalseMonkey = int.Parse(lines[5][30..]);

        return new Monkey(monkeyId, items, operation, divisor, testTrueMonkey, testFalseMonkey);
    }

    public override ValueTask<string> Solve_1()
    {
        Dictionary<int, long> monkeyValues = Play(_monkeysPart1, 20, input => input / 3);
        return new ValueTask<string>(CalculateBusiness(monkeyValues).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int factor = _monkeysPart2.Aggregate(1, (c, m) => c * m.Value.TestDivisor);
        Dictionary<int, long> monkeyValues = Play(_monkeysPart2, 10000, input => input % factor);
        return new ValueTask<string>(CalculateBusiness(monkeyValues).ToString());
    }

    private long CalculateBusiness(Dictionary<int, long> monkeyValues)
    {
        return monkeyValues.OrderByDescending(pair => pair.Value)
            .Take(2)
            .Select(pair => pair.Value)
            .Aggregate((a, b) => a * b);
    }

    private Dictionary<int, long> Play(IReadOnlyDictionary<int, Monkey> monkeys, int rounds, Func<long, long> stressOperation)
    {
        
        Dictionary<int, long> monkeyValues = new();
        
        for (int i = 0; i < rounds; i++)
        {
            foreach (Monkey monkey in monkeys.Select(monkeyPair => monkeyPair.Value))
            {
                while (monkey.Items.Count > 0)
                {
                    long item = monkey.Items.Dequeue();
                    monkeyValues[monkey.Id] = monkeyValues.GetValueOrDefault(monkey.Id) + 1;
                    
                    item = monkey.CalculateWorryLevel(item);
                    item = stressOperation(item);

                    if (item % monkey.TestDivisor == 0) monkeys[monkey.TestTrueMonkey].Items.Enqueue(item);
                    else monkeys[monkey.TestFalseMonkey].Items.Enqueue(item);
                }
            }
        }

        return monkeyValues;

    }
    
}