namespace AdventOfCode.Solvers.Year2024.Day11;

public class Day11Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        decimal blinks = 0;
        Dictionary<decimal, decimal> stones = new();

        foreach (var stone in puzzle[0].Split(" ").Select(decimal.Parse))
        {
            if (stones.ContainsKey(stone))
            {
                stones[stone]++;
            }
            else
            {
                stones[stone] = 1;
            }
        }

        while (blinks < 75)
        {
            var newStones = new Dictionary<decimal, decimal>();
            foreach (var stone in stones)
            {
                //If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
                if (stone.Key == 0)
                {
                    if (newStones.ContainsKey(1))
                    {
                        newStones[1] += stone.Value;
                    }
                    else
                    {
                        newStones[1] = stone.Value;
                    }
                }
                //If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
                else if (stone.Key.ToString().Length % 2 == 0)
                {
                    var lefstStone = decimal.Parse(stone.Key.ToString().Substring(0, stone.Key.ToString().Length / 2));
                    var rightStone = decimal.Parse(stone.Key.ToString().Substring(stone.Key.ToString().Length / 2));

                    if (newStones.ContainsKey(lefstStone))
                    {
                        newStones[lefstStone] += stone.Value;
                    }
                    else
                    {
                        newStones[lefstStone] = stone.Value;
                    }
                    if (newStones.ContainsKey(rightStone))
                    {
                        newStones[rightStone] += stone.Value;
                    }
                    else
                    {
                        newStones[rightStone] = stone.Value;
                    }
                }
                else
                {
                    if (newStones.ContainsKey(stone.Key * 2024))
                    {
                        newStones[stone.Key * 2024] += stone.Value;
                    }
                    else
                    {
                        newStones[stone.Key * 2024] = stone.Value;
                    }
                }
                //If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.

            }
            blinks++;
            stones = newStones;

            if (blinks == 25)
                GiveAnswer1(stones.Sum(s => s.Value));
        }

        GiveAnswer2(stones.Sum(s => s.Value));
    }
}
