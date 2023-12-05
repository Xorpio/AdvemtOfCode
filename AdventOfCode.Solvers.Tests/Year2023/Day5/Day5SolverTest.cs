using AdventOfCode.Solvers.Year2023.Day5;

namespace AdventOfCode.Solvers.Tests.Year2023.Day5;

public class Day5SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    seeds: 79 14 55 13

    seed-to-soil map:
    50 98 2
    52 50 48

    soil-to-fertilizer map:
    0 15 37
    37 52 2
    39 0 15

    fertilizer-to-water map:
    49 53 8
    0 11 42
    42 0 7
    57 7 4

    water-to-light map:
    88 18 7
    18 25 70

    light-to-temperature map:
    45 77 23
    81 45 19
    68 64 13

    temperature-to-humidity map:
    0 69 1
    1 0 69

    humidity-to-location map:
    60 56 37
    56 93 4
    """;

    public Day5SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 5 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day5SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day5Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("35");
    }

    [Fact(DisplayName ="2023 Day 5 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day5SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day5Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("46");
    }

    [Fact]
    public async Task FindOverlap()
    {
        (decimal start, decimal range)[] ranges = new (decimal start, decimal range)[]
        {
            (929142010, 467769747),
            (2497466808, 210166838),
            (3768123711, 33216796),
            (1609270159, 86969850),
            (199555506, 378609832),
            (1840685500, 314009711),
            (1740069852, 36868255),
            (2161129344, 170490105),
            (2869967743, 265455365),
            (3984276455, 31190888),
        };

        // loop over every combination
        for (int i = 0; i < ranges.Length; i++)
        {
            for (int j = i + 1; j < ranges.Length; j++)
            {
                var (start1, range1) = ranges[i];
                var (start2, range2) = ranges[j];

                var overlap = RangesOverlap(start1, start1 + range1, start2, start2 + range2);

                overlap.Should().BeFalse();

                if (overlap)
                {
                    _output.WriteLine($"Overlap between {start1} and {start1 + range1} and {start2} and {start2 + range2}");
                }
            }
        }
    }

    public static bool RangesOverlap(decimal start1, decimal end1, decimal start2, decimal end2)
    {
        return (start1 <= end2) && (end1 >= start2);
    }
}
