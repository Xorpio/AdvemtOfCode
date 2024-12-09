namespace AdventOfCode.Solvers.Year2024.Day9;

public class Day9Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        bool isFile = true;
        var id = 0;
        var list = new List<int?>();
        foreach (int n in puzzle[0].Select(c => int.Parse(c.ToString())))
        {
            if (isFile)
            {
                for (int i = 0; i < n; i++)
                {
                    list.Add(id);
                }

                id++;
            }
            else
            {
                for (int i = 0; i < n; i++)
                    list.Add(null);
            }

            isFile = !isFile;
        }

        int index = 0;
        decimal total = 0;
        //remote . on end?
        while (true)
        {
            if (list[index] == null)
            {
                id = list.Last(i => i != null).Value;
                var indexOf = list.LastIndexOf(id);

                if (indexOf < index)
                {
                    break;
                }
                list[indexOf] = null;
            }
            else
            {
                id = list[index].Value;
            }

            total += index * id;
            index++;

            if (index >= list.Count)
                break;
        }

        GiveAnswer1(total);
        GiveAnswer2("");
    }
}
