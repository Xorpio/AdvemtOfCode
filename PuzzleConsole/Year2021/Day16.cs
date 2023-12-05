namespace PuzzleConsole.Year2021.Day16;

public class Day16 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var pointer = 0;

        var msg = DecodeToBin(puzzle[0]);

        var answer = 0;

        decimal calculation = 0;

        do
        {
            //get header
            var lengAndPoints = GetPoints(msg[pointer..]);

            calculation += lengAndPoints.calculation;
            answer += lengAndPoints.points;
            pointer += lengAndPoints.length;
        } while (pointer < puzzle.Length); // string ?

        return new[] { answer.ToString(), calculation.ToString() };
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

    public (int points, int length, decimal calculation) GetPoints(string puzzle)
    {
        var points = Convert.ToInt32(puzzle[0..3], 2);
        var type = Convert.ToInt16(puzzle[3..6], 2);

        if (type == 4)
        {
            var (number, length) = GetLiteralLength(puzzle[6..]);
            return (points, length + 6, number);
        }

        var count = 0;
        var p = 0;
        var lenMsg = 0;
        var messages = new List<(int points, int length, decimal calculation)>();

        if (puzzle[6] == '0')
        {
            lenMsg = Convert.ToInt16(puzzle[7..22], 2);
            p = 22; //start of msg
            count = p;
            while (count < p + lenMsg)
            {
                var lp = GetPoints(puzzle[count..]);

                count += lp.length;
                messages.Add(lp);
            }

        }
        else
        {
            lenMsg = Convert.ToInt16(puzzle[7..18], 2);
            p = 18; //start of msg
            count = p;
            for (var i = 0; i < lenMsg; i++)
            {
                var lp = GetPoints(puzzle[count..]);

                count += lp.length;
                messages.Add(lp);
            }

        }

        var sum = messages.Aggregate((acc, res) => (acc.points + res.points, acc.length + res.length, acc.calculation + res.calculation));
        sum.points += points;
        sum.length += p;

        decimal answer;

        switch (type)
        {
            case 1:
                answer = 1;
                foreach (var msg in messages)
                {
                    answer = answer * msg.calculation;
                }
                sum.calculation = answer;
                break;
            case 2:
                answer = decimal.MaxValue;
                foreach (var msg in messages)
                {
                    if (msg.calculation < answer)
                        answer = msg.calculation;
                }
                sum.calculation = answer;
                break;
            case 3:
                answer = decimal.MinValue;
                foreach (var msg in messages)
                {
                    if (msg.calculation > answer)
                        answer = msg.calculation;
                }
                sum.calculation = answer;
                break;
            case 4:
                answer = decimal.MinValue;
                foreach (var msg in messages)
                {
                    if (msg.calculation > answer)
                        answer = msg.calculation;
                }
                sum.calculation = answer;
                break;
            case 5:
                sum.calculation = messages[0].calculation > messages[1].calculation ? 1 : 0;
                break;
            case 6:
                sum.calculation = messages[0].calculation < messages[1].calculation ? 1 : 0;
                break;
            case 7:
                sum.calculation = messages[0].calculation == messages[1].calculation ? 1 : 0;
                break;
        }

        return sum;
    }

    public (decimal num, int length) GetLiteralLength(string s)
    {
        int leng = 5;
        string number = "";
        while (s[(leng - 5)] == '1')
        {
            number += s[(leng - 4)..leng];
            leng += 5;
        }

        number += s[(leng - 4)..leng];
        return (Convert.ToInt64(number, 2), leng);
    }
}
