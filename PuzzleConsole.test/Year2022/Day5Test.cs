using PuzzleConsole.Year2022.Day5;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "5")]
public partial class Day5Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day5();

        var sampleInput = """
            [D]
        [N] [C]
        [Z] [M] [P]
         1   2   3

        move 1 from 2 to 1
        move 3 from 1 to 3
        move 2 from 2 to 1
        move 1 from 1 to 2
        """;
        var lines = sampleInput.Split("\n");

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Splitten van puzzle en start [ stacks ]", () =>
        {
            var res = sut.SplitInput(lines);

            res.Stacks.Should().HaveCount(3);

            var crates = new List<Crate>()
            {
                new Crate("N"),
                new Crate("Z"),
            };

            res.Stacks[0].Should().BeEquivalentTo(crates);
        });

        scenario.Fact("Splitten van puzzle en start [ instructions ]", () =>
        {
            var res = sut.SplitInput(lines);

            var ex = new List<Instruction>()
            {
                new Instruction(1, 2, 1),
                new Instruction(3, 1, 3),
                new Instruction(2, 2, 1),
                new Instruction(1, 1, 2),
            };

            res.Instructions.Should().HaveCount(4);
            res.Instructions.Should().BeEquivalentTo(ex);
        });

        scenario.Fact("Solve part 1", () =>
        {
            sut.Solve(lines)[0].Should().Be("CMZ");
        });

        scenario.Fact("Solve part 2", () =>
        {
            sut.Solve(lines)[1].Should().Be("MCD");
        });
    }
}