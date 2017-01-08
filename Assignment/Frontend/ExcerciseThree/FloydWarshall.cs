using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace Frontend.ExcerciseThree
{
    public class FloydWarshall
    {
        private bool[,] adjacency;
        public int[,] distance { get; private set; }
        public Tuple<Vector2, Vector2>[,] predecessor { get; private set; }

        public FloydWarshall(Tuple<Vector2, Vector2>[] roads)
        {

            adjacency = new bool[roads.Length, roads.Length];
            distance = new int[roads.Length, roads.Length];
            predecessor = new Tuple<Vector2, Vector2>[roads.Length, roads.Length];

            int v = roads.Length;

            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    adjacency[i, j] = (roads[i].Item1.Equals(roads[j].Item2) || roads[i].Item2.Equals(roads[j].Item1));
                }
            }
            
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
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
            
            for (int k = 0; k < v; k++)
            {
                Console.Clear();
                //Console.WriteLine("\nIteration {0} / {1}", k, v);
                Console.WriteLine("Progress " + ((100f / v) * k) + "%");
                for (int i = 0; i < v; i++)
                {
                    for (int j = 0; j < v; j++)
                    {
                        int dist = distance[i, k] + distance[k, j];
                        if (dist < distance[i, j])
                        {
                            Console.WriteLine("distance: " + dist);
                            distance[i, j] = dist;
                            predecessor[i, j] = roads[k];
                        }
                        
                    }
                }
            }
        }

        public void SaveAdjacencyToFile()
        {
            using (StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\adjacency.txt"))
            {
                for (int i = 0; i < adjacency.GetLength(0); i++)
                {
                    for (int j = 0; j < adjacency.GetLength(1); j++)
                    {
                        if (adjacency[i, j])
                            file.Write("1 ");
                        else
                            file.Write("0 ");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }

        public void SaveDistanceToFile()
        {
            using (StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\distance.txt"))
            {
                for (int i = 0; i < distance.GetLength(0); i++)
                {
                    for (int j = 0; j < distance.GetLength(1); j++)
                    {
                        file.Write(distance[i, j] + " ");
                    }
                    file.Write(Environment.NewLine);
                }
            }
        }

        public void SavePredecessorToFile()
        {
            using (StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\predecessor.txt"))
            {
                for (int i = 0; i < predecessor.GetLength(0); i++)
                {
                    for (int j = 0; j < predecessor.GetLength(1); j++)
                    {
                        if (predecessor[i, j] != null)
                            file.Write(predecessor[i, j] + " ");
                        else
                            file.Write(" -  ");
                    }
                    file.Write(Environment.NewLine);                                                                
                }
            }
        }
    }
}
