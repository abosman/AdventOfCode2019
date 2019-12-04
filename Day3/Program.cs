using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        static void Main()
        {
            var wires = File.ReadAllLines(@"Input.txt");
            var wireTrajects1 = ParseWirePath(wires[0].Split(','));
            var wireTrajects2 = ParseWirePath(wires[1].Split(','));
            var crossings = wireTrajects1.Intersect(wireTrajects2).ToList();
            PartOne(crossings);
            PartTwo(crossings, wireTrajects1.ToList(), wireTrajects2.ToList());
        }

        private static void PartOne(List<Tuple<int, int>> crossings)
        {
            var minimalDistance = Math.Abs(crossings[0].Item1) + Math.Abs(crossings[0].Item2);
            minimalDistance = crossings.Select(crossing => Math.Abs(crossing.Item1) + Math.Abs(crossing.Item2))
                .Concat(new[] {minimalDistance}).Min();
            Console.WriteLine(
                $"Manhattan distance from the central port to the closest intersection is {minimalDistance}"); // 709
        }

        private static void PartTwo(List<Tuple<int, int>> crossings, List<Tuple<int, int>> wireTraject1,
            List<Tuple<int, int>> wireTraject2)
        {
            var minimalSteps = wireTraject1.IndexOf(crossings[0]) + 1 + wireTraject2.IndexOf(crossings[0]) + 1;
            minimalSteps = crossings.Select(crossing =>
                    wireTraject1.IndexOf(crossing) + 1 + wireTraject2.IndexOf(crossing) + 1)
                .Concat(new[] {minimalSteps}).Min();
            Console.WriteLine($"Minimal steps is {minimalSteps}"); // 13836
        }

        private static IEnumerable<Tuple<int, int>> ParseWirePath(IEnumerable<string> wirePoints)
        {
            var wireCoordinates = new List<Tuple<int, int>>();
            var point = new Tuple<int, int>(0, 0);
            foreach (var wirePoint in wirePoints)
            {
                var direction = wirePoint.Substring(0, 1);
                var distance = int.Parse(wirePoint.Substring(1));
                for (int i = 1; i <= distance; i++)
                {
                    wireCoordinates.Add(
                        direction switch
                        {
                            "U" => new Tuple<int, int>(point.Item1, point.Item2 + i),
                            "D" => new Tuple<int, int>(point.Item1, point.Item2 - i),
                            "L" => new Tuple<int, int>(point.Item1 - i, point.Item2),
                            "R" => new Tuple<int, int>(point.Item1 + i, point.Item2)
                        });
                }

                point = wireCoordinates.Last();
            }
            return wireCoordinates;
        }
    }
}
