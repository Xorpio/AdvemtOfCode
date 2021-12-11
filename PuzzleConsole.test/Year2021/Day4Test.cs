using PuzzleConsole.Year2021;
using Xunit;

namespace PuzzleConsole.test.Year2021;

public class Day4Test
{
    [Fact]
    public void SolveTest()
    {
        var sut = new Day4();

        var puzzle = new string[]
        {
            "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
            "",
            "22 13 17 11  0",
            " 8  2 23  4 24",
            "21  9 14 16  7",
            " 6 10  3 18  5",
            " 1 12 20 15 19",
            "",
            " 3 15  0  2 22",
            " 9 18 13 17  5",
            "19  8  7 25 23",
            "20 11 10 24  4",
            "14 21 16 12  6",
            "",
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };

        var solution = sut.Solve(puzzle);

        solution.Should().HaveCount(2);
        solution.First().Should().Be("4512");
        solution.Last().Should().Be("1924");
    }

    [Fact]
    public void TestBoard()
    {
        var input = new string[]{
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };
        var board = new Board(input);

        board.Should().NotBeNull();

        board.Cols.Count().Should().Be(5);
        board.Cols.First().Count().Should().Be(5);
        board.Cols.First().First().Should().Be("14");
        board.Cols.First().Last().Should().Be("2");
        board.Rows.Count().Should().Be(5);
        board.Rows.First().Count().Should().Be(5);
        board.Rows.First().Last().Should().Be("4");
        board.Rows.Last().Last().Should().Be("7");

        board.Solved().Should().BeFalse();
    }

    [Fact]
    public void TestBoardSolveRow()
    {
        var input = new string[]{
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };
        var board = new Board(input);

        board.Should().NotBeNull();

        board.Call("14");
        board.Rows.First().Count().Should().Be(4);
        board.Solved().Should().BeFalse();
        board.Call("24");
        board.Rows.First().Count().Should().Be(3);
        board.Solved().Should().BeFalse();
        board.Call("17");
        board.Rows.First().Count().Should().Be(2);
        board.Solved().Should().BeFalse();
        board.Call("21");
        board.Rows.First().Count().Should().Be(1);
        board.Solved().Should().BeFalse();
        board.Call("4");
        board.Rows.First().Count().Should().Be(0);
        board.Solved().Should().BeTrue();
    }

    [Fact]
    public void TestBoardSolveCol()
    {
        var input = new string[]{
            "14 21 17 24  4",
            "10 16 15  9 19",
            "18  8 23 26 20",
            "22 11 13  6  5",
            " 2  0 12  3  7"
        };
        var board = new Board(input);

        board.Should().NotBeNull();

        board.Call("10");
        board.Solved().Should().BeFalse();
        board.Call("14");
        board.Solved().Should().BeFalse();
        board.Call("18");
        board.Solved().Should().BeFalse();
        board.Call("22");
        board.Solved().Should().BeFalse();
        board.Call("2");
        board.Solved().Should().BeTrue();
    }
}