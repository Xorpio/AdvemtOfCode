namespace PuzzleConsole.Year2021.Day20;

public class Day20 : ISolver
{
    public string lookup;

    public string[] Solve(string[] puzzle)
    {
        lookup = puzzle[0];

        puzzle = puzzle[2..];

        var answer = 0;
        for (int i = 0; i < 50; i++)
        {
            puzzle = Enhance(puzzle, i);

            if (i == 1)
            {
                answer = puzzle.ToList()
                    .Select(l => l.ToCharArray()
                        .Where(c => c == '#')
                        .Count()
                    )
                    .Sum();
                foreach(var lin in puzzle)
                {
                    Console.WriteLine(lin);
                }
            }

        }

        var answer2 = puzzle.ToList()
            .Select(l => l.ToCharArray()
                .Where(c => c == '#')
                .Count()
            )
            .Sum();

                foreach(var lin in puzzle)
                {
                    Console.WriteLine(lin);
                }


        return new string[] {answer.ToString(), answer2.ToString()};
    }

    public string[] Enhance(string[] puzzle, int i)
    {
        var output = new List<string>();
        for(int l = -1; l < puzzle.Length + 1; l++)
        {
            var line = new List<char>();
            for (int c = -1; c < puzzle[0].Length + 1; c++)
            {
                line.Add(EnhancePixel(puzzle, l, c, i));
            }
            output.Add(string.Join("", line));
        }

        return output.ToArray();
    }

    public char EnhancePixel(string[] puzzle, int l, int c, int i)
    {
        var bin = "";
        for (var li = l - 1; li <= l + 1; li++)
        {
            for (var ci = c - 1; ci <= c + 1; ci++)
            {
                if (li < 0 || ci < 0 || li >= puzzle.Length || ci >= puzzle[0].Length)
                {
                    if (i % 2 == 1 && lookup[0] == '#')
                    {
                        bin += "1";
                    }
                    else
                    {
                        bin += "0";
                    }
                }
                else
                {
                    bin += (puzzle[li][ci] == '.') ?  "0" : "1";
                }
            }
        }

        var index = Convert.ToInt32(bin, 2);

        return lookup[index];
    }
}
