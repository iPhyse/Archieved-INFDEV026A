using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

class Graph
{
    Dictionary<Vector2, Dictionary<Vector2, int>> vertices = new Dictionary<Vector2, Dictionary<Vector2, int>>();

    public void AddNode(Vector2 location)
    {
        if (!vertices.ContainsKey(location))
        {
            Dictionary<Vector2, int> emptyRoadsList = new Dictionary<Vector2, int>();
            vertices[location] = emptyRoadsList;
        }
    }
    public void AddRoad(Vector2 roadpoint1, Vector2 roadpoint2, int length)
    {
        if (!vertices[roadpoint1].ContainsKey(roadpoint2))
        {
            vertices[roadpoint1].Add(roadpoint2, length);
        }
        if (!vertices[roadpoint2].ContainsKey(roadpoint1))
        {
            vertices[roadpoint2].Add(roadpoint1, length);
        }
    }
    public Dictionary<Vector2, Dictionary<Vector2, int>> getVertices()
    {
        return vertices;
    }

    public List<Tuple<Vector2, Vector2>> ShortestPath(Vector2 startPoint, Vector2 endPoint)
    {
        var previous = new Dictionary<Vector2, Vector2>();
        var distances = new Dictionary<Vector2, int>();
        var nodes = new List<Vector2>();

        List<Tuple<Vector2, Vector2>> path = null;

        foreach (var node in vertices)
        {
            if (node.Key == startPoint)
            {
                distances[node.Key] = 0;
            }
            else
            {
                distances[node.Key] = int.MaxValue;
            }
            nodes.Add(node.Key);
        }

        while (nodes.Count != 0)
        {
            // 
            nodes.Sort((x, y) => distances[x] - distances[y]);

            // 
            var smallest = nodes[0];
            // 
            nodes.Remove(smallest);

            // If the shortestroute equals the endpoint, it means the end is reached.
            if (smallest == endPoint)
            {
                path = new List<Tuple<Vector2, Vector2>>();
                while (previous.ContainsKey(smallest))
                {
                    Tuple<Vector2, Vector2> pair;
                    pair = new Tuple<Vector2, Vector2>(smallest, previous[smallest]);
                    path.Add(pair);
                    smallest = previous[smallest];
                }
                //path.Add(startPoint);
                break;
            }

            // If there the smallest distance equals the int.MaxValue
            // That means there is no connection available from this location.
            if (distances[smallest] == int.MaxValue)
            {
                break;
            }

            // For each node connected to the current node by a road
            foreach (var neighbor in vertices[smallest])
            {
                // Calculate he totalDistance to the node/vector/coordinate
                var totalDistance = distances[smallest] + neighbor.Value;
                if (totalDistance < distances[neighbor.Key])
                {
                    // Replace the int.MaxValue into totalDistance from start to the neighbor key
                    distances[neighbor.Key] = totalDistance;
                    // Set the last road to the shortest distance
                    previous[neighbor.Key] = smallest;
                }
            }
        }
        return path;
    }
}