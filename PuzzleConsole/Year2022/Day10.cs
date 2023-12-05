namespace PuzzleConsole.Year2022.Day10;

public class Day10 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var signalStrenthMeasurements = new int[] { 20, 60, 100, 140, 180, 220 };

        var x = 1;
        var measueredx = 0;
        var cycle = 1;

        var instructions = new Queue<string>(puzzle);
        string? instruction = instructions.Dequeue();

        var addx = 0;

        var Rows = new List<string>();
        string row = "";

        do
        {
            if (signalStrenthMeasurements.Contains(cycle))
            {
                measueredx += cycle * x;
            }

            if (Between(row.Length, x - 1, x + 1))
            {
                row += "#";
            }
            else
            {
                row += ".";
            }

            if (cycle % 40 == 0)
            {
                Rows.Add(row);
                row = "";
            }

            x += addx;

            if (instruction == "noop")
            {
                instruction = null;
                addx = 0;
            }
            else if(instruction.StartsWith("addx"))
            {
                var split = instruction.Split(' ');
                addx = int.Parse(split[1]);

                instruction = "noop";
            }
            else
            {
                throw new("unexpected instruction");
            }

            cycle++;
            if (string.IsNullOrEmpty(instruction))
            {
                instructions.TryDequeue(out instruction);
            }
        } while (!string.IsNullOrEmpty(instruction));

        var output = new List<string>() { measueredx.ToString() };
        output.AddRange(Rows);
        return output.ToArray();
    }

    public bool Between(int i, int a, int b)
    {
        return (i >= a && i <= b);
    }
}