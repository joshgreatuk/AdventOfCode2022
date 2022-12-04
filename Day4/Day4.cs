using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    public class Day4 : Solution
    {
        string inputFile = "Day4/Puzzle.txt";

        public override float[] GetSolution(string[] args)
        {
            string[] fileInput;
            try
            {
                fileInput = File.ReadAllLines((args.Length > 0 && args[0] != string.Empty) ? args[0] : inputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new float[2];
            }
            float[] solution = new float[2];

            //Solution
            int conflictPairs = 0;
            for (int i=0; i < fileInput.Length; i++)
            {
                //Ensure we that index 0 is the bigger range so the maths will work
                string[] pairsFull = fileInput[i].Split(",");
                int[] pairA = Array.ConvertAll(pairsFull[0].Split("-"), Int32.Parse);
                int[] pairB = Array.ConvertAll(pairsFull[1].Split("-"), Int32.Parse);
                if (pairA[1] - pairA[0] < pairB[1] - pairB[0])
                {
                    int[] tempPair = pairB;
                    pairB = pairA;
                    pairA = tempPair;
                }
                if (pairA[0] <= pairB[0] && pairA[1] >= pairB[1]) conflictPairs++;
            }
            solution[0] = conflictPairs;

            //Pt2
            conflictPairs = 0;
            for (int i=0; i < fileInput.Length; i++)
            {
                string[] pairsFull = fileInput[i].Split(",");
                int[] pairA = Array.ConvertAll(pairsFull[0].Split("-"), Int32.Parse);
                int[] pairB = Array.ConvertAll(pairsFull[1].Split("-"), Int32.Parse);
                if (pairA[0] > pairB[0])
                {
                    int[] tempPair = pairB;
                    pairB = pairA;
                    pairA = tempPair;
                }
                if (pairB[0] - pairA[1] <= 0) conflictPairs++;
            }
            solution[1] = conflictPairs;

            return solution;
        }
    }
}