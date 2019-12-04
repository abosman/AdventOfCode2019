using System;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main()
        {
            var minValue = 246515;
            var maxValue = 739105;
            var counter = 0;
            for (int i = minValue; i <= maxValue; i++)
            {
                //if (IsValidPasswordPartOne(i))
                //{
                //    counter++;
                //} // 1048
                if (IsValidPasswordPartTwo(i))
                {
                    counter++;
                } // 677
            }
            Console.WriteLine($"{counter} valid passwords found within range");
        }

        static private bool IsValidPasswordPartOne(int number)
        {
            var passwordToCheck = number.ToString();
            var lastchar = passwordToCheck[0];
            var hasdouble = false;
            for (int i = 1; i < passwordToCheck.Length; i++)
            {
                if (passwordToCheck[i] == lastchar)
                {
                    hasdouble = true;
                }
                if (passwordToCheck[i] < lastchar)
                {
                    return false;
                }
                lastchar = passwordToCheck[i];
            }
            return hasdouble;
        }

        static private bool IsValidPasswordPartTwo(int number)
        {
            var passwordToCheck = number.ToString();
            var hasdouble = false;
            for (int i = 1; i < passwordToCheck.Length; i++)
            {
                if (passwordToCheck[i] == passwordToCheck[i - 1]
                    && !DoubleInLargerGroup(passwordToCheck,new string(passwordToCheck[i],2)))
                {
                    hasdouble = true;
                }

                if (passwordToCheck[i] < passwordToCheck[i - 1])
                {
                    return false;
                }
            }
            return hasdouble;
        }


        static private bool DoubleInLargerGroup(string passwordToCheck, string doubleStr)
        {
            var pattern = $"(?={doubleStr})";
            return Regex.Matches(passwordToCheck, pattern).Count > 1;
        }
    }
}
