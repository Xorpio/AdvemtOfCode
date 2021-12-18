using System.Reactive.Linq;

namespace PuzzleConsole.Year2021.Day9;

public class Day9 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var map = puzzle.Select(l => l.Select(s => int.Parse(s.ToString())).ToArray()).ToArray();

        var risk = 0;

        var basins = new List<int>();
        for (var y = 0; y < map.Length; y++)
        {
            for(var x = 0; x < map[y].Length; x++)
            {
                if((x > 0 && map[y][x] < map[y][x -1]) || x == 0)
                {
                    if((y > 0 && map[y][x] < map[y - 1][x]) || y == 0)
                    {
                        if((x < map[y].Length - 1 && map[y][x] < map[y][x + 1]) || x == map[y].Length - 1)
                        {
                            if((y < map.Length - 1 && map[y][x] < map[y + 1][x]) || y == map.Length - 1)
                            {
                                risk += map[y][x] + 1;
                                var score = AddFrom(x,y,map);
                                basins.Add(score);
                            }
                        }
                    }
                }
            }
        }

        basins.Sort();
        basins.Reverse();
        var answer = basins.Take(3).Aggregate((acc, cum) => acc * cum);

        return new string[] { risk.ToString(), answer.ToString()};
    }

    public int AddFrom(int x, int y, int[][] map)
    {
        return calcRecur(x,y,new List<string>(), map);
    }

    public int calcRecur(int x, int y, IList<string> visited, int[][] map)
    {
        var score = 1;
        visited.Add($"{x}.{y}");
        if (x > 0 && map[y][x] < map[y][x -1] && map[y][x -1] != 9 && !visited.Contains($"{x - 1}.{y}"))
        {
            score += calcRecur(x -1, y, visited, map);
        }

        if (y > 0 && map[y][x] < map[y - 1][x] && map[y -1][x] != 9 && !visited.Contains($"{x}.{y - 1}"))
        {
            score += calcRecur(x, y - 1, visited, map);
        }

        if (x < map[y].Length - 1 && map[y][x] < map[y][x + 1] && map[y][x + 1] != 9 && !visited.Contains($"{x + 1}.{y}"))
        {
            score += calcRecur(x + 1, y, visited, map);
        }

        if (y < map.Length - 1 && map[y][x] < map[y + 1][x] && map[y + 1][x] != 9  && !visited.Contains($"{x}.{y + 1}"))
        {
            score += calcRecur(x, y + 1, visited, map);
        }
        return score;
    }
}
