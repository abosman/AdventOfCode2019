using System;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        private static int[] _numbers;

        static void Main()
        {
            //PartOne();
            PartTwo();
        }

        static void PartOne()
        {
            var input = File.ReadAllText(@"Input.txt");
            _numbers = input.Split(',').Select(int.Parse).ToArray();
            _numbers[1] = 12;
            _numbers[2] = 2;
            RunProgram();
            Console.WriteLine($"Value at position 0: {_numbers[0]}"); // 5110675
        }

        static void PartTwo()
        {
            var result = 19690720;
            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var input = File.ReadAllText(@"Input.txt");
                    _numbers = input.Split(',').Select(int.Parse).ToArray();
                    _numbers[1] = noun;
                    _numbers[2] = verb;
                    RunProgram();
                    if (_numbers[0] == result)
                    {
                        Console.WriteLine($"Noun: {noun} and verb: {verb} produces the outcome {result}");
                        Console.WriteLine($"Answer: 100 * noun + verb = {100 * noun + verb}"); //4847
                        return;
                    }
                }
            }
        }

        static void RunProgram()
        {
            for (int i = 0; i < _numbers.Length; i += 4)
            {
                var opCode = _numbers[i];
                var operand1 = _numbers[_numbers[i + 1]];
                var operand2 = _numbers[_numbers[i + 2]];
                var outputIndex = _numbers[i + 3];

                if (opCode == 99)
                {
                    break;
                }

                _numbers[outputIndex] = opCode switch
                {
                    1 => (operand1 + operand2),
                    2 => (operand1 * operand2)
                };
            }
        }
    }
}
