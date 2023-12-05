using Spectre.Console;

namespace PuzzleConsole.Year2021.Day24;

public class Day24 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        //Table table = null;
        //List<string> p = null;

        //var count = 1;
        //foreach (var line in puzzle)
        //{
        //    if (line.StartsWith("inp"))
        //    {
        //        if (p != null)
        //        {
        //            table = new Table();
        //            table.AddColumn("index");
        //            table.AddColumn("w");
        //            table.AddColumn("x");
        //            table.AddColumn("y");
        //            table.AddColumn("z");
        //            for(int i = 1; i<= 9; i++)
        //            {
        //                var alu = new Alu(p.ToArray());
        //                var answers = alu.Run(new int[] {i});
        //                table.AddRow(i.ToString(),
        //                    answers["w"].ToString(),
        //                    answers["x"].ToString(),
        //                    answers["y"].ToString(),
        //                    answers["z"].ToString()
        //                    );
        //            }
        //            AnsiConsole.Write(table);
        //        }
        //        AnsiConsole.WriteLine("Input nr " + count.ToString());
        //        p.Add(line);
        //        count++;
        //    } else
        //    {
        //        p.Add(line);
        //    }
        //}
        //table = new Table();
        //table.AddColumn("index");
        //table.AddColumn("w");
        //table.AddColumn("x");
        //table.AddColumn("y");
        //table.AddColumn("z");
        //for(int i = 1; i<= 9; i++)
        //{
        //    var alu = new Alu(p.ToArray());
        //    var answers = alu.Run(new int[] {i});
        //    table.AddRow(i.ToString(),
        //        answers["w"].ToString(),
        //        answers["x"].ToString(),
        //        answers["y"].ToString(),
        //        answers["z"].ToString()
        //        );
        //}
        //    AnsiConsole.Write(table);

                //throw new Exception("Solution not found");
        return AnsiConsole.Status()
            .Start<string[]>("Working..,", ctx =>
            {
                var alu = new Alu(puzzle);

                for (double i = 9; i > 0; i--)
                {
                    var input = i.ToString();
                    for(int c = 0; c < 15; c++)
                    {
                        input += i.ToString();
                    }
                    ctx.Status($"Workign on {input}");
                    if (input.Contains('0'))
                        continue;

                    var output = alu.Run(input.Select(c => int.Parse(c.ToString())).ToArray());
                    var table = new Table();
                    table.AddColumn("index");
                    table.AddColumn("w");
                    table.AddColumn("x");
                    table.AddColumn("y");
                    table.AddColumn("z");
                    table.AddRow(i.ToString(),
                            output["w"].ToString(),
                            output["x"].ToString(),
                            output["y"].ToString(),
                            output["z"].ToString()
                            );
                    AnsiConsole.Write(table);

                    if (output["z"] == 0)
                        return new string[] { input };
                }
                throw new Exception("Solution not found");
            });
    }
}

public class Alu
{
    public Alu(string[] program)
    {
        Variables.Add("w", 0);
        Variables.Add("x", 0);
        Variables.Add("y", 0);
        Variables.Add("z", 0);
        Program = program;
    }

    public Dictionary<string, double> Run(int[] input)
    {
        var count = 0;
        Variables["w"] = 0;
        Variables["x"] = 0;
        Variables["y"] = 0;
        Variables["z"] = 0;
        foreach (var line in Program)
        {
            var instructions = line.Split(' ');
            double b = 0;
            if(instructions[0] != "inp")
            {
                if (!double.TryParse(instructions[2], out b))
                {
                    b = Variables[instructions[2]];
                }
            }
            switch(instructions[0])
            {
                case "inp":
                    Variables[instructions[1]] = input[count];
                    count++;
                    break;

                case "mul":
                    Variables[instructions[1]] = Variables[instructions[1]] * b;
                    break;

                case "eql":
                    Variables[instructions[1]] = Variables[instructions[1]] == b ? 1 : 0;
                    break;

                case "add":
                    Variables[instructions[1]] = Variables[instructions[1]] + b;
                    break;

                case "div":
                    if (b != 0)
                        Variables[instructions[1]] = Variables[instructions[1]] / b;
                    break;

                case "mod":
                    if (Variables[instructions[1]] >= 0 && b  > 0)
                        Variables[instructions[1]] = Variables[instructions[1]] % b;
                    break;

                default:
                    throw new NotSupportedException($"Instruction [{line[..3]}] is not supported");
            }
        }

        return Variables;
    }

    public Dictionary<string, double> Variables { get; set; } = new Dictionary<string, double>();
    public string[] Program { get; }
}
