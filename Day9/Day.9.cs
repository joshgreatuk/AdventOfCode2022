using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AOC22
{
    public class Day9 : Solution
    {
        string inputFile = "Day9/Puzzle.txt";

        Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>()
        {
            {"U", new Vector2(0, 1)},
            {"D", new Vector2(0, -1)},
            {"R", new Vector2(1, 0)},
            {"L", new Vector2(-1, 0)}
        };

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
            //Read instructions one by one, for each movement, if the distance between 2 vectors is above 1 move the tail towards the head
            //we can move towards the head by finding the direction, which is tailPosition - headPosition, round x and y then apply it
            solution[0] = SimulateRope(1,fileInput).ToString();
            solution[1] = SimulateRope(9, fileInput).ToString();
            return solution;
        }

        public int SimulateRope(int knotCount, string[] instructions)
        {
            List<Vector2> rope = new List<Vector2>();
            for (int i=0; i < knotCount+1; i++) rope.Add(new Vector2()); //Add knots to the rope
            List<Vector2> visitedVectors = new List<Vector2>();
            foreach (string instruction in instructions)
            {
                string[] instructionSplit = instruction.Split(" ");
                for (int i=0; i < Int32.Parse(instructionSplit[1]); i++)
                {
                    rope[0] += directions[instructionSplit[0]];
                    for (int j=1; j < rope.Count; j++)
                    {
                        while (Vector2.Distance(rope[j-1], rope[j]) > 1.5f)
                        {
                            Vector2 direction = (rope[j-1] - rope[j]);
                            if (direction.X > 1.5f) direction.X = 1;
                            if (direction.Y > 1.5f) direction.Y = 1;
                            if (direction.X < -1.5f) direction.X = -1;
                            if (direction.Y < -1.5f) direction.Y = -1;
                            rope[j] += new Vector2((float)Math.Round(direction.X), (float)Math.Round(direction.Y));
                            if (!visitedVectors.Contains(rope.Last())) visitedVectors.Add(rope.Last());
                        }
                    }
                }
            }
            return visitedVectors.Count;
        }
    }
} 