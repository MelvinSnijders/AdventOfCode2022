using System.ComponentModel;

namespace AdventOfCode.Day4;

[Description("In how many assignment pairs does one range fully contain the other?")]
public class Part1
{
    private readonly string[] _assignmentPairs;

    public Part1()
    {
        string input = File.ReadAllText("Day4/input.txt");
        _assignmentPairs = input.Split("\n");
    }


    public int Solve()
    {
        
        return _assignmentPairs.Where(assignmentPair =>
        {
            string[] pairs = assignmentPair.Split(",");
            
            string sectionsAssigned1 = pairs[0];
            string sectionsAssigned2 = pairs[1];
            
            string[] sections1 = sectionsAssigned1.Split('-');
            int from1 = int.Parse(sections1[0]);
            int to1 = int.Parse(sections1[1]);
            
            string[] sections2 = sectionsAssigned2.Split('-');
            int from2 = int.Parse(sections2[0]);
            int to2 = int.Parse(sections2[1]);
            
            if (from1 <= from2 && to1 >= to2)
            {
                return true;
            }
        
            return from2 <= from1 && to2 >= to1;
        }).Count();

    }
}