namespace AdventOfCode.Solvers.Year2024.Day22;

public class Day22Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        GiveAnswer1(puzzle.Select(decimal.Parse).Select(x => calcSecret(x, 2000)).Sum());
        GiveAnswer2("");
    }

    public decimal calcSecret(decimal secret, int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            var r = secret * 64;
            secret = XorDecimals(secret, r) % 16777216M;

            r = Math.Floor(secret / 32);
            secret = XorDecimals(secret, r) % 16777216M;

            r = secret * 2048;
            secret = XorDecimals(secret, r) % 16777216M;
        }

        return secret;
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
