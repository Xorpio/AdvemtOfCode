using System.Reactive.Subjects;

namespace AdventOfCode.Solvers.Year2024.Day17;

public class Day17Solver : BaseSolver
{
    public override void Solve(string[] puzzle)
    {
        var device = new Device(puzzle, logger, int.MaxValue);
        GiveAnswer1(device.Run());

        var inputs = puzzle[4].Split(": ")[1].Split(",").Select(decimal.Parse).ToArray();
        var max = inputs.Length;

        decimal count = 0;

        if (puzzle[4] == "Program: 0,3,5,4,3,0")
        {
            count = findCountTest(inputs, 0);
        }
        if (puzzle[4] == "Program: 2,4,1,3,7,5,4,2,0,3,1,5,5,5,3,0")
        {
            count = findCountTestActual(inputs, 0);
        }

        puzzle[0] = $"Register A: {count}";
        device = new Device(puzzle, new ReplaySubject<string>(), max);
        var output = device.Run();
        logger.OnNext($"A: {count} = output: {output}");

        GiveAnswer2(count);
    }

    private decimal findCountTest(decimal[] input, decimal current)
    {
        var count = 0;
        while (true)
        {
            decimal a = current * 8;
            a = a + count;

            if (Math.Floor(a / 8) % 8 == input[^1])
            {
                if (input.Length == 1)
                {
                    return a;
                }
                var answer = findCountTest(input[..^1], a);
                if (answer != 0)
                {
                    return answer;
                }
            }
            count++;

            if (Math.Floor(a / 8) != current)
                return 0;
        }
    }

    private decimal findCountTestActual(decimal[] input, decimal current)
    {
        var count = 0;
        while (true)
        {
            decimal a = current * 8;
            a = a + count;

            //2,4 : B = A % 8
            decimal b = a % 8;
            //1,3 : B = B xor 3
            b = XorDecimals(b, 3);
            //7,5 : C = Floor(A / (2 ^ B) )
            decimal c = Math.Floor(a / (decimal)Math.Pow(2, (double)b));
            //4,2 : B = B xor C
            b = XorDecimals(b, c);
            ////0,3 : A = Floor(A / (2 ^ 3) )
            //a = Math.Floor(a / (decimal)Math.Pow(2, 3));
            //1,5 : B = B xor 5
            b = XorDecimals(b, 5);


            if (b % 8 == input[^1])
            {
                if (input.Length == 1)
                {
                    return a;
                }

                logger.OnNext($"current: {current}, count: {count}, input: {string.Join(",", input[..^1])}, a: {a}");
                var answer = findCountTestActual(input[..^1], a);
                if (answer != 0)
                {
                    return answer;
                }
            }
            count++;

            if (Math.Floor(a / 8) != current)
                return 0;
        }
    }
    public static decimal XorDecimals(decimal a, decimal b)
    {
        // Convert decimals to byte arrays
        byte[] bytesA = decimal.GetBits(a).SelectMany(BitConverter.GetBytes).ToArray();
        byte[] bytesB = decimal.GetBits(b).SelectMany(BitConverter.GetBytes).ToArray();

        // Perform XOR operation on byte arrays
        byte[] resultBytes = new byte[bytesA.Length];
        for (int i = 0; i < bytesA.Length; i++)
        {
            resultBytes[i] = (byte)(bytesA[i] ^ bytesB[i]);
        }

        // Convert result byte array back to decimal
        int[] resultBits = new int[resultBytes.Length / 4];
        for (int i = 0; i < resultBits.Length; i++)
        {
            resultBits[i] = BitConverter.ToInt32(resultBytes, i * 4);
        }

        return new decimal(resultBits);
    }
}

public class Device
{
    public double A { get; set; } = 0;
    public double B { get; set; } = 0;
    public double C { get; set; } = 0;

    int pointer = 0;

    decimal _max = 0;

    int[] program = [];

    IList<double> output = new List<double>();
    private readonly ReplaySubject<string> _logger;

    public Device(string[] puzzle, System.Reactive.Subjects.ReplaySubject<string> logger, decimal max = int.MaxValue)
    {
        A = double.Parse(puzzle[0].Split(": ")[1]);
        B = double.Parse(puzzle[1].Split(": ")[1]);
        C = double.Parse(puzzle[2].Split(": ")[1]);

        program = puzzle[4].Split(": ")[1].Split(",").Select(int.Parse).ToArray();
        this._logger = logger;

        _max = max;
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

            if (step > 1000 || output.Count > _max)
            {
                _logger.OnNext("might have infinte loop?");
                break;
            }

            switch (program[pointer])
            {
                case 0:
                    _logger.OnNext($"adv({program[pointer]}, {program[pointer + 1]})");
                    adv(program[pointer], program[pointer + 1]);
                    break;
                case 1:
                    _logger.OnNext($"bxl({program[pointer]}, {program[pointer + 1]})");
                    bxl(program[pointer], program[pointer + 1]);
                    break;
                case 2:
                    _logger.OnNext($"bst({program[pointer]}, {program[pointer + 1]})");
                    bst(program[pointer], program[pointer + 1]);
                    break;
                case 3:
                    _logger.OnNext($"jnz({program[pointer]}, {program[pointer + 1]})");
                    var p = pointer;
                    jnz(program[pointer], program[pointer + 1]);
                    if (p != pointer)
                    {
                        continue;
                    }
                    break;
                case 4:
                    _logger.OnNext($"bxc({program[pointer]}, {program[pointer + 1]})");
                    bxc(program[pointer], program[pointer + 1]);
                    break;
                case 5:
                    _logger.OnNext($"outInstruction({program[pointer]}, {program[pointer + 1]})");
                    outInstruction(program[pointer], program[pointer + 1]);
                    if (_max == int.MaxValue)
                        break;

                    for (int i = 0; i < output.Count; i++)
                    {
                        if (output[i] != program[i])
                        {
                            return "";
                        }
                    }
                    break;
                case 6:
                    _logger.OnNext($"bdv({program[pointer]}, {program[pointer + 1]})");
                    bdv(program[pointer], program[pointer + 1]);
                    break;
                case 7:
                    _logger.OnNext($"cdv({program[pointer]}, {program[pointer + 1]})");
                    cdv(program[pointer], program[pointer + 1]);
                    break;
                default:
                    throw new NotImplementedException($"{program[pointer]} was not yet implemented");
            }

            pointer += 2;
        }
        return string.Join(",", output);
    }

    //The adv instruction (opcode 0) performs division. The numerator is the value in the A register. The denominator is found by raising 2 to the power of the instruction's combo operand. (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.) The result of the division operation is truncated to an integer and then written to the A register.


    private void adv(decimal instruction, decimal operand)
    {
        var combo = GetOperand(operand);

        A = double.Floor(A / Math.Pow(2, combo));
    }


    //The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.
    private void bxl(decimal instruction, decimal operand)
    {
        //B = (int)B ^ (int)operand;
        B = (double)Day17Solver.XorDecimals((decimal)B, operand);
    }

    // The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.
    private void bst(decimal instruction, decimal operand)
    {
        var combo = GetOperand(operand);
        B = combo % 8;
    }

    //The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.
    private void jnz(decimal instruction, decimal operand)
    {
        if (A != 0)
        {
            pointer = (int)operand;
        }
    }

    //The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)
    private void bxc(decimal instruction, decimal operand)
    {
        B = (double)Day17Solver.XorDecimals((decimal)B, (decimal)C);
    }

    //The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value. (If a program outputs multiple values, they are separated by commas.)
    private void outInstruction(decimal instruction, decimal operand)
    {
        var combo = GetOperand(operand);
        output.Add(combo % 8);
    }

    //The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register. (The numerator is still read from the A register.)
    private void bdv(decimal instruction, decimal operand)
    {
        var combo = GetOperand(operand);
        B = double.Floor(A / Math.Pow(2, combo));
    }

    //The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register. (The numerator is still read from the A register.)
    private void cdv(decimal instruction, decimal operand)
    {
        var combo = GetOperand(operand);
        C = double.Floor(A / Math.Pow(2, combo));
    }

    private double GetOperand(decimal o)
    {
        double v = o switch
        {
            < 4 => (int)o,
            4 => A,
            5 => B,
            6 => C,
            _ => throw new ArgumentOutOfRangeException("7 is not valid for combo")
        };
        _logger.OnNext($"GetOperand({o}) = {v}");
        return v;
    }
}
