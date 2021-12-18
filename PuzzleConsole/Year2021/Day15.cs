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
        var visited = new List<(int x, int y)>();
        var unvisited = new List<(int x, int y)>();

        var lookupTable = new Dictionary<(int x, int y), Node>();

        for (int y = 0; y < puzzle.GetLength(0); y++)
        {
            for (int x = 0; x < puzzle.GetLength(1); x++)
            {
                var node = (x, y);
                unvisited.Add(node);
                lookupTable.Add(node, new Node() { Risk = Decimal.MaxValue });
            }
        }

        lookupTable[(0,0)].Risk = 0;

        do
        {
            var currentNode = lookupTable
                .OrderBy(ln => ln.Value.Risk)
                .First(ln => unvisited.Contains(ln.Key)).Key;

            unvisited.Remove(currentNode);

            var neighbours = unvisited.Where(xy =>
                (Math.Abs(xy.x - currentNode.x) == 1 && xy.y == currentNode.y) ||
                (Math.Abs(xy.y - currentNode.y) == 1 && xy.x == currentNode.x));

            foreach (var n in neighbours)
            {
                var newRisk = lookupTable[currentNode].Risk + puzzle[n.x, n.y];
                if (newRisk < lookupTable[n].Risk)
                {
                    lookupTable[n].Risk = lookupTable[currentNode].Risk + puzzle[n.x, n.y];
                    lookupTable[n].Prev = currentNode;
                }
            }

            visited.Add(currentNode);
        } while (unvisited.Any());

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
