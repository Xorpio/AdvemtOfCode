using System.Reflection;

namespace PuzzleConsole.Year2021.Day15;

public class Day15 : ISolver
{
    public int Part { get; set; } = 2;
    public string[] Solve(string[] puzzle)
    {
        if (Part > 1)
        {
            puzzle = Duplicate(puzzle);
        }

        var map = new int[puzzle[0].Length, puzzle.Length];

        for (int y = 0; y < puzzle.Length; y++)
        {
            for (int x = 0; x < puzzle[y].Length; x++)
            {
                map[x,y] = Int32.Parse(puzzle[y][x].ToString());
            }
        }

        var answer = FindPath(map);
        return new string[] { answer.ToString() };
    }

    public decimal FindPath(int[,] puzzle)//, int x, int y, string[] visited)
    {
        var visited = new Dictionary<string, bool>();

        var lookupTable = new Dictionary<(int x, int y), Node>();

        for (int y = 0; y < puzzle.GetLength(0); y++)
        {
            for (int x = 0; x < puzzle.GetLength(1); x++)
            {
                var node = (x, y);
                visited.Add($"{node.x}.{node.y}", false);
                // lookupTable.Add(node, new Node() { Risk = Decimal.MaxValue });
            }
        }

        (int x, int y) maxkey = (puzzle.GetLength(0) - 1, puzzle.GetLength(1) - 1);

        lookupTable.Add((0,0), new Node() { Risk = 0 });
        lookupTable.Add(maxkey, new Node() { Risk = Decimal.MaxValue });
        var max = visited.Count();


        do
        {
            var currentNode = lookupTable
                .OrderBy(ln => ln.Value.Risk)
                .First(ln => !visited[$"{ln.Key.x}.{ln.Key.y}"]).Key;

            Console.WriteLine($"Looking at node {currentNode}. ({max}) with risk {lookupTable[currentNode].Risk}");

            visited[$"{currentNode.x}.{currentNode.y}"] = true;

            var neighbours = new List<(int x, int y)>();
            if (currentNode.x > 0 &&
                !visited[$"{currentNode.x - 1}.{currentNode.y}"])
            {
                neighbours.Add((currentNode.x -1, currentNode.y));
            }
            if (currentNode.y > 0 &&
                !visited[$"{currentNode.x}.{currentNode.y - 1}"])
            {
                neighbours.Add((currentNode.x, currentNode.y - 1));
            }

            if (currentNode.x < puzzle.GetLength(0) - 1 &&
                !visited[$"{currentNode.x + 1}.{currentNode.y}"])
            {
                neighbours.Add((currentNode.x + 1, currentNode.y));
            }
            if (currentNode.y < puzzle.GetLength(1) - 1 &&
                !visited[$"{currentNode.x}.{currentNode.y + 1}"])
            {
                neighbours.Add((currentNode.x, currentNode.y + 1));
            }

            foreach (var n in neighbours)
            {
                lookupTable.TryAdd(n, new Node() { Risk = Decimal.MaxValue, Prev = currentNode});
                var newRisk = lookupTable[currentNode].Risk + puzzle[n.x, n.y];
                if (newRisk < lookupTable[n].Risk)
                {
                    lookupTable[n].Risk = lookupTable[currentNode].Risk + puzzle[n.x, n.y];
                    lookupTable[n].Prev = currentNode;
                }
            }

        } while (lookupTable[maxkey].Risk == Decimal.MaxValue);

        return lookupTable[(puzzle.GetLength(0) - 1, puzzle.GetLength(1) - 1)].Risk;
    }

    public string[] Duplicate(string[] puzzle)
    {
        var modifier = new int[][]
        {
            new int[] { 0, 1, 2, 3, 4 },
            new int[] { 1, 2, 3, 4, 5 },
            new int[] { 2, 3, 4, 5, 6 },
            new int[] { 3, 4, 5, 6, 7 },
            new int[] { 4, 5, 6, 7, 8 },
        };

        var p = puzzle.ToList()
            .Select(l => l
                .Select(c => int.Parse(c.ToString()))
                .ToArray()
            ).ToArray();

        var r = new List<string>();


        foreach (var rowModifier in modifier)
        {
            foreach (var puzzleLine in p)
            {
                string rline = "";
                foreach (var m in rowModifier)
                {
                    foreach (var i in puzzleLine)
                    {
                        var newI = i + m;
                        if (newI > 9)
                        {
                            newI = newI % 9;
                        }

                        rline += newI.ToString();
                    }
                }
                r.Add((rline));
            }
        }

        return r.ToArray();
    }
}

public record Node()
{
    public decimal Risk { get; set; }
    public (int x, int y) Prev { get; set; }
}
