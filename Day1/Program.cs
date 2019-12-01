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
                Console.WriteLine($"Mass of {module}, required fuel {fuel}");
                return fuel;

            });
            Console.WriteLine($"Total required fuel: {requiredFuel}");
        }
    }
}
