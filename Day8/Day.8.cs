using System;
using System.IO;
using System.Linq;

namespace AOC22
{
    public class Day8 : Solution
    {
        string inputFile = "Day8/Puzzle.txt";

        public class Vector2
        {
            public int x;
            public int y;

            public Vector2(int newX=0, int newY=0)
            {
                x = newX;
                y = newY;
            }

            public static Vector2 operator +(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x + v2.x, v1.y + v2.y);
            }

            public static Vector2 operator -(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x - v2.x, v1.y - v2.y);
            }

            public static Vector2 operator *(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x * v2.x, v1.y * v2.y);
            }

            public static Vector2 operator /(Vector2 v1, Vector2 v2)
            {
                return new Vector2(v1.x / v2.x, v1.y / v2.y);
            }

            public static bool operator ==(Vector2 v1, Vector2 v2)
            {
                return (v1.x == v2.x && v1.y == v2.y);
            }
            
            public static bool operator !=(Vector2 v1, Vector2 v2)
            {
                return !(v1.x == v2.x && v1.y == v2.y);
            }

            // override object.Equals
            public override bool Equals(object? obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }
                return this == (Vector2)obj;
            }
            
            // override object.GetHashCode
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override string ToString()
            {
                return $"{x.ToString()},{y.ToString()}";
            }
        }

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
            maxCoords.y = fileInput.Length;
            for (int i=0; i < fileInput.Length; i++) // Y
            {
                if (maxCoords.x == 0) maxCoords.x = fileInput[i].Length;
                for (int j=0; j < fileInput[i].Length; j++) // X
                {
                    map.Add(new Vector2(j, i).ToString(), Int32.Parse(fileInput[i][j].ToString()));
                }
            }

            int visibleTrees = (maxCoords.x*2) + (maxCoords.y*2) - 4;
            for (int i=1; i < maxCoords.y-1; i++) // Y
            {
                for (int j=1; j < maxCoords.x-1; j++) // X
                {
                    if (VisibleFromEdge(new Vector2(i, j))) visibleTrees++;
                }
            }
            solution[0] = visibleTrees.ToString();

            //PT2
            int topScore = 0;
            for (int i=0; i < maxCoords.y; i++)
            {
                for (int j=0; j < maxCoords.x; j++)
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
            return (gridPos.x == 0 || gridPos.x == maxCoords.x-1 || gridPos.y == 0 || gridPos.y == maxCoords.y-1);
        }
    }
}