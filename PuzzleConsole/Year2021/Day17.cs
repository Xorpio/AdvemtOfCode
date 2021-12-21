namespace PuzzleConsole.Year2021.Day17;

public class Day17 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var target = GetMinMax(puzzle[0]);

        var maxY = FindMaxYVelocity(target);

        var Hit = 0;
        for (int y = target.YMax; y < Math.Abs(target.YMax); y++)
        {
            for (int x = 0; x <= target.XMax; x++)
                Hit += (Fire(x, y, target)) ? 1 : 0;
        }

        return new string[] { maxY.maxY.ToString(), Hit.ToString() };
    }

    public (int XMin, int XMax, int YMin, int YMax) GetMinMax(string input)
    {
        //var input = "target area: x=20..30, y=-10..-5";
        var split = input.Split(": ")[1].Split(", ");
        var x = split[0]
            .Split('=')[1]
            .Split("..");
        var y = split[1]
            .Split('=')[1]
            .Split("..");

        return (
            int.Parse(x[0]),
            int.Parse(x[1]),
            int.Parse(y[1]),
            int.Parse(y[0])
            );
    }

    public bool Fire(int xVelocity, int yVelocity, (int XMin, int XMax, int YMin, int YMax) target)
    {
        (int x, int y) pos = new(0, 0);

        var mis = false;

        while (!mis)
        {
            pos.x += xVelocity;
            pos.y += yVelocity;
            
            if(xVelocity > 0)
                xVelocity--;
            if (xVelocity < 0)
                xVelocity++;

            yVelocity--;

            if (pos.x >= target.XMin && pos.x <= target.XMax &&
                pos.y <= target.YMin && pos.y >= target.YMax)
            {
                return true;
            }

            if (pos.x > target.XMax || pos.y < target.YMax)
            {
                mis = true;
            }
        }

        return false;
    }

    public (int maxY, int yVelocity) FindMaxYVelocity((int XMin, int XMax, int YMin, int YMax) target)
    {
        var yVelocity = Math.Abs(target.YMax) - 1;
        return new(FireY(yVelocity, target.YMin, target.YMax).maxY, yVelocity);
    }
    public (int maxY, bool result) FireY(int yVelocity, int YMin, int YMax)
    {
        var pos = 0;
        var max = 0;

        var mis = false;

        while (!mis)
        {
            pos += yVelocity;
            if (pos > max)
                max = pos;
            
            yVelocity--;

            if (pos <= YMin && pos >= YMax)
            {
                return (max, true);
            }

            if (pos < YMax)
            {
                mis = true;
            }
        }

        return (max, false);
    }
}
