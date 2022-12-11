using System.Data;

namespace AdventOfCode.Days._11;

public class Monkey
{
    public int Id { get; }
    public Queue<long> Items { get; }
    public string Operation { get; }
    public int TestDivisor { get; }
    public int TestTrueMonkey { get; }
    public int TestFalseMonkey { get; }

    public Monkey(int id, Queue<long> items, string operation, int testDivisor, int testTrueMonkey, int testFalseMonkey)
    {
        Id = id;
        Items = items;
        Operation = operation;
        TestDivisor = testDivisor;
        TestTrueMonkey = testTrueMonkey;
        TestFalseMonkey = testFalseMonkey;
    }

    public long CalculateWorryLevel(long currentLevel)
    {
        string[] operators = Operation.Split(" ");

        long left = operators[0].Equals("old") ? currentLevel : int.Parse(operators[0]);
        char operation = operators[1][0];
        long right = operators[2].Equals("old") ? currentLevel : int.Parse(operators[2]);

        return new Calculator(left, operation, right).Calculate();
        
    }

    public override string ToString()
    {
      return $"Monkey {Id}:\n" +
               $"  Starting items: {string.Join(", ", Items)}\n" +
               $"  Operation: new = {Operation}\n" +
               $"  Test: divisible by {TestDivisor}\n" +
               $"    If true: throw to monkey {TestTrueMonkey}\n" +
               $"    If false: throw to monkey {TestFalseMonkey}\n";
    }
}