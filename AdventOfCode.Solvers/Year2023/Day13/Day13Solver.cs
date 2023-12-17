

using System.Runtime.CompilerServices;

namespace AdventOfCode.Solvers.Year2023.Day13;

public class Day13Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        List<string> pattern = [];
        double answer1 = 0;
        double answer2 = 0;
        for (int row = 0; row < puzzle.Length; row++)
        {
            if (puzzle[row].Length == 0) 
            {
                answer1 += findPart1(pattern);
                answer2 += findPart2(pattern);
                pattern = [];
                continue;
            }

            pattern.Add(puzzle[row]);
        }

        answer1 += findPart1(pattern);
        answer2 += findPart2(pattern);

        GiveAnswer1(answer1);

        GiveAnswer2(answer2);
    }

    private int findPart1(List<string> pattern)
    {
        var ans = FindAnswer(pattern) * 100;
        if (ans > 0)
            return ans;

        pattern = flipPattern(pattern);
        ans = FindAnswer(pattern);

        if (ans == 0)
            throw new Exception("No answer found for pattern:");

        return ans;
    }

    private int findPart2(List<string> pattern)
    {
        var ans = findWithSmudge(pattern) * 100;
        if (ans > 0)
            return ans;

        pattern = flipPattern(pattern);
        ans = findWithSmudge(pattern);

        if (ans == 0)
            throw new Exception("No answer found for pattern:");

        return ans;
    }

    private int findWithSmudge(List<string> pattern)
    {
        for (int i = 0; i < pattern.Count; i++)
        {
            for(int j = i + 1; j < pattern.Count; j++)
            {
                if (pattern[i] == pattern[j])
                    continue;

                var count = 0;
                for(int c = 0; c < pattern[i].Length; c++)
                {
                    if (pattern[i][c] != pattern[j][c])
                        count++;
                }

                if (count != 1)
                    continue;

                string[] p1 = pattern.ToArray();
                p1[i] = p1[j];

                var ismirror = true;
                //now check rest
                int ii = i;
                int jj = j;
                while(ii < jj)
                {
                    if (p1[ii] != p1[jj])
                    {
                        ismirror = false;
                        break;
                    }
                    ii++;
                    jj--;
                }

                for (int x = 0; x <= jj; x++)
                {
                    if ((p1.Length > jj + x + 1) && p1[jj - x] != p1[jj + x + 1])
                    {
                        ismirror = false;
                        break;
                    }
                }

                if (ismirror)
                    return ii;
            }
        }
        return 0;
    }

    private List<string> flipPattern(List<string> pattern)
    {
        var r = new string[pattern[0].Length];
        foreach(var row in pattern)
        {
            for(int i = 0; i < row.Length; i++)
            {
                r[i] += row[i];
            }
        }

        return r.ToList();
    }

    private int FindAnswer(List<string> pattern)
    {
        //try find horizontal
        for(int i = 0; i < pattern.Count - 1; i++)
        {
            if (pattern[i] == pattern[i + 1])
            {
                var ismirror = true;
                //now check rest
                for (int x = 0; x <= i; x++)
                {
                    if (pattern.Count > i + x + 1 && pattern[i - x] != pattern[i + x + 1])
                    {
                        ismirror = false;
                        break;
                    }
                }
                if (ismirror)
                    return i + 1;
            }
        }

        return 0;
    }
}

public record Point(int row, int col);
