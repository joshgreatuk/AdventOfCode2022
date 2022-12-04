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
            {4, new Day4()}
        };
        public static List<int> days = new List<int>() { 4 };

        public static void Main(string[] args)
        {
            for (int i=0; i < solutions.Count; i++)
            {
                
                if (days.Contains(i+1)) 
                {
                    Stopwatch timer = new Stopwatch();
                    float[] solution = solutions[i+1].GetSolution(args);
                    timer.Stop();
                    Console.WriteLine($"Solutions for day '{i+1.ToString()}' = '[{solution[0]},{solution[1]}]' in {timer.Elapsed.TotalMilliseconds}ms");
                }
            }
        }
    }
}