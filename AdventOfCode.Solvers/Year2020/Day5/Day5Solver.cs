namespace AdventOfCode.Solvers.Year2020.Day5;

public class Day5Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var seats = new List<int>();

        var highestSeatId = 0;
        foreach (var line in puzzle)
        {
            var f = 0;
            var b = 127;
            var l = 0;
            var r = 7;

            foreach (var c in line)
            {
                switch (c)
                {
                    case 'F':
                        b = (b + f) / 2;
                        break;
                    case 'B':
                        f = (b + f) / 2 + 1;
                        break;
                    case 'L':
                        r = (r + l) / 2;
                        break;
                    case 'R':
                        l = (r + l) / 2 + 1;
                        break;
                }
            }

            var seatId = f * 8 + l;
            seats.Add(seatId);

            if (seatId > highestSeatId)
            {
                highestSeatId = seatId;
            }
        }

        seats.Sort();
        var mySeatId = 0;
        for (var i = 0; i < seats.Count - 1; i++)
        {
            if (seats[i + 1] - seats[i] == 2)
            {
                mySeatId = seats[i] + 1;
            }
        }

        GiveAnswer1(highestSeatId.ToString());
        GiveAnswer2(mySeatId.ToString());
    }
}
