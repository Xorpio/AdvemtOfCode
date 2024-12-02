using Microsoft.VisualBasic;

namespace AdventOfCode.Solvers.Year2024.Day2;

public class Day2Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var safe = 0;
        var safe2 = 0;
        foreach (var line in puzzle)
        {
            var numbers = line.Split(' ').Select(int.Parse).ToArray();

            if (isReportSafe(numbers))
            {
                safe++;
                safe2++;
            }
            else
            {
                //remove one number
                for (int i = 0; i < numbers.Length; i++)
                {
                    var newNumbers = new List<int>(numbers);
                    newNumbers.RemoveAt(i);
                    if (isReportSafe(newNumbers.ToArray()))
                    {
                        safe2++;
                        break;
                    }
                }
            }
        }
        GiveAnswer1(safe.ToString());
        GiveAnswer2(safe2.ToString());
    }

    private bool isReportSafe(int[] numbers)
    {
        Sort? sort = null;
        for (int i = 0; i < numbers.Length - 1; i++)
        {
            if (sort == null)
            {
                sort = numbers[i] < numbers[i + 1] ? Sort.Ascending : Sort.Descending;
            }

            if (sort == Sort.Ascending)
            {
                if (numbers[i] > numbers[i + 1] || numbers[i + 1] - numbers[i] > 3 || numbers[i] == numbers[i + 1])
                {
                    return false;
                }
            }
            else
            {
                if (numbers[i] < numbers[i + 1] || numbers[i] - numbers[i + 1] > 3 || numbers[i] == numbers[i + 1])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private enum Sort { Ascending, Descending }
}
