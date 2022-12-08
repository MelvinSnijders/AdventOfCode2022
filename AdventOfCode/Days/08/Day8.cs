using AoCHelper;

namespace AdventOfCode.Days._08;

public sealed class Day8 : BaseDay
{
    private readonly int[][] _grid;

    public Day8()
    {
        string input = File.ReadAllText(InputFilePath);

        string[] lines = input.Split("\n");
        _grid = lines.ToList()
            .Select(line =>
                line.Select(ch => int.Parse(ch.ToString())).ToArray())
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {
        int visible = 0;

        for (int i = 0; i < _grid.Length; i++) // Vertical position
        {
            for (int j = 0; j < _grid[i].Length; j++) // Horizontal position
            {
                int height = _grid[j][i];

                if (j == 0 || i == 0 || j == _grid[i].Length - 1 || i == _grid.Length - 1)
                {
                    visible++;
                    continue;
                }

                // Check to the left
                bool treeIsVisible = true;
                for (int k = i - 1; k >= 0; k--)
                {
                    if (_grid[j][k] < height) continue;
                    treeIsVisible = false;
                    break;
                }

                if (treeIsVisible)
                {
                    visible++;
                    continue;
                }

                // Check to the right
                treeIsVisible = true;
                for (int k = i + 1; k < _grid[i].Length; k++)
                {
                    if (_grid[j][k] < height) continue;
                    treeIsVisible = false;
                    break;
                }

                if (treeIsVisible)
                {
                    visible++;
                    continue;
                }

                // Check above
                treeIsVisible = true;
                for (int k = j - 1; k >= 0; k--)
                {
                    if (_grid[k][i] < height) continue;
                    treeIsVisible = false;
                    break;
                }

                if (treeIsVisible)
                {
                    visible++;
                    continue;
                }

                // Check below
                treeIsVisible = true;
                for (int k = j + 1; k < _grid.Length; k++)
                {
                    if (_grid[k][i] < height) continue;
                    treeIsVisible = false;
                    break;
                }

                if (!treeIsVisible) continue;
                visible++;
            }
        }

        return new ValueTask<string>(visible.ToString());
    }

    public override ValueTask<string> Solve_2()
    {

        int highestScenicScore = 0;
        
        for (int i = 0; i < _grid.Length; i++) // Vertical position
        {
            for (int j = 0; j < _grid[i].Length; j++) // Horizontal position
            {
                int height = _grid[j][i];

                // Check to the left
                int viewDistanceLeft = 0;
                for (int k = i - 1; k >= 0; k--)
                {
                    viewDistanceLeft++;
                    if (_grid[j][k] >= height) break;
                  
                }

                // Check to the right
                int viewDistanceRight = 0;
                for (int k = i + 1; k < _grid[i].Length; k++)
                {
                    viewDistanceRight++;
                    if (_grid[j][k] >= height) break;
                  
                }

                // Check above
                int viewDistanceAbove = 0;
                for (int k = j - 1; k >= 0; k--)
                {
                    viewDistanceAbove++;
                    if (_grid[k][i] >= height) break;
                 
                }
                
                // Check below
                int viewDistanceBelow = 0;
                for (int k = j + 1; k < _grid.Length; k++)
                {
                    viewDistanceBelow++;
                    if (_grid[k][i] >= height) break;
              
                }

                int scenicScore = viewDistanceLeft * viewDistanceRight * viewDistanceBelow * viewDistanceAbove;
                if(scenicScore > highestScenicScore) highestScenicScore = scenicScore;
            }
        }

        return new ValueTask<string>(highestScenicScore.ToString());
    }
}