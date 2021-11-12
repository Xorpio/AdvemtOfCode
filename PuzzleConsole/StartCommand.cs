using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace PuzzleConsole;

public class StartCommand : Command<StartCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [Description("Jaar van de puzzle")]
        [CommandOption("-y|--year")]
        [DefaultValue("2021")]
        public string Year { get; set; }

        [Description("dag van de puzzle")]
        [CommandOption("-d|--day")]
        [DefaultValue("1")]
        public string Day { get; set; }

        [CommandArgument(0, "<path>")]
        [Description("Path naar input file")]
        public string Filepath { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        var objectToInstantiate = $"PuzzleConsole.Year{settings.Year}.Day{settings.Day}, PuzzleConsole";

        var objectType = Type.GetType(objectToInstantiate);


        if (objectType == null)
        {
            AnsiConsole.MarkupLine($"[red]Year({settings.Year}) or Day({settings.Day}) not found[/]");
            return -1;
        }

        ISolver instantiatedObject = Activator.CreateInstance(objectType) as ISolver;

        if (!File.Exists(settings.Filepath))
        {
            AnsiConsole.MarkupLine($"[red]File not found: {settings.Filepath}[/]");
            return -1;
        }

        var input = File.ReadAllLines(settings.Filepath);
        
        var answers = instantiatedObject.Solve(input);

        Console.WriteLine("Answer: ");
        foreach(var answer in answers)
        {
            Console.WriteLine(answer);
        }

        return 0;
    }
}
