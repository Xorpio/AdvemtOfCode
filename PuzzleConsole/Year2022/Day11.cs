namespace PuzzleConsole.Year2022.Day11;

public class Day11 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var monkeys = new List<Monkey>();
        string Name = "", TrueMonkey = "", FalseMonkey = "";
        Func<int, int> Operation = null;
        int Test = 0;
        Stack<int> Items = new Stack<int>();
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
                            Operation = new Func<int, int>((old) => old * old);
                            break;
                        case "+":
                            Operation = new Func<int, int>((old) => old + old);
                            break;
                        default:
                            throw new Exception("Operation not supported");
                    }

                }
                else
                {
                    var number = int.Parse(ops[^1]);
                    switch (ops[^2])
                    {
                        case "*":
                            Operation = new Func<int, int>((old) => old * number);
                            break;
                        case "+":
                            Operation = new Func<int, int>((old) => old + number);
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
                Test = int.Parse(test[^1]);
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
                monkeys.Add(new Monkey(Name, TrueMonkey, FalseMonkey, Items, Test, Operation));
                continue;
            }

            if (split[0].StartsWith("Starting"))
            {
                Items = new Stack<int>();
                foreach (var i in split[1].Split(',')
                             .Select(s => int.Parse(s)))
                {
                    Items.Push(i);
                }
                continue;
            }

            throw new Exception($"Case {line} not supported");
        }

        monkeys.Add(new Monkey(Name, TrueMonkey, FalseMonkey, Items, Test, Operation));

        for (int i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.TryPop(out int item))
                {
                    monkey.Inspections++;
                    item = monkey.Operation.Invoke(item);
                    item = Convert.ToInt32(Math.Floor((decimal)(item / 3)));

                    var newMonkey = (item % monkey.Test == 0)
                        ? monkeys.First(m => m.Name.EndsWith(monkey.TrueMonkey))
                        : monkeys.First(m => m.Name.EndsWith(monkey.FalseMonkey));

                    newMonkey.Items.Push(item);
                }
            }
        }

        var res = monkeys.Select(m => m.Inspections)
            .OrderByDescending(i => i)
            .Take(2).ToArray();

        return new string[]
        {
            (res[0] * res[1]).ToString()
        };
    }
}

public class Monkey
{
    public Monkey(string name, string trueMonkey, string falseMonkey, Stack<int> items, int test, Func<int, int> operation)
    {
        Name = name;
        TrueMonkey = trueMonkey;
        FalseMonkey = falseMonkey;
        Items = items;
        Test = test;
        Operation = operation;
    }

    public string Name { get; init; }
    public string TrueMonkey { get; init; }
    public string FalseMonkey { get; init; }
    public Stack<int> Items { get; set; }
    public int Test { get; init; }
    public Func<int, int> Operation { get; set; }
    public int Inspections { get; set; } = 0;
}