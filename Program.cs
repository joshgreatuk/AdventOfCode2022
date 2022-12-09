using System;
using System.Diagnostics;

namespace AOC22
{
    public class Program
    {
        public static Dictionary<int, Solution> solutions = new Dictionary<int, Solution>() { 
            {1, new Day1()},
            {2, new Day2()},
            {3, new Day3()},
            {4, new Day4()},
            {5, new Day5()},
            {6, new Day6()},
            {7, new Day7()},
            {8, new Day8()},
            {9, new Day9()}
        };
        public static List<int> days = new List<int>() { 9 };

        public static void Main(string[] args)
        {
            for (int i=0; i < solutions.Count; i++)
            {
                
                if (days.Contains(i+1)) 
                {
                    Stopwatch timer = new Stopwatch();
                    string[] solution = solutions[i+1].GetSolution(args);
                    Console.WriteLine($"Solutions for day '{(i+1).ToString()}' = '[{solution[0]},{solution[1]}]' in {timer.Elapsed.TotalMilliseconds}ms");
                    timer.Stop();
                }
            }
        }
    }
}