using System.Numerics;
using System.Reactive.Concurrency;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Solvers.Year2024.Day23;

public class Day23Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        Dictionary<string, IList<string>> lan = [];
        var networks = new List<List<string>>();
        var nodes = new List<string>();

        Dictionary<string, IList<string>> network = [];

        foreach (var line in puzzle)
        {
            var parts = line.Split("-");
            AddToNetwork(network, parts[0], parts[1]);
            AddToNetwork(network, parts[1], parts[0]);
        }

        var count = 0;
        var seen = new HashSet<string>();

        foreach (var kv in network) // A
        {
            foreach (var n in kv.Value) // B
            {
                foreach (var nn in network[n]) // C
                {
                    var key = string.Join(",", new List<string>() { kv.Key, n, nn }.Order());
                    if (seen.Contains(key))
                        continue;

                    seen.Add(key);

                    if (network[nn].Contains(kv.Key) && (kv.Key[0] == 't' || n[0] == 't' || nn[0] == 't'))
                    {
                        count++;
                    }
                }
            }
        }
        GiveAnswer1(count);


        foreach (var line in puzzle)
        {
            var parts = line.Split("-");
            AddToNetwork(lan, parts[0], parts[1]);
            AddToNetwork(lan, parts[1], parts[0]);
            networks.Add(new List<string> { parts[0] });
            nodes.Add(parts[0]);
        }

        bool changed = false;
        do
        {
            changed = false;

            foreach (var net in networks)
            {
                foreach (var node in nodes)
                {
                    if (net.All(n => lan[n].Contains(node)) && !net.Contains(node))
                    {
                        net.Add(node);
                        changed = true;
                    }
                }
            }

        } while (changed);

        foreach (var n in networks.Where(n => n.Count == 3 && n.Any(s => s[0] == 't')))
        {
            logger.OnNext(string.Join(",", n));
        }

        GiveAnswer2(string.Join(",", networks.OrderByDescending(n => n.Count).First().OrderBy(s => s)));
    }

    private void AddToNetwork(Dictionary<string, IList<string>> network, string a, string b)
    {
        if (!network.ContainsKey(a))
        {
            network[a] = new List<string>();
        }

        if (!network[a].Contains(b))
            network[a].Add(b);
    }
}
