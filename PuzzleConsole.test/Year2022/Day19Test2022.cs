using PuzzleConsole.Year2022.Day19;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "19")]
public partial class Day19Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day19();

        var sampleInput = """
        Blueprint 1: Each ore robot costs 4 ore. Each clay robot costs 2 ore. Each obsidian robot costs 3 ore and 14 clay. Each geode robot costs 2 ore and 7 obsidian.
        Blueprint 2: Each ore robot costs 2 ore. Each clay robot costs 3 ore. Each obsidian robot costs 3 ore and 8 clay. Each geode robot costs 3 ore and 12 obsidian.
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var print1 = new BluePrint(lines[0]);
        var print2 = new BluePrint(lines[1]);
        scenario.Skip("To slow");

        scenario.Fact("Test blueprint 1", () =>
        {
            print1.State.Id.Should().Be(1);
            print1.State.OreRobotCost.Should().Be(4);
            print1.State.ClayRobotCost.Should().Be(2);
            print1.State.ObsidianRobotCost.Should().Be((3, 14));
            print1.State.GeodeRobotCost.Should().Be((2, 7));
            print1.GetMax("", 24, print1.State).Item2.Should().Be(9);
            print1.GetMax("", 32, print1.State).Item2.Should().Be(56);
        });

        scenario.Fact("Test blueprint 2", () =>
        {
            print2.GetMax("", 24, print2.State).Item2.Should().Be(12);
            print2.GetMax("", 32, print2.State).Item2.Should().Be(62);
        });

        scenario.Fact("Small sample test", () =>
        {
            sut.Solve(lines)[0].Should().Be("33");
        });


    }
}