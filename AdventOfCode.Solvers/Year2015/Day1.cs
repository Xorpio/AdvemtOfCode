using AdventOfCode.Lib;

namespace AdventOfCode.Solvers.Year2015;
public class Day1 : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var line = puzzle[0];

        var floor = 0;
        var pos = 1;
        foreach(var c in line)
        {
            if (c == '(')
            {
                floor++;
            }
            else if (c == ')')
            {
                floor--;
            }

            if (floor == -1)
            {
                answer2.OnNext(pos.ToString());
                answer2.OnCompleted();
            }
            pos++;
        }

        answer1.OnNext(floor.ToString());
        answer1.OnCompleted();
    }
}
