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

        monkeys["root"] = new Monkey("root: pppw = sjmn");

        return new string[]
        {
            monkeys["root"].ToeString(monkeys)
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
            _answer = double.Parse(_operation[1]);
        }
    }

    private double? _answer = null;

    public string Name => _operation[0][..^1];

    private string[] _operation;

    public double Call(Dictionary<string, Monkey> monkeys)
    {
        if (_answer == null)
        {
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