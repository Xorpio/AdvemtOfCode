using System.Runtime.InteropServices;

namespace AdventOfCode.Solvers.Year2023.Day20;

public class Day20Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var modules = new Dictionary<string, Module>();
        Dictionary<string, bool> flipflops = [];
        Dictionary<string, Dictionary<string,bool>> conjunctions = [];

        decimal countHigh = 0;
        decimal countLow = 0;

        foreach (var line in puzzle)
        {
            var parts = line.Split(" -> ");
            var name = parts[0][1..];
            var type = parts[0][0];
            var connections = parts[1].Split(", ");

            modules[name] = new Module(name, connections, type);

            if (type == '%')
            {
                flipflops[name] = false;
            }

            foreach (var connetion in connections)
            {
                if (!conjunctions.ContainsKey(connetion))
                {
                    conjunctions[connetion] = [];
                }

                conjunctions[connetion][name] = false;
            }
        }

        var hasRx = puzzle.Any(l => l.EndsWith("rx"));

        // modules["output"] = new Module("output", [], 'o');

        logger.OnNext("button -low-> broadcaster");
        Queue<(string target, bool pulse, string from)> queue = new();

        var nodes = new Dictionary<string, List<int>>();

        if (hasRx)
        {
            //find conjunctions that rx is connected to
            var rxConjunctions = modules.Where(m => m.Value.Connections.Contains("rx")).Select(m => m.Key).Single();

            foreach(var n in conjunctions[rxConjunctions])
            {
                nodes[n.Key] = [0];
            }
        }

        int i = 1;
        while (true)
        {
            // logger.OnNext($"Pressing {i +1}");
            // logger.OnNext("");
            //
            // logger.OnNext($"button -low-> broadcaster");
            var CountRx = 0;

            queue.Enqueue(("roadcaster", false, "button"));
            do
            {
                var next = queue.Dequeue();

                if (next.pulse)
                {
                    countHigh++;
                }
                else
                {
                    countLow++;
                }

                if (next.target == "rx")
                {
                    // logger.OnNext($"rx received pulse from {next.from} ({next.pulse})");
                    if (!next.pulse)
                    {
                        CountRx++;
                    }
                }

                if (!modules.ContainsKey(next.target))
                {
                    continue;
                }

                if (nodes.ContainsKey(next.target) && !next.pulse)
                {
                    nodes[next.target].Add(i);
                    logger.OnNext($"received pulse on {next.target}. last: {nodes[next.target][^2]}. now: {i}. diff: {i - nodes[next.target][^2]}");
                }

                switch (modules[next.target].type)
                {
                    case 'b':
                        foreach(var connection in modules[next.target].Connections)
                        {
                            // logger.OnNext($"broadcaster -{(next.pulse ? "high" : "low")}-> {connection}");
                            queue.Enqueue((connection, next.pulse, next.target));
                        }
                        break;

                    case '%':
                        if (!next.pulse)
                        {
                            var flipflop = !flipflops[next.target];
                            flipflops[next.target] = flipflop;
                            foreach(var connection in modules[next.target].Connections)
                            {
                                // logger.OnNext($"{next.target} -{(flipflop ? "high" : "low")}-> {connection}");
                                queue.Enqueue((connection, flipflop, next.target));
                            }
                        }
                        break;

                    case '&':
                        var memory = conjunctions[next.target];
                        memory[next.from] = next.pulse;
                        var conjunction = !memory.All(kvp => kvp.Value);
                        foreach(var connection in modules[next.target].Connections)
                        {
                            // logger.OnNext($"{next.target} -{(conjunction ? "high" : "low")}-> {connection}");
                            queue.Enqueue((connection, conjunction, next.target));
                        }
                        break;

                    default:
                        throw new Exception($"Unknown type {modules[next.target].type}");
                }
            } while (queue.Count > 0);

            if (CountRx > 0)
                logger.OnNext($"rx received {CountRx} pulses");

            if (CountRx == 1)
            {
                GiveAnswer2(CountRx);
                return;
            }

            if (i == 1000)
            {
                GiveAnswer1(countLow * countHigh);
                if (!hasRx)
                {
                    GiveAnswer2("");
                    return;
                }
            }

            if (nodes.All(n => n.Value.Count > 2) && hasRx)
            {
                var numbs = new List<double>();
                foreach (var n in nodes)
                {
                    numbs.Add(n.Value.Last() - n.Value[^2]);
                }

                GiveAnswer2(FindLeastCommonMultiple(numbs));
                return;
            }

            i++;
        }
    }

    public double FindLeastCommonMultiple(List<double> numbers)
    {
        double lcm = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = CalculateLCM(lcm, numbers[i]);
        }

        return lcm;
    }

    private double CalculateLCM(double a, double b)
    {
        return (a * b) / CalculateGCD(a, b);
    }

    private double CalculateGCD(double a, double b)
    {
        while (b != 0)
        {
            double remainder = a % b;
            a = b;
            b = remainder;
        }

        return a;
    }
}

public record Module(string name, string[] Connections, char type);
