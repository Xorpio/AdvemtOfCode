namespace AdventOfCode.Lib.Tests;

public class TestSolver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        GiveAnswer1($"answer1{puzzle[0]}");
        GiveAnswer2($"answer2{puzzle[1]}");
    }
}
