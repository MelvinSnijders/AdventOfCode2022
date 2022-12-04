namespace AdventOfCode.Day4;

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

        int fullyContainsCount = 0;
        
        foreach (string assignmentPair in _assignmentPairs)
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
                fullyContainsCount++;
            } else if (from2 <= from1 && to2 >= to1)
            {
                fullyContainsCount++;
            }

        }

        return fullyContainsCount;

    }
}