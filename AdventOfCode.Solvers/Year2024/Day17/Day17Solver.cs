using System.Reactive.Subjects;

namespace AdventOfCode.Solvers.Year2024.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var device = new Device(puzzle, logger);
        GiveAnswer1(device.Run());
        GiveAnswer2("");
    }
}

public class Device
{
    double A = 0;
    double B = 0;
    double C = 0;

    int pointer = 0;

    int[] program = [];

    IList<double> output = new List<double>();
    private readonly ReplaySubject<string> _logger;

    public Device(string[] puzzle, System.Reactive.Subjects.ReplaySubject<string> logger)
    {
        A = double.Parse(puzzle[0].Split(": ")[1]);
        B = double.Parse(puzzle[1].Split(": ")[1]);
        C = double.Parse(puzzle[2].Split(": ")[1]);

        program = puzzle[4].Split(": ")[1].Split(",").Select(int.Parse).ToArray();
        this._logger = logger;
    }

    public string Run()
    {
        var step = 1;
        while (true)
        {
            _logger.OnNext($"A: {A}, B: {B}, C: {C}, pointer: {pointer}, output: {string.Join(",", output)}");
            step++;
            if (pointer >= program.Length)
            {
                break;
            }

            switch (program[pointer])
            {
                case 0:
                    adv(program[pointer], program[pointer + 1]);
                    break;
                case 1:
                    bxl(program[pointer], program[pointer + 1]);
                    break;
                case 2:
                    bst(program[pointer], program[pointer + 1]);
                    break;
                case 3:
                    var p = pointer;
                    jnz(program[pointer], program[pointer + 1]);
                    if (p != pointer)
                    {
                        continue;
                    }
                    break;
                case 4:
                    bxc(program[pointer], program[pointer + 1]);
                    break;
                case 5:
                    outInstruction(program[pointer], program[pointer + 1]);
                    break;
                case 6:
                    bdv(program[pointer], program[pointer + 1]);
                    break;
                case 7:
                    cdv(program[pointer], program[pointer + 1]);
                    break;
                default:
                    throw new NotImplementedException($"{program[pointer]} was not yet implemented");
            }

            pointer += 2;

            if (step > 10000)
            {
                throw new Exception("might have infinte loop?");
            }
        }
        return string.Join(",", output);
    }

    //The adv instruction (opcode 0) performs division. The numerator is the value in the A register. The denominator is found by raising 2 to the power of the instruction's combo operand. (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.) The result of the division operation is truncated to an integer and then written to the A register.


    private void adv(int instruction, int operand)
    {
        var combo = GetOperand(operand);

        A = double.Floor(A / Math.Pow(combo, 2));
    }


    //The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.
    private void bxl(int instruction, int operand)
    {
        B = (int)B ^ operand;
    }

    // The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.
    private void bst(int instruction, int operand)
    {
        var combo = GetOperand(operand);
        B = combo % 8;
    }

    //The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
    private void jnz(int instruction, int operand)
    {
        if (A != 0)
        {
            pointer = operand;
        }
    }

    //The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
    private void bxc(int instruction, int operand)
    {
        B = (int)B ^ (int)C;
    }

    //The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value. (If a program outputs multiple values, they are separated by commas.)
    private void outInstruction(int instruction, int operand)
    {
        var combo = GetOperand(operand);
        output.Add(combo % 8);
    }

    //The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register. (The numerator is still read from the A register.)
    private void bdv(int instruction, int operand)
    {
        var combo = GetOperand(operand);
        B = double.Floor(A / Math.Pow(combo, 2));
    }

    //The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register. (The numerator is still read from the A register.)
    private void cdv(int instruction, int operand)
    {
        var combo = GetOperand(operand);
        C = double.Floor(A / Math.Pow(combo, 2));
    }

    private double GetOperand(int o) => o switch
    {
        < 4 => o,
        4 => A,
        5 => B,
        6 => C,
        _ => throw new ArgumentOutOfRangeException("7 is not valid for combo")
    };

}
