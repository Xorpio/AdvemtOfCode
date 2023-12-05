using PuzzleConsole.Year2021.Day24;

namespace PuzzleConsole.test.Year2021;

[Trait("Year", "2021")]
[Trait("Day", "24")]
public partial class Day24Test {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day24();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("Negate program", () =>
        {
            var program = new string[]
            {
                "inp x",
                "mul x -1",
            };

            var alu = new Alu(program);

            var input = new int[] { 4 };

            var expected = -4;

            var output = alu.Run(input);

            output["x"].Should().Be(expected);
        });

        scenario.Fact("To many inputs should crash", () =>
        {
            var program = new string[]
            {
                "inp x",
                "inp y",
                "mul x -1",
            };

            var alu = new Alu(program);

            var input = new int[] { 4 };

            alu.Invoking(a => a.Run(input)).Should().Throw<IndexOutOfRangeException>();

        });

        var inputs = new (int a, int b, double expected)[]
        {
            new (1,3,1),
            new (3,9,1),
            new (1,2,0),
        };
        foreach (var inp in inputs)
        {
            scenario.Theory("Check if greater then 3", inp, () =>
            {
                var program = new string[]
                {
                    "inp z",
                    "inp x",
                    "mul z 3",
                    "eql z x",

                };

                var alu = new Alu(program);

                var input = new int[] { inp.a, inp.b };

                var output = alu.Run(input);

                output["z"].Should().Be(inp.expected);
            });
        }

        inputs = new (int a, int b, double expected)[]
        {
            new (1,3,4),
            new (3,9,12),
            new (1,2,3),
        };
        foreach (var inp in inputs)
        {
            scenario.Theory("Check if add works", inp, () =>
            {
                var program = new string[]
                {
                    "inp y",
                    "inp x",
                    "add y x",

                };

                var alu = new Alu(program);

                var input = new int[] { inp.a, inp.b };

                var output = alu.Run(input);

                output["y"].Should().Be(inp.expected);
            });
        }

        inputs = new (int a, int b, double expected)[]
        {
            new (9,3,3),
        };
        foreach (var inp in inputs)
        {
            scenario.Theory("Check if div works", inp, () =>
            {
                var program = new string[]
                {
                    "inp w",
                    "inp x",
                    "div w x",

                };

                var alu = new Alu(program);

                var input = new int[] { inp.a, inp.b };

                var output = alu.Run(input);

                output["w"].Should().Be(inp.expected);
            });
        }

        inputs = new (int a, int b, double expected)[]
        {
            new (9,3,0),
            new (8,3,2),
        };
        foreach (var inp in inputs)
        {
            scenario.Theory("Check if mod works", inp, () =>
            {
                var program = new string[]
                {
                    "inp w",
                    "inp x",
                    "mod w x",

                };

                var alu = new Alu(program);

                var input = new int[] { inp.a, inp.b };

                var output = alu.Run(input);

                output["w"].Should().Be(inp.expected);
            });
        }

        scenario.Fact("Puzzle should test while z != 0", () =>
        {
            var puzzle = new string[]
            {
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp w",
                "inp z",
                "mod z 6",

            };

            var solution = sut.Solve(puzzle);

            solution.First().Should().Be("99999999999996");
        });

        scenario.Fact("if div 0 then pass", () =>
        {
            var program = new string[]
            {
                "inp w",
                "div w 0",

            };

            var alu = new Alu(program);

            var input = new int[] { 1 };

            var output = alu.Run(input);

            output["w"].Should().Be(1);
        });

        scenario.Fact("if mod 0 then pass", () =>
        {
            var program = new string[]
            {
                "inp w",
                "inp x",
                "mod w 2",
                "mod x 0",

            };

            var alu = new Alu(program);

            var input = new int[] { -1,2 };

            var output = alu.Run(input);

            output["w"].Should().Be(-1);
            output["x"].Should().Be(2);
        });
    }
}
