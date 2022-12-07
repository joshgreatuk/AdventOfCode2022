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

            //Read the directory tree
            Directory rootDirectory = new Directory("/");
            Directory currentDirectory = rootDirectory;
            for (int i=0; i < fileInput.Length; i++)
            {
                string[] line = fileInput[i].Split(" ");
                if (line[0] == "$")
                {
                    if (line[1] == "cd")
                    {
                        if (line[2] == ".." && currentDirectory.parentDir != null)
                        {
                            currentDirectory = currentDirectory.parentDir;
                        }
                        else if (line[2] != ".." && currentDirectory.dirList.Count > 0)
                        {
                            currentDirectory = (Directory)currentDirectory.dirList.Aggregate((result, element) => result = (element.dirName == line[2]) ? element : result);
                        }
                    }
                }
                else if (line[0] == "dir")
                {
                    currentDirectory.dirList.Add(new Directory(line[1], currentDirectory));
                }
                else
                {
                    currentDirectory.fileList.Add(string.Join(" ", line));
                    currentDirectory.dirSize += Int32.Parse(line[0]);
                }
            }

            //Go through tree and add directories together
            rootDirectory.dirSize += AddDirsTogether(rootDirectory);

            //Go through tree again to get what we want
            solution[0] = FindTotalOfDirsBelow(rootDirectory, 100000).ToString();
            int totalSpace = 70000000;
            int freeSpace = totalSpace - rootDirectory.dirSize;
            int neededSpace = 30000000 - freeSpace;
            solution[1] = FindDirsAbove(rootDirectory, neededSpace).Aggregate((result, element) => result = (element.dirSize < result.dirSize) ? element : result).dirSize.ToString();

            return solution;
        }

        int AddDirsTogether(Directory rootElement)
        {
            int totalSize = 0;
            foreach (Directory dir in rootElement.dirList)
            {
                if (dir.dirList.Count > 0) dir.dirSize += AddDirsTogether(dir);
                totalSize += dir.dirSize;
            }
            return totalSize;
        }

        int FindTotalOfDirsBelow(Directory rootElement, int maxSize)
        {
            int totalSize = 0;
            foreach (Directory dir in rootElement.dirList)
            {
                if (dir.dirList.Count > 0) totalSize += FindTotalOfDirsBelow(dir, maxSize);
                if (dir.dirSize <= maxSize) totalSize += dir.dirSize;
            }
            return totalSize;
        }

        List<Directory> FindDirsAbove(Directory rootElement, int minSize)
        {
            List<Directory> dirs = new List<Directory>();
            foreach (Directory dir in rootElement.dirList)
            {
                if (dir.dirList.Count > 0) dirs.AddRange(FindDirsAbove(dir, minSize));
                if (dir.dirSize > minSize) dirs.Add(dir);
            }
            return dirs;
        }
    }

    public class Directory
    {
        public string dirName = "";
        public Directory? parentDir = null;
        public List<Directory> dirList = new List<Directory>();
        public List<string> fileList = new List<string>();
        public int dirSize = 0;

        public Directory(string name, Directory? parent=null)
        {
            dirName = name;
            parentDir = parent;
        }
    }
}