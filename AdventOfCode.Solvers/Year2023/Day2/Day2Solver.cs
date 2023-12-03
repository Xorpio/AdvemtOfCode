
namespace AdventOfCode.Solvers.Year2023.Day2;

[RegisterSingleton<BaseSolver>(ServiceKey = "2023-2")]
public class Day2Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var games = puzzle.Select(p => new Game(p));

        var answer1 = games.Where(g => g.Red <= 12 && g.Green <= 13 && g.Blue <= 14)
            .Select(g => g.Id)
            .Sum();

        var answer2 = games.Sum(g => g.Power) - 0;

        GiveAnswer1($"{answer1}");
        GiveAnswer2($"{answer2}");
    }
}
