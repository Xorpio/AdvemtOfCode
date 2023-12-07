using AdventOfCode.Solvers.Year2021.Day24;

namespace AdventOfCode.Solvers.Tests.Year2021.Day24;

public class Day24SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    mul x 0
    add x z
    mod x 26
    div z 1
    add x 14
    eql x w
    eql x 0
    mul y 0
    add y 25
    mul y x
    add y 1
    mul z y
    mul y 0
    add y w
    add y 12
    mul y x
    add z y
    """;

    public Day24SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2021 Day 24 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day24SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day24Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("?");
    }

    [Fact(DisplayName ="2021 Day 24 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day24SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day24Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }

    [Fact]
    public void test()
    {
        for(double i = -10; i < 30; i++)
        {
            var ans = Math.Floor(i / 26);

            _output.WriteLine($"{i} / 26 = {ans}");
            ans.Should().Be(0); 
        }

    }

    // [Fact]
    // public void basicAlu()
    // {
    //     var alu = new ALU(Array.Empty<string>());

    //     alu.W.Should().Be(0);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(0);
    // }

    // [Fact]
    // public void basicAluinpa()
    // {
    //     var alu = new ALU(new[] { "inp w" });

    //     alu.Run(new int[] {1});

    //     alu.W.Should().Be(1);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(0);
    // }

    // [Fact]
    // public void basicAluAdd()
    // {
    //     var alu = new ALU(new[] { "inp w", "add z w" });

    //     alu.Run(new int[] {1});

    //     alu.W.Should().Be(1);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(1);
    // }

    // [Fact]
    // public void basicAluMul()
    // {
    //     var alu = new ALU(new[] { "inp w", "mul w -1" });

    //     alu.Run(new int[] {1});

    //     alu.W.Should().Be(-1);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(0);
    // }

    // [Fact]
    // public void basicAluDiv()
    // {
    //     var alu = new ALU(new[] { "inp w", "div w 2" });

    //     alu.Run(new int[] {5});

    //     alu.W.Should().Be(2);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(0);
    // }

    // [Fact]
    // public void basicAlumod()
    // {
    //     var alu = new ALU(new[] { "inp w", "mod w 3" });

    //     alu.Run(new int[] {1});

    //     alu.W.Should().Be(1);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(0);
    // }

    // [Fact]
    // public void basicAluEql()
    // {
    //     var alu = new ALU(new[] { "inp w", "eql w 10" });

    //     alu.Run(new int[] {1});

    //     alu.W.Should().Be(0);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.Z.Should().Be(0);
    // }

    // [Fact]
    // public void testmod()
    // {
    //     var puzzle = """
    //                  inp w
    //                  add z w
    //                  mod z 2
    //                  div w 2
    //                  add y w
    //                  mod y 2
    //                  div w 2
    //                  add x w
    //                  mod x 2
    //                  div w 2
    //                  mod w 2
    //                  """;

    //     var alu = new ALU(puzzle.Split(Environment.NewLine));

    //     alu.Run(new int[] {9});

    //     alu.Z.Should().Be(1);
    //     alu.Y.Should().Be(0);
    //     alu.X.Should().Be(0);
    //     alu.W.Should().Be(1);
    // }
}
