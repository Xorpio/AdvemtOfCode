using PuzzleConsole.Year2022.Day16;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "16")]
public partial class Day16Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day16();

        var sampleInput = """
        Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
        Valve BB has flow rate=13; tunnels lead to valves CC, AA
        Valve CC has flow rate=2; tunnels lead to valves DD, BB
        Valve DD has flow rate=20; tunnels lead to valves CC, AA, EE
        Valve EE has flow rate=3; tunnels lead to valves FF, DD
        Valve FF has flow rate=0; tunnels lead to valves EE, GG
        Valve GG has flow rate=0; tunnels lead to valves FF, HH
        Valve HH has flow rate=22; tunnel leads to valve GG
        Valve II has flow rate=0; tunnels lead to valves AA, JJ
        Valve JJ has flow rate=21; tunnel leads to valve II
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Small sample test", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(1651);
        });

        scenario.Fact("Small sample test part 2", () =>
        {
            int.Parse(sut.Solve(lines)[1]).Should().Be(1707);
        });
    }
}