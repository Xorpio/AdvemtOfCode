using System.Reactive.Subjects;

namespace AdventOfCode.Solvers.Year2024.Day1;

public class Day1Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var list1 = new List<int>();
        var list2 = new List<int>();
        foreach(var line in puzzle)
        {
            var numbers = line.Split("   ");
            var a = int.Parse(numbers[0]);
            var b = int.Parse(numbers[1]);

            list1.Add(a);
            list2.Add(b);
        }

        list1.Sort();
        list2.Sort();

        var answer = list1.Zip(list2, (a, b) => {
            if (a > b)
            {
                return a - b;
            }
            else
            {
                return b - a;
            }   
        }).Sum().ToString();
        GiveAnswer1(answer);

        var sum = 0;
        foreach(var n in list1)
        {
            sum += n * list2.Where(x => x == n).Count();
        }

        GiveAnswer2(sum.ToString());
    }
}
