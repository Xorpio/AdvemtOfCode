namespace AdventOfCode.Solvers.Year2024.Day9;

public class Day9Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        bool isFile = true;
        var id = 0;
        var output = "";
        foreach (int n in puzzle[0].Select(c => int.Parse(c.ToString())))
        {
            if (isFile)
            {
                logger.OnNext($"id: {id}, n: {n}");
                output = output.PadRight(output.Length + n, char.Parse(id.ToString()));
                id++;
            }
            else
            {
                output = output.PadRight(output.Length + n, '.');
            }

            isFile = !isFile;
        }

        //remote . on end?

        while (output.Contains("."))
        {
            var indexOfDot = output.IndexOf('.');

            //swap chars
            output = output.Remove(indexOfDot, 1);
            output = output.Insert(indexOfDot, output[^1].ToString());
            output = output[..^1];
        }

        decimal total = 0;
        for (int i = 0; i < output.Length; i++)
        {
            int c = int.Parse(output[i].ToString());

            total += i * c;
        }

        GiveAnswer1(total);
        GiveAnswer2("");
    }
}
