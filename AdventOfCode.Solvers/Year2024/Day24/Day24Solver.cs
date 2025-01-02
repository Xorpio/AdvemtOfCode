using AdventOfCode.Solvers.Year2024.Day16;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.ConstrainedExecution;

namespace AdventOfCode.Solvers.Year2024.Day24;

public class Day24Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        List<(string wire, int val)> values = new List<(string wire, int val)>();
        List<(string wire1, string op, string wire2, string output)> wires = new List<(string wire1, string op, string wire2, string output)>();
        var allWires = new HashSet<string>();

        var isWires = false;
        foreach (var line in puzzle)
        {
            if (line == "")
            {
                isWires = true;
                continue;
            }

            if (isWires)
            {
                var parts = line.Split(" ");
                wires.Add((parts[0], parts[1], parts[2], parts[4]));
                allWires.Add(parts[0]);
                allWires.Add(parts[2]);
                allWires.Add(parts[4]);
            }
            else
            {
                var parts = line.Split(": ");
                values.Add((parts[0], int.Parse(parts[1])));
                allWires.Add(parts[0]);
            }
        }


        (var x, var y, var z) = Run(values, wires);

        GiveAnswer1(ConvertBinaryStringToBigInteger(z).ToString());

        GiveAnswer2(SolvePart2Again(wires, []));
    }

    public List<(string wire1, string wire2)> GetPairs(List<string> badWires)
    {
        var swaps = new List<(string wire1, string wire2)>();
        for (int i = 0; i < badWires.Count; i++)
        {
            string output1 = badWires[i];
            for (int ii = i + 1; ii < badWires.Count; ii++)
            {
                string output2 = badWires[ii];
                swaps.Add((output1, output2));
            }
        }

        return swaps;
    }

    private string SolvePart2Again(List<(string wire1, string op, string wire2, string output)> wires, List<string> swaps, int bit = 0)
    {
        var badBit = Validate(wires);

        var swapsString = string.Join(",", swaps.Order());

        if (badBit is null)
        {
            return swapsString;
        }

        if (swaps.Count == 8)
        {
            return "";
        }

        var goodWires = new List<string>();
        for (int i = 0; i < badBit; i++)
        {
            goodWires.AddRange(FindWires(wires, $"z{i:d2}"));
        }


        var allOutputs = wires.Select(w => w.output).Except(goodWires).Except(swaps).ToList();

        var curSwaps = new List<(string wire1, string wire2)>();

        for (int i = 0; i < allOutputs.Count; i++)
        {
            string output1 = allOutputs[i];
            for (int ii = i + 1; ii < allOutputs.Count; ii++)
            {
                string output2 = allOutputs[ii];
                curSwaps.Add((output1, output2));
            }
        }

        var count = 0;
        foreach (var curSwap in curSwaps)
        {
            if (count % 1000 == 0)
                logger.OnNext($"swap {count} of {curSwaps.Count} - {bit} - {swapsString}");

            count++;
            if (swaps.Contains(curSwap.wire1) || swaps.Contains(curSwap.wire2))
            {
                continue;
            }

            var swappedWires = SwapWires(wires, new List<(string wire1, string wire2)> { curSwap });
            var badBit2 = Validate(swappedWires) ?? int.MaxValue;
            if (badBit2 > badBit)
            {
                var response = SolvePart2Again(swappedWires, swaps.Concat(new List<string>() { curSwap.wire1, curSwap.wire2 }).ToList(), badBit.Value);
                if (response != "")
                {
                    return response;
                }
            }
        }

        return "";
    }

    public int? Validate(List<(string wire1, string op, string wire2, string output)> wires)
    {
        for (int i = 0; i < 50; i++)
        {
            var values = new List<(string wire, int value)>();
            for (int v = 0; v < 50; v++)
            {
                values.Add(($"x{v:d2}", v == i ? 1 : 0));
                values.Add(($"y{v:d2}", v == (IsRunningFromUnitTest() ? i : 0) ? 1 : 0));
            }

            (var x, var y, var z) = Run(values, wires);

            (int bit, bool res) result = TestOutputBit(x, y, z);
            if (!result.res)
            {
                return result.bit;
            }
        }

        return null;
    }

    private string outputLogic((string wire1, string op, string wire2, string output) logic, List<(string wire1, string op, string wire2, string output)> wires)
    {
        var part1 = wires.FirstOrDefault(w => w.output == logic.wire1);
        var part2 = wires.FirstOrDefault(w => w.output == logic.wire2);
        return $"{(part1 is (null, null, null, null) ? logic.wire1 : $"[{logic.wire1}]({outputLogic(part1, wires)})")} {logic.op} {(part2 is (null, null, null, null) ? logic.wire2 : $"[{logic.wire2}]({outputLogic(part2, wires)})")}";
    }

    private List<string> FindWires(List<(string wire1, string op, string wire2, string output)> wires, string index)
    {
        var q = new Queue<string>();
        var seen = new HashSet<string>();
        q.Enqueue(index);
        var result = new List<string>();
        while (q.Count > 0)
        {
            var next = q.Dequeue();

            if (seen.Contains(next))
            {
                continue;
            }
            seen.Add(next);

            result.Add(next);

            var wire = wires.FirstOrDefault(w => w.output == next);
            if (wire == default)
            {
                continue;
            }

            q.Enqueue(wire.wire1);
            q.Enqueue(wire.wire2);
        }

        return result;
    }

    private (int, bool) TestOutputBit(string x, string y, string z)
    {
        if (x == "false" && y == "false" && z == "false")
        {
            return (0, false);
        }

        var calc = IsRunningFromUnitTest()
            ? ConvertBinaryStringToBigInteger(x) & ConvertBinaryStringToBigInteger(y)
            : ConvertBinaryStringToBigInteger(x) + ConvertBinaryStringToBigInteger(y);
        var expected = new string(Convert.ToString(calc, 2).PadLeft(50, '0').Reverse().ToArray());
        var actual = new string(z.PadLeft(50, '0').Reverse().ToArray());

        if (expected == actual)
        {
            return (0, true);
        }

        for (int i = 0; i < (IsRunningFromUnitTest() ? 6 : 45); i++)
        {
            if (expected[i] != actual[i])
            {
                return (i, false);
            }
        }

        return (0, true);
        //convert back to binary
    }

    private List<(string wire1, string op, string wire2, string output)> SwapWires(List<(string wire1, string op, string wire2, string output)> wires, List<(string wire1, string wire2)> swaps)
    {
        List<(string wire1, string op, string wire2, string output)> copy = [];

        foreach (var wire in wires)
        {
            if (swaps.Any(s => s.wire1 == wire.output))
            {
                copy.Add((wire.wire1, wire.op, wire.wire2, swaps.First(s => s.wire1 == wire.output).wire2));
            }
            else if (swaps.Any(s => s.wire2 == wire.output))
            {
                copy.Add((wire.wire1, wire.op, wire.wire2, swaps.First(s => s.wire2 == wire.output).wire1));
            }
            else
            {
                copy.Add(wire);
            }
        }

        if (copy.Count != wires.Count)
        {
            throw new Exception("Something went wrong");
        }
        return copy;
    }

    private (string x, string y, string z) Run(List<(string wire, int val)> values, List<(string wire1, string op, string wire2, string output)> wires)
    {

        var subjects = new Dictionary<string, ReplaySubject<bool>>();

        foreach (var val in values)
        {
            subjects.Add(val.wire, new ReplaySubject<bool>());
            subjects[val.wire].OnNext(val.val == 1);
        }

        foreach (var wire in wires)
        {
            if (!subjects.ContainsKey(wire.wire1))
            {
                subjects.Add(wire.wire1, new ReplaySubject<bool>());
            }
            if (!subjects.ContainsKey(wire.wire2))
            {
                subjects.Add(wire.wire2, new ReplaySubject<bool>());
            }
            if (!subjects.ContainsKey(wire.output))
            {
                subjects.Add(wire.output, new ReplaySubject<bool>());
            }
            switch (wire.op)
            {
                case "AND":
                    subjects[wire.wire1].Zip(subjects[wire.wire2], (a, b) => a && b).Subscribe(subjects[wire.output]);
                    break;
                case "OR":
                    subjects[wire.wire1].Zip(subjects[wire.wire2], (a, b) => a || b).Subscribe(subjects[wire.output]);
                    break;
                case "XOR":
                    subjects[wire.wire1].Zip(subjects[wire.wire2], (a, b) => a ^ b).Subscribe(subjects[wire.output]);
                    break;
            }
        }

        var x = "";
        foreach (var k in subjects.Keys.Where(k => k.StartsWith("x")).Order())
        {
            int? res = null;
            subjects[k].Subscribe(b =>
            {
                res = b ? 1 : 0;
            });
            if (res == null)
            {
                return ("false", "false", "false");
            }
            x = res + x;
        }

        var y = "";
        foreach (var k in subjects.Keys.Where(k => k.StartsWith("y")).Order())
        {
            int? res = null;
            subjects[k].Subscribe(b =>
            {
                res = b ? 1 : 0;
            });
            if (res == null)
            {
                return ("false", "false", "false");
            }
            y = res + y;
        }

        var z = "";
        foreach (var k in subjects.Keys.Where(k => k.StartsWith("z")).Order())
        {
            int? res = null;
            subjects[k].Subscribe(b =>
            {
                res = b ? 1 : 0;
            });
            if (res == null)
            {
                return ("false", "false", "false");
            }
            z = res + z;
        }

        return (x, y, z);
    }
    static long ConvertBinaryStringToBigInteger(string binaryString)
    {
        long result = 0;
        foreach (char c in binaryString)
        {
            result <<= 1;
            if (c == '1')
            {
                result |= 1;
            }
        }
        return result;
    }
}
