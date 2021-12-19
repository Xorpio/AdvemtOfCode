using System.Diagnostics;
using System.Globalization;
using PuzzleConsole;

namespace PuzzleConsole.Year2021.Day16;

public class Day16 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var pointer = 0;

        var msg = DecodeToBin(puzzle[0]);

        var answer = 0;

        do
        {
            //get header
            var lengAndPoints = GetPoints(msg[pointer..]);

            answer += lengAndPoints.points;
            pointer += lengAndPoints.length;
        } while (pointer < puzzle.Length); // string ?

        return new[] { answer.ToString() };
    }

    private int GetNewPointerOperater(string puzzle, int pointer)
    {
        var begin = pointer;
        pointer += 2;
        var msg = DecodeToBin(puzzle[begin..pointer]);
        if (msg[6] == '0')
        {
            pointer += 5;
            msg = DecodeToBin(puzzle[begin..pointer]);
            var msgLength = Convert.ToInt16(msg[7..22], 2);
            while (msg.Length < msgLength + 22)
            {
                pointer++;
                msg += DecodeToBin(puzzle[(pointer - 1)..pointer]);
            }
        }
        else
        {
            pointer += 4;
            msg = DecodeToBin(puzzle[begin..pointer]);
            var msgLength = Convert.ToInt16(msg[7..18], 2);
            var p = 18;
            for (int i = 0; i < msgLength; i++)
            {
                pointer = getNewInnerPointer(puzzle[begin..], p, pointer);
            }
        }

        return pointer + 1;
    }

    int getNewInnerPointer(string puzzle, int point, int pointer)
    {
        var add = Math.Ceiling((point + 6) / 4.0);
        var msg = DecodeToBin(puzzle[pointer..(pointer + (int)add)]);
        if (msg[(point + 3)..(point + 6)] != "100")
        {
            throw new Exception("Msg type not supported");
        }

        return int.MaxValue;
    }

    private int GetNewPointerLiteral(string puzzle, int pointer)
    {
        var begin = pointer;
        pointer += 2;
        var msg = DecodeToBin(puzzle[begin..pointer]);
        var point = 6;

        bool more = false;

        do
        {
            while (msg.Length < point + 4)
            {
                pointer++;
                msg = DecodeToBin(puzzle[begin..pointer]);
            }

            more = msg[point] == '1';
            point += 5;
        } while (more);

        return pointer + 1;
    }

    public (int Version, int Type) GetHeader(string input)
    {
        var bin = DecodeToBin(input);

        var version = Convert.ToInt16(bin[0..3], 2);
        var type = Convert.ToInt16(bin[3..6], 2);

        return (version, type);
    }

    public string DecodeToBin(string input)
    {
        var ret = "";
        foreach (var c in input)
        {
            ret += c switch
            {
                'a' => "0001",
                '0' => "0000",
                '1' => "0001",
                '2' => "0010",
                '3' => "0011",
                '4' => "0100",
                '5' => "0101",
                '6' => "0110",
                '7' => "0111",
                '8' => "1000",
                '9' => "1001",
                'A' => "1010",
                'B' => "1011",
                'C' => "1100",
                'D' => "1101",
                'E' => "1110",
                'F' => "1111",
                _ => throw new NotSupportedException($"The value ({c}) is not supported")
            }
            ;
        }

        return ret;
    }

    public (int points, int length) GetPoints(string puzzle)
    {
        var points = Convert.ToInt16(puzzle[0..3], 2);
        var type = Convert.ToInt16(puzzle[3..6], 2);

        if (type == 4)
        {
            return (points, GetLiteralLength(puzzle[6..]) + 6);
        }

        if (puzzle[6] == '0')
        {
            var lenMsg = Convert.ToInt16(puzzle[7..22], 2);
            var p = 22; //start of msg
            var count = p;
            while(count < p + lenMsg)
            {
                points += Convert.ToInt16(puzzle[count..(count + 3)], 2);
                if(Convert.ToInt16(puzzle[(count + 3)..(count + 6)], 2) != 4)
                {
                    throw new Exception("Expected literal");
                }

                count += GetLiteralLength(puzzle[(count + 6)..]) + 6;
            }
        }


        return (points, 0);

    }

    public int GetLiteralLength(string s)
    {
        int leng = 5;
        while (s[(leng - 5)] == '1')
        {
            leng += 5;
        }

        return leng;
    }
}

public enum PacketType
{

}