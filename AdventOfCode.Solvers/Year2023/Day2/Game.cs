namespace AdventOfCode.Solvers.Year2023.Day2;

public record Game
{
    public int Id { get; init; }
    public int Blue { get; init; }
    public int Red { get; init; }
    public int Green { get; init; }
    public int Power { get; init; }

    public Game (string input)
    {
        //input example: "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
        var parts = input.Trim().Split(":");
        Id = int.Parse(parts[0].Split(" ")[1]);
        var cubeSets = parts[1].Split(";");
        foreach (var cubeSet in cubeSets)
        {
            var cubes = cubeSet.Split(",");
            foreach (var cube in cubes)
            {
                var cubeParts = cube.Trim().Split(" ");
                var color = cubeParts[1];
                var count = int.Parse(cubeParts[0]);
                switch (color)
                {
                    case "blue":
                        if (count > Blue)
                            Blue = count;
                        break;
                    case "red":
                        if (count > Red)
                            Red = count;
                        break;
                    case "green":
                        if (count > Green)
                            Green = count;
                        break;
                }
            }
        }

        Power = Blue * Red * Green;
    }
}
