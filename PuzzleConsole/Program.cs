using Spectre.Console.Cli;

namespace PuzzleConsole;

class Program
{
    static int Main(string[] args)
    {
        var app = new CommandApp<StartCommand>();

        return app.Run(args);
    }
}
