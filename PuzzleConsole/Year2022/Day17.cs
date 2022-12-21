using System.Text.RegularExpressions;

namespace PuzzleConsole.Year2022.Day17;

public class Day17 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var tetris = new Tetris(puzzle[0]);

        tetris.Play(2022);

        var part1 = tetris.Ceiling.ToString();

        tetris = new Tetris(puzzle[0]);

        // tetris.Play(1000000000000);

        return new string[] { part1, tetris.Ceiling.ToString() };
    }
}

public class Tetris
{
    private int jetPodoubleer = 0;
    private readonly string _jets;

    private int rockPodoubleer = 0;
    private readonly string[] _rocks = new string[]
    {
        "-", "+", "l", "i", "c"
    };

    public List<Coord> Cave;
    public List<Coord> Rock;

    public Tetris(string jets)
    {
        _jets = jets;

        Cave = new();
        for (double i = 0; i < 7; i++)
        {
            Cave.Add(new(i, 0));
        }
    }

    public void Play(double rounds)
    {
        var ceilings = new List<double>();
        var resolution = 10000;
        double lastCeiling = 0;
        for (double round = 0; round < rounds; round++)
        {
            SpawnRock();

            var moved = true;
            while (moved)
            {
                _ = tryMove(_jets[jetPodoubleer]);
                jetPodoubleer++;
                if (jetPodoubleer > _jets.Length - 1)
                {
                    jetPodoubleer = 0;
                    lastCeiling = Ceiling;
                }
                moved = tryMove('V');
            }

            Cave.AddRange(Rock);

            Ceiling = Cave.Select(r => r.height).Max();
            ceilings.Add(Ceiling - lastCeiling);
            lastCeiling = Ceiling;

            if (round % resolution == 0 && round > 10)
            {
                Console.WriteLine($"Playing round {round} {Ceiling} {Ceiling - lastCeiling}");
                findPattern(ceilings);

                throw new Exception("");
                // ceilings = new List<double>();
                lastCeiling = Ceiling;
            }
            Cave.RemoveAll(r => r.height < Ceiling - 100);
        }
    }


    private void findPattern(List<double> list)
    {
        string s = string.Join("x", list);
        Console.WriteLine(s);
        var longMatch = "";
        for (int i = 0; i < s.Length; i++)
        {
            Match match = Regex.Match(s[i..], @"(?<foo>.*)x?(\k<foo>)");
            if (match.Success)
            {
                if (match.Groups["foo"].Value.Length > longMatch.Length)
                {
                    longMatch = match.Groups["foo"].Value;
                    Console.WriteLine($"Found at index {i} with length{longMatch.Length}");
                }
            }
        }

        Console.WriteLine("Pattern:");
        Console.WriteLine(longMatch);
    }

    private bool tryMove(char dir)
    {
        var rock = Move(dir);

        if (rock.Any(r => r.col < 0 || r.col > 6 || Cave.Contains(r)))
        {
            return false;
        }

        Rock = rock.ToList();
        return true;
    }

    private IEnumerable<Coord> Move(char dir) => dir switch
    {
        '>' => Rock.Select(r => r with { col = r.col + 1}),
        '<' => Rock.Select(r => r with { col = r.col - 1}),
        'V' => Rock.Select(r => r with { height = r.height - 1}),
        _ => throw new NotImplementedException($"{dir} is not supported")
    };

    private void SpawnRock()
    {
        switch (_rocks[rockPodoubleer])
        {
            case "-":
                Rock = new List<Coord>()
                {
                    new(2, Ceiling + 4),
                    new(3, Ceiling + 4),
                    new(4, Ceiling + 4),
                    new(5, Ceiling + 4),
                };
                break;
            case "+":
                Rock = new List<Coord>()
                {
                    new(3, Ceiling + 4),
                    new(3, Ceiling + 5),
                    new(3, Ceiling + 6),
                    new(2, Ceiling + 5),
                    new(4, Ceiling + 5),
                };
                break;
            case "l":
                Rock = new List<Coord>()
                {
                    new(4, Ceiling + 4),
                    new(3, Ceiling + 4),
                    new(2, Ceiling + 4),
                    new(4, Ceiling + 5),
                    new(4, Ceiling + 6),
                };
                break;
            case "i":
                Rock = new List<Coord>()
                {
                    new(2, Ceiling + 4),
                    new(2, Ceiling + 5),
                    new(2, Ceiling + 6),
                    new(2, Ceiling + 7),
                };
                break;
            case "c":
                Rock = new List<Coord>()
                {
                    new(2, Ceiling + 4),
                    new(2, Ceiling + 5),
                    new(3, Ceiling + 4),
                    new(3, Ceiling + 5),
                };
                break;
            default:
                throw new Exception($"Rock {_rocks[rockPodoubleer]}");
        }

        rockPodoubleer++;

        if (rockPodoubleer > _rocks.Length - 1)
        {
            rockPodoubleer = 0;
        }
    }

    public double Ceiling { get; set; } = 0;
}
public record Coord(double col, double height);