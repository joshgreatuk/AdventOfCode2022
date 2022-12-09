using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace AOC22
{
    public class Day8 : Solution
    {
        string inputFile = "Day8/Puzzle.txt";

        Dictionary<string, Vector2> directions = new Dictionary<string, Vector2>()
        {
            {"DOWN", new Vector2(0, 1)},
            {"UP", new Vector2(0, -1)},
            {"LEFT", new Vector2(-1, 0)},
            {"RIGHT", new Vector2(1, 0)}
        };

        Vector2 maxCoords = new Vector2();
        Dictionary<string, int> map = new Dictionary<string, int>();

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
            //Read map, then go through values from 1 - max-2 coords and fire VisibleFromEdge for each, count visible trees
            maxCoords.Y = fileInput.Length;
            for (int i=0; i < fileInput.Length; i++) // Y
            {
                if (maxCoords.X == 0) maxCoords.X = fileInput[i].Length;
                for (int j=0; j < fileInput[i].Length; j++) // X
                {
                    map.Add(new Vector2(j, i).ToString(), Int32.Parse(fileInput[i][j].ToString()));
                }
            }

            float visibleTrees = (maxCoords.X*2) + (maxCoords.Y*2) - 4;
            for (int i=1; i < maxCoords.Y-1; i++) // Y
            {
                for (int j=1; j < maxCoords.X-1; j++) // X
                {
                    if (VisibleFromEdge(new Vector2(i, j))) visibleTrees++;
                }
            }
            solution[0] = visibleTrees.ToString();

            //PT2
            int topScore = 0;
            for (int i=0; i < maxCoords.Y; i++)
            {
                for (int j=0; j < maxCoords.X; j++)
                {
                    int currentScore = GetTreeScore(new Vector2(j, i));
                    if (currentScore > topScore) topScore = currentScore;
                }
            }
            solution[1] = topScore.ToString();

            return solution;
        }

        bool VisibleFromEdge (Vector2 gridPos)
        {
            bool result = false;
            foreach (Vector2 dir in directions.Values) if (VisibleInDirection(gridPos, dir)) result = true;
            return result;
        }

        bool VisibleInDirection(Vector2 gridPos, Vector2 direction)
        {
            bool result = true;
            Vector2 currentPos = gridPos + direction;
            int gridPosHeight = map[gridPos.ToString()];
            while (!IsEdge(currentPos-direction))
            {
                int currentPosHeight = map[(currentPos).ToString()];
                if (currentPosHeight >= gridPosHeight) 
                {
                    result = false;
                    break;
                }
                currentPos += direction;
            }
            return result;
        }

        int GetTreeScore(Vector2 gridPos)
        {
            int score = 0;
            foreach (Vector2 dir in directions.Values)
            {
                if (score == 0) score = GetViewDistance(gridPos, dir);
                else score *= GetViewDistance(gridPos, dir);
            }
            return score;
        }

        int GetViewDistance(Vector2 gridPos, Vector2 direction)
        {
            int result = 0;
            Vector2 currentPos = gridPos + direction;
            int gridPosHeight = map[gridPos.ToString()];
            while (!IsEdge(currentPos-direction))
            {
                int currentPosHeight = map[(currentPos).ToString()];
                if (currentPosHeight >= gridPosHeight) 
                {
                    result++;
                    break;
                }
                result++;
                currentPos += direction;
            }
            return result;
        }

        bool IsEdge(Vector2 gridPos)
        {
            return (gridPos.X == 0 || gridPos.X == maxCoords.X-1 || gridPos.Y == 0 || gridPos.Y == maxCoords.Y-1);
        }
    }
}