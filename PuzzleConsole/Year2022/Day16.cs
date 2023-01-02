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

        var valves = new List<Valve>();

        foreach (var line in puzzle)
        {
            var words = line.Split(' ');
            var name = words[1];
            var flowrate = int.Parse(words[4].Split(';')[0].Split('=')[1]);
            var otherValves = words[9..].Select(w => w.Replace(",", "")).ToList();

            valves.Add(new()
            {
                Name = name,
                flowRate = flowrate,
                Connections = otherValves
            });
        }

        // var part2 = FindSolutionPart2(valves);
        var res = FindSolution(valves, 30, new List<string>(), "AA");
        var part2 = FindSolutionPart2Recursive(new[]
        {
            (26, "AA"),
            (26, "AA"),
        }, valves.Where(v => v.flowRate > 0).Select(v => v.Name).ToArray(), valves);
        Console.WriteLine("Found part 2");

        return new string[]
        {
            res.ToString(),
            part2.ToString()
        };
    }

    private int FindSolutionPart2Recursive((int TimeLeft, string position)[] players, string[] valvesToOpen, List<Valve> valves)
    {
        var p = players[0].TimeLeft > players[1].TimeLeft ? 0 : 1;

        var best = 0;
        foreach (var valve in valvesToOpen)
        {
            if (valvesToOpen.Length > 10)
            {
                // Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                Console.WriteLine($"From: {players[p].position} with timeleft {players[p].TimeLeft} to {valve}. best: {best}");
            }
            var move = GetPath(players[p].position, valve, valves);

            if (players[p].TimeLeft - (move + 1) > 0)
            {

                var ps = new (int TimeLeft, string position)[]
                {
                    players[0],
                    players[1]
                };
                ps[p].TimeLeft -= (move + 1);
                ps[p].position = valve;

                var result = FindSolutionPart2Recursive(ps, valvesToOpen.Where(v => v != valve).ToArray(), valves)
                                + (valves.Where(v => v.Name == valve).First().flowRate * ps[p].TimeLeft);

                if (best < result)
                    best = result;
            }
        }

        return best;
    }

    private int openCounter = 0;

    private string[] ToOpen()
    {
        var valves = new string[]
        {
            "JJ",
            "DD",
            "HH",
            "BB",
            "CC",
            "EE"
        };
        return new[] { valves[openCounter++] };
    }

    private int FindSolutionPart2(List<Valve> valves)
    {
        var universes = new List<(
            (string L, string? T, int M)[] Players,
            string[] OpenValves,
            int Time,
            int Flowed
            )>();

        var toOpenValves = valves.Where(v => v.flowRate > 0)
            .Select(v => v.Name);

        universes.Add((new (string, string, int)[]
        {
            ("AA", null, 26),
            ("AA", null, 26),
        }, new string[]
        {
        }, 26, 0));

        var max = 0;

        var seen = new List<(string opened, int flowed)>();

        ((string L, string T, int M)[] Players, string[] OpenValves, int Time, int Flowed) universe;
        while (universes.Any())
        {
            var universeNoTarget = universes
                .Where(u => u.Players[0].T == null || u.Players[1].T == null);


            if (universeNoTarget.Any())
            {
                universe = universeNoTarget.First();
                universes.Remove(universe);

                var pIndex = (universe.Players[0].T == null) ? 0 : 1;
                var p2 = universe.Players[pIndex == 0 ? 1 : 0];
                var toOpen = toOpenValves.Where(v => !universe.OpenValves.Contains(v) && p2.T != v);

                if (!toOpen.Any())
                {
                    var flowed =
                        + universe.Flowed
                        + valves.Where(v => universe.OpenValves.Contains(v.Name))
                        .Select(v => v.flowRate)
                        .Sum() * (universe.Time)
                        + valves.Where(v => v.Name == p2.T)
                            .Select( v => v.flowRate)
                            .Sum() * (p2.M - 1)
                        ;
                    if (flowed > max)
                        max = flowed;
                    continue;
                }

                // toOpen = ToOpen();
                foreach (var open in toOpen)
                {
                    var path = GetPath(universe.Players[pIndex].L, open, valves);
                    var newPlayers = new (string L, string? T, int M)[]
                    {
                        universe.Players[pIndex],
                        p2
                    };
                    newPlayers[0].M = universe.Time - path;
                    newPlayers[0].T = open;

                    // check for duplicates
                    if (!ContainsUniverse(universes, newPlayers, universe, seen))
                    {
                        universes.Add((newPlayers, universe.OpenValves, universe.Time, universe.Flowed));
                    }
                    continue;
                }
            }
            else
            {
                var universeToTarget = universes
                        .OrderByDescending(u => u.Time)
                        .ThenByDescending(u => u.Flowed)
                    ;

                universe = universeToTarget.First();
                universes.Remove(universe);

                //shortest first
                universe.Players = universe.Players.OrderByDescending(p => p.M).ToArray();
                universe.Players[0].L = universe.Players[0].T;
                universe.Players[0].T = null;

                if (universe.Time > universe.Players[0].M - 1)
                {
                    var time = universe.Time;
                    universe.Time = universe.Players[0].M - 1;
                    universe.Flowed += valves
                        .Where(v => universe.OpenValves.Contains(v.Name))
                        .Select(v => v.flowRate)
                        .Sum() * (time - universe.Time);
                }

                var openValves = new List<string>(universe.OpenValves);
                openValves.Add(universe.Players[0].L);
                universe.OpenValves = openValves.ToArray();

                var opened = string.Join('-', universe.OpenValves.OrderBy(u => u));
                if (!seen.Where(s => s.opened == opened && s.flowed >= universe.Flowed).Any())
                {
                    universes.Add(universe);
                }
                else
                {
                    seen.Add((opened, universe.Flowed));
                }
            }

            Console.SetCursorPosition(Console.GetCursorPosition().Top, 0);
            Console.WriteLine($"{max} - {universes.Count}".PadRight(10, ' '));
        }

        return max;
    }

    private bool ContainsUniverse(
        List<((string L, string T, int M)[] Players, string[] OpenValves, int Time, int Flowed)> universes,
        (string L, string T, int M)[] newPlayers,
        ((string L, string T, int M)[] Players, string[] OpenValves, int Time, int Flowed) universe,
        List<(string opened, int flowed)> seen)
    {
        var us = universes.Where(u => Enumerable.SequenceEqual(u.OpenValves, universe.OpenValves));
        us = us.Where(u => Enumerable.SequenceEqual(
            u.Players.OrderBy(p => p.L).ThenBy(p => p.T).ThenBy(p => p.T),
            newPlayers.OrderBy(p => p.L).ThenBy(p => p.T).ThenBy(p => p.T)));
        us = us.Where(u => u.Time == universe.Time);
        us = us.Where(u => u.Flowed == universe.Flowed);

        var opened = string.Join('-', universe.OpenValves.OrderBy(u => u));
        return us.Any() || seen.Where(s => s.opened == opened && s.flowed >= universe.Flowed).Any();
    }

    private bool shouldContinue((string me, string? meTar, string el, string? elTar, string[] open, int time, int flowed) solution, int currentMax, List<Valve> valves)
    {
        var timeLeft = 26 - solution.time;
        var ToFlow = valves
            .Select(v => v.flowRate * timeLeft)
            .Sum() + solution.flowed;

        return ToFlow >= currentMax;
    }

    private string findNext(string me, string target, List<Valve> valves)
    {
        if (target == "")
            return me;

        var toCheck = new List<(string[] name, int distance)>();
        var hasChecked = new HashSet<string>();
        toCheck.Add((new string[] { me }, 0));
        while (true)
        {
            var t = toCheck.OrderBy(d => d.distance).First();
            toCheck.Remove(t);
            hasChecked.Add(t.name[^1]);
            var node = valves.First(v => v.Name == t.name[^1]);

            if (node.Connections.Contains(target))
            {
                return t.name.Length > 1 ? t.name[1] : target;
            }

            foreach (var n in node.Connections)
            {
                if (!hasChecked.Contains(n))
                {
                    var names = new string[t.name.Length + 1];
                    Array.Copy(t.name, names, t.name.Length);
                    names[^1] = n;
                    toCheck.Add((names , t.distance + 1));
                }
            }
        }
    }

    private int FindSolution(List<Valve> valves, int minutes, List<string> Opened, string current)
    {
        var toOpen = valves
                .Where(v => v.flowRate > 0)
                .Where(v => !Opened.Contains(v.Name))
            ;

        var solutions = new List<int>(){0};
        foreach (var top in toOpen)
        {
            var move = GetPath(current, top.Name, valves);
            if (minutes - (move + 1) > 0)
            {
                var o = new List<string>(Opened);
                o.Add(top.Name);
                // Console.WriteLine($"Checking in minute {minutes} for path: {string.Join("->", o)}");
                var deeperSol = FindSolution(valves, minutes - (move + 1), o, top.Name);
                solutions.Add(deeperSol + (top.flowRate * (minutes - (move + 1))));
            }
        }

        return solutions.Max();
    }

    private Dictionary<(string from, string to), int> Paths = new Dictionary<(string from, string to), int>();
    private int GetPath(string current, string destination, List<Valve> valves)
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
            var node = valves.First(v => v.Name == t.name);

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