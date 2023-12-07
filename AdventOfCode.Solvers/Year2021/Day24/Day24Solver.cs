namespace AdventOfCode.Solvers.Year2021.Day24;

public class Day24Solver : BaseSolver
{
    private string[] _puzzle;
    public override void Solve(string[] puzzle)
    {
        _puzzle = puzzle;

        var alu = new List<(bool, Func<int, int, int>)>();
        var countInBlock = 1;
        var aluInstruction = new Func<int, int, int>((w, z) => 0);
        bool isType1 = false;
        for (int i = 1; i < _puzzle.Length; i++)
        {
            if (_puzzle[i] == "inp w")
            {
                countInBlock = 0;
                alu.Add((isType1, aluInstruction));
            }
        
            if (countInBlock == 4)
            {
                isType1 = _puzzle[i] == "div z 1";
            }
        
            if (countInBlock == 5 && !isType1)
            {
                var v = int.Parse(_puzzle[i].Split(' ')[2]);
                aluInstruction = (w, z) =>
                    {
                        var result =  (z % 26) + v;
                        return result;
                    };
            }

            if (countInBlock == 15 && isType1)
            {
                var v = int.Parse(_puzzle[i].Split(' ')[2]);
                logger.OnNext(_puzzle[i]);
                aluInstruction = (w, z) => (26 * z) + w + v;
            }
        
            countInBlock++;
        }
        
        alu.Add((isType1, aluInstruction));
        
        var a = s(alu.ToArray(), 0);

        var b = s2(alu.ToArray(), 0);

        GiveAnswer1(a);
        GiveAnswer2(b);
    }

    private string s((bool type, Func<int,int,int> f)[] alu, int z)
    {
        var (type, f) = alu[0];
        if (type)
        {
            for (int w = 9; w > 0; w--)
            {
                var newz = f(w, z);

                if (alu.Length == 1)
                {
                    return newz == 0 ? $"{w}" : null;
                }

                var result = s(alu[1..], newz);

                if (result != null)
                {
                    return $"{w}{result}";
                }
            }

            return null;
        }

        var newW = f(0, z);
        var newZ = (int)Math.Floor((decimal)z / 26);
        if (alu.Length == 1)
        {
            return newZ == 0 && newW > 0 && newW <= 9 ? $"{newW}" : null;
        }

        if (newW > 9 || newW == 0)
            newZ *= 26;
        var res = s(alu[1..], newZ);
        return (res == null) ? null : $"{newW}{res}";
    }

    private string s2((bool type, Func<int,int,int> f)[] alu, int z)
    {
        var (type, f) = alu[0];
        if (type)
        {
            for (int w = 1; w <= 10; w++)
            {
                var newz = f(w, z);

                if (alu.Length == 1)
                {
                    return newz == 0 ? $"{w}" : null;
                }

                var result = s2(alu[1..], newz);

                if (result != null)
                {
                    return $"{w}{result}";
                }
            }

            return null;
        }

        var newW = f(0, z);
        var newZ = (int)Math.Floor((decimal)z / 26);
        if (alu.Length == 1)
        {
            return newZ == 0 && newW > 0 && newW <= 9 ? $"{newW}" : null;
        }

        if (newW > 9 || newW < 1)
            newZ *= 26;
        var res = s2(alu[1..], newZ);
        return (res == null) ? null : $"{newW}{res}";
    }
}
