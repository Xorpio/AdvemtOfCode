using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PuzzleConsole.Year2021.Day2;

public class Day2 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var source = new Subject<string>();

        int x1 = 0;
        int y1 = 0;

        int x2 = 0;
        int y2 = 0;
        int aim = 0;

        source
            .Select(s => s.Split(" "))
            .Select(s => new { direction = s.First(), num = int.Parse(s.Last()) })
            .Subscribe(cur => 
            {
                switch (cur.direction)
                {
                    case "forward":
                        x1 += cur.num;
                        x2 += cur.num;
                        y2 += aim * cur.num;
                        break;
                    case "down":
                        y1 += cur.num;
                        aim += cur.num; ;
                        break;
                    case "up":
                        y1 -= cur.num;
                        aim -= cur.num;
                        break;
                    default:
                        throw new Exception($"direction: {cur.direction} not supported");
                }
            });
        //.Subscribe(v => val = v);
        foreach (var item in puzzle)
        {
            source.OnNext(item);
        }

        source.OnCompleted();

        return new string[] { $"{x1 * y1}", $"{ x2 * y2 }" };
    }
}
