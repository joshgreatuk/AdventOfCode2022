using System;
using System.IO;
using System.Linq;

namespace AOC22
{
    public class Day7 : Solution
    {
        string inputFile = "Day7/Puzzle.txt";

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
            //We will read the list of commands to make a directory tree, then process that directory
            Dictionary<string, Directory> dirList = new Dictionary<string, Directory>();
            Directory currentDir = new Directory("/");
            dirList.Add("/", currentDir);

            for (int i=0; i < fileInput.Length; i++)
            {
                string[] line = fileInput[i].Split(' ');
                if (line[0] == "$")
                {
                    if (line[1] == "cd")
                    {
                        if (line[2] == ".." && currentDir.parentDir != string.Empty) 
                        {
                            if (!currentDir.addedToParent)
                            {
                                dirList[currentDir.parentDir].dirSize += currentDir.dirSize;
                                currentDir.addedToParent = true;
                            }
                            currentDir = dirList[currentDir.parentDir];
                        }
                        else if (line[2] != "..") 
                        {
                            if (currentDir.parentDir != string.Empty && !currentDir.addedToParent) 
                            {
                                dirList[currentDir.parentDir].dirSize += currentDir.dirSize;
                                currentDir.addedToParent = true;
                            }
                            currentDir = dirList[line[2]];
                        }
                    }
                }
                else if (line[0] == "dir") 
                {
                    if (!dirList.ContainsKey(line[1])) dirList.Add(line[1], new Directory(line[1], currentDir.dirName));
                }
                else 
                {
                    currentDir.dirSize += Int32.Parse(line[0]);
                    currentDir.fileList.Add(string.Join(" ", line));
                    currentDir.addedToParent = false;
                }
            }
            int totalSize = 0;
            foreach (Directory dir in dirList.Select(element => element.Value))
            {
                if (dir.dirSize <= 100000) totalSize += dir.dirSize;
            }
            solution[0] = totalSize.ToString();

            return solution;
        }
    }

    public class Directory
    {
        public string dirName = "";
        public string parentDir = "";
        public bool addedToParent = false;
        public List<string> fileList = new List<string>();
        public int dirSize = 0;

        public Directory(string name, string parent="")
        {
            dirName = name;
            parentDir = parent;
        }
    }
}