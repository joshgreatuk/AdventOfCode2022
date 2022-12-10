using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    public class Day10 : Solution
    {
        string inputFile = "Day10/Puzzle.txt";

        public override string[] GetSolution(string[] args)
        {
            string[] fileInput;
            try
            {
                fileInput = File.ReadAllLines((args.Length > 0 && args[0] != string.Empty) ? args[0] : inputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new string[2];
            }

            string[] solution = new string[2];
            //Solution
            int cycle = 1;
            int register = 1;
            int signalStrength = 0;
            string image = "\n";
            int lineNumber = 0;
            foreach (string instruction in fileInput)
            {
                string[] instructionSplit = instruction.Split(" ");
                signalStrength += ProcessSignal(cycle, register);
                int currentIndex = cycle - (40 * lineNumber);
                if (currentIndex == register || currentIndex == register+1 || currentIndex == register+2) image += "#";
                else image += ".";
                if (currentIndex % 40 == 0) { lineNumber++; image += "\n"; }
                if (instructionSplit[0] != "noop")
                {
                    cycle++;
                    signalStrength += ProcessSignal(cycle, register);
                    currentIndex = cycle - (40 * lineNumber);
                    if (currentIndex == register || currentIndex == register+1 || currentIndex == register+2) image += "#";
                    else image += ".";
                    if (currentIndex % 40 == 0) { lineNumber++; image += "\n"; }
                    register += Int32.Parse(instructionSplit[1]);
                }
                cycle++;
            }
            solution[0] = signalStrength.ToString();
            solution[1] = image;
            return solution;
        }

        int ProcessSignal(int cycle, int register)
        {
            return ((cycle-20) % 40 == 0) ? cycle * register : 0;
        }
    }
}