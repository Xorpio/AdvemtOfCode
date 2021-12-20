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

        var cases = new (string input, string answer, string calc)[]
        {
            new("D2FE28", "6", "2021"),
            new("38006F45291200", "9", null),
            new("EE00D40C823060", "14", null),
            new("8A004A801A8002F478", "16", null),
            new("620080001611562C8802118E34", "12", null),
            new("C0015000016115A2E0802F182340", "23", null),
            new("A0016C880162017C3686B18A3D4780", "31", null),
        };
        foreach (var caseAnswer in cases)
        {
            scenario.Theory("SolveTest part 1 2 packets", caseAnswer, () =>
            {
                var solution = sut.Solve(new string[]{ caseAnswer.input });

                solution.First().Should().Be(caseAnswer.answer);

                if (caseAnswer.calc != null)
                {
                    solution.Count().Should().Be(2);
                    solution.Last().Should().Be(caseAnswer.calc);
                }
            });
        }

        var otherCases = new (string input, string answer)[]
        {
            new("C200B40A82","3"),
            new("04005AC33890", "54"),
            new("880086C3E88112", "7"),
            new("CE00C43D881120", "9"),
            new("D8005AC2A8F0", "1"),
            new("F600BC2D8F", "0"),
            new("9C005AC2F8F0", "0"),
            new("9C0141080250320F1802104A08", "1"),
        };
        foreach (var caseAnswer in otherCases)
        {
            scenario.Theory("SolveTest part 2", caseAnswer, () =>
            {
                var solution = sut.Solve(new string[]{ caseAnswer.input });

                solution.Count().Should().Be(2);
                solution.Last().Should().Be(caseAnswer.answer);
            });
        }
    }
}
