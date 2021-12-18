using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PuzzleConsole.Year2021.Day14;

public class Day14 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var puzzleMap = new Dictionary<string, decimal>();

        var source = new Subject<string>();
        source.Buffer(2, 1)
            .Select(b => b[0] + b[1])
            .Subscribe(s =>
            {
                puzzleMap.TryAdd(s, 0);
                puzzleMap[s]++;
            });

        foreach (var c in puzzle[0])
        {
            source.OnNext(c.ToString());
        }


        for (var i = 0; i < 10; i++)
        { 
            var newPuzzle = new Dictionary<string, decimal>();

            foreach(var line in puzzle[2..])
            {
                var expand = line.Split(" -> ");

                var a = expand[0][0] + expand[1];
                var b = expand[1] + expand[0][1];

                newPuzzle.TryAdd(a, 0);
                newPuzzle.TryAdd(b, 0);

                decimal add = 0;
                puzzleMap.TryGetValue(expand[0], out add);

                newPuzzle[a] += add;
                newPuzzle[b] += add;
            }

            puzzleMap = newPuzzle;
        }

        var answerDict = new Dictionary<string, decimal>();
        foreach (var kv in puzzleMap)
        {
            answerDict.TryAdd(kv.Key[0].ToString(), 0);
            answerDict.TryAdd(kv.Key[1].ToString(), 0);
            answerDict[kv.Key[0].ToString()] += kv.Value;
            answerDict[kv.Key[1].ToString()] += kv.Value;
        }

        var max = Math.Ceiling(answerDict.MaxBy(kv => kv.Value).Value / 2);
        var min = Math.Ceiling(answerDict.MinBy(kv => kv.Value).Value / 2);

        for (var i = 0; i < 30; i++)
        { 
            var newPuzzle = new Dictionary<string, decimal>();

            foreach(var line in puzzle[2..])
            {
                var expand = line.Split(" -> ");

                var a = expand[0][0] + expand[1];
                var b = expand[1] + expand[0][1];

                newPuzzle.TryAdd(a, 0);
                newPuzzle.TryAdd(b, 0);

                decimal add = 0;
                puzzleMap.TryGetValue(expand[0], out add);

                newPuzzle[a] += add;
                newPuzzle[b] += add;
            }

            puzzleMap = newPuzzle;
        }

        answerDict = new Dictionary<string, decimal>();
        foreach (var kv in puzzleMap)
        {
            answerDict.TryAdd(kv.Key[0].ToString(), 0);
            answerDict.TryAdd(kv.Key[1].ToString(), 0);
            answerDict[kv.Key[0].ToString()] += kv.Value;
            answerDict[kv.Key[1].ToString()] += kv.Value;
        }

        var max2 = Math.Ceiling(answerDict.MaxBy(kv => kv.Value).Value / 2);
        var min2 = Math.Ceiling(answerDict.MinBy(kv => kv.Value).Value / 2);


        return new string[] { $"{max - min}", $"{max2 - min2}" };
    }
}
