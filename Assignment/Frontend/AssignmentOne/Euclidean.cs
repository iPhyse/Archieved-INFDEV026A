using Microsoft.Xna.Framework;
using System;

namespace Frontend.AssignmentOne
{
    public static class Euclidean
    {
        //Euclidean distance between vector house and buidling, square root( square( x.house - x.building ) + square( y.house - y.building ) )
        public static double Distance(Vector2 house, Vector2 building)
        {
            var x = Math.Pow(house.X - building.X, 2);
            var y = Math.Pow(house.Y - building.Y, 2);
            var distance = Math.Sqrt(x + y);
            return distance;
        }
    }
}
