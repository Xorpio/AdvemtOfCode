using System.Globalization;
using Spectre.Console;

namespace PuzzleConsole.Year2022.Day9;

public class Day9 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var trailPart1 = new HashSet<Coord>();
        var trailPart2 = new HashSet<Coord>();

        var head = new Coord(0, 0);
        var tail = head;

        var knots = new Coord[]
        {
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
            new Coord(0,0),
        };

        trailPart1.Add(tail);
        trailPart2.Add(knots[9]);

        foreach (var instruction in puzzle)
        {
            var split = instruction.Split(' ');
            var directions = split[0];
            var ammount = int.Parse(split[1]);

            switch (directions)
            {
                case "R":
                    while (ammount > 0)
                    {
                        head = head with { X = head.X + 1 };
                        tail = FollowHead(head, tail);
                        trailPart1.Add(tail);

                        knots[0] = knots[0] with { X = knots[0].X + 1 };
                        for (var index = 1; index < knots.Length; index++)
                        {
                            knots[index] = FollowHead(knots[index - 1], knots[index]);
                        }
                        trailPart2.Add(knots[9]);

                        ammount--;
                    }
                    break;

                case "U":
                    while (ammount > 0)
                    {
                        head = head with { Y = head.Y + 1 };
                        tail = FollowHead(head, tail);
                        trailPart1.Add(tail);

                        knots[0] = knots[0] with { Y = knots[0].Y + 1 };
                        for (var index = 1; index < knots.Length; index++)
                        {
                            knots[index] = FollowHead(knots[index - 1], knots[index]);
                        }
                        trailPart2.Add(knots[9]);

                        ammount--;
                    }
                    break;

                case "D":
                    while (ammount > 0)
                    {
                        head = head with { Y = head.Y - 1 };
                        tail = FollowHead(head, tail);
                        trailPart1.Add(tail);

                        knots[0] = knots[0] with { Y = knots[0].Y - 1 };
                        for (var index = 1; index < knots.Length; index++)
                        {
                            knots[index] = FollowHead(knots[index - 1], knots[index]);
                        }
                        trailPart2.Add(knots[9]);

                        ammount--;
                    }
                    break;

                case "L":
                    while (ammount > 0)
                    {
                        head = head with { X = head.X - 1 };
                        tail = FollowHead(head, tail);
                        trailPart1.Add(tail);

                        knots[0] = knots[0] with { X = knots[0].X - 1 };
                        for (var index = 1; index < knots.Length; index++)
                        {
                            knots[index] = FollowHead(knots[index - 1], knots[index]);
                        }

                        trailPart2.Add(knots[9]);
                        ammount--;
                    }
                    break;

                default:
                    throw new Exception($"Case {instruction}");
            }

            trailPart1.Add(tail);
            trailPart2.Add(knots[9]);
        }

        //visualize Part1
        var maxX = trailPart2.Select(r => r.X)
            .Max() + 2;
        var maxY = trailPart2.Select(r => r.Y)
            .Max() + 2;
        var minX = trailPart2.Select(r => r.X)
            .Min();
        var minY = trailPart2.Select(r => r.Y)
            .Min();

        var canvas = new Canvas(maxX - minX, maxY - minY);

        foreach (var s in trailPart2)
        {
            Draw(s.X, s.Y, Color.Yellow, canvas, minX, minY);
        }

        AnsiConsole.Write(canvas);

        return new[] { trailPart1.Count().ToString(), trailPart2.Count().ToString() };
    }

    public void Draw(int x, int y, Color color, Canvas canvas, int minX, int minY)
    {
        x = x + (minX * -1) + 1;
        y = y + (minY * -1) + 1;
        canvas.SetPixel(x, y, color);
    }
    public Coord FollowHead(Coord head, Coord tail)
    {
        if (head.X > tail.X + 1 && head.Y > tail.Y + 1)
        {
            return head with { X = head.X - 1, Y = head.Y - 1 };
        }
        if (head.X > tail.X + 1 && head.Y < tail.Y - 1)
        {
            return head with { X = head.X - 1, Y = head.Y + 1 };
        }
        if (head.X < tail.X - 1 && head.Y > tail.Y + 1)
        {
            return head with { X = head.X + 1, Y = head.Y - 1 };
        }
        if (head.X < tail.X - 1 && head.Y < tail.Y - 1)
        {
            return head with { X = head.X + 1, Y = head.Y + 1 };
        }
        if (head.X > tail.X + 1)
        {
            return head with { X = head.X - 1 };
        }
        if (head.X < tail.X - 1)
        {
            return head with { X = head.X + 1 };
        }
        if (head.Y > tail.Y + 1)
        {
            return head with { Y = head.Y - 1 };
        }
        if (head.Y < tail.Y - 1)
        {
            return head with { Y = head.Y + 1 };
        }
        return tail;
    }
}

public record Coord (int X, int Y);