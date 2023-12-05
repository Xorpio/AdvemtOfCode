namespace AdventOfCode.Solvers.Year2020.Day3;

public class Day3Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var y = 0;

        var slopes = new List<(int right, int down)>()
        {
            (1, 1),
            (3, 1),
            (5, 1),
            (7, 1),
            (1, 2)
        };

        decimal allTress = 0;
        foreach (var slope in slopes)
        {
            var seenTrees = 0;
            y = 0;

            for(int x = 0; x < puzzle.Length; x += slope.down)
            {
                var col = y % puzzle[x].Length;
                if (puzzle[x][col] == '#')
                {
                    seenTrees++;
                }

                y += slope.right;
            }

            if (slope.down == 1 && slope.right == 3)
            {
                GiveAnswer1(seenTrees.ToString());
            }

            if (allTress == 0)
                allTress = seenTrees;
            else
                allTress = seenTrees * allTress;
        }

        GiveAnswer2(allTress.ToString());
    }
}
