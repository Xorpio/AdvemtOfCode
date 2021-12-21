using PuzzleConsole.Year2021.Day18;
using Spectre.Console;

namespace PuzzleConsole.test.Year2021;

public partial class Day18Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day18();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var inputs = new string[]
        {
            "[1,2]",
            "[[1,2],3]",
            "[9,[8,7]]",
            "[[1,9],[8,5]]",
            "[[[[1,2],[3,4]],[[5,6],[7,8]]],9]",
            "[[[9,[3,8]],[[0,9],6]],[[[3,7],[4,9]],3]]",
            "[[[[1,3],[5,3]],[[1,3],[8,7]]],[[[4,9],[6,9]],[[8,2],[7,3]]]]",
        };
        foreach (var input in inputs)
        {
            scenario.Theory("Snailfish shoul be created from value", input, () =>
            {
                var num = SnailfishNumber.FromString(input);

                num.ToString().Should().Be(input);
            });
        }

        scenario.Fact("add 2 numbers", () =>
        {
            var a = SnailfishNumber.FromString("[1,2]");
            var b = SnailfishNumber.FromString("[[3,4],5]");

            var c = a + b;
            c.ToString().Should().Be("[[1,2],[[3,4],5]]");
        });

        var examples = new (string input, string answer)[]
        {
            new("[[[[[9,8],1],2],3],4]", "[[[[0,9],2],3],4]"),
            new("[7,[6,[5,[4,[3,2]]]]]", "[7,[6,[5,[7,0]]]]"),
            new("[[6,[5,[4,[3,2]]]],1]", "[[6,[5,[7,0]]],3]"),
            new("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]."),
        };
        foreach (var example in examples)
        {
            scenario.Theory("Test reduce",example, () =>
            {
                var num = SnailfishNumber.FromString(example.input);

                num.ToString().Should().Be(example.answer);
            });
        }
    }
}
