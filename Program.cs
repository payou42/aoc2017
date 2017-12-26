﻿using System;
using Aoc.Framework;

namespace Aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the command line
            string[] command = new string[2] { "execute", "all" };
            if (args.Length > 0)
            {
                command = args;
            }

            // Register all days
            Days.RegisterAll();

            // Execute command
            switch (command[0])
            {
                case "execute":
                {
                    Execute(command[1]);
                    return;
                }

                case "generate":
                {
                    Generate(command[1], command[2]);
                    return;
                }
            }
        }

        static void Execute(string what)
        {
            if (what == "all")
            {
                Days.RunAll();
            }
            else
            {
                Days.RunSingle(what);
            }
        }

        static void Generate(string year, string day)
        {
            // Get the title
            Console.WriteLine(Generator.GetTitle(year, day));
            string pday = day.PadLeft(2, '0');

        }
    }
}
