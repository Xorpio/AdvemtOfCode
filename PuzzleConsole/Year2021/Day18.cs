namespace PuzzleConsole.Year2021.Day18;

public class Day18 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
	    return puzzle;
    }
}

public class SnailfishNumber
{
    public SnailfishNumber LeftPart { get; set; }
    public int? LeftNumber { get; set; }
    public SnailfishNumber RightPart { get; set; }
    public int? RightNumber { get; set; }

    public override string ToString()
    {
        var left = LeftPart?.ToString() ?? LeftNumber.ToString();
        var right = RightPart?.ToString() ?? RightNumber.ToString();
        return $"[{left},{right}]";
    }

    public static SnailfishNumber operator + (SnailfishNumber a, SnailfishNumber b)
    {
        return FromString($"[{a},{b}]");
    }

    public static SnailfishNumber FromString(string input)
    {
        var number = new SnailfishNumber();
        var count = 0;
        for (int i = 1; i < input.Length; i++)
        {
            count += input[i] switch
            {
                '[' => 1,
                ']' => -1,
                _ => 0
            };

            if (input[i] == ',' && count == 0)
            {
                var left = input[1..i];
                var right = input[(i + 1)..^1];

                if (left[0] == '[')
                {
                    number.LeftPart = FromString(left);
                }
                else
                {
                    number.LeftNumber = Int32.Parse(left);
                }
                if (right[0] == '[')
                {
                    number.RightPart = FromString(right);
                }
                else
                {
                    number.RightNumber = Int32.Parse(right);
                }

                number.Reduce();

                return number;
            }
        }

        throw new Exception("Number not created");
    }

    private void Reduce()
    {
        var explode = false;
        var split = false;
        do
        {
            explode = Explode(0).exploded;
        } while (explode || split);
    }

    public (bool exploded, int left, int right) Explode(int depth)
    {
        if (LeftPart != null)
        {
            var left = LeftPart.Explode(depth + 1);
            if (left.exploded)
            {
                LeftPart = null;
                LeftNumber = 0;
                if (left.right > 0)
                {
                    if (RightPart is SnailfishNumber)
                    {
                        RightPart.AddFromLeft(left.right);
                    }
                    else
                    {
                        RightNumber += left.right;
                    }
                    left.right = 0;
                }
                return left;
            }
        }

        if (RightPart != null)
        {
            return RightPart.Explode(depth + 1);
        }

        if (depth > 4)
        {
            return (true, LeftNumber.Value, RightNumber.Value);
        }

        return (false, 0, 0);
    }

    public void AddFromLeft(int num)
    {
        if (LeftPart is SnailfishNumber)
        {
            LeftPart.AddFromLeft(num);
        }
        else
        {
            LeftNumber += num;
        }
    }
}
