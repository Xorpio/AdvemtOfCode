using AdventOfCode.Solvers.Year2023.Day14;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Disposables;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Solvers.Year2024.Day16;

public class Day16Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        (int row, int col) start = (0, 0);
        (int row, int col) end = (0, 0);

        for (int r = 0; r < puzzle.Length; r++)
        {
            for (int c = 0; c < puzzle[r].Length; c++)
            {
                if (puzzle[r][c] == 'S')
                {
                    start = (r, c);
                }
                else if (puzzle[r][c] == 'E')
                {
                    end = (r, c);
                }
            }
        }

        var distances = new Dictionary<Node, (List<Node> nodes, int score)>();

        var queue = new PriorityQueue<(Node current, Node? previous, int score), int>();

        queue.Enqueue((new Node(start.row, start.col, Direction.Right), null, 0), 0);

        int maxScore = int.MaxValue;

        while (queue.Count > 0)
        {
            (Node current, Node? previous, int score) = queue.Dequeue();

            if (!distances.ContainsKey(current))
            {
                distances[current] = (new List<Node>(), score);
            }

            if (distances[current].score < score)
            {
                continue;
            }

            if (previous != null)
            {
                distances[current].nodes.Add(previous);

                if (distances[current].nodes.Count > 1)
                    continue;
            }

            if (current.row == end.row && current.col == end.col)
            {
                maxScore = score;
                GiveAnswer1(maxScore);
                continue;
            }

            if (score >= maxScore)
            {
                continue;
            }

            // right, left, down , up
            foreach (var (dr, dc) in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) })
            {
                var newRow = current.row + dr;
                var newCol = current.col + dc;
                if (puzzle[newRow][newCol] == '#')
                {
                    continue;
                }

                var newDir = GetDirection(dr, dc);

                if (
                    (current.dir == Direction.Right && newDir == Direction.Left) ||
                    (current.dir == Direction.Left && newDir == Direction.Right) ||
                    (current.dir == Direction.Up && newDir == Direction.Down) ||
                    (current.dir == Direction.Down && newDir == Direction.Up)
                )
                {
                    continue;
                }

                var newScore = score + ((current.dir == newDir) ? 1 : 1001);
                var newNode = new Node(newRow, newCol, newDir);

                if (!distances.ContainsKey(newNode) || newScore <= distances[newNode].score)
                {
                    queue.Enqueue((newNode, current, newScore), newScore);
                }
            }
        }

        var visited = new HashSet<Node>();
        var q = new Queue<Node>();
        foreach (var n in distances.Keys.Where(k => k.row == end.row && k.col == end.col))
        {
            q.Enqueue(n);
        }

        while (q.Count > 0)
        {
            var current = q.Dequeue();
            visited.Add(current);
            foreach (var n in distances[current].nodes)
            {
                q.Enqueue(n);
            }
        }

        GiveAnswer2(visited.GroupBy(n => (n.row, n.col)).Count());
    }

    public Direction GetDirection(int dr, int dc) => (dr, dc) switch
    {
        (0, 1) => Direction.Right,
        (0, -1) => Direction.Left,
        (1, 0) => Direction.Down,
        (-1, 0) => Direction.Up,
        _ => throw new InvalidOperationException()
    };
}

public record Node(int row, int col, Direction dir);

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
