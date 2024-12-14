
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Solvers.Year2024.Day14;

public class Day14Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var isTest = puzzle.Length < 15;
        var cols = isTest ? 11 : 101;
        var rows = isTest ? 7 : 103;

        var robots = puzzle.Select(ToRobot).ToArray();

var seconds = 0;
        while (true)
        {
            // printRobots(robots, rows, cols, seconds);
            for (int i = 0; i < robots.Length; i++)
            {
                robots[i].col = (robots[i].col + robots[i].colSpeed);
                robots[i].row = (robots[i].row + robots[i].rowSpeed);
                if (robots[i].col < 0)
                    robots[i].col += cols;
                if (robots[i].row < 0)
                    robots[i].row += rows;
                robots[i].col = robots[i].col % cols;
                robots[i].row = robots[i].row % rows;

            }
            seconds++;

            if (seconds == 100)
            {
                var q1 = robots.Where(r => r.row < Math.Floor((decimal)rows / 2) && r.col < Math.Floor((decimal)cols / 2)).Count();
                var q2 = robots.Where(r => r.row < Math.Floor((decimal)rows / 2) && r.col > Math.Floor((decimal)cols / 2)).Count();
                var q3 = robots.Where(r => r.row > Math.Floor((decimal)rows / 2) && r.col < Math.Floor((decimal)cols / 2)).Count();
                var q4 = robots.Where(r => r.row > Math.Floor((decimal)rows / 2) && r.col > Math.Floor((decimal)cols / 2)).Count();
                GiveAnswer1(q1 * q2 * q3 * q4);
            }

            if (robots.GroupBy(r => r.row).Where(g => g.Count() > 30).Count() > 1 &&
            robots.GroupBy(r => r.col).Where(g => g.Count() > 30).Count() > 1)
            {
                printRobots(robots, rows, cols, seconds);
                GiveAnswer2(seconds);
                break;
            }

            if (isTest && seconds > 100)
            {
                GiveAnswer2("");
                break;
            }
        }
    }

    private void printRobots((int col, int row, int colSpeed, int rowSpeed)[] robots, int rows, int cols, int seconds)
    {
        logger.OnNext($"Seconds: {seconds}");
        string line = "";
        for(int r = 0; r < rows; r++)
        {
            for(int c = 0; c < cols; c++)
            {
                var count = robots.Where(rob => rob.col == c && rob.row == r).Count();
                line += count == 0 ? "." : count.ToString();
            }
            logger.OnNext(line);
            line = "";
        }
    }

    private (int col, int row, int colSpeed, int rowSpeed) ToRobot(string line)
    {
        var parts = line.Split(' ');
        var colrow = parts[0][2..].Split(',');
        (int col, int row) = (int.Parse(colrow[0]), int.Parse(colrow[1]));
        var speed = parts[1][2..].Split(',');
        (int colSpeed, int rowSpeed) = (int.Parse(speed[0]), int.Parse(speed[1]));

        return (col,row,colSpeed,rowSpeed);
    }
}
