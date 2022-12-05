namespace PuzzleConsole.Year2022.Day5;

public class Day5 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        (var stacks, var inst) = SplitInput(puzzle);

        var s = new Queue<Crate>();
        //perform instructions
        foreach (var instruction in inst)
        {
            //unstack
            for (int count = 0; count < instruction.Count; count++)
            {
                s.Enqueue(stacks[instruction.From - 1].Pop());
            }

            while (s.TryDequeue(out var c))
            {
                stacks[instruction.To - 1].Push(c);
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

        return new[] { res };
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