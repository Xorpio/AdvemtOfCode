namespace AdventOfCode.Solvers.Year2020.Day6;

public class Day6Solver : BaseSolver
{
    private int _count = 0;
    public override void Solve(string[] puzzle)
    {
        var answerPart1 = new HashSet<char>();
        var answerPart2 = new List<char>();
        var persons = 0;
        var answerPart2Count = 0;
        foreach(var line in puzzle)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                _count += answerPart1.Count;

                answerPart2Count += answerPart2.GroupBy(c => c).Where(g => g.Count() == persons).Count();

                persons = 0;
                answerPart1.Clear();
                answerPart2.Clear();
            }
            else
            {
                persons++;
                foreach (char c in line)
                {
                    answerPart1.Add(c);
                    answerPart2.Add(c);
                }
            }
        }

        _count += answerPart1.Count;
        answerPart2Count += answerPart2.GroupBy(c => c).Where(g => g.Count() == persons).Count();

        GiveAnswer1(_count.ToString());
        GiveAnswer2(answerPart2Count.ToString());
    }
}
