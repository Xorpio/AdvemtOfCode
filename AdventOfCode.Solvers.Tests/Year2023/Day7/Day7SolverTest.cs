using AdventOfCode.Solvers.Year2023.Day7;

namespace AdventOfCode.Solvers.Tests.Year2023.Day7;

public class Day7SolverTest
{
    private readonly ITestOutputHelper _output;

    private string example = """
    32T3K 765
    T55J5 684
    KK677 28
    KTJJT 220
    QQQJA 483
    """;

    public Day7SolverTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName ="2023 Day 7 Solver Has Correct Solution For Part 1 sample input")]
    public async Task Day7SolverHasCorrectSolutionForPart1SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day7Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer1.LastAsync();

        answer.Should().Be("6440");
    }

    [Fact(DisplayName ="2023 Day 7 Solver Has Correct Solution For Part 2 sample input")]
    public async Task Day7SolverHasCorrectSolutionForPart2SampleInputAsync()
    {
        var lines = example.Split(Environment.NewLine);

        var solver = new Day7Solver();

        solver.Logger.Subscribe(msg => _output.WriteLine(msg));

        solver.Solve(lines);

        var answer = await solver.Answer2.LastAsync();

        answer.Should().Be("?");
    }

    [Theory]
    [InlineData("33332 1", "2AAAA 1")]
    [InlineData("77888 1", "77788 1")]
    [InlineData("A1111 1", "K1111 1")]
    [InlineData("K1111 1", "Q1111 1")]
    [InlineData("Q1111 1", "J1111 1")]
    [InlineData("J1111 1", "T1111 1")]
    [InlineData("T1111 1", "91111 1")]
    [InlineData("91111 1", "81111 1")]
    [InlineData("81111 1", "71111 1")]
    [InlineData("61111 1", "51111 1")]
    [InlineData("51111 1", "41111 1")]
    [InlineData("41111 1", "31111 1")]
    [InlineData("31111 1", "21111 1")]
    [InlineData("12346 1", "12345 1")]
    public void CompareHands(string in1, string in2)
    {
        var hand1 = new Hand(in1);
        var hand2 = new Hand(in2);

        hand1.CompareTo(hand2).Should().BePositive();
    }

    [Fact]
    public void equalThrows()
    {
        var hand1 = new Hand("12345 1");
        var hand2 = new Hand("12345 2");

        var act = () => hand1.CompareTo(hand2);

        act.Should().Throw<Exception>();
    }

    [Fact]
    public void ThreeOfAKind()
    {
        var input = "54J34 824";
        var hand = new Hand(input);
        hand.HandType.Should().Be(TypeOfHand.OnePair);
    }

    [Fact]
    public void FullHouse()
    {
        var input = "TTTJJ 824";
        var hand = new Hand(input);
        hand.HandType.Should().Be(TypeOfHand.FullHouse);
    }
}
