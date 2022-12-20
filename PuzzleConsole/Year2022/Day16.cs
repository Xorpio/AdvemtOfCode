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

        var paths = new Queue<(string me, string? meTar, string el, string? elTar, string[] open, int time, int flowed)>();
        paths.Enqueue(("AA", null, "AA", null, Array.Empty<string>(), 0, 0));

        var currentMax = 0;

        var sw = new System.Diagnostics.Stopwatch();
        for (int minutes = 1; minutes <= 26; minutes++)
        {
            sw.Reset();
            sw.Start();
            while(paths.Count > 0 && paths.Peek().time == minutes - 1)
            {
                var p = paths.Dequeue();

                if (!shouldContinue(p, currentMax, valves))
                {
                    continue;
                }

                var others = paths.Where(_p => _p.time >= p.time
                    && _p.elTar == p.meTar
                    && _p.meTar == p.elTar
                    && _p.me == p.el
                    && _p.el == p.me
                    && _p.flowed >= p.flowed
                    && _p.open.Except(p.open).Any()
                );
                if (others.Any())
                {
                    continue;
                }

                if (valves.Where(v => v.flowRate > 0).All(v => p.open.Contains(v.Name)))
                {
                    var timeLeft = 26 - p.time;
                    var max = valves
                        .Select(v => v.flowRate * timeLeft)
                        .Sum() + p.flowed;

                    if (max > currentMax)
                        currentMax = max;

                    Console.WriteLine($"Found max {max} (1 of {paths.Count}");
                    continue;
                }

                //count opened
                p.flowed += valves.Where(v => p.open.Contains(v.Name))
                    .Select(v => v.flowRate)
                    .Sum();

                if (p.flowed > currentMax)
                    currentMax = p.flowed;

                p.time = minutes;


                if (p.meTar == p.me && p.elTar == p.el)
                {
                    var open = p.open
                        .Append(p.meTar)
                        .Append(p.elTar)
                        .ToArray();

                    p.open = open;
                    p.elTar = null;
                    p.meTar = null;
                }
                else if (p.meTar == p.me)
                {
                    var open = p.open
                        .Append(p.meTar)
                        .ToArray();

                    p.open = open;
                    p.meTar = null;
                }
                else if (p.elTar == p.el)
                {
                    var open = p.open
                        .Append(p.elTar)
                        .ToArray();

                    p.open = open;
                    p.elTar = null;
                }

                var targets = valves.Where(v => !p.open.Contains(v.Name))
                    .Where(v => v.flowRate > 0)
                    .Select(v => v.Name).ToList();


                //both elefant and me are not assigned
                if (p.meTar is null && p.elTar is null)
                {
                    var list =  targets.SelectMany(tme => targets
                        .Where(tel => tel != tme)
                        .Select(tel => (tme, tel)));

                    if (targets.Count() == 1)
                    {
                        if (GetPath(p.me, targets.First(), valves) >
                            GetPath(p.el, targets.First(), valves))
                        {
                            p.meTar = "";
                            p.elTar = targets.First();
                        }
                        else
                        {
                            p.elTar = "";
                            p.meTar = targets.First();
                        }

                        p.me = findNext(p.me, p.meTar, valves);
                        p.el = findNext(p.el, p.elTar, valves);
                        paths.Enqueue(p);

                        continue;
                    }

                    if (list.Count() == 0)
                    {
                        p.meTar = "";
                        p.elTar = "";
                        paths.Enqueue(p);
                        continue;
                    }

                    foreach (var t in list)
                    {
                        var nextMe = findNext(p.me, t.tme, valves);
                        var nextEl = findNext(p.el, t.tel, valves);

                       paths.Enqueue((nextMe, t.tme, nextEl,t.tel, p.open, minutes, p.flowed));
                    }

                    continue;
                }

                if (p.meTar is null)
                {
                    var list = targets
                            .Where(tme => p.elTar != tme);

                    if (list.Count() == 0)
                    {
                        p.meTar = "";
                        p.el = findNext(p.el, p.elTar, valves);
                        paths.Enqueue(p);
                        continue;
                    }

                    foreach (var t in list)
                    {
                        var nextMe = findNext(p.me, t, valves);
                        var nextEl = findNext(p.el, p.elTar, valves);

                       paths.Enqueue((nextMe, t, nextEl, p.elTar, p.open, minutes, p.flowed));
                    }

                    continue;
                }

                if (p.elTar is null)
                {
                    var list = targets
                            .Where(tme => p.meTar != tme);

                    if (list.Count() == 0)
                    {
                        p.elTar = "";
                        p.me = findNext(p.me, p.meTar, valves);
                        paths.Enqueue(p);
                        continue;
                    }

                    foreach (var t in list)
                    {
                        var nextMe = findNext(p.me, p.meTar, valves);
                        var nextEl = findNext(p.el, t, valves);

                       paths.Enqueue((nextMe, p.meTar, nextEl, t, p.open, minutes, p.flowed));
                    }

                    continue;
                }


                p.me = findNext(p.me, p.meTar, valves);
                p.el = findNext(p.el, p.elTar, valves);

                paths.Enqueue(p);
            }

            sw.Stop();
            Console.WriteLine($"minute {minutes} done. Found {paths.Count} paths in {sw.Elapsed}");
        }

        var res = FindSolution(valves, 30, new List<string>(), "AA");

        return new string[]
        {
            res.ToString(), currentMax.ToString(),
        };
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
                Console.WriteLine($"Checking in minute {minutes} for path: {string.Join("->", o)}");
                var deeperSol = FindSolution(valves, minutes - (move + 1), o, top.Name);
                solutions.Add(deeperSol + (top.flowRate * (minutes - (move + 1))));
            }
        }

        return solutions.Max();
    }

    private int GetPath(string current, string destination, List<Valve> valves)
    {
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

public class Valve
{
    public string Name { get; set; }
    public int flowRate { get; set; }
    public List<string> Connections { get; set; }
}