using PuzzleConsole.Year2022.Day13;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "13")]
public partial class Day13Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day13();

        var sampleInput = """
        [1,1,3,1,1]
        [1,1,5,1,1]

        [[1],[2,3,4]]
        [[1],4]

        [9]
        [[8,7,6]]

        [[4,4],4,4]
        [[4,4],4,4,4]

        [7,7,7,7]
        [7,7,7]

        []
        [3]

        [[[]]]
        [[]]

        [1,[2,[3,[4,[5,6,7]]]],8,9]
        [1,[2,[3,[4,[5,6,0]]]],8,9]
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var sampleInputs = new (string, int)[] {
            ("""
            [1,1,3,1,1]
            [1,1,5,1,1]
            """
            ,1),( """
            [[1],[2,3,4]]
            [[1],4]
            """
            ,1), ("""
            [9]
            [[8,7,6]]
            """
            ,0), ("""
            [[4,4],4,4]
            [[4,4],4,4,4]
            """
            ,1),("""
            [7,7,7,7]
            [7,7,7]
            """
            ,0),("""
            []
            [3]
            """
            ,1),("""
            [[[]]]
            [[]]
            """
            ,0),("""
            [1,[2,[3,[4,[5,6,7]]]],8,9]
            [1,[2,[3,[4,[5,6,0]]]],8,9]
            """
            ,0)
        };

        foreach (var i in sampleInputs)
        {
            scenario.Theory("Sample moet slage", i, () =>
            {
                var l = i.Item1.Split("\n").Select(s => s.Trim()).ToArray();
                int.Parse(sut.Solve(l)[0]).Should().Be(i.Item2);
            });
        }

        scenario.Fact("Small sample test", () =>
        {
            int.Parse(sut.Solve(lines)[0]).Should().Be(13);
        });

        scenario.Fact("Small sample test part 2", () =>
        {
            int.Parse(sut.Solve(lines)[1]).Should().Be(140);
        });
    }
}