using System.Globalization;

namespace PuzzleConsole.Year2022.Day9;

public class Day9 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var trail = new HashSet<Coord>();

        var head = new Coord(0, 0);
        var tail = head;

        trail.Add(tail);

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
                        trail.Add(tail);
                        ammount--;
                    }
                    break;

                case "U":
                    while (ammount > 0)
                    {
                        head = head with { Y = head.Y + 1 };
                        tail = FollowHead(head, tail);
                        trail.Add(tail);
                        ammount--;
                    }
                    break;

                case "D":
                    while (ammount > 0)
                    {
                        head = head with { Y = head.Y - 1 };
                        tail = FollowHead(head, tail);
                        trail.Add(tail);
                        ammount--;
                    }
                    break;

                case "L":
                    while (ammount > 0)
                    {
                        head = head with { X = head.X - 1 };
                        tail = FollowHead(head, tail);
                        trail.Add(tail);
                        ammount--;
                    }
                    break;

                default:
                    throw new Exception($"Case {instruction}");
            }
        }

        return new[] { trail.Count().ToString() };
    }

    private Coord FollowHead(Coord head, Coord tail)
    {
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