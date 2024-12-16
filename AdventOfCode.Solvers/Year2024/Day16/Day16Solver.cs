
namespace AdventOfCode.Solvers.Year2024.Day16;

public class Day16Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        (int startrow, int startcol, int endrow, int endcol) = (0, 0, 0, 0);

        for(int row = 0; row < puzzle.Length; row++)
        {
            for(int col = 0; col < puzzle[row].Length; col++)
            {
                if(puzzle[row][col] == 'S')
                {
                    startrow = row;
                    startcol = col;
                }
                else if(puzzle[row][col] == 'E')
                {
                    endrow = row;
                    endcol = col;
                }
            }
        }

        var score = dijkstra(puzzle, startrow, startcol, endrow, endcol);

        GiveAnswer1(score);
        GiveAnswer2("");
    }

    private decimal dijkstra(string[] puzzle, int startrow, int startcol, int endrow, int endcol)
    {
        var paths = new Dictionary<(int row, int col, int rowd, int cold), decimal>();

        var x = new Queue<(int row, int col, int rowd, int cold)>();
        (int rowd, int cold) direction = (0, 1); // east
        while (true)
        {
        }
        return 0;
    }
}
