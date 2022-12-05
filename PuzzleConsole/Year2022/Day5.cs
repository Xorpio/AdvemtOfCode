namespace PuzzleConsole.Year2022.Day5;

public class Day5 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        (var stacks, var inst) = SplitInput(puzzle);
        (var stacks2, _) = SplitInput(puzzle);

        var s1 = new Queue<Crate>();
        var s2 = new Stack<Crate>();
        //perform instructions
        foreach (var instruction in inst)
        {
            //unstack
            for (int count = 0; count < instruction.Count; count++)
            {
                s1.Enqueue(stacks[instruction.From - 1].Pop());
                s2.Push(stacks2[instruction.From - 1].Pop());
            }

            while (s1.TryDequeue(out var c))
            {
                stacks[instruction.To - 1].Push(c);
            }
            while (s2.TryPop(out var c))
            {
                stacks2[instruction.To - 1].Push(c);
            }
        }

        var res = "";
        foreach (var stack in stacks)
        {
            if (stack.TryPeek(out var crate))
            {
                res += crate.Name;
            }
        }

        var res2 = "";
        foreach (var stack2 in stacks2)
        {
            if (stack2.TryPeek(out var crate))
            {
                res2 += crate.Name;
            }
        }


        return new[] { res, res2 };
    }

    public (IList<Stack<Crate>> Stacks, IList<Instruction> Instructions) SplitInput(string[] lines)
    {
        var stacks = new List<Stack<Crate>>();
        var instructions = new List<Instruction>();
        bool GetInstrictions = true;
        bool GetStacks = true;
        for (var index = lines.Length - 1; index >= 0; index--)
        {
            var line = lines[index];


            if (string.IsNullOrWhiteSpace(line))
            {
                GetInstrictions = false;
                continue;
            }

            if (GetInstrictions)
            {
                var s = line.Split(' ');
                instructions.Add(new Instruction(
                    int.Parse(s[1]),
                    int.Parse(s[3]),
                    int.Parse(s[5])
                ));
            }
            else
            {
                if (GetStacks)
                {
                    line = line.Trim();
                    var count = int.Parse(line.Split(" ").Last());

                    for (int i = 0; i < count; i++)
                    {
                        stacks.Add(new Stack<Crate>());
                    }

                    GetStacks = false;
                    continue;
                }

                for (int i = 0; i < stacks.Count(); i++)
                {
                    var c = TryGetCrate(line, i);

                    if (c is not null)
                    {
                        stacks[i].Push(c);
                    }
                }
            }
        }

        instructions.Reverse();

        return (stacks, instructions);
    }

    private Crate? TryGetCrate(string line, int i)
    {
        var index = (4 * i) + 1;

        if (line.Length >= index)
        {
            if (line[index] != ' ')
            {
                return new Crate(line[index].ToString());
            }
        }

        return null;
    }
}

public record Crate(string Name);
public record Instruction(int Count, int From, int To);