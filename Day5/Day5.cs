using System;
using System.Linq;
using System.IO;

namespace AOC22
{
    public class Day5 : Solution
    {
        string inputFile = "Day5/Puzzle.txt";

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
            /*  
                We need to grab the container tree from the file, before reading the instructions
                to do this we will go through each line till we find just a number on index [1] of the line
                then read how many numbers there are in that line, make an array for each with the size
                of the lines we missed (i)
            */
            int fileIndex = 0;
            List<List<string>> containerStack = new List<List<string>>();
            while (fileInput.Length > fileIndex)
            {
                if (Int32.TryParse(fileInput[fileIndex][1].ToString(), out int temp))
                {
                    //We have found the numbers column of the container tree
                    for (int j=0; j < fileInput[fileIndex].Split("   ").Length; j++)
                    {
                        List<string> stackList = new List<string>();
                        for (int i=fileIndex-1; i >= 0; i--)
                        {
                            int lineIndex = 1+(4*j);
                            if (fileInput[i][lineIndex].ToString() != " ")
                            {
                                stackList.Add(fileInput[i][lineIndex].ToString());
                            }
                        }
                        containerStack.Add(stackList);
                    }   
                    break;
                }
                fileIndex++;
            }

            List<List<string>> savedContainerStack = new List<List<string>>();
            for (int i=0; i < containerStack.Count; i++) savedContainerStack.Add(new List<string>(containerStack[i].GetRange(0, containerStack[i].Count)));

            //Now read the instructions and move the crates
            fileIndex += 2; //To ensure we are at the instructions
            int[,] instructions = new int[fileInput.Length-fileIndex,3];
            for (int i=0; i < fileInput.Length-fileIndex; i++)
            {
                string[] instruction = fileInput[i+fileIndex].Split(new string[] {"move", "from", "to"}, StringSplitOptions.RemoveEmptyEntries);
                for (int j=0; j < instruction.Length; j++) instructions[i,j] = Int32.Parse(instruction[j]);
            }
            for (int i=0; i < instructions.Length/3; i++)
            {
                for (int j=0; j < instructions[i,0]; j++)
                {
                    string container = containerStack[instructions[i,1]-1].Last();
                    containerStack[instructions[i,2]-1].Add(container);
                    containerStack[instructions[i,1]-1].RemoveAt(containerStack[instructions[i,1]-1].Count-1);
                }
            }
            for (int i=0; i < containerStack.Count; i++) solution[0] += containerStack[i].Last();
            
            containerStack = savedContainerStack;

            //PT2
            for (int i=0; i < instructions.Length/3; i++)
            {
                string[] stack = new string[instructions[i,0]];
                for (int j=0; j < instructions[i,0]; j++)
                {
                    stack[instructions[i,0]-1-j] = containerStack[instructions[i,1]-1].Last();
                    containerStack[instructions[i,1]-1].RemoveAt(containerStack[instructions[i,1]-1].Count-1);
                }
                containerStack[instructions[i,2]-1].AddRange(stack);
            }
            for (int i=0; i < containerStack.Count; i++) solution[1] += containerStack[i].Last();

            return solution;
        }
    }
}