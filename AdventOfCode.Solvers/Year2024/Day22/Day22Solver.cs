using System.Formats.Asn1;
using System.Security.Cryptography;

namespace AdventOfCode.Solvers.Year2024.Day22;

public class Day22Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var answers = puzzle.Select(decimal.Parse).Select(x => calcSecret(x, 2000));

        GiveAnswer1(answers.Select(a => a.Last()).Sum());

        var sequences = new Dictionary<(int, int, int, int), int>();

        foreach (var answer in answers)
        {
            var ints = answer.Select(n => int.Parse(n.ToString()[^1].ToString())).ToArray();
            var seen = new HashSet<(int, int, int, int)>();
            for (int i = 1; i < 1997; i++)
            {
                int x1 = ints[i] - ints[i - 1];
                int x2 = ints[i + 1] - ints[i];
                int x3 = ints[i + 2] - ints[i + 1];
                int x4 = ints[i + 3] - ints[i + 2];


                if (seen.Contains((x1, x2, x3, x4)))
                {
                    continue;
                }

                if (!sequences.ContainsKey((x1, x2, x3, x4)))
                {
                    sequences.Add((x1, x2, x3, x4), 0);
                }
                sequences[(x1, x2, x3, x4)] += ints[i + 3];

                seen.Add((x1, x2, x3, x4));
            }
        }

        GiveAnswer2(sequences.OrderByDescending(x => x.Value).First().Value);
    }

    public IList<decimal> calcSecret(decimal secret, int iterations)
    {
        var res = new List<decimal>();
        for (int i = 0; i < iterations; i++)
        {
            var r = secret * 64;
            secret = XorDecimals(secret, r) % 16777216M;

            r = Math.Floor(secret / 32);
            secret = XorDecimals(secret, r) % 16777216M;

            r = secret * 2048;
            secret = XorDecimals(secret, r) % 16777216M;

            res.Add(secret);
        }

        return res;
    }
    public static decimal XorDecimals(decimal a, decimal b)
    {
        // Convert decimals to byte arrays
        byte[] bytesA = decimal.GetBits(a).SelectMany(BitConverter.GetBytes).ToArray();
        byte[] bytesB = decimal.GetBits(b).SelectMany(BitConverter.GetBytes).ToArray();

        // Perform XOR operation on byte arrays
        byte[] resultBytes = new byte[bytesA.Length];
        for (int i = 0; i < bytesA.Length; i++)
        {
            resultBytes[i] = (byte)(bytesA[i] ^ bytesB[i]);
        }

        // Convert result byte array back to decimal
        int[] resultBits = new int[resultBytes.Length / 4];
        for (int i = 0; i < resultBits.Length; i++)
        {
            resultBits[i] = BitConverter.ToInt32(resultBytes, i * 4);
        }

        return new decimal(resultBits);
    }
}
