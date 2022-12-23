using System.ComponentModel.DataAnnotations;
using Spectre.Console;

namespace PuzzleConsole.Year2022.Day20;

public class Day20 : ISolver
{
    public string[] Solve(string[] puzzle)
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
        var stop = node.Id;
        var finalList = new List<int>();
        do
        {
            finalList.Add(node.NodeValue);
            node = node.Right;
        } while (node.Id != stop);
        //
        // var a = finalList[1000 % listSize];
        // var b = finalList[2000 % listSize];
        // var c = finalList[3000 % listSize];

        var a = 0;
        var b = 0;
        var c = 0;

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



        // Console.WriteLine($"FinealList: {finalList.Count}");
        Console.WriteLine($"a, b c: {a}, {b}, {c}");
        Console.WriteLine($"nodes count: {nodes.Count}");
        Console.WriteLine($"listSite: {finalList.Count()}");

        var answer = a + b + c;
        return new[]
        {
            answer.ToString()
        };
        //
        // return nodes.Select(n => n.NodeValue.ToString()).ToArray() ;
        //
        // var list = new List<int>();
        // var dictionary = new Dictionary<int, string>();
        // for (var index = 0; index < puzzle.Length; index++)
        // {
        //     var line = puzzle[index];
        //     dictionary.Add(index, line);
        //     list.Add(index);
        // }
        // var listSize = list.Count() - 1;
        //
        // var answers = new List<string>();
        //
        // foreach (var kv in dictionary)
        // {
        //     var move = int.Parse(kv.Value);
        //     if (move == 0)
        //     {
        //         var j = string.Join(", ", list.Select(l => dictionary[l]));
        //         answers.Add(kv.Value + ", " + j);
        //
        //         continue;
        //     }
        //     var index = list.IndexOf(kv.Key);
        //     var newIndex = (index + move) % listSize;
        //
        //     while (newIndex < 0)
        //     {
        //         newIndex += listSize;
        //     }
        //
        //     //note before
        //     var after = list[newIndex];
        //
        //     list.Remove(kv.Key);
        //
        //     newIndex = list.IndexOf(after) + 1;
        //
        //     list.Insert(newIndex, kv.Key);
        //
        //     var join = string.Join(", ", list.Select(l => dictionary[l]));
        //     answers.Add(kv.Value + ", " + join);
        //     // "1, 2, 3, -2, -3, 0, 4",
        //     // "1, 2, -2, -3, 0, 3, 4",
        //     // "1, 2, -3, 0, 3, 4, -2",
        //     // "1, 2, -3, 0, 3, 4, -2",
        //     // "1, 2, -3, 4, 0, 3, -2"
        // }
        //
        // // return answers.ToArray();
        //
        // var indexOf0 = list.IndexOf(dictionary.First(kv => kv.Value == "0").Key);
        //
        // var a = int.Parse(dictionary[list[(indexOf0 + 1000) % listSize]]);
        // var b = int.Parse(dictionary[list[(indexOf0 + 2000) % listSize]]);
        // var c = int.Parse(dictionary[list[(indexOf0 + 3000) % listSize]]);
        // var answer = a + b + c;
        // return new[]
        // {
        //     answer.ToString()
        // };
    }

    public string Display(IList<Node> nodes)
    {
        var node = nodes.First();
        var tripd = node.Id;
        var finalList = new List<int>();
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
    public int NodeValue { get; set; }
    public string Id { get; set; }
    public override string ToString()
    {
        return $"L: {Left.NodeValue} - {NodeValue} - R: {Right.NodeValue}";
    }
}