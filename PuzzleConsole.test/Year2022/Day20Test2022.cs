using PuzzleConsole.Year2022.Day20;

namespace PuzzleConsole.test.Year2022;

[Trait("Year", "2022")]
[Trait("Day", "20")]
public partial class Day20Test2022 {
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day20();

        var sampleInput = """
        1
        2
        -3
        3
        -2
        0
        4
        """;
        var lines = sampleInput.Split("\n").Select(s => s.Trim()).ToArray();

        scenario.Fact("Sut should not be null", () =>
        {
            sut.Should().NotBeNull();
        });

        var answers = new string[]
        {
            "1, 2, 1, -3, 3, -2, 0, 4",
            "2, 1, -3, 2, 3, -2, 0, 4",
            "-3, 1, 2, 3, -2, -3, 0, 4",
            "3, 1, 2, -2, -3, 0, 3, 4",
            "-2, 1, 2, -3, 0, 3, 4, -2",
            "0, 1, 2, -3, 0, 3, 4, -2",
            "4, 1, 2, -3, 4, 0, 3, -2"
        };


        scenario.Fact("Small sample test", () =>
        {
            // sut.Solve(lines).Should().BeEquivalentTo(answers);
            sut.Solve(lines)[0].Should().Be("3");
        });

        scenario.Fact("Small sample test part 2", () =>
        {
            // sut.Solve(lines).Should().BeEquivalentTo(answers);
            sut.Solve(lines)[1].Should().Be("1623178306");
        });

        // var tests = new (string, string)[]
        // {
        //     ("1,0,0",  "0, 1, 0"),
        //     ("2,0,0",  "0, 0, 2"),
        //     ("3,0,0",  "3, 0, 0"),
        //
        //     ("0,1,0",  "0, 0, 1"),
        //     ("0,2,0",  "2, 0, 0"),
        //     ("0,3,0",  "0, 3, 0"),
        //
        //     ("-1,0,0",  "0, -1, 0"),
        //     ("-2,0,0",  "0, 0, 2"),
        //     ("-3,0,0",  "3, 0, 0"),
        //
        //     ("0,-1,0",  "-1, -2, 0"),
        //     ("0,-2,0",  "2, 0, 0"),
        //     ("0,-3,0",  "0, 3, 0"),
        // };
        //
        // foreach (var test in tests)
        // {
        //     scenario.Theory("Testing", test, () =>
        //     {
        //         sut.Solve(test.Item1.Split(','))[^1].Should().EndWith(test.Item2);
        //     });
        // }

        var puzzle = new string[]
        {
            "1",
            "2",
            "3",
            "4",
            "5"
        };
        var nodes = puzzle.Select(l => int.Parse(l))
            .Select(i => new Node()
            {
                NodeValue = i
            }).ToList();

        var listSize = puzzle.Length;

        for (var index = 0; index < nodes.Count; index++)
        {
            var n = nodes[index];
            n.Id = index.ToString();
            n.Left = nodes[index == 0 ? ^1 : index - 1];
            n.Right = nodes[(index + 1) % listSize];
        }


        scenario.Fact("test shift left", () =>
        {
            var node = nodes[2];

            var ll = node.Left.Left; // 1
            var l = node.Left; // 2
            var r = node.Right; //4
            var rr = node.Right.Right; // 5

            ll.Right = node;

            l.Left = node;
            l.Right = r;

            node.Left = ll;
            node.Right = l;

            r.Left = l;
            sut.Display(nodes).Should().Be("1, 3, 2, 4, 5");
        });

        scenario.Fact("test shift right", () =>
        {
            var node = nodes[2];

            var ll = node.Left.Left; // 1
            var l = node.Left; // 2
            var r = node.Right; //4
            var rr = node.Right.Right; // 5

            l.Right = r;

            node.Left = r;
            node.Right = rr;

            r.Left = l;
            r.Right = node;

            rr.Left = node;

            sut.Display(nodes).Should().Be("1, 2, 4, 3, 5");
        });

        scenario.Fact("test shift right more", () =>
        {
            var node = nodes[2];

            for(int i = 0; i < 4; i++)
            {
                var l = node.Left; // 2
                var r = node.Right; //4
                var rr = node.Right.Right; // 5

                l.Right = r;

                node.Left = r;
                node.Right = rr;

                r.Left = l;
                r.Right = node;

                rr.Left = node;
            }
            sut.Display(nodes).Should().Be("1, 2, 3, 4, 5");
        });


    }
}