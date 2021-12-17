using PuzzleConsole.Year2021;

namespace PuzzleConsole.test.Year2021;

public partial class Day13Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day13();

        scenario.Fact("split should return puzzle and folds", () =>
        {
            var input = new string[] {
                "6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
                "",
                "fold along y=7",
                "fold along x=5",
            };
            var puzzle = new string[] {
                "6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
            };

            var folds = new string[] {
                "fold along y=7",
                "fold along x=5",
            };
            var res = sut.splitPuzzle(input);
            res.paper.Should().BeEquivalentTo(puzzle);
            res.foldInstructions.Should().BeEquivalentTo(folds);
        });

        scenario.Fact("Puzzle should be able to be converted", () =>
        {
            var puzzle = new string[] {
                "6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
            };

            var paper = sut.ToPaper(puzzle);

            var res = sut.paperToString(paper);

            var expected = new string[]
            {
                "...#..#..#.",
                "....#......",
                "...........",
                "#..........",
                "...#....#.#",
                "...........",
                "...........",
                "...........",
                "...........",
                "...........",
                ".#....#.##.",
                "....#......",
                "......#...#",
                "#..........",
                "#.#........",

            };

            res.Should().BeEquivalentTo(expected);
        });

        scenario.Fact("DetermineFlder", () =>
        {
            var foldi = sut.DeterminFold("fold along y=7");
            foldi.isX.Should().BeFalse();
            foldi.line.Should().Be(7);
        });

        scenario.Fact("Puzzle should be able to be folded once", () =>
        {
            var puzzle = new string[] {
                "6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
            };

            var paper = sut.ToPaper(puzzle);

            paper = sut.fold(paper, "fold along y=7");

            var res = sut.paperToString(paper);

            var expected = new string[]
            {
                "#.##..#..#.",
                "#...#......",
                "......#...#",
                "#...#......",
                ".#.#..#.###",
                "...........",
                "...........",

            };

            res.Should().BeEquivalentTo(expected);

            paper = sut.fold(paper, "fold along x=5");

            expected = new string[]
            {
                "#####",
                "#...#",
                "#...#",
                "#...#",
                "#####",
                ".....",
                ".....",

            };

            res = sut.paperToString(paper);

            res.Should().BeEquivalentTo(expected);
        });

        scenario.Fact("SolveTest", () => { 
            var input = new string[] {
                "6,10",
                "0,14",
                "9,10",
                "0,3",
                "10,4",
                "4,11",
                "6,0",
                "6,12",
                "4,1",
                "0,13",
                "10,12",
                "3,4",
                "3,0",
                "8,4",
                "1,10",
                "2,14",
                "8,10",
                "9,0",
                "",
                "fold along y=7",
                "fold along x=5",
            };

            var sol = sut.Solve(input);

            sol.First().Should().Be("17");
        })
    }
}
