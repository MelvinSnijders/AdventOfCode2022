using System.Numerics;
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

        _monkeysPart1 = unparsedMonkeys.Select(ParseMonkey)
            .ToDictionary(keySelector: m => m.Id, elementSelector: m => m);

        _monkeysPart2 = unparsedMonkeys.Select(ParseMonkey)
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
        int rounds = 20;

        Dictionary<int, long> monkeyValues = new();

        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in _monkeysPart1.Select(monkeyPair => monkeyPair.Value))
            {
                while (monkey.Items.Count > 0)
                {
                    long item = monkey.Items.Dequeue();
                    monkeyValues[monkey.Id] = monkeyValues.GetValueOrDefault(monkey.Id) + 1;
                    
                    long worryLevel = monkey.CalculateWorryLevel(item) / 3;
                    if (worryLevel % monkey.TestDivisor == 0)
                        _monkeysPart1[monkey.TestTrueMonkey].Items.Enqueue(worryLevel);
                    else _monkeysPart1[monkey.TestFalseMonkey].Items.Enqueue(worryLevel);
                }
            }
        }

        long monkeyBusiness = monkeyValues.OrderByDescending(pair => pair.Value).Take(2).Select(pair => pair.Value)
            .Aggregate((a, b) => a * b);
        return new ValueTask<string>(monkeyBusiness.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int rounds = 10_000;

        Dictionary<int, long> monkeyValues = new();
        var factor = _monkeysPart2.Aggregate(1, (c, m) => c * m.Value.TestDivisor);

        for (int i = 0; i < rounds; i++)
        {
            foreach (Monkey monkey in _monkeysPart2.Select(monkeyPair => monkeyPair.Value))
            {
                while (monkey.Items.Count > 0)
                {
                    long item = monkey.Items.Dequeue();
                    monkeyValues[monkey.Id] = monkeyValues.GetValueOrDefault(monkey.Id) + 1;

                    item = monkey.CalculateWorryLevel(item) % factor;
                    if (item % monkey.TestDivisor == 0) _monkeysPart2[monkey.TestTrueMonkey].Items.Enqueue(item);
                    else _monkeysPart2[monkey.TestFalseMonkey].Items.Enqueue(item);
                }
            }
        }

        long monkeyBusiness = monkeyValues.OrderByDescending(pair => pair.Value).Take(2).Select(pair => pair.Value)
            .Aggregate((a, b) => a * b);
        return new ValueTask<string>(monkeyBusiness.ToString());
    }
}