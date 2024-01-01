using System.IO.Compression;
using System.Text;

namespace AdventOfCode.Solvers.Year2023.Day22;

public class Day22Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var bricks = new List<Brick>();
        int index = 0;
        foreach(var line in puzzle)
        {
            var brick = new Brick(line, index++);
            bricks.Add(brick);
        }

        bricks = FallDown(bricks);
        logger.OnNext("After falling down:");
        
        decimal disintigrate = 0;
        decimal chain = 0;
        foreach(var brick in bricks)
        {
            var result = CanDisintigrate(bricks, brick);
            if (result == 0)
            {
                disintigrate += 1;
            }
            else
            {
                logger.OnNext($"Brick {brick.Index} disintigrates {result} bricks");
                chain += result;
            }
        }

        GiveAnswer1(disintigrate);
        GiveAnswer2(chain);
    }

    private List<Brick> FallDown(List<Brick> bricks)
    {
        var result = new List<Brick>();
        var seen = new HashSet<Brick>();
        int z = 0;
        while(seen.Count < bricks.Count)
        {
            for(int i = 0; i < bricks.Count; i++)
            {
                if(seen.Contains(bricks[i]) || bricks[i].ZStart > z)
                {
                    continue;
                }
                var brick = bricks[i];
                seen.Add(brick);

                //do falling here
                while(brick.ZStart > 1 && result.All(b => b.Clears(brick with { ZStart = brick.ZStart - 1, ZEnd = brick.ZEnd - 1 })))
                {
                    brick = brick with { ZStart = brick.ZStart - 1, ZEnd = brick.ZEnd - 1 };
                }

                result.Add(brick);
            }
            z++;
        }
        return result;
    }

    private int CanDisintigrate(List<Brick> bricks, Brick originalBrick)
    {
        var fallen = new HashSet<int>();
        var seen = new HashSet<Brick>();
        var result = new List<Brick>();
        int z = 0;
        while(seen.Count < bricks.Count - 1)
        {
            for(int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Index == originalBrick.Index)
                {
                    continue;
                }

                if(seen.Contains(bricks[i]) || bricks[i].ZStart > z)
                {
                    continue;
                }
                var brick = bricks[i];
                seen.Add(brick);

                if (brick.ZEnd >= originalBrick.ZStart)
                {
                    //do falling here
                    while(brick.ZStart > 1 && result.All(b => b.Clears(brick with { ZStart = brick.ZStart - 1, ZEnd = brick.ZEnd - 1 })))
                    {
                        if (!fallen.Contains(brick.Index))
                            fallen.Add(brick.Index);
                        brick = brick with { ZStart = brick.ZStart - 1, ZEnd = brick.ZEnd - 1 };
                    }
                }

                result.Add(brick);
            }
            z++;
        }
        return fallen.Count();
    }
}

public record Brick
{
    public int XStart { get; init; }
    public int YStart { get; init; }
    public int ZStart { get; init; }
    public int XEnd { get; init; }
    public int YEnd { get; init; }
    public int ZEnd { get; init; }
    public int Index { get; }

    public Brick(string input, int index)
    {
        var parts = input.Split('~');
        var left = parts[0].Split(',');
        var right = parts[1].Split(',');

        XStart = int.Parse(left[0]);
        YStart = int.Parse(left[1]);
        ZStart = int.Parse(left[2]);

        XEnd = int.Parse(right[0]);
        YEnd = int.Parse(right[1]);
        ZEnd = int.Parse(right[2]);
        Index = index;
    }

    internal bool Clears(Brick brick)
    {
        if (XStart > brick.XEnd || XEnd < brick.XStart)
        {
            return true;
        }
        if (YStart > brick.YEnd || YEnd < brick.YStart)
        {
            return true;
        }
        if (ZStart > brick.ZEnd || ZEnd < brick.ZStart)
        {
            return true;
        }
        return false;
    }
}
