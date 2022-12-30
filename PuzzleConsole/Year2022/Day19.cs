namespace PuzzleConsole.Year2022.Day19;

public class Day19 : ISolver
{
    public string[] Solve(string[] puzzle)
    {
	    return puzzle;
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

    public int GetMax(State state)
    {
        var seen = new Dictionary<State, int>();

        var choices = new List<(string choice, int minutes, State s, List<string> choiceMade)>();
        choices.Add(("", 0, state, new ()));

        var maxOre = new int[]
        {
            state.OreRobotCost,
            state.ClayRobotCost,
            state.ObsidianRobotCost.ore,
            state.GeodeRobotCost.ore
        }.Max();
        var maxClay = state.ObsidianRobotCost.clay;
        var maxObsidian = state.GeodeRobotCost.obsidian;

        var answer = 0;

        var maxTime = 24;

        (string choice, int minutes, State s, List<string> choiceMade) c;
        (string choice, int minutes, State s, List<string> choiceMade) best;
        while (choices.Any())
        {
            c = choices.First();
            choices.Remove(c);

            if (seen.ContainsKey(c.s) && c.minutes > seen[c.s])
            {
                continue;
            }


            if (!seen.ContainsKey(c.s))
            {
                seen.Add(c.s, c.minutes);
            }
            else if(c.minutes < seen[c.s])
            {
                seen[c.s] = c.minutes;
            }

            if (c.choice == "nothing")
            {
                var a = c.s.Geode + (maxTime - c.minutes) * c.s.GeodeRobot;
                if (answer < a)
                {
                    answer = a;
                    best = c;
                }

                continue;
            }

            if (c.choice == "ore")
            {
                //time to ore
                var needed = c.s.Ore - c.s.OreRobotCost;
                if (needed > 0)
                {
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = (c.s.Ore + c.s.OreRobot) - c.s.OreRobotCost,
                        Clay = c.s.Clay + c.s.ClayRobot,
                        Obsidian = c.s.Obsidian + c.s.ObsidianRobot,
                        Geode = c.s.Geode + c.s.GeodeRobot,
                        OreRobot = c.s.OreRobot + 1
                    };
                    c.minutes++;
                }
                else
                {
                    var time = (needed * -1) + 1;

                    //build and continue
                    c.s = c.s with
                    {
                        Ore = c.s.Ore + (c.s.OreRobot * time) - c.s.OreRobotCost,
                        Clay = c.s.Clay + (c.s.ClayRobot * time),
                        Obsidian = c.s.Obsidian + (c.s.ObsidianRobot * time),
                        Geode = c.s.Geode + (c.s.GeodeRobot * time),
                        OreRobot = c.s.OreRobot + 1
                    };
                    c.minutes += time;
                }
            }

            if (c.choice == "clay")
            {
                //time to ore
                var needed = c.s.Ore - c.s.ClayRobotCost;
                if (needed > 0)
                {
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = (c.s.Ore + c.s.OreRobot) - c.s.ClayRobotCost,
                        Clay = c.s.Clay + c.s.ClayRobot,
                        Obsidian = c.s.Obsidian + c.s.ObsidianRobot,
                        Geode = c.s.Geode + c.s.GeodeRobot,
                        ClayRobot = c.s.ClayRobot + 1
                    };
                    c.minutes++;
                }
                else
                {
                    var time = (needed * -1) + 1;
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = c.s.Ore + (c.s.OreRobot * time) - c.s.ClayRobotCost,
                        Clay = c.s.Clay + (c.s.ClayRobot * time),
                        Obsidian = c.s.Obsidian + (c.s.ObsidianRobot * time),
                        Geode = c.s.Geode + (c.s.GeodeRobot * time),
                        ClayRobot = c.s.ClayRobot + 1
                    };
                    c.minutes += time;
                }
            }

            if (c.choice == "obsidian")
            {
                //time to ore
                var needed = new int[]
                {
                    c.s.Ore - c.s.ObsidianRobotCost.ore,
                    c.s.Clay - c.s.ObsidianRobotCost.clay,
                }.Min();

                if (needed > 0)
                {
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = (c.s.Ore + c.s.OreRobot) - c.s.ObsidianRobotCost.ore,
                        Clay = (c.s.Clay + c.s.ClayRobot) - c.s.ObsidianRobotCost.clay,
                        Obsidian = c.s.Obsidian + c.s.ObsidianRobot,
                        Geode = c.s.Geode + c.s.GeodeRobot,
                        ObsidianRobot = c.s.ObsidianRobot + 1
                    };
                    c.minutes++;
                }
                else
                {
                    var time = (needed * -1) + 1;
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = c.s.Ore + (c.s.OreRobot * time) - c.s.ObsidianRobotCost.ore,
                        Clay = c.s.Clay + (c.s.ClayRobot * time) - c.s.ObsidianRobotCost.clay,
                        Obsidian = c.s.Obsidian + (c.s.ObsidianRobot * time),
                        Geode = c.s.Geode + (c.s.GeodeRobot * time),
                        ObsidianRobot = c.s.ObsidianRobot + 1
                    };
                    c.minutes += time;
                }
            }

            if (c.choice == "geode")
            {
                //time to ore
                var needed = new int[]
                {
                    c.s.Ore - c.s.GeodeRobotCost.ore,
                    c.s.Obsidian - c.s.GeodeRobotCost.obsidian
                }.Min();

                if (needed > 0)
                {
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = (c.s.Ore + c.s.OreRobot) - c.s.GeodeRobotCost.ore,
                        Clay = c.s.Clay + c.s.ClayRobot,
                        Obsidian = (c.s.Obsidian + c.s.ObsidianRobot) - c.s.GeodeRobotCost.obsidian,
                        Geode = c.s.Geode + c.s.GeodeRobot,
                        GeodeRobot = c.s.GeodeRobot + 1
                    };
                    c.minutes++;
                }
                else
                {
                    var time = (needed * -1) + 1;
                    //build and continue
                    c.s = c.s with
                    {
                        Ore = c.s.Ore + (c.s.OreRobot * time) - c.s.GeodeRobotCost.ore,
                        Clay = c.s.Clay + (c.s.ClayRobot * time),
                        Obsidian = c.s.Obsidian + (c.s.ObsidianRobot * time) - c.s.GeodeRobotCost.obsidian,
                        Geode = c.s.Geode + (c.s.GeodeRobot * time),
                        GeodeRobot = c.s.GeodeRobot + 1
                    };
                    c.minutes += time;
                }
            }

            var _choicess = new List<string>()
            {
                "nothing",
                "ore",
                "clay",
            };

            if (c.s.ClayRobot > 0)
            {
                _choicess.Add("obsidian");
            }

            if (c.s.Obsidian > 0)
            {
                _choicess.Add("geode");
            }

            if (c.s.OreRobot >= maxOre)
                _choicess.Remove("ore");

            if (c.s.ClayRobot >= maxClay)
                _choicess.Remove("clay");

            if (c.s.ObsidianRobot >= maxObsidian)
                _choicess.Remove("obsidian");

            if (c.minutes > maxTime)
                continue;

            choices.RemoveAll(_c => _c.s == c.s && _c.minutes >= c.minutes);

            choices.AddRange(
                _choicess.Select(_c => (_c, c.minutes, c.s, appendAndClone(c.choiceMade, $"{maxTime - c.minutes}: {_c}")))
            );
            choices.RemoveAll(_c =>
                (_c.s.Geode + (_c.s.GeodeRobot * _c.minutes) + fact(maxTime - _c.minutes)) < answer);
        }

        return answer;

        //
        // var answers = choices
        //     .Select(c => GetMax(c, minutesLeft, s));
        // return answers
        //     .Max();
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