using AdventOfCode.Solvers.Year2024.Day17;

namespace AdventOfCode.Solvers.Tests.Year2024.Day17;

public class Day17SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example1 = """
    Register A: 729
    Register B: 0
    Register C: 0

    Program: 0,1,5,4,3,0
    """;

    public Day17SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "2024 Day 17 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day17SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example1.Split(Environment.NewLine);

        var solver = new Day17Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("4,6,3,5,6,3,5,2,1,0");
    }

    [Fact(DisplayName = "2024 Day 17 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day17SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        string example2 = """
        Register A: 2024
        Register B: 0
        Register C: 0

        Program: 0,3,5,4,3,0
        """;

        var lines = example2.Split(Environment.NewLine);

        var solver = new Day17Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("117440");
    }

    [Fact(DisplayName = "Test sample 1")]
    public void TestSample1()
    {
        // If register C contains 9, the program 2,6 would set register B to 1.
        var lines = new string[]
        {
            "Register A: 0",
            "Register B: 0",
            "Register C: 9",
            "",
            "Program: 2,6"
        };
        System.Reactive.Subjects.ReplaySubject<string> logger = new System.Reactive.Subjects.ReplaySubject<string>();
        logger.Subscribe(_output.WriteLine);
        var device = new Device(lines, logger);

        device.Run();
        device.B.Should().Be(1);
    }

    [Fact(DisplayName = "Test sample 2")]
    public void TestSample2()
    {
        // If register A contains 10, the program 5,0,5,1,5,4 would output 0,1,2.
        var lines = new string[]
        {
            "Register A: 10",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 5,0,5,1,5,4"
        };
        System.Reactive.Subjects.ReplaySubject<string> logger = new System.Reactive.Subjects.ReplaySubject<string>();
        logger.Subscribe(_output.WriteLine);
        var device = new Device(lines, logger);
        var output = device.Run();

        output.Should().Be("0,1,2");
    }

    [Fact(DisplayName = "Test sample 3")]
    public void TestSample3()
    {
        // If register A contains 2024, the program 0,1,5,4,3,0 would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.

        var lines = new string[]
        {
            "Register A: 2024",
            "Register B: 0",
            "Register C: 0",
            "",
            "Program: 0,1,5,4,3,0"
        };
        System.Reactive.Subjects.ReplaySubject<string> logger = new System.Reactive.Subjects.ReplaySubject<string>();
        logger.Subscribe(_output.WriteLine);
        var device = new Device(lines, logger);
        var output = device.Run();
        output.Should().Be("4,2,5,6,7,7,7,7,3,1,0");
        device.A.Should().Be(0);
    }

    [Fact(DisplayName = "Test sample 4")]
    public void TestSample4()
    {
        // If register B contains 29, the program 1,7 would set register B to 26.

        var lines = new string[]
        {
            "Register A: 0",
            "Register B: 29",
            "Register C: 0",
            "",
            "Program: 1,7"
        };
        System.Reactive.Subjects.ReplaySubject<string> logger = new System.Reactive.Subjects.ReplaySubject<string>();
        logger.Subscribe(_output.WriteLine);
        var device = new Device(lines, logger);
        device.Run();
        device.B.Should().Be(26);
    }

    [Fact(DisplayName = "Test sample 5")]
    public void TestSample5()
    {
        // If register B contains 2024 and register C contains 43690, the program 4,0 would set register B to 44354.

        var lines = new string[]
        {
            "Register A: 0",
            "Register B: 2024",
            "Register C: 43690",
            "",
            "Program: 4,0"
        };
        System.Reactive.Subjects.ReplaySubject<string> logger = new System.Reactive.Subjects.ReplaySubject<string>();
        logger.Subscribe(_output.WriteLine);
        var device = new Device(lines, logger);
        device.Run();
        device.B.Should().Be(44354);
    }

}
