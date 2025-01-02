namespace AdventOfCode.Solvers.Year2024.Day25;

public class Day25Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {

        /*

    .....
    .....
    .....
    #....
    #.#..
    #.#.#
    #####

        */

        var locks = new List<int[]>();
        var keys = new List<int[]>();

        for (int i = 0; i < puzzle.Length; i = i + 8)
        {
            if (puzzle[i][0] == '.')
                keys.Add(GetKey(puzzle[i..(i + 7)]));
            else
                locks.Add(GetLock(puzzle[i..(i + 7)]));
        }

        decimal count = 0;
        foreach (var key in keys)
        {
            foreach (var slot in locks)
            {
                var fits = true;
                for (int i = 0; i < key.Length; i++)
                {
                    if (key[i] + slot[i] >= 6)
                    {
                        fits = false;
                        break;
                    }
                }

                if (fits)
                    count++;
            }
        }

        GiveAnswer1(count);
        GiveAnswer2("");
    }

    private int[] GetLock(string[] puzzle)
    {
        var l = new int[puzzle[0].Length];
        for (int i = 1; i < puzzle.Length; i++)
        {
            for (int j = 0; j < puzzle[i].Length; j++)
            {
                if (puzzle[i][j] == '#')
                    l[j]++;
            }
        }
        return l;
    }
    private int[] GetKey(string[] puzzle)
    {
        var k = new int[puzzle[0].Length];

        for (int i = puzzle.Length - 2; i >= 0; i--)
        {
            for (int j = 0; j < puzzle[i].Length; j++)
            {
                if (puzzle[i][j] == '#')
                    k[j]++;
            }
        }
        return k;
    }
}
