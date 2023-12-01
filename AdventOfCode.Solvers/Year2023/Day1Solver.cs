using System.Diagnostics.Contracts;
using System.Reactive.Subjects;
using AdventOfCode.Lib;

namespace AdventOfCode.Solvers.Year2023;

public class Day1Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var answer1 = puzzle.Select(SolveLine)
        .Sum().ToString();

        var answer2 = puzzle.Select(SolveLinePart2)
        .Sum().ToString();

        GiveAnswer1(answer1);
        GiveAnswer2(answer2);
    }

    public int SolveLine(string puzzle)
    {
        //find first number in string
        var firstNumber = puzzle.FirstOrDefault(char.IsDigit);
        //find last number in string
        var lastNumber = puzzle.LastOrDefault(char.IsDigit);

        //return sum of first and last number
        var succes = int.TryParse($"{firstNumber}{lastNumber}" , out int result);

        if (succes)
        {
            return result;
        }

        return 0;
    }

    public int SolveLinePart2(string line)
    {
        int firstNumber = findFirstNumber(line);
        int lastNumber = findlastNumber(line);

        return int.Parse($"{firstNumber}{lastNumber}");
    }

    private int findlastNumber(string line)
    {
        for (int i = line.Length -1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }

            switch (line[i])
            {
                case 'n':
                    if (line[i..].StartsWith("nine"))
                    {
                        return 9;
                    }
                    break;
                case 'e':
                    if (line[i..].StartsWith("eight"))
                    {
                        return 8;
                    }
                    break;
                case 's':
                    if (line[i..].StartsWith("seven"))
                    {
                        return 7;
                    }
                    if (line[i..].StartsWith("six"))
                    {
                        return 6;
                    }
                    break;
                case 'f':
                    if (line[i..].StartsWith("five"))
                    {
                        return 5;
                    }
                    if (line[i..].StartsWith("four"))
                    {
                        return 4;
                    }
                    break;
                case 't':
                    if (line[i..].StartsWith("three"))
                    {
                        return 3;
                    }
                    if (line[i..].StartsWith("two"))
                    {
                        return 2;
                    }
                    break;
                case 'o':
                    if (line[i..].StartsWith("one"))
                    {
                        return 1;
                    }
                    break;
            }
        }

        throw new Exception($"No first number found in string {line}");
    }

    public int findFirstNumber(string line)
    {
        for(int i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                return int.Parse(line[i].ToString());
            }

            switch(line[i])
            {
                case 'n':
                    if (line[i..].StartsWith("nine"))
                    {
                        return 9;
                    }
                    break;
                case 'e':
                    if (line[i..].StartsWith("eight"))
                    {
                        return 8;
                    }
                    break;
                case 's':
                    if (line[i..].StartsWith("seven"))
                    {
                        return 7;
                    }
                    if (line[i..].StartsWith("six"))
                    {
                        return 6;
                    }
                    break;
                case 'f':
                    if (line[i..].StartsWith("five"))
                    {
                        return 5;
                    }
                    if (line[i..].StartsWith("four"))
                    {
                        return 4;
                    }
                    break;
                case 't':
                    if (line[i..].StartsWith("three"))
                    {
                        return 3;
                    }
                    if (line[i..].StartsWith("two"))
                    {
                        return 2;
                    }
                    break;
                case 'o':
                    if (line[i..].StartsWith("one"))
                    {
                        return 1;
                    }
                    break;
            }
        }
        throw new Exception($"No last number found in string {line}");
    }
}
