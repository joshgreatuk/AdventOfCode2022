using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    public class Day1 : Solution
    {
        string inputFile = "Day1/PuzzleInput.txt";

        public override string[] GetSolution(string[] args)
        {
            string fileInput = "";
            try
            {
                fileInput = File.ReadAllText((args.Length > 0 && args[0] != string.Empty) ? args[0] : inputFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new string[2];
            }

            string[] elfCalories = fileInput.Replace("\r", string.Empty).Split("\n\n");
            float[] elfTotalCalories = elfCalories.Select<string, float>(x => Array.ConvertAll(x.Split("\n"), float.Parse).Sum()).ToArray();

            string[] solution = new string[2];
            solution[0] = GetTopScoresSum((float[])elfTotalCalories.Clone(), 1).ToString();
            solution[1] = GetTopScoresSum((float[])elfTotalCalories.Clone(), 3).ToString();

            return solution;
        }

        public float GetTopScoresSum(float[] scores, int topScoresToAdd)
        {    
            float scoreTotal = 0;
            for (int i=0; i < topScoresToAdd; i++)
            {   
                float bestScore = scores.Aggregate((result, value) => result = (value > result) ? value : result);
                scores[Array.IndexOf(scores, bestScore)] = 0 ;
                scoreTotal += bestScore;
            }
            return scoreTotal;
        }
    }
}