using System.Diagnostics.Tracing;
using System.Security;

namespace PuzzleConsole.Year2022.Day16;

public class Day16 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        // Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
        // Valve BB has flow rate=13; tunnels lead to valves CC, AA
        // Valve CC has flow rate=2; tunnels lead to valves DD, BB
        // Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
        // Valve EE has flow rate=3; tunnels lead to valves FF, DD
        // Valve FF has flow rate=0; tunnels lead to valves EE, GG
        // Valve GG has flow rate=0; tunnels lead to valves FF, HH
        // Valve HH has flow rate=22; tunnel leads to valve GG
        // Valve II has flow rate=0; tunnels lead to valves AA, JJ
        // Valve JJ has flow rate=21; tunnel leads to valve II

        _valves = new Dictionary<string,Valve>();

        foreach (var line in puzzle)
        {
            var words = line.Split(' ');
            var name = words[1];
            var flowrate = int.Parse(words[4].Split(';')[0].Split('=')[1]);
            var otherValves = words[9..].Select(w => w.Replace(",", "")).ToList();

            _valves.Add(name, new()
            {
                Name = name,
                flowRate = flowrate,
                Connections = otherValves
            });
        }

        // var part2 = FindSolutionPart2(valves);
        var res = FindSolution(30, new List<string>(), "AA");
        var part2 = FindSolutionPart2Recursive(new[]
            {
                (26, "AA"),
                (26, "AA"),
            }, _valves.Where(v => v.Value.flowRate > 0).Select(v => v.Value.Name).ToArray()
            , 0);
        Console.WriteLine("Found part 2");

        return new string[]
        {
            res.ToString(),
            part2.ToString()
        };
    }

    private int? _currentBest;
    private int FindSolutionPart2Recursive((int TimeLeft, string position)[] players, string[] valvesToOpen, int current)
    {
        var best = 0;

        var outer = _currentBest == null;
        _currentBest = 0;

        for (int p = 0; p < 2; p++)
        {
            if (outer)
                p++;

            var potential = valvesToOpen
                .Select(v => _valves[v].flowRate)
                .Sum() * players[p].TimeLeft;
            if ((current + potential) <= _currentBest)
            {
                continue;
            }

            foreach (var valve in valvesToOpen)
            {
                if (outer)
                {
                    // Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                    Console.WriteLine($"From: {players[p].position} with timeleft {players[p].TimeLeft} to {valve}. best: {best}");
                }

                var move = GetPath(players[p].position, valve);

                if (players[p].TimeLeft - (move + 1) > 0)
                {
                    var ps = new (int TimeLeft, string position)[]
                    {
                        players[0],
                        players[1]
                    };
                    ps[p].TimeLeft -= (move + 1);
                    ps[p].position = valve;

                    var result = FindSolutionPart2Recursive(ps, valvesToOpen.Where(v => v != valve).ToArray(), best)
                                 + (_valves[valve].flowRate * ps[p].TimeLeft);

                    if (best < result)
                        best = result;

                    if (_currentBest < best)
                    {
                        _currentBest = best;
                    }
                }
            }
        }

        return best;
    }

    private int FindSolution(int minutes, List<string> Opened, string current)
    {
        var toOpen = _valves
            .Where(v => v.Value.flowRate > 0)
            .Where(v => !Opened.Contains(v.Value.Name))
            .Select(v => v.Value);

        var solutions = new List<int>(){0};
        foreach (var top in toOpen)
        {
            var move = GetPath(current, top.Name);
            if (minutes - (move + 1) > 0)
            {
                var o = new List<string>(Opened);
                o.Add(top.Name);
                // Console.WriteLine($"Checking in minute {minutes} for path: {string.Join("->", o)}");
                var deeperSol = FindSolution(minutes - (move + 1), o, top.Name);
                solutions.Add(deeperSol + (top.flowRate * (minutes - (move + 1))));
            }
        }

        return solutions.Max();
    }

    private Dictionary<(string from, string to), int> Paths = new Dictionary<(string from, string to), int>();
    private Dictionary<string, Valve> _valves;

    private int GetPath(string current, string destination)
    {
        if (Paths.ContainsKey((current, destination)))
        {
            return Paths[(current, destination)];
        }

        var toCheck = new List<(string name, int distance)>();
        var hasChecked = new HashSet<string>();
        toCheck.Add((current, 0));
        while (true)
        {
            var t = toCheck.OrderBy(d => d.distance).First();
            toCheck.Remove(t);
            hasChecked.Add(t.name);
            var node = _valves[t.name];

            if (node.Connections.Contains(destination))
            {
                Paths.Add((current, destination), t.distance + 1);
                return t.distance + 1;
            }

            foreach (var n in node.Connections)
            {
                if (!hasChecked.Contains(n))
                {
                    toCheck.Add((n, t.distance + 1));
                }
            }
        }
    }
}

public record Valve
{
    public string Name { get; set; }
    public int flowRate { get; set; }
    public List<string> Connections { get; set; }
}
//
// public record Players
// {
//     public ()
//
//     public bool Equ
// }