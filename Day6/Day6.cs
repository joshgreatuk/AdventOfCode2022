using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    public class Day6 : Solution
    {
        string inputFile = "Day6/Puzzle.txt";

        public override string[] GetSolution(string[] args)
        {
            string fileInput;
            try
            {
                fileInput = File.ReadAllText((args.Length > 0 && args[0] != string.Empty) ? args[0] : inputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new string[2];
            }

            string[] solution = new string[2];

            //Solution, better way to do this
            List<char> currentString = new List<char>();
            int fileIndex = 0;
            for (; fileIndex < fileInput.Length; fileIndex++)
            {
                currentString.Add(fileInput[fileIndex]);
                if (currentString.Count == 4 && AreCharsDifferent(currentString)) { solution[0] = (fileIndex+1).ToString(); break; }
                if (currentString.Count >= 4) currentString.RemoveAt(0);
            }

            //Pt2, use the same string for ease :D
            for (fileIndex=fileIndex++; fileIndex < fileInput.Length; fileIndex++)
            {
                currentString.Add(fileInput[fileIndex]);
                if (currentString.Count == 14 && AreCharsDifferent(currentString)) { solution[1] = (fileIndex+1).ToString(); break; }
                if (currentString.Count >= 14) currentString.RemoveAt(0);
            }

            return solution;
        }

        bool AreCharsDifferent(List<char> toCheck)
        {
            bool result = true;
            for (int i=0; i < toCheck.Count; i++)
            {
                char checkChar = toCheck[i];
                toCheck[i] = ',';
                if (toCheck.Contains(checkChar)) result = false;
                toCheck[i] = checkChar;
            }
            return result;
        }
    }
}