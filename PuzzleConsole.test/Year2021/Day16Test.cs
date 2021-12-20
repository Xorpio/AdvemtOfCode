using System.Globalization;
using PuzzleConsole.Year2021.Day16;
using Spectre.Console;

namespace PuzzleConsole.test.Year2021;

public partial class Day16Test
{
    [Scenario]
    public void SolveTest(ScenarioContext scenario)
    {
        var sut = new Day16();

        scenario.Fact("Sut should be puzzle", () =>
        {
            sut.Should().NotBeNull();
        });

        scenario.Fact("DecodeStep1", () =>
        {
            var input = "D2FE28";
            var expected = "110100101111111000101000";

            string output = sut.DecodeToBin(input);

            output.Should().Be(expected);
        });

        scenario.Fact("GetPacket", () =>
        {
            var input = "D2";
            var res = sut.GetHeader(input);
            res.Version.Should().Be(6);
            res.Type.Should().Be(4);
        });

        scenario.Fact("Cast to int", () =>
        {
            var input = "000000000011011";
            var res = Convert.ToInt16(input, 2);

            res.Should().Be(27);
        });

        var cases = new (string input, string answer)[]
        {
            new("D2FE28", "6"),
            new("38006F45291200", "1"),
            new("EE00D40C823060", "7"),
            new("8A004A801A8002F478", "16"),
            new("620080001611562C8802118E34", "12"),
            new("C0015000016115A2E0802F182340", "23"),
            new("A0016C880162017C3686B18A3D4780", "31"),
        };
        foreach (var caseAnswer in cases)
        {
            scenario.Theory("SolveTest 2 packets", caseAnswer, () =>
            {
                var solution = sut.Solve(new string[]{ caseAnswer.input });

                solution.First().Should().Be(caseAnswer.answer);
            });
        }
    }
}
