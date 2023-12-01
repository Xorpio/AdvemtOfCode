using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

namespace AdventOfCode.Lib.Tests;

public class TestSolver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        GiveAnswer1($"answer1{puzzle[0]}");
        GiveAnswer2($"answer2{puzzle[1]}");
    }
}
