using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines(@"Input.txt");
            var modules = input.Select(c => int.Parse(c.ToString())).ToList();
            var requiredFuel = modules.Sum(module =>
            {
                var fuel = module / 3 - 2;
                int extraFuel;
                var inputMass = fuel;
                do
                {
                    extraFuel = inputMass / 3 - 2;
                    if (extraFuel < 0)
                    {
                        extraFuel = 0;
                    }
                    fuel += extraFuel;
                    inputMass = extraFuel;
                } while (extraFuel>0);
                return fuel;
            });
            Console.WriteLine($"Total required fuel: {requiredFuel}");
        }
    }
}
