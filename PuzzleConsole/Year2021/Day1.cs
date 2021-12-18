using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PuzzleConsole.Year2021.Day1;

public class Day1 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var source = new Subject<int>();

        int val = 0;
        int val2 = 0;
        source.Buffer<int>(2, 1)
            .Select(b => b.Count() == 2 && b[0] < b[1] ? 1 : 0)
            .Sum()
            .Subscribe(v => val = v);

        source.Buffer<int>(3, 1)
            .Select(b => b.Sum())
            .Buffer<int>(2, 1)
            .Select(b => b.Count() == 2 && b[0] < b[1] ? 1 : 0)
            .Sum()
            .Subscribe(v => val2 = v);

        foreach(var item in puzzle)
        {
            int.TryParse(item, out var depth);
            source.OnNext(depth);
        }

        source.OnCompleted();

        return new string[] { val.ToString(), val2.ToString() };
    }
}
