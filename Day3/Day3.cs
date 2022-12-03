using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    public class Day3 : Solution
    {
        string inputFile = "Day3/Puzzle.txt";

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
            //Read by lines, split string in half, go through first, create array of items
            //go through second, if theres a reoccurance then break once we know which item it is
            //do this per rucksack, keeping a score
            int score = 0;
            for (int i=0; i < fileInput.Length; i++) //Each rucksack
            {
                string[] compartments = fileInput[i].Insert(fileInput[i].Length/2, ":").Split(':');
                char[] listItems = new char[compartments[0].Length];
                for (int j=0; j < compartments[0].Length; j++) listItems[j] = compartments[0][j];
                for (int k=0; k < compartments[1].Length; k++) 
                {
                    if (Array.Exists(listItems, element => element == compartments[1][k]))
                    {
                        //We take away 64 as ascii letters start, however, since upper and lower characters are flipped we wanna unflip them by checking and subtracting an 38 or 96
                        score += (int)compartments[1][k] - (char.IsUpper(compartments[1][k]) ? 38 : 96);
                        //Console.WriteLine($"Item '{compartments[1][k]}' was found as an error at score '{(int)compartments[1][k] - (char.IsUpper(compartments[1][k]) ? 38 : 96)}'");
                        break;
                    }
                }
            }
            solution[0] = score;

            //Pt2
            score = 0;
            
            for (int i=0; i < fileInput.Length/3; i++)
            {
                //i0 = [0+(3*i)]
                string[] groupRucksacks = new string[] { fileInput[0+(3*i)], fileInput[1+(3*i)], fileInput[2+(3*i)] };
                char[] firstRucksack = new char[groupRucksacks[0].Length];
                for (int j=0; j < groupRucksacks[0].Length; j++) firstRucksack[j] = groupRucksacks[0][j];
                List<char> secondRucksack = new List<char>();
                for (int k=0; k < groupRucksacks[1].Length; k++) 
                { if (Array.Exists(firstRucksack, element => element == groupRucksacks[1][k])) secondRucksack.Add(groupRucksacks[1][k]); }
                for (int l=0; l < groupRucksacks[2].Length; l++)
                { 
                    if (Array.Exists<char>(secondRucksack.ToArray(), element => element == groupRucksacks[2][l]))
                    {
                        score += (int)groupRucksacks[2][l] - (char.IsUpper(groupRucksacks[2][l]) ? 38 : 96);
                        break;
                    } 
                }
            }
            solution[1] = score;

            return solution;
        }
    }
}