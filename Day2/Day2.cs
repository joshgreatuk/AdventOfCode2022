using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    /*
        A X = 0 DRAW
        A Y = 1 WIN
        A Z = 2 LOSS

        B X = -1 LOSS
        B Y = 0 DRAW
        B Z = 1 WIN

        C X = -2 WIN
        C Y = -1 LOSS
        C Z = 0 DRAW

        PT2
        A Y = DRAW = A
        B X = LOSE = A
        C Z = WIN = A
    */

    public enum RPS
    {
        A = 1, //Rock
        X = 1,
        B = 2, //Paper
        Y = 2,
        C = 3, //Scissors
        Z = 3
    }

    public enum RESULT
    {
        LOSE = 1,
        DRAW = 2,
        WIN = 3
    }

    public class Day2 : Solution
    {
        string inputFile = "Day2/PuzzleInput.txt";

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

            string[] solution = new string[2];

            // //Solution Pt1 Method 1
            // float score = 0;
            // string[] inputLines = fileInput.Replace("\r", "").Split('\n');
            // for (int i=0; i < inputLines.Length; i++)
            // {
            //     string[] shapes = inputLines[i].Split(" ");
            //     RPS opponentShape = (RPS)Enum.Parse(typeof(RPS), shapes[0]);
            //     int shapeToPlay = (int)Enum.Parse(typeof(RPS), shapes[1]);

            //     switch (opponentShape)
            //     {
            //         case RPS.A:
            //             score += (int)Enum.Parse(typeof(RPS), shapes[1]) + ((shapeToPlay == (int)RPS.Y) ? 6 : (shapeToPlay == (int)RPS.X) ? 3 : 0);
            //             break;
            //         case RPS.B:
            //             score += (int)Enum.Parse(typeof(RPS), shapes[1]) + ((shapeToPlay == (int)RPS.Z) ? 6 : (shapeToPlay == (int)RPS.Y) ? 3 : 0);
            //             break;
            //         case RPS.C:
            //             score += (int)Enum.Parse(typeof(RPS), shapes[1]) + ((shapeToPlay == (int)RPS.X) ? 6 : (shapeToPlay == (int)RPS.Z) ? 3 : 0);
            //             break;
            //     }
            // }
            // solution[0] = score;

            // //Solution Pt1 Method 2 (Pt1 Only)
            // float score = 0;
            // string[] inputLines = fileInput.Replace("\r", "").Split('\n');
            // for (int i=0; i < inputLines.Length; i++)
            // {
            //     string[] shapes = inputLines[i].Split(" ");
            //     int opponentShape = (int)Enum.Parse(typeof(RPS), shapes[0]);
            //     int shapeToPlay = (int)Enum.Parse(typeof(RPS), shapes[1]);
            //     int difference = shapeToPlay - opponentShape;
            //     if (difference == 1 || difference == -2) score += shapeToPlay + 6; //WIN
            //     else if (difference == 2 || difference == -1) score += shapeToPlay; //LOSS
            //     else score += shapeToPlay + 3; //DRAW
            // }
            // solution[0] = score;

            //Solution Pt1 Method 3 (For Pt2)
            float score = 0;
            string[] inputLines = fileInput.Replace("\r", "").Split('\n');
            for (int i=0; i < inputLines.Length; i++)
            {
                string[] shapes = inputLines[i].Split(" ");
                score += GetRPSOutcome((RPS)Enum.Parse(typeof(RPS), shapes[0]), (RPS)Enum.Parse(typeof(RPS), shapes[1]));   
            }
            solution[0] = score.ToString();

            //Solution Pt2 (A mess :P)
            score = 0;
            for (int i=0; i < inputLines.Length; i++)
            {
                string[] shapes = inputLines[i].Split(" ");
                RPS opponentShape = (RPS)Enum.Parse(typeof(RPS), shapes[0]);
                RESULT expectedOutcome = (RESULT)((int)Enum.Parse(typeof(RPS), shapes[1]));
                RPS yourShape = (expectedOutcome == RESULT.WIN) ? (RPS)((int)opponentShape + 1) : (expectedOutcome == RESULT.LOSE) ? (RPS)((int)opponentShape - 1): opponentShape;
                score += GetRPSOutcome(opponentShape, ((int)yourShape == 4) ? (RPS)1 : ((int)yourShape == 0) ? (RPS)3 : yourShape);
            }
            solution[1] = score.ToString();

            //I accidentally swapped X and Y for this one, it took me a while and 2 methods to figure that out. . .
            return solution;
        }
        
        private float GetRPSOutcome(RPS opponentShape, RPS yourShape)
        {
            int difference = (int)yourShape - (int)opponentShape;
            if (difference == 1 || difference == -2) return (int)yourShape + 6; //WIN
            else if (difference == 2 || difference == -1) return (int)yourShape; //LOSS
            else return (int)yourShape + 3; //DRAW
        }
    }
}