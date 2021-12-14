namespace PuzzleConsole.Year2021;

public class Day9 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var map = puzzle.Select(l => l.Select(s => int.Parse(s.ToString())).ToArray()).ToArray();

        var risk = 0;

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
                            }
                        }
                    }
                }
            }
        }

        return new string[] { risk.ToString() };
    }
}
