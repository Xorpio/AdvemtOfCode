namespace PuzzleConsole.Year2022.Day19;

public class Day19 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
        var answer = puzzle.Select(l => new BluePrint(l))
            .Select(b => b.State.Id * b.GetMax("", 24, b.State).Item2)
            .Sum();

        var answer2 = puzzle.Select(l => new BluePrint(l))
            .Take(3)
            .Select(b => b.GetMax("", 32, b.State))
            .ToList();

        Console.WriteLine($"{answer2[0].Item1}");
        Console.WriteLine($"{answer2[1].Item1}");
        Console.WriteLine($"{answer2[2].Item1}");

        return new string[]
        {
            answer.ToString(),
            (answer2[0].Item2 * answer2[1].Item2 * answer2[2].Item2).ToString()
        };
    }
}

public class BluePrint
{
    public State State;
    public BluePrint(string input)
    {
        var words = input.Split(' ');
        var id = int.Parse(words[1][..^1]);
        var oreRobotCost = int.Parse(words[6]);
        var clayRobotCost = int.Parse(words[12]);
        var obsidianRobotCost = (int.Parse(words[18]), int.Parse(words[21]));
        var geodeRobotCost = (int.Parse(words[27]), int.Parse(words[30]));

        State = new State(id,
            0, 1, oreRobotCost,
            0, 0, clayRobotCost,
            0, 0, obsidianRobotCost,
            0, 0, geodeRobotCost
        );
    }

    public (string, int) GetMax(string choice, int timeLeft, State state)
    {
        var maxOre = new int[]
        {
            state.OreRobotCost,
            state.ClayRobotCost,
            state.ObsidianRobotCost.ore,
            state.GeodeRobotCost.ore
        }.Max();
        var maxClay = state.ObsidianRobotCost.clay;
        var maxObsidian = state.GeodeRobotCost.obsidian;

        if (choice == "nothing")
        {
            return (choice, state.Geode + timeLeft * state.GeodeRobot);
        }

        if (choice == "ore")
        {
            //time to ore
            var needed = state.Ore - state.OreRobotCost;
            if (needed >= 0)
            {
                //build and continue
                state = state with
                {
                    Ore = (state.Ore + state.OreRobot) - state.OreRobotCost,
                    Clay = state.Clay + state.ClayRobot,
                    Obsidian = state.Obsidian + state.ObsidianRobot,
                    Geode = state.Geode + state.GeodeRobot,
                    OreRobot = state.OreRobot + 1
                };
                timeLeft--;
            }
            else
            {
                int time = (int)Math.Ceiling((double)Math.Abs(needed) / state.OreRobot) +1;
                //build and continue
                state = state with
                {
                    Ore = state.Ore + (state.OreRobot * time) - state.OreRobotCost,
                    Clay = state.Clay + (state.ClayRobot * time),
                    Obsidian = state.Obsidian + (state.ObsidianRobot * time),
                    Geode = state.Geode + (state.GeodeRobot * time),
                    OreRobot = state.OreRobot + 1
                };
                timeLeft -= time;
            }
        }

        if (choice == "clay")
        {
            //time to ore
            var needed = state.Ore - state.ClayRobotCost;
            if (needed >= 0)
            {
                //build and continue
                state = state with
                {
                    Ore = (state.Ore + state.OreRobot) - state.ClayRobotCost,
                    Clay = state.Clay + state.ClayRobot,
                    Obsidian = state.Obsidian + state.ObsidianRobot,
                    Geode = state.Geode + state.GeodeRobot,
                    ClayRobot = state.ClayRobot + 1
                };
                timeLeft--;
            }
            else
            {
                int time = (int)Math.Ceiling((double)Math.Abs(needed) / state.OreRobot) +1;
                //build and continue
                state = state with
                {
                    Ore = state.Ore + (state.OreRobot * time) - state.ClayRobotCost,
                    Clay = state.Clay + (state.ClayRobot * time),
                    Obsidian = state.Obsidian + (state.ObsidianRobot * time),
                    Geode = state.Geode + (state.GeodeRobot * time),
                    ClayRobot = state.ClayRobot + 1
                };
                timeLeft -= time;
            }
        }

        if (choice == "obsidian")
        {
            //time to ore
            var needed = new int[]
            {
                state.Ore - state.ObsidianRobotCost.ore,
                state.Clay - state.ObsidianRobotCost.clay,
            }.Min();

            if (needed >= 0)
            {
                //build and continue
                state = state with
                {
                    Ore = (state.Ore + state.OreRobot) - state.ObsidianRobotCost.ore,
                    Clay = (state.Clay + state.ClayRobot) - state.ObsidianRobotCost.clay,
                    Obsidian = state.Obsidian + state.ObsidianRobot,
                    Geode = state.Geode + state.GeodeRobot,
                    ObsidianRobot = state.ObsidianRobot + 1
                };
                timeLeft--;
            }
            else
            {
                var time = new int[]
                {
                    (int)Math.Ceiling((double)Math.Abs(state.Ore - state.ObsidianRobotCost.ore) / state.OreRobot),
                    (int)Math.Ceiling((double)Math.Abs(state.Clay - state.ObsidianRobotCost.clay) / state.ClayRobot),
                }.Max() + 1;
                //build and continue
                state = state with
                {
                    Ore = state.Ore + (state.OreRobot * time) - state.ObsidianRobotCost.ore,
                    Clay = state.Clay + (state.ClayRobot * time) - state.ObsidianRobotCost.clay,
                    Obsidian = state.Obsidian + (state.ObsidianRobot * time),
                    Geode = state.Geode + (state.GeodeRobot * time),
                    ObsidianRobot = state.ObsidianRobot + 1
                };
                timeLeft -= time;
            }
        }

        if (choice == "geode")
        {
            //time to ore
            var needed = new int[]
            {
                state.Ore - state.GeodeRobotCost.ore,
                state.Obsidian - state.GeodeRobotCost.obsidian
            }.Min();

            if (needed >= 0)
            {
                //build and continue
                state = state with
                {
                    Ore = (state.Ore + state.OreRobot) - state.GeodeRobotCost.ore,
                    Clay = state.Clay + state.ClayRobot,
                    Obsidian = (state.Obsidian + state.ObsidianRobot) - state.GeodeRobotCost.obsidian,
                    Geode = state.Geode + state.GeodeRobot,
                    GeodeRobot = state.GeodeRobot + 1
                };
                timeLeft--;
            }
            else
            {
                var time = new int[]
                {
                    (int)Math.Ceiling((double)Math.Abs(state.Ore - state.GeodeRobotCost.ore) / state.OreRobot),
                    (int)Math.Ceiling((double)Math.Abs(state.Obsidian - state.GeodeRobotCost.obsidian) / state.ObsidianRobot),
                }.Max() + 1;
                //build and continue
                state = state with
                {
                    Ore = state.Ore + (state.OreRobot * time) - state.GeodeRobotCost.ore,
                    Clay = state.Clay + (state.ClayRobot * time),
                    Obsidian = state.Obsidian + (state.ObsidianRobot * time) - state.GeodeRobotCost.obsidian,
                    Geode = state.Geode + (state.GeodeRobot * time),
                    GeodeRobot = state.GeodeRobot + 1
                };
                timeLeft -= time;
            }
        }

        var _choicess = new List<string>()
        {
            "nothing",
            "ore",
            "clay",
        };

        if (state.ClayRobot > 0)
        {
            _choicess.Add("obsidian");
        }

        if (state.ObsidianRobot > 0)
        {
            _choicess.Add("geode");
        }

        if (state.OreRobot >= maxOre)
            _choicess.Remove("ore");

        if (state.ClayRobot >= maxClay)
            _choicess.Remove("clay");

        if (state.ObsidianRobot >= maxObsidian)
            _choicess.Remove("obsidian");

        if (timeLeft < 0)
            return ("", 0);

        var answers = new List<(string, int)>();
        foreach (var coic in _choicess)
        {
            answers.Add(GetMax(coic, timeLeft, state));
        }
        // var answers = _choicess
        //     .Select(c => GetMax(c, timeLeft, state));
        var a = answers.OrderByDescending(a => a.Item2)
            .First();
        return ($"({timeLeft}){choice}-{a.Item1}", a.Item2);
    }

    private List<string> appendAndClone(List<string> list, string append)
    {
        var newList = new List<string>(list);
        newList.Add(append);
        return newList;
    }

    private double fact(double n)
    {
       for (double i = n - 1; i > 0; i--)
       {
           n *= i;
       }

       return n;
    }

    public int GetMaxGeodes()
    {
        var states = new Dictionary<int,State>();
        states.Add(0, State);

        var choices = new string[]
        {
            "nothing",
            "ore",
            "clay",
            "obsidian",
            "geode"
        };

        // for (int minute = 26; minute > 0; minute--)
        // {
        //     foreach (var state in states)
        //     {
        //         // gather
        //         var newState = state.Value with
        //         {
        //             Ore = state.Value.Ore + state.Value.OreRobot,
        //             Clay = state.Value.Clay + state.Value.ClayRobot,
        //             Obsidian = state.Value.Obsidian + state.Value.ObsidianRobot,
        //             Geode = state.Value.Geode + state.Value.GeodeRobot
        //         };
        //
        //         states[state.Key] = newState;
        //
        //         if (newState.Ore >= newState.ClayRobotCost && newState.ClayRobot <= newState.ObsidianRobot)
        //         {
        //             states.Add(states.Count(), newState with
        //             {
        //                 Ore = newState.Ore - newState.ClayRobotCost,
        //                 ClayRobot = newState.ClayRobot + 1
        //             });
        //         }
        //
        //         //
        //         // if (Ore >= ObsidianRobotCost.ore && Clay >= ObsidianRobotCost.clay && ObsidianRobot <= GeodeRobot)
        //         // {
        //         //     Ore -= ObsidianRobotCost.ore;
        //         //     Clay -= ObsidianRobotCost.clay;
        //         //     ObsidianRobot++;
        //         // }
        //         //
        //         // if (Ore >= GeodeRobotCost.ore && Obsidian >= GeodeRobotCost.obsidian)
        //         // {
        //         //     Ore -= GeodeRobotCost.ore;
        //         //     Obsidian -= GeodeRobotCost.obsidian;
        //         //     GeodeRobot++;
        //         // }
        //     }
        // }

        return states.Select(s => s.Value.Geode).Max();
    }
}

public record State(
    int Id,

    int Ore,
    int OreRobot,
    int OreRobotCost,

    int Clay,
    int ClayRobot,
    int ClayRobotCost,

    int Obsidian,
    int ObsidianRobot,
    (int ore, int clay) ObsidianRobotCost,

    int Geode,
    int GeodeRobot,
    (int ore, int obsidian) GeodeRobotCost
);