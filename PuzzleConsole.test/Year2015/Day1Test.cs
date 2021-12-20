using FluentAssertions;
using Moq.AutoMock;
using PuzzleConsole.Year2015;
using ScenarioTests;

namespace PuzzleConsole.test.Year2015;

public partial class Day1Test
{
    // [Scenario]
    // public void TestStartup(ScenarioContext scenario)
    // {
    //     var mocker = new AutoMocker(Moq.MockBehavior.Strict);

    //     var sut = mocker.CreateInstance<Day1>();

    //     scenario.Fact("object should be created", () =>
    //     {
    //         sut.Should().NotBeNull();
    //     });

    //     scenario.Fact("Input should constist of 2 int", () =>
    //     {
    //         sut.Invoking(s => s.Solve(new string[] { }))
    //         .Should().Throw<InvalidParameterException>();

    //         sut.Invoking(s => s.Solve(new string[] { "" }))
    //         .Should().Throw<InvalidParameterException>();

    //         sut.Invoking(s => s.Solve(new string[] { "abc" }))
    //         .Should().Throw<InvalidParameterException>();
    //         sut.Invoking(s => s.Solve(new string[] { "123,asd" }))
    //         .Should().Throw<InvalidParameterException>();
    //         sut.Invoking(s => s.Solve(new string[] { "asd,123" }))
    //         .Should().Throw<InvalidParameterException>();
    //         sut.Invoking(s => s.Solve(new string[] { "1,1" }))
    //         .Should().NotThrow<InvalidParameterException>();
    //     });

    //     scenario.Fact("1,1 shouldreturn start", () =>
    //     {
    //         var answer = sut.Solve(new string[] { "1,1" });

    //         answer.Should().HaveCount(1);
    //         const string start = "20151125";
    //         answer[0].Should().Be(start);
    //     });

    //     scenario.Fact("2,2 shouldreturn answer", () =>
    //     {
    //         var answer = sut.Solve(new string[] { "2,2" });

    //         answer.Should().HaveCount(1);
    //         const string expected = "21629792";
    //         answer[0].Should().Be(expected);
    //     });
    // }
}
