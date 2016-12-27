using Frontend.ExcerciseOne;
using Frontend.ExcerciseThree;
using Frontend.ExcerciseTwo;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EntryPoint
{
    public static class Program
    {
        static void Main()
        {
            var fullscreen = false;

            Console.WriteLine("Enter number of simulation to run - [1 - 4, q]");

            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        using (var game = VirtualCity.RunAssignment1(SortSpecialBuildingsByDistance,
                                                 fullscreen))
                            game.Run();
                        break;

                    case "2":
                        using (var game = VirtualCity.RunAssignment2(FindSpecialBuildingsWithinDistanceFromHouse,
                                                 fullscreen))
                            game.Run();
                        break;

                    case "3":
                        using (var game = VirtualCity.RunAssignment3(FindRoute, fullscreen))
                            game.Run();
                        break;

                    case "4":
                        using (var game = VirtualCity.RunAssignment4(FindRoutesToAll, fullscreen))
                            game.Run();
                        break;

                    case "q":
                        return;

                    default:
                        Console.WriteLine("Invalid input! Try again! - [1 - 4, q]");
                        break;
                }
            }
        }

        private static IEnumerable<Vector2> SortSpecialBuildingsByDistance(Vector2 house,
                                           IEnumerable<Vector2> specialBuildings)
        {
            var buildings = specialBuildings.ToList();

            //List of buildings and distances related to the building by the euclidean formula
            List<KeyValuePair<Vector2, double>> specialBuildingDistances = buildings.Select(building => new KeyValuePair<Vector2, double>(building, Euclidean.Distance(house, building))).ToList();
            
            ////START; output for debug, can be completely removed
            Console.WriteLine("--[Unsorted before Mergesort]--");
            for (int i = 0; i < specialBuildingDistances.Count; i++)
            {
                Console.WriteLine(specialBuildingDistances[i].Value); 
            }
            Console.WriteLine("\n--[Euclidean and MergesSort]--");
            ////END; output for debug

            MergeSort.Sort(specialBuildingDistances, 0, specialBuildingDistances.Count - 1);
            for (int i = 0; i < specialBuildingDistances.Count; i++)
            {
                buildings[i] = specialBuildingDistances[i].Key;
                Console.WriteLine(specialBuildingDistances[i].Value); //output for debug
            }

            return buildings;
            //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(IEnumerable<Vector2> specialBuildings, IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            List<List<Vector2>> foundInRange = new List<List<Vector2>>();

            //Create a new tree root and fill the tree with all building entities
            var root = new Node<Vector2>();
            foreach (Vector2 specialBuilding in specialBuildings)
            {
                root = Tree.Insert(specialBuilding, root, true);
                Console.WriteLine("building value : " + specialBuilding); //output for debug
            }
            
            foreach (var houseAndDistance in housesAndDistances)
            {
                List<Vector2> inRange = new List<Vector2>();
                Console.WriteLine("Building item 1: " + houseAndDistance.Item1 + ", Building item 2: " + houseAndDistance.Item2);
                InRange.GetBuildings(root, houseAndDistance.Item1, houseAndDistance.Item2, inRange);
                foundInRange.Add(inRange);

            }
            
            return foundInRange;

            /*
            return
            from h in housesAndDistances
            select
            from s in specialBuildings
            where Vector2.Distance(h.Item1, s) <= h.Item2
            select s;*/
        }

        private static IEnumerable<Tuple<Vector2, Vector2>> FindRoute(Vector2 startingBuilding,
                                          Vector2 destinationBuilding,
                                          IEnumerable<Tuple<Vector2, Vector2>> roads)
        {
            var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
            List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
            var prevRoad = startingRoad;
            for (int i = 0; i < 30; i++)
            {
                prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, destinationBuilding)).First());
                fakeBestPath.Add(prevRoad);
            }
            return fakeBestPath;
        }

        private static IEnumerable<IEnumerable<Tuple<Vector2, Vector2>>> FindRoutesToAll(Vector2 startingBuilding,
                                                 IEnumerable<Vector2> destinationBuildings,
                                                 IEnumerable<Tuple<Vector2,
                                                 Vector2>> roads)
        {

            FloydWarshall fwshall = new FloydWarshall(roads.ToArray());

            List<List<Tuple<Vector2, Vector2>>> routes = new List<List<Tuple<Vector2, Vector2>>>();

            IEnumerator roadsEnumerator = roads.GetEnumerator();
            int i = 0;
            while (roadsEnumerator.MoveNext())
            {
                if (((Tuple<Vector2, Vector2>)roadsEnumerator.Current).Item1 == startingBuilding)
                    break;
                i++;
            }

            IEnumerator destionation = destinationBuildings.GetEnumerator();
            while (destionation.MoveNext())
            {
                for (int j = 0; j < fwshall.predecessor.GetLength(1); j++)
                {
                    if (fwshall.predecessor[i, j].Item2 == (Vector2)destionation.Current)
                    {
                        Console.WriteLine("Distance from: " + startingBuilding + " -> " + destionation.Current + " = " + fwshall.distance[i, j]); //output for debug
                        List<Tuple<Vector2, Vector2>> temp = new List<Tuple<Vector2, Vector2>>();
                        temp.Add((Tuple<Vector2, Vector2>) destionation.Current);
                        routes.Add(temp);
                    }
                }
            }

            return new List<List<Tuple<Vector2, Vector2>>>();


            /*List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
            foreach (var d in destinationBuildings)
            {
                var startingRoad = roads.Where(x => x.Item1.Equals(startingBuilding)).First();
                List<Tuple<Vector2, Vector2>> fakeBestPath = new List<Tuple<Vector2, Vector2>>() { startingRoad };
                var prevRoad = startingRoad;
                for (int i = 0; i < 30; i++)
                {
                    prevRoad = (roads.Where(x => x.Item1.Equals(prevRoad.Item2)).OrderBy(x => Vector2.Distance(x.Item2, d)).First());
                    fakeBestPath.Add(prevRoad);
                }
                result.Add(fakeBestPath);
            }
            return result;*/
        }
    }
}
