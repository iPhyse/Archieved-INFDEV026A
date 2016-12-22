using Frontend.ExcerciseOne;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Frontend.ExcerciseTwo
{
    public static class InRange
    {
        public static void GetBuildings(Node<Vector2> node, Vector2 target, float range, List<Vector2> inRange)
        {
            if (node != null)
            {
                if (Euclidean.Distance(node.Vector2, target) <= range)
                {
                    inRange.Add(node.Vector2);
                }
                GetBuildings(node.Left, target, range, inRange); //Recursive left nodes
                GetBuildings(node.Right, target, range, inRange); //Recursive right nodes
            }
        }
    }
}
