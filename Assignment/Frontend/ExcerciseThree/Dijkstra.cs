using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Frontend.ExcerciseThree
{
    class Dijkstra
    {
        
        public static Graph insertGraph(List<Tuple<Vector2, Vector2>> roads, Vector2 startLocation, Vector2 endLocation)
        {
            Graph graph = new Graph();
            graph.AddNode(startLocation);

            for (int i = 0; i < roads.Count; i++)
            {
                graph.AddNode((Vector2)roads[i].Item1); //First point
                graph.AddNode((Vector2)roads[i].Item2); //Secont point
            }

            graph.AddNode(endLocation);

            for (int i = 0; i < roads.Count; i++)
            {
                Vector2 item1 = roads[i].Item1; //vector 1
                Vector2 item2 = roads[i].Item2; //vector 2
                int distance = calculateDistance(item1, item2);

                graph.AddRoad(item1, item2, distance);
            }

            return graph;
        }

        private static int calculateDistance(Vector2 start, Vector2 end)
        {
            int result = (int)Vector2.Distance(start, end);
            return result;
        }
    }
}
