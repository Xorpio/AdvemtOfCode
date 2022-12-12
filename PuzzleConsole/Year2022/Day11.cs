namespace PuzzleConsole.Year2022.Day11;

public class Day11 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var MonkeysPart1 = new List<Monkey>();
        var MonkeysPart2 = new List<Monkey>();
        string Name = "", TrueMonkey = "", FalseMonkey = "";
        Func<double, double> Operation = null;
        double Test = 0;
        Stack<double> Items = new Stack<double>();
        foreach (var line in puzzle)
        {
            var split = line.Trim().Split(':');
            if (split[0].StartsWith("Monkey"))
            {
                Name = split[0];
                continue;
            }

            if (split[0].StartsWith("Operation"))
            {
                var ops = split[1].Split(' ');
                if (ops[^1] == "old")
                {
                    switch (ops[^2])
                    {
                        case "*":
                            Operation = new Func<double, double>((old) => old * old);
                            break;
                        case "+":
                            Operation = new Func<double, double>((old) => old + old);
                            break;
                        default:
                            throw new Exception("Operation not supported");
                    }

                }
                else
                {
                    var number = double.Parse(ops[^1]);
                    switch (ops[^2])
                    {
                        case "*":
                            Operation = new Func<double, double>((old) => old * number);
                            break;
                        case "+":
                            Operation = new Func<double, double>((old) => old + number);
                            break;
                        default:
                            throw new Exception("Operation not supported");
                    }
                }
                continue;
            }


            if (split[0].StartsWith("Test"))
            {
                var test = split[1].Split(' ');
                Test = double.Parse(test[^1]);
                continue;
            }


            if (split[0].StartsWith("If true"))
            {
                var ifTrue = split[1].Split(' ');
                TrueMonkey = ifTrue[^1];
                continue;
            }

            if (split[0].StartsWith("If false"))
            {
                var ifFalse = split[1].Split(' ');
                FalseMonkey = ifFalse[^1];
                continue;
            }

            if (string.IsNullOrEmpty(line))
            {
                MonkeysPart1.Add(new Monkey(Name, TrueMonkey, FalseMonkey, Items, Test, Operation));
                MonkeysPart2.Add(new Monkey(Name, TrueMonkey, FalseMonkey, Items, Test, Operation));
                continue;
            }

            if (split[0].StartsWith("Starting"))
            {
                Items = new Stack<double>();
                foreach (var i in split[1].Split(',')
                             .Select(s => double.Parse(s)))
                {
                    Items.Push(i);
                }
                continue;
            }

            throw new Exception($"Case {line} not supported");
        }

        MonkeysPart1.Add(new Monkey(Name, TrueMonkey, FalseMonkey, Items, Test, Operation));
        MonkeysPart2.Add(new Monkey(Name, TrueMonkey, FalseMonkey, Items, Test, Operation));

        for (double i = 0; i < 20; i++)
        {
            foreach (var monkey in MonkeysPart1)
            {
                while (monkey.Items.TryPop(out double item))
                {
                    monkey.Inspections++;
                    item = monkey.Operation.Invoke(item);
                    item = Convert.ToInt32(Math.Floor((double)(item / 3)));

                    var newMonkey = (item % monkey.Test == 0)
                        ? MonkeysPart1.First(m => m.Name.EndsWith(monkey.TrueMonkey))
                        : MonkeysPart1.First(m => m.Name.EndsWith(monkey.FalseMonkey));

                    newMonkey.Items.Push(item);
                }
            }
        }

        for (double i = 0; i < 10_000; i++)
        {
            foreach (var monkey in MonkeysPart2)
            {
                while (monkey.Items.TryPop(out double item))
                {
                    monkey.Inspections++;
                    item = monkey.Operation.Invoke(item);

                    var newMonkey = (item % monkey.Test == 0)
                        ? MonkeysPart2.First(m => m.Name.EndsWith(monkey.TrueMonkey))
                        : MonkeysPart2.First(m => m.Name.EndsWith(monkey.FalseMonkey));

                    newMonkey.Items.Push(item);
                }
            }
        }

        var res = MonkeysPart1.Select(m => m.Inspections)
            .OrderByDescending(i => i)
            .Take(2).ToArray();

        var res2 = MonkeysPart2.Select(m => m.Inspections)
            .OrderByDescending(i => i)
            .Take(2).ToArray();

        return new string[]
        {
            (res[0] * res[1]).ToString(),
            (res2[0] * res2[1]).ToString()
        };
    }
}

public class Monkey
{
    public Monkey(string name, string trueMonkey, string falseMonkey, Stack<double> items, double test, Func<double, double> operation)
    {
        Name = name;
        TrueMonkey = trueMonkey;
        FalseMonkey = falseMonkey;
        Items = new Stack<double>(items);
        Test = test;
        Operation = operation;
    }

    public string Name { get; init; }
    public string TrueMonkey { get; init; }
    public string FalseMonkey { get; init; }
    public Stack<double> Items { get; set; }
    public double Test { get; init; }
    public Func<double, double> Operation { get; set; }
    public double Inspections { get; set; } = 0;
}