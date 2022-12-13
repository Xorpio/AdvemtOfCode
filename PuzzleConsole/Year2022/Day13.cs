using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.VisualBasic.CompilerServices;

namespace PuzzleConsole.Year2022.Day13;

public class Day13 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var comparer = new packetComparer();
        var results = new List<bool>();
        for (var index = 0; index < puzzle.Length; index+= 3)
        {
            var line1 = puzzle[index];
            var line2 = puzzle[index + 1];

            var p1 = ToPacket(line1);
            var p2 = ToPacket(line2);

            var list = new List<Packet>() { p1, p2 };
            list.Sort(comparer);

            results.Add(list[0] == p1);
        }

        var answer = 0;
        for (var index = 0; index < results.Count; index++)
        {
            var r = results[index];
            if (r)
            {
                answer += index + 1;
            }
        }

        return new string[] { answer.ToString() };
    }

    public Packet ToPacket(string line)
    {
        if (line[0] != '[')
        {
            return new Item()
            {
                Val = int.Parse(line)
            };
        }

        var p = new PacketList();

        if (line == "[]")
            return p;

        //buitenste overslaan
        var buffer = "";
        var nested = 0;
        for (var index = 1; index < line.Length - 1; index++)
        {
            var c = line[index];
            if (c != ',' || nested > 0)
            {
                buffer += c;
            }

            if (c == '[')
            {
                nested++;
            }

            if (c == ']')
            {
                nested--;
            }

            if (c == ',' && nested == 0)
            {
                p.Packets.Add(ToPacket(buffer));
                buffer = "";
            }
        }

        p.Packets.Add(ToPacket(buffer));

        return p;
    }
}

public class packetComparer : IComparer<Packet>
{
    public int Compare(Packet x, Packet y)
    {
        if (x is Item i1 && y is Item i2)
        {
            return i1.Val - i2.Val;
        }

        if (x is PacketList p1 && y is PacketList p2)
        {
            var count = (p1.Packets.Count > p2.Packets.Count)
                ? p2.Packets.Count
                : p1.Packets.Count;

            for (var index = 0; index < count; index++)
            {
                var v1 = p1.Packets[index];
                var v2 = p2.Packets[index];

                var res = Compare(v1, v2);

                if (res != 0)
                {
                    return res;
                }
            }

            return p1.Packets.Count - p2.Packets.Count;
        }

        if (x is PacketList)
            return Compare(x, new PacketList()
            {
                Packets = new List<Packet>(){y}
            });

        return Compare( new PacketList()
        {
            Packets = new List<Packet>() { x }
        }, y);
    }
}

public record Packet
{
}

record Item : Packet
{
    public int Val;
}

record PacketList : Packet
{
    public List<Packet> Packets = new List<Packet>();
}