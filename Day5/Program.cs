using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        private static int[] _numbers;

        static void Main()
        {
            PartOne();
        }

        static void PartOne()
        {
            var input = File.ReadAllText(@"Input.txt");
            _numbers = input.Split(',').Select(int.Parse).ToArray();
            RunProgram();
        }
        
        static void RunProgram()
        {
            var index = 0;
            while (true)
            {
                var instruction = ReadInstruction(index);
                if (instruction.OperationCode == 99)
                {
                    break;
                }
                PerformInstruction(instruction,ref index);
                if (!instruction.Jumped)
                {
                    index = index + instruction.Parameters.Count + 1;
                }
            }
        }

        private static void PerformInstruction(Instruction instruction, ref int index)
        {
            if (instruction.OperationCode == 1)
            {
                var param1 = instruction.Parameters[0].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[0].AddressOrValue
                    : _numbers[instruction.Parameters[0].AddressOrValue];
                var param2 = instruction.Parameters[1].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[1].AddressOrValue
                    : _numbers[instruction.Parameters[1].AddressOrValue];
                var result = param1 + param2;
                var register = instruction.Parameters.Last().AddressOrValue;
                _numbers[register] = result;
            }
            else if (instruction.OperationCode == 2)
            {
                var param1 = instruction.Parameters[0].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[0].AddressOrValue
                    : _numbers[instruction.Parameters[0].AddressOrValue];
                var param2 = instruction.Parameters[1].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[1].AddressOrValue
                    : _numbers[instruction.Parameters[1].AddressOrValue];
                var result = param1 * param2;
                _numbers[instruction.Parameters.Last().AddressOrValue] = result;
            }
            else if (instruction.OperationCode == 3)
            {
                var c = Console.ReadKey().KeyChar;
                var result = int.Parse(c.ToString());
                Console.WriteLine();
                _numbers[instruction.Parameters.Last().AddressOrValue] = result;
            }
            else if (instruction.OperationCode == 4)
            {
                var param1 = instruction.Parameters.First().Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters.First().AddressOrValue
                    : _numbers[instruction.Parameters.First().AddressOrValue];
                Console.WriteLine($"Output: {param1}");
            }
            else if (instruction.OperationCode == 5)
            {
                var param1 = instruction.Parameters.First().Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters.First().AddressOrValue
                    : _numbers[instruction.Parameters.First().AddressOrValue];
                if (param1 == 0) return;
                var param2 = instruction.Parameters[1].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[1].AddressOrValue
                    : _numbers[instruction.Parameters[1].AddressOrValue];
                index = param2;
                instruction.Jumped = true;
            }
            else if (instruction.OperationCode == 6)
            {
                var param1 = instruction.Parameters.First().Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters.First().AddressOrValue
                    : _numbers[instruction.Parameters.First().AddressOrValue];
                if (param1 != 0) return;
                var param2 = instruction.Parameters[1].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[1].AddressOrValue
                    : _numbers[instruction.Parameters[1].AddressOrValue];
                index = param2;
                instruction.Jumped = true;
            }
            else if (instruction.OperationCode == 7)
            {
                var param1 = instruction.Parameters.First().Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters.First().AddressOrValue
                    : _numbers[instruction.Parameters.First().AddressOrValue];
                var param2 = instruction.Parameters[1].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[1].AddressOrValue
                    : _numbers[instruction.Parameters[1].AddressOrValue];
                _numbers[instruction.Parameters.Last().AddressOrValue] = param1 < param2 ? 1 : 0;
            }
            else if (instruction.OperationCode == 8)
            {
                var param1 = instruction.Parameters.First().Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters.First().AddressOrValue
                    : _numbers[instruction.Parameters.First().AddressOrValue];
                var param2 = instruction.Parameters[1].Mode == Parameter.ParameterMode.Immediate
                    ? instruction.Parameters[1].AddressOrValue
                    : _numbers[instruction.Parameters[1].AddressOrValue];
                _numbers[instruction.Parameters.Last().AddressOrValue] = param1 == param2 ? 1 : 0;
            }
            else
            {
                Console.WriteLine($"Unexpected opcode {instruction.OperationCode}");
            }
        }

        static Instruction ReadInstruction(int index)
        {
            var opCode = _numbers[index] % 100;
            var instruction = new Instruction {OperationCode = opCode, OperationRaw = _numbers[index]};
            if (instruction.OperationCode != 99)
            {
                for (int i = 1; i <= NumberOfParameters(opCode); i++)
                {
                    instruction.Parameters.Add(new Parameter
                    {
                        AddressOrValue = _numbers[index + i],
                        Mode = _numbers[index] % (int)Math.Pow(10, 2 + i) 
                               >= (int)Math.Pow(10, 1+i)
                            ? Parameter.ParameterMode.Immediate
                            : Parameter.ParameterMode.Position
                    });
                }
            }
            return instruction;
        }

        static int NumberOfParameters(int opCode)
        {
            switch (opCode)
            {
                case 1: return 3;
                case 2: return 3;
                case 3: return 1;
                case 4: return 1;
                case 5: return 2;
                case 6: return 2;
                case 7: return 3;
                case 8: return 3;
                default:throw new InvalidOperationException("Unexpected opcode");
            }
        }

        static void PrintRegisterAddress(int index)
        {
            Console.WriteLine($"register {index}: {_numbers[index]}");
        }
    }
}
