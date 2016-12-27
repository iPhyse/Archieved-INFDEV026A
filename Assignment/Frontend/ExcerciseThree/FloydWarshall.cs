using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace Frontend.ExcerciseThree
{
    public class FloydWarshall
    {
        private bool[,] adjacency;
        public int[,] distance;
        public Tuple<Vector2, Vector2>[,] predecessor;

        public FloydWarshall(Tuple<Vector2, Vector2>[] roads)
        {

            adjacency = new bool[roads.Length, roads.Length];
            distance = new int[roads.Length, roads.Length];
            predecessor = new Tuple<Vector2, Vector2>[roads.Length, roads.Length];

            for (int i = 0; i < roads.Length; i++)
            {
                for (int j = 0; j < roads.Length; j++)
                {
                    adjacency[i, j] = (roads[i].Item1.Equals(roads[j].Item2) || roads[i].Item2.Equals(roads[j].Item1));
                }
            }
            
            for (int i = 0; i < roads.Length; i++)
            {
                for (int j = 0; j < roads.Length; j++)
                {
                    if (i == j)
                    {
                        distance[i, j] = 0;
                        continue;
                    }
                    
                    if (adjacency[i, j])
                    {
                        predecessor[i, j] = roads[i];
                        distance[i, j] = 1;
                    }
                    else
                        distance[i, j] = Int16.MaxValue;
                }
            }

            // calculate shortes path
            int lengthOfRoads = roads.Length;
            for (int k = 0; k < lengthOfRoads; k++)
            {
                Console.Clear();
                Console.WriteLine("\nIteration {0} / {1}", k, lengthOfRoads);
                
                for (int i = 0; i < lengthOfRoads; i++)
                {
                    for (int j = 0; j < lengthOfRoads; j++)
                    {
                        if (distance[i, j] > distance[i, k] + distance[k, j])
                        {
                            distance[i, j] = distance[i, k] + distance[k, j];
                            predecessor[i, j] = roads[k];
                        }
                        
                    }
                }
            }
        }

    }
}
