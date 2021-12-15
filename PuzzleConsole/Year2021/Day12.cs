using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PuzzleConsole.Year2021;

public class Day12 : ISolver
{
    private Dictionary<string, (bool IsBig, List<string> Paths)> _system;

    public bool Debug { get; set; } = false;

    public string[] Solve(string[] puzzle)
	{
        _system = new Dictionary<string, (bool IsBig, List<string> Paths)>();

        foreach (var line in puzzle)
        {
            var caves = line.Split('-');

            addToSystem(caves[0], caves[1]);
            addToSystem(caves[1], caves[0]);
        }



        var paths = findPath("start", Array.Empty<string>())
            .Select(list => string.Join(",", list)).ToArray();

        return Debug ? paths : new string[] { paths.Count().ToString() };
	}

    private List<List<string>>? findPath(string cave, string[] visited)
    {
        var rBase = new List<string>() { cave };
        var v = visited.ToList();
        v.Add(cave);

        if (cave == "end")
        {
            return new List<List<string>>() { rBase };
        }

        if (_system[cave].Paths.All(p => visited.Contains(p) && !_system[p].IsBig))
        {
            return new List<List<string>>() { };
        }

        var response = new List<List<string>>() { };  
        foreach (var path in _system[cave].Paths)
        {
            if (_system[path].IsBig || !visited.Contains(path))
            {
                var responses = findPath(path, v.ToArray());
                foreach (var r in responses)
                {
                    response.Add(rBase.Concat(r).ToList());
                }
            }
        }

        return response;
    }

    private void addToSystem(string cave, string path)
    {
        if (!_system.ContainsKey(cave))
        {
            _system.Add(cave, new (isUpper(cave), new List<string>()));
        }

        _system[cave].Paths.Add(path);
    }

    private bool isUpper(string input) => input == input.ToUpper();
}
