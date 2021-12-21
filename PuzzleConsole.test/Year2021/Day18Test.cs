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
            "[[[[5,4],6],2],1]",
            "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]",

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
            new("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]", "[[3,[2,[8,0]]],[9,[5,[7,0]]]]"),
        };
        foreach (var example in examples)
        {
            scenario.Theory("Test reduce", example, () =>
             {
                 var num = SnailfishNumber.FromString(example.input);

                 num.ToString().Should().Be(example.answer);
             });
        }

        examples = new (string input, string answer)[]
        {
            new("[10,3]", "[[5,5],3]"),
            new("[1,13]", "[1,[6,7]]"),
        };
        foreach (var example in examples)
        {
            scenario.Theory("Test split", example, () =>
             {
                 var num = SnailfishNumber.FromString(example.input);

                 num.ToString().Should().Be(example.answer);
             });
        }

        scenario.Fact("Add 2 number with explode and split", () => {
            var a = SnailfishNumber.FromString("[[[[4,3],4],4],[7,[[8,4],9]]]");
            var b = SnailfishNumber.FromString("[1,1]");

            var c = a + b;

            c.ToString().Should().Be("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]");
        });

        var magnitudeExamples = new (string input, int answer)[]
        {
            new("[9,1]", 29),
            new("[1,9]", 21),
            new("[[9,1],[1,9]]", 129)
        };
        foreach (var example in magnitudeExamples)
        {
            scenario.Theory("Test magnitude", example, () =>
            {
                var num = SnailfishNumber.FromString(example.input);

                num.Magnitude.Should().Be(example.answer);
            });
        }

        scenario.Fact("Solve simple", () =>
        {
            var puzzle = new string[]
            {
                "[1,1]",
                "[2,2]",
                "[3,3]",
                "[4,4]",
                "[5,5]",
                "[6,6]",

            };
            var solution = sut.Solve(puzzle);

            solution.Last().Should().Be("[[[[5,0],[7,4]],[5,5]],[6,6]]");

        });

        scenario.Fact("Solve COmplex add", () => {
            var a = SnailfishNumber.FromString("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]");
            var b = SnailfishNumber.FromString("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");

            a.ToString().Should().Be("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]");
            b.ToString().Should().Be("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");

            var c = a + b;

            c.Should().Be("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");
        });

        scenario.Fact("Solve", () =>
        {
            var puzzle = new string[]
            {
                "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
                "[[[5,[2,8]],4],[5,[[9,9],0]]]",
                "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
                "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
                "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
                "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
                "[[[[5,4],[7,7]],8],[[8,3],8]]",
                "[[9,3],[[9,9],[6,[4,9]]]]",
                "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
                "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]",

            };
            var solution = sut.Solve(puzzle);

            //solution.First().Should().Be("4140");
            //solution.Last().Should().Be("[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]]");

        });

    }
}
