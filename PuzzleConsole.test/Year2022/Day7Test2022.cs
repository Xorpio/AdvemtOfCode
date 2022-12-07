using PuzzleConsole.Year2022.Day7;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "7")]
public partial class Day7Test2022
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day7();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var sampleInput = """
        $ cd /
        $ ls
        dir a
        14848514 b.txt
        8504156 c.dat
        dir d
        $ cd a
        $ ls
        dir e
        29116 f
        2557 g
        62596 h.lst
        $ cd e
        $ ls
        584 i
        $ cd ..
        $ cd ..
        $ cd d
        $ ls
        4060174 j
        8033020 d.log
        5626152 d.ext
        7214296 k
        """;
        var lines = sampleInput.Split("\n");

        scenario.Fact("Solve should have result part 1", () =>
        {
            sut.Solve(lines)[0].Should().Be("95437");
        });

        scenario.Fact("Solve should have result part 2", () =>
        {
            sut.Solve(lines)[1].Should().Be("24933642");
        });
    }
}