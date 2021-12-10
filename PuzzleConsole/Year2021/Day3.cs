namespace PuzzleConsole.Year2021;

public class Day3 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        string gammaRate = "";
        string epsilonRate = "";

        for (int i = 0; i < puzzle[0].Length; i++)
        {
            int one = 0;
            int zero = 0;

            foreach(var line in puzzle)
            {
                if (line[i] == '1')
                {
                    one++;
                } 
                else
                {
                    zero++;
                }
            }

            gammaRate += one > zero ? "1" : "0";
            epsilonRate += one < zero ? "1" : "0";
        }

        var gamma = Convert.ToInt32(gammaRate, 2);
        var epsilon = Convert.ToInt32(epsilonRate, 2);

        var output = gamma * epsilon;

        var oxygenin = new List<string>(puzzle);

        var index = 0;
        do
        {

            int one = 0;
            int zero = 0;

            foreach (var line in oxygenin)
            {
                if (line[index] == '1')
                {
                    one++;
                }
                else
                {
                    zero++;
                }
            }

            if (one >= zero)
            {
                oxygenin.RemoveAll(s => s[index] == '0');
            }
            else
            {
                oxygenin.RemoveAll(s => s[index] == '1');
            }

            index++;
        }
        while (oxygenin.Count() > 1);

        var co2in = new List<string>(puzzle);
        index = 0;
        do
        {

            int one = 0;
            int zero = 0;

            foreach (var line in co2in)
            {
                if (line[index] == '1')
                {
                    one++;
                }
                else
                {
                    zero++;
                }
            }

            if (one < zero)
            {
                co2in.RemoveAll(s => s[index] == '0');
            }
            else
            {
                co2in.RemoveAll(s => s[index] == '1');
            }

            index++;
        }
        while (co2in.Count() > 1);

        var oxygen = Convert.ToInt32(oxygenin.First(), 2);
        var co2 = Convert.ToInt32(co2in.First(), 2);


        return new string[] { output.ToString(), $"{oxygen * co2}" };
    }
}
