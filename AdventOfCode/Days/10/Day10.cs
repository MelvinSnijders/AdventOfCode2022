using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode.Days._10;

public sealed class Day10 : BaseDay
{

    private readonly string[] _instructions;
    private int _sum;
    private readonly List<int> _pixels = new();
    
    public Day10()
    {
        _instructions = File.ReadAllText(InputFilePath)
            .Split("\n");

    }
    
    public override ValueTask<string> Solve_1()
    {
        
        int register = 1;
        int currentCycle = 1;
        
        foreach (var instruction in _instructions)
        {

            if (instruction.Equals("noop"))
            {
                currentCycle = AddCycles(1, currentCycle, register);
                continue;
            }

            if (!instruction.StartsWith("addx")) continue;
            
            int addAmount = int.Parse(instruction.Split(" ")[1]);
            currentCycle = AddCycles(2, currentCycle, register);
            register += addAmount;

        }

        return new ValueTask<string>(_sum.ToString());

    }

    private int AddCycles(int amount, int currentCycle, int register)
    {
        for (int i = 0; i < amount; i++)
        {

            if ((currentCycle + 20) % 40 == 0 && currentCycle <= 220)
            {
                Console.WriteLine(currentCycle);
                _sum += register * currentCycle;
            }

            currentCycle++;
        }

        return currentCycle;
    }

    public override ValueTask<string> Solve_2()
    {

        int register = 1;
        int currentCycle = 1;
        
        foreach (var instruction in _instructions)
        {

            if (instruction.Equals("noop"))
            {
                currentCycle = AddCrtCycles(1, currentCycle, register);
                continue;
            }

            if (!instruction.StartsWith("addx")) continue;
            
            int addAmount = int.Parse(instruction.Split(" ")[1]);
            currentCycle = AddCrtCycles(2, currentCycle, register);
            register += addAmount;

        }

        string crt = new string(Enumerable.Range(1, currentCycle - 1).Select(i => _pixels.Contains(i) ? '#' : '.').ToArray());
        string answer = Regex.Replace(crt, ".{40}", "$0\n");
        
        return new ValueTask<string>(answer);
    }
    
    private int AddCrtCycles(int amount, int currentCycle, int register)
    {
        for (int i = 0; i < amount; i++)
        {
            
            if (register - 1 == (currentCycle - 1) % 40 || register + 1 == (currentCycle - 1) % 40 || register == (currentCycle - 1) % 40)
            {
                _pixels.Add(currentCycle);
            }

            currentCycle++;
        }

        return currentCycle;
    }
    
}