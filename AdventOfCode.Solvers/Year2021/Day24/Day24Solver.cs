using System.Diagnostics;
using System.Net.NetworkInformation;

namespace AdventOfCode.Solvers.Year2021.Day24;

public class Day24Solver : BaseSolver
{
    private string[] _puzzle;
    public override void Solve(string[] puzzle)
    {
        _puzzle = puzzle;

        var part1 = solvePuzzle(0, 0, 0) ?? throw new Exception();

        GiveAnswer1("");
        GiveAnswer2("");
    }

    public string? solvePuzzle(int count, int z, int level)
    {
        var instruction = _puzzle[count];

        //check for z / 26
        var divInstruction = getZDivInstruction(count);

        if (divInstruction.EndsWith("1"))
        {
            for (int n = 9; n > 0; n--)
            {
                logger.OnNext("n: ".PadRight(level) + n);
                var (newZ, newCount) = runBlock(count + 1, n, z);

                if (newCount == _puzzle.Length)
                {
                    return newZ == 0 ? $"{n}" : null;
                }

                var result = solvePuzzle(newCount, newZ, level + 1);
                if (result != null)
                {
                    return $"{n}{result}";
                }
            }
        }
        else
        {
            //calc n
            for (int n = 9; n > 0; n--)
            {
                var (newZ, newCount) = runBlock(count + 1, n, z);

                if (newZ < z)
                {
                    logger.OnNext("n: ".PadRight(level) + n);
                    if (newCount == _puzzle.Length)
                    {
                        return newZ == 0 ? $"{n}" : null;
                    }

                    var result = solvePuzzle(newCount, newZ, level + 1);
                    if (result != null)
                    {
                        return $"{n}{result}";
                    }
                }
            }

            return null;
        }

        return null;
    }

    public (int z, int count) runBlock(int count, int w, int z)
    {
        var x = 0;
        var y = 0;
        var line = _puzzle[count];
        do
        {
            var instructions = line.Split(' ');

            //get value
            var value = instructions[2] switch
            {
                "w" => w,
                "x" => x,
                "y" => y,
                "z" => z,
                _ => int.Parse(instructions[2])
            };

            switch (instructions[0])
            {
                case "mul":
                    switch (instructions[1])
                    {
                        case "w":
                            w = w * value;
                            break;
                        case "x":
                            x = x * value;
                            break;
                        case "y":
                            y = y * value;
                            break;
                        case "z":
                            z = z * value;
                            break;
                        default: throw new Exception();
                    }
                    break;
                case "add":
                    switch (instructions[1])
                    {
                        case "w":
                            w = w + value;
                            break;
                        case "x":
                            x = x +value;
                            break;
                        case "y":
                            y = y + value;
                            break;
                        case "z":
                            z = z + value;
                            break;
                        default: throw new Exception();
                    }
                    break;
                case "div":
                    if (value != 0)
                    {
                        switch (instructions[1])
                        {
                            case "w":
                                w = (int)Math.Floor((decimal)(w / value));
                                break;
                            case "x":
                                x = (int)Math.Floor((decimal)(x / value));
                                break;
                            case "y":
                                y = (int)Math.Floor((decimal)(y / value));
                                break;
                            case "z":
                                z = (int)Math.Floor((decimal)(z / value));
                                break;
                            default: throw new Exception();
                        }
                    }
                    break;
                case "mod":
                    if (value > 0)
                    {
                        switch (instructions[1])
                        {
                            case "w":
                                if (w >= 0)
                                {
                                    w = w % value;
                                }
                                break;
                            case "x":
                                if (x >= 0)
                                {
                                    x = x % value;
                                }
                                break;
                            case "y":
                                if (y >= 0)
                                {
                                    y = y % value;
                                }
                                break;
                            case "z":
                                if (z >= 0)
                                {
                                    z = z % value;
                                }
                                break;
                            default: throw new Exception();
                        }
                    }
                    break;
                case "eql":
                    switch (instructions[1])
                    {
                        case "w":
                            w = w == value ? 1 : 0;
                            break;
                        case "x":
                            x = x == value ? 1 : 0;
                            break;
                        case "y":
                            y = y == value ? 1 : 0;
                            break;
                        case "z":
                            z = z == value ? 1 : 0;
                            break;
                        default: throw new Exception();
                    }
                    break;
                default: throw new Exception();
            }

            count++;

            if (count == _puzzle.Length)
            {
                return (z, count);
            }

            line = _puzzle[count];
        } while (!line.StartsWith("inp"));

        return (z, count);
    }

    private string getZDivInstruction(int count)
    {
        for(int i = count; i < _puzzle.Length; i++)
        {
            var instruction = _puzzle[i];

            if(instruction.StartsWith("div z"))
            {
                return instruction;
            }
        }

        throw new Exception("no div instruction found");
    }
}
