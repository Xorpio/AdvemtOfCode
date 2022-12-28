using System.Data;
using System.Security.Cryptography;

namespace PuzzleConsole.Year2022.Day21;

public class Day21 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var monkeys = puzzle.Select(l => new Monkey(l))
            .ToDictionary(m => m.Name);

        var part1 = monkeys["root"].Call(monkeys).ToString();

        monkeys = null;

        monkeys = puzzle.Select(l => new Monkey(l))
            .ToDictionary(m => m.Name);

        monkeys["root"] = new Monkey($"root: {monkeys["root"]._operation[1]} = {monkeys["root"]._operation[3]}");
        monkeys["humn"] = new Monkey("humn: x");

        var part2 = monkeys["root"].Call(monkeys).ToString();

        return new string[]
        {
            part1, part2
        };
    }
}

public class Monkey
{
    public Monkey(string input)
    {
        _operation = input.Split(' ');
        if (_operation.Length == 2)
        {
            try
            {
                _answer = double.Parse(_operation[1]);
            }
            catch (Exception e)
            {
                if (Name != "humn")
                    throw e;
                //human
            }
        }
    }

    private double? _answer = null;

    public string Name => _operation[0][..^1];

    public string[] _operation;

    public double Call(Dictionary<string, Monkey> monkeys)
    {
        if (_answer == null)
        {
            double? monkey1 = null;
            double? monkey2 = null;
            try
            {
                monkey1 = monkeys[_operation[1]].Call(monkeys);
                monkey2 = monkeys[_operation[3]].Call(monkeys);
            }
            catch (Exception e)
            {
                if (monkey1 is null)
                    monkey2 = monkeys[_operation[3]].Call(monkeys);

                if (Name == "root")
                {
                    var answer = monkey1 ?? monkey2;
                    var ex = e as MonkeyException;

                    while (ex is MonkeyException)
                    {
                        switch (ex.Operation)
                        {
                            case "+":
                                answer -= ex.Part1 is null ? ex.Part2 : ex.Part1;
                                break;
                            case "/":
                                answer *= ex.Part1 is null ? ex.Part2 : ex.Part1;
                                break;
                            case "*":
                                answer /= ex.Part1 is null ? ex.Part2 : ex.Part1;
                                break;
                            case "-":
                                answer += ex.Part1 is null ? ex.Part2 : ex.Part1;
                                break;
                        }
                        ex = ex.InnerException as MonkeyException;
                    }

                    return answer.Value;
                }
                throw new MonkeyException("Message", e, monkey1, monkey2, _operation[2]);
            }

            switch(_operation[2])
            {
                case "+":
                    _answer = monkeys[_operation[1]].Call(monkeys) + monkeys[_operation[3]].Call(monkeys);
                    break;
                case "/":
                    _answer = monkeys[_operation[1]].Call(monkeys) / monkeys[_operation[3]].Call(monkeys);
                    break;
                case "*":
                    _answer = monkeys[_operation[1]].Call(monkeys) * monkeys[_operation[3]].Call(monkeys);
                    break;
                case "-":
                    _answer = monkeys[_operation[1]].Call(monkeys) - monkeys[_operation[3]].Call(monkeys);
                    break;
                case "=":
                    _answer = monkeys[_operation[1]].Call(monkeys) - monkeys[_operation[3]].Call(monkeys);
                    break;

                default:
                    throw new Exception($"Monkey {Name} could not resolve {_operation[2]}");
            }
        }

        return _answer.Value;
    }

    public string ToeString(Dictionary<string, Monkey> monkeys)
    {

        if (Name == "humn")
        {
            return "x";
        }

        if (_operation.Length < 3)

        {
            return _operation[1].ToString();
        }

        return $"( {monkeys[_operation[1]].ToeString(monkeys)} {_operation[2]} {monkeys[_operation[3]].ToeString(monkeys)} )";
    }
}

public class MonkeyException : Exception
{
    public MonkeyException(string message, Exception innerException, double? part1, double? part2, string operation) : base(message, innerException)
    {
        Part1 = part1;
        Part2 = part2;
        Operation = operation;
    }

    public double? Part1 { get; set; }
    public double? Part2 { get; set; }
    public string Operation { get; set; }
}