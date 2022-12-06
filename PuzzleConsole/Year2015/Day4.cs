using System.Security.Cryptography;
using System.Text;

namespace PuzzleConsole.Year2015.Day4;

public class Day4 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var start = 0;

        string input = "ckczppom";
        string hash = "";

        do
        {
            start++;
            hash = md5($"{input}{start}");
            Console.WriteLine($"{start} - {hash}");
        } while (!hash.StartsWith("000000"));

        return new[] { start.ToString(), hash };
    }

   public string ToHex(byte[] bytes, bool upperCase)
   {
       StringBuilder result = new StringBuilder(bytes.Length*2);

       for (int i = 0; i < bytes.Length; i++)
           result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

       return result.ToString();
   }

   public string md5(string input)
   {
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        using (MD5 md5 = MD5.Create())
        {
            byte[] byteHashedPassword = md5.ComputeHash(bytes);
            return ToHex(byteHashedPassword, false);
        }
   }
}