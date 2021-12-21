using System.Linq;
using PuzzleConsole;

namespace PuzzleConsole.Year2021.Day18;

public class Day18 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var nums = puzzle.Select(l => SnailfishNumber.FromString(l));

        decimal max = 0;
        SnailfishNumber sum = null;

        foreach (var num in nums)
        {
            foreach (var num2 in nums)
            {
                if (num.ToString() != num2.ToString())
                {
                    var magA = (num + num2).Magnitude;
                    var magB = (num + num2).Magnitude;
                    if (magA > max)
                    {
                        max = magA;
                    }
                    if (magB > max)
                    {
                        max = magB;
                    }
                }
            }

            sum = (sum is null) ? num : sum + num;
        }

        return new string[]
        {
            sum.Magnitude.ToString(), max.ToString()
        };
    }
}

public class SnailfishNumber
{
    public SnailfishNumber LeftPart { get; set; }
    public int? LeftNumber { get; set; }
    public SnailfishNumber RightPart { get; set; }
    public int? RightNumber { get; set; }
    public decimal Magnitude
    {
        get
        {
            var left = (LeftPart is SnailfishNumber) ? LeftPart.Magnitude : LeftNumber.Value;
            var right = (RightPart is SnailfishNumber) ? RightPart.Magnitude : RightNumber.Value;

            return (left * 3) + (right * 2);
        }
    }

    public override string ToString()
    {
        var left = LeftPart?.ToString() ?? LeftNumber.ToString();
        var right = RightPart?.ToString() ?? RightNumber.ToString();
        return $"[{left},{right}]";
    }

    public static SnailfishNumber operator +(SnailfishNumber a, SnailfishNumber b)
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
            explode = false;
            split = false;

            explode = Explode(0).exploded;
            if (!explode)
            {
                split = Split();
            }
        } while (explode || split);
    }

    private bool Split()
    {
        if (LeftNumber > 9)
        {
            var (left, right) = SplitNum(LeftNumber.Value);
            LeftNumber = null;
            LeftPart = new SnailfishNumber();
            LeftPart.LeftNumber = left;
            LeftPart.RightNumber = right;
            return true;
        }
        if (LeftPart is SnailfishNumber && LeftPart.Split())
        {
            return true;
        }
        if (RightNumber > 9)
        {
            var (left, right) = SplitNum(RightNumber.Value);
            RightNumber = null;
            RightPart = new SnailfishNumber();
            RightPart.LeftNumber = left;
            RightPart.RightNumber = right;
            return true;
        }
        if (RightPart is SnailfishNumber && RightPart.Split())
        {
            return true;
        }
        return false;
    }

    private (int left, int right) SplitNum(int num)
    {
        var left = (int)Math.Floor(num / 2.0);
        var right = (int)Math.Ceiling(num / 2.0);

        return (left, right);
    }

    public (bool exploded, int left, int right) Explode(int depth)
    {
        if (LeftPart != null)
        {
            var left = LeftPart.Explode(depth + 1);
            if (left.exploded)
            {
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
            var right = RightPart.Explode(depth + 1);
            if (right.exploded)
            {
                if (right.left > 0)
                {
                    if (LeftPart is SnailfishNumber)
                    {
                        LeftPart.AddFromRight(right.left);
                    }
                    else
                    {
                        LeftNumber += right.left;
                    }
                    right.left = 0;
                }
                return right;
            }
        }

        if (depth >= 3 && LeftPart is SnailfishNumber)
        {
            var left = LeftPart.LeftNumber.Value;
            var right = LeftPart.RightNumber.Value;
            LeftPart = null;
            LeftNumber = 0;


            if (RightPart is SnailfishNumber)
            {
                RightPart.AddFromLeft(right);
            }
            else
            {
                RightNumber += right;
            }

            return (true, left, 0);
        }

        if (depth >= 3 && RightPart is SnailfishNumber)
        {
            var left = RightPart.LeftNumber.Value;
            var right = RightPart.RightNumber.Value;
            RightPart = null;
            RightNumber = 0;

            if (LeftPart is SnailfishNumber)
            {
                LeftPart.AddFromLeft(left);
            }
            else
            {
                LeftNumber += left;
            }

            return (true, 0, right);
        }

        return (false, 0, 0);
    }

    private void AddFromRight(int num)
    {
        if (RightPart is SnailfishNumber)
        {
            RightPart.AddFromRight(num);
        }
        else
        {
            RightNumber += num;
        }
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
