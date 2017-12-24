using System;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace Aoc
{
    public class Days
    {
        private static Dictionary<string, Day> _registry;

        static Days()
        {
            _registry = new Dictionary<string, Day>();
        }

        public static void Register(Type type)
        {
            Day instance = (Day)Activator.CreateInstance(type);
            _registry[instance.Codename] = instance;
        }

        public static void RegisterAll()
        {
            Assembly current = Assembly.GetExecutingAssembly();
            foreach (Type type in current.GetTypes())
            {
                Type[] interfaces = type.FindInterfaces((typeObj, criteriaObj) => typeObj.ToString() == (string)criteriaObj, "Aoc.Day");
                if (interfaces.Length > 0)
                {
                    Register(type);
                }
            }
        }

        public static void RunAll()
        {
            foreach (Day day in _registry.Values)
            {
                RunSingle(day.Codename);
            }
        }
        
        public static void RunSingle(string codename)
        {
            int width = 80;
            Day day = _registry[codename];
            ConsoleColor savedColor = Console.ForegroundColor;

            Stopwatch performance = new Stopwatch();
            performance.Start();
            day.Init();
            performance.Stop();
            string p0 = performance.Elapsed.ToString();
            
            performance.Reset();
            performance.Start();
            string part1 = day.Run(Part.Part1);
            performance.Stop();
            string p1 = performance.Elapsed.ToString();

            performance.Reset();
            performance.Start();
            string part2 = day.Run(Part.Part2);
            performance.Stop();
            string p2 = performance.Elapsed.ToString();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+" + "".PadLeft(width / 2 - 1, '-').PadRight(width - 2, '-') + "+");
            Console.Write("|");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(day.Codename.PadLeft(width / 2 - 1 + day.Codename.Length / 2, ' ').PadRight(width - 2, ' '));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|");
            Console.Write("|");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(day.Name.PadLeft(width / 2 - 1 + day.Name.Length / 2, ' ').PadRight(width - 2, ' '));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|");
            Console.WriteLine("+" + "".PadLeft(width / 2 - 1, '-').PadRight(width - 2, '-') + "+");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Part 1: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(part1);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Part 2: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(part2);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Time taken for init   = {0}", p0);
            Console.WriteLine("Time taken for part 1 = {0}", p1);
            Console.WriteLine("Time taken for part 2 = {0}", p2);
            Console.WriteLine("");

            Console.ForegroundColor = savedColor;
        }
    }
}