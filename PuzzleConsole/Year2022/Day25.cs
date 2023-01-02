using System.Text.RegularExpressions;

namespace PuzzleConsole.Year2022.Day25;

public class Day25 : PuzzleConsole.ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var answer = puzzle.Select(l => new SNAFU(l))
            .Aggregate(new SNAFU(0), (acc, x) => acc + x);

        return new []
        {
            answer.ToString()
        };
    }
}

public record SNAFU
{
    public double Value { get; private set; }

    public SNAFU(string input)
    {
        var inp = input.Reverse().ToArray();
        Value = 0;
        for (var index = 0; index < input.Length; index++)
        {
            var c = inp[index];
            Value += c switch
            {
                '0' => 0,
                '1' => 1 * Math.Pow(5,index),
                '2' => 2 * Math.Pow(5,index),
                '-' => -1 * Math.Pow(5,index),
                '=' => -2 * Math.Pow(5,index),
            };
        }
    }

    public SNAFU(double value)
    {
        Value = value;
    }

    public static SNAFU operator +(SNAFU a, SNAFU b)
    {
        return new SNAFU(a.Value + b.Value);
    }

    public override string ToString()
    {
        var pow = 0;
        while (Math.Pow(5, pow) <= Value)
        {
            pow++;
        }

        double remainder = Value;
        var number = new List<double>(){0};
        do
        {
            var num = Math.Floor(remainder / Math.Pow(5, pow - 1));
            number.Add(num);
            remainder -= num * Math.Pow(5, pow - 1);
            pow--;
        } while (pow > 0);

        var ret = "";

        for (var index = number.Count - 1; index >= 0; index--)
        {
            var n = number[index];

            if (number[index] > 2)
            {
                number[index] -= 5;
                number[index - 1]++;
            }
        }

        foreach(var n in number)
        {
            ret += n switch
            {
                -2 => "=",
                -1 => "-",
                _ => n.ToString(),
            };
        }


        return Regex.Replace(ret, "^0*", "");
    }
}