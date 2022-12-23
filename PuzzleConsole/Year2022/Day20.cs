using System.ComponentModel.DataAnnotations;
using Spectre.Console;

namespace PuzzleConsole.Year2022.Day20;

public class Day20 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        return new[]
        {
            SolvePart1(puzzle),
            SolvePart2(puzzle)
        };
    }
    public string SolvePart1(string[] puzzle)
    {
        var nodes = puzzle.Select(l => int.Parse(l))
            .Select(i => new Node()
            {
                NodeValue = i
            }).ToList();

        var listSize = puzzle.Length;

        for (var index = 0; index < nodes.Count; index++)
        {
            var n = nodes[index];
            n.Id = index.ToString();
            n.Left = nodes[index == 0 ? ^1 : index - 1];
            n.Right = nodes[(index + 1) % listSize];
        }

        var list = Display(nodes);

        Node node;

        for (var index = 0; index < puzzle.Length; index++)
        {
            node = nodes.First(n => n.Id == index.ToString());
            var move = node.NodeValue;

            move %= listSize - 1;

            if (move == 0)
            {
                continue;
            }

            while(move < 0)
            {
                var ll = node.Left.Left; // 1
                var l = node.Left; // 2
                var r = node.Right; //4

                ll.Right = node;

                l.Left = node;
                l.Right = r;

                node.Left = ll;
                node.Right = l;

                r.Left = l;

                move++;
            }

            while (move > 0)
            {
                var l = node.Left; // 2
                var r = node.Right; //4
                var rr = node.Right.Right; // 5

                l.Right = r;

                node.Left = r;
                node.Right = rr;

                r.Left = l;
                r.Right = node;

                rr.Left = node;
                move--;
            }

            list = Display(nodes);
        }

        node = nodes.First(n => n.NodeValue == 0);

        double a = 0;
        double b = 0;
        double c = 0;

        for(int i = 0; i < listSize; i++)
        {
            if (1000 % listSize == i)
            {
                a = node.NodeValue;
            }
            if (2000 % listSize == i)
            {
                b = node.NodeValue;
            }
            if (3000 % listSize == i)
            {
                c = node.NodeValue;
            }
            node = node.Right;
        }

        var answer = a + b + c;
        return answer.ToString();
    }

    public string SolvePart2(string[] puzzle)
    {
        var nodes = puzzle.Select(l => double.Parse(l))
            .Select(i => new Node()
            {
                NodeValue = i * 811589153
            }).ToList();

        var listSize = puzzle.Length;

        for (var index = 0; index < nodes.Count; index++)
        {
            var n = nodes[index];
            n.Id = index.ToString();
            n.Left = nodes[index == 0 ? ^1 : index - 1];
            n.Right = nodes[(index + 1) % listSize];
        }

        var list = Display(nodes);

        Node node;

        for (var count = 0; count < 10; count++)
        for (var index = 0; index < puzzle.Length; index++)
        {
            node = nodes.First(n => n.Id == index.ToString());
            var move = node.NodeValue;

            move %= listSize - 1;

            if (move == 0)
            {
                continue;
            }

            while(move < 0)
            {
                var ll = node.Left.Left; // 1
                var l = node.Left; // 2
                var r = node.Right; //4

                ll.Right = node;

                l.Left = node;
                l.Right = r;

                node.Left = ll;
                node.Right = l;

                r.Left = l;

                move++;
            }

            while (move > 0)
            {
                var l = node.Left; // 2
                var r = node.Right; //4
                var rr = node.Right.Right; // 5

                l.Right = r;

                node.Left = r;
                node.Right = rr;

                r.Left = l;
                r.Right = node;

                rr.Left = node;
                move--;
            }

            list = Display(nodes);
        }

        node = nodes.First(n => n.NodeValue == 0);

        double a = 0;
        double b = 0;
        double c = 0;

        for(int i = 0; i < listSize; i++)
        {
            if (1000 % listSize == i)
            {
                a = node.NodeValue;
            }
            if (2000 % listSize == i)
            {
                b = node.NodeValue;
            }
            if (3000 % listSize == i)
            {
                c = node.NodeValue;
            }
            node = node.Right;
        }

        var answer = a + b + c;
        return answer.ToString();
    }

    public string Display(IList<Node> nodes)
    {
        var node = nodes.First();
        var tripd = node.Id;
        var finalList = new List<double>();
        do
        {
            finalList.Add(node.NodeValue);
            node = node.Right;
        } while (node.Id != tripd);

        return string.Join(", ", finalList);
    }
}

public class Node
{
    public Node Left { get; set; }
    public Node Right { get; set; }
    public double NodeValue { get; set; }
    public string Id { get; set; }
    public override string ToString()
    {
        return $"L: {Left.NodeValue} - {NodeValue} - R: {Right.NodeValue}";
    }
}