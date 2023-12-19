
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Solvers.Year2023.Day14;

public class Day14Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        int maxRow = puzzle.Length;
        int maxCol = puzzle[0].Length;

        var map = new char[maxRow, maxCol];

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                map[row, col] = puzzle[row][col];
            }
        }

        // (int row, char c)[][] map = [];
        // for(int row = 0; row < map.Length; row++)
        // {
        //     for(int b = 0; b < map[row].Length; b++)
        //     {
        //         if (map[row][b].c == '#')
        //             continue;

        //         if (b == 0)
        //         {
        //             map[row][b].row = 0;
        //             continue;
        //         }

        //         map[row][b].row = map[row][b - 1].row + 1;
        //     }
        // }
        
        // printmap(map);

        map = Tilt(map, Direction.north);

        // printmap(map);

        var answer = 0;
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                answer += map[row, col] == 'O' ? maxRow - row : 0;
            }
        }
        GiveAnswer1(answer);

        //reset map

        for (int row = 0; row < puzzle.Length; row++)
        {
            for (int col = 0; col < puzzle[row].Length; col++)
            {
                map[row, col] = puzzle[row][col];
            }
        }

        HashSet<string> cache = [];
        List<(string key, int i)> pattern = [];
        bool start = false;
        bool skipped = false;
        var loop = 0;
        for(int i = 0; i < 1000000000; i++)
        {
            for(int dir = 0; dir < 4; dir++)
            {
                map = Tilt(map, (Direction)dir);
            }

            string key = getKey(map, null);

            // if (skipped && cache.Contains(key))
            // {
            //     logger.OnNext($"loop detected at {i}");
            //     // break;
            // }

            if (!start && cache.Contains(key))
            {
                start = true;
            }

            if (start)
            {
                if (!skipped && pattern.Any(p => p.key == key))
                {
                    logger.OnNext($"loop detected at {i}");

                    i = i + pattern.Count * (int)Math.Floor((double)(1000000000 - i) / pattern.Count);
                    logger.OnNext($"skipping to {i}");
                    skipped = true;
                }
                pattern.Add((key, i));
            }

            // if (skipped)
            // {
            //     loop++;
            //     answer = 0;
            //     for (int row = 0; row < map.GetLength(0); row++)
            //     {
            //         for (int col = 0; col < map.GetLength(1); col++)
            //         {
            //             answer += map[row, col] == 'O' ? maxRow - row : 0;
            //         }
            //     }

            //     logger.OnNext($"loop {loop} answer {answer}");
            // }

            logger.OnNext($"i: {i}");
            // printmap(map);
            cache.Add(key);
        }
        printmap(map);

        answer = 0;
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                answer += map[row, col] == 'O' ? maxRow - row : 0;
            }
        }
        GiveAnswer2(answer);
    }

    private void printmap(char[,] map)
    {
        // var maxRow = map.Max(kvp => kvp.p.row);
        // var maxCol = map.Max(kvp => kvp.p.col);

        for (int row = 0; row < map.GetLength(0); row++)
        {
            var line = "";

            for (int col = 0; col < map.GetLength(1); col++)
            {
                line += map[row, col];
            }
            logger.OnNext(line);
        }

        logger.OnNext("");
    }

    private string getKey(char[,] map, Direction? direction)
    {
        var key = "";
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                key += map[row, col];
            }
        }
        if (direction.HasValue)
            key += direction;
        return key;
    }

    private char[,] Tilt(char[,] map, Direction direction)
    {
        if (direction == Direction.north)
        {
            int[] counts = new int[map.GetLength(1)];
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    switch (map[row, col])
                    {
                        case 'O':
                            if (row > counts[col])
                            {
                                map[counts[col], col] = 'O';
                                map[row, col] = '.';
                            }
                            counts[col]++;
                            break;
                        case '#':
                            counts[col] = row + 1;
                            break;
                    }
                }
            }
        }

        if (direction == Direction.east)
        {
            var counts = Enumerable.Range(0, map.GetLength(0)).Select(i => map.GetLength(1) - 1).ToArray();
            for(int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = map.GetLength(1) - 1; col >= 0; col--)
                {
                    switch (map[row, col])
                    {
                        case 'O':
                            if (col < counts[row])
                            {
                                map[row, counts[row]] = 'O';
                                map[row, col] = '.';
                            }
                            counts[row]--;
                            break;
                        case '#':
                            counts[row] = col - 1;
                            break;
                    }
                }
            }
        }

        if (direction == Direction.south)
        {
            var counts = Enumerable.Range(0, map.GetLength(1)).Select(i => map.GetLength(0) - 1).ToArray();
            for (int row = map.GetLength(0) - 1; row >= 0; row--)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    switch (map[row, col])
                    {
                        case 'O':
                            if (row < counts[col])
                            {
                                map[counts[col], col] = 'O';
                                map[row, col] = '.';
                            }
                            counts[col]--;
                            break;
                        case '#':
                            counts[col] = row - 1;
                            break;
                    }
                }
            }
        }

        if (direction == Direction.west)
        {
            int[] counts = new int[map.GetLength(0)];
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    switch (map[row, col])
                    {
                        case 'O':
                            if (col > counts[row])
                            {
                                map[row, counts[row]] = 'O';
                                map[row, col] = '.';
                            }
                            counts[row]++;
                            break;
                        case '#':
                            counts[row] = col + 1;
                            break;
                    }
                }
            }
        }

        return map;
    }

    // private void TiltNorth(char[,] map)
    // {
    //     var mapCopy = _map.Select(kvp => kvp).ToList().OrderBy(kvp => kvp.p.row).ThenBy(kvp => kvp.p.col).ToArray();
    //     var length = mapCopy.Length;

    //     for (int i = 0; i < mapCopy.Length; i++)
    //     {
    //         var (p, c) = mapCopy[i];

    //         if (c == '#')
    //             continue;

    //         var newP = new Point(p.row, p.col);

    //         do
    //         {
    //             if (mapCopy.Any(kvp => kvp.p == newP with {row= newP.row - 1}) || newP.row == 0)
    //             {
    //                 break;
    //             }
    //             newP = new Point(newP.row - 1, newP.col);
    //         } while(true);

    //         mapCopy[i].p = newP;
    //     }

    //     _map = mapCopy.ToList();
    // }
}

public record Point(int row, int col);

public enum Direction
{
    north = 0,
    west = 1,
    south = 2,
    east = 3
}
