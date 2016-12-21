using Microsoft.Xna.Framework;
using System;
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
            List<KeyValuePair<Vector2, double>> specialBuildingDistances = buildings.Select(building => new KeyValuePair<Vector2, double>(building, EuclideanDistance(house, building))).ToList();
            Console.WriteLine("--[Unsorted before Mergesort]--"); //output for debug
            for (int i = 0; i < specialBuildingDistances.Count; i++)
            {
                Console.WriteLine(specialBuildingDistances[i].Value); //output for debug
            }


            MergeSort(specialBuildingDistances, 0, specialBuildingDistances.Count - 1);

            Console.WriteLine("\n--[Euclidean and MergesSort]--"); //output for debug
            for (int i = 0; i < specialBuildingDistances.Count; i++)
            {
                buildings[i] = specialBuildingDistances[i].Key;
                Console.WriteLine(specialBuildingDistances[i].Value); //output for debug
            }

            return buildings;
            //return specialBuildings.OrderBy(v => Vector2.Distance(v, house));
        }

        private static IEnumerable<IEnumerable<Vector2>> FindSpecialBuildingsWithinDistanceFromHouse(IEnumerable<Vector2> specialBuildings,
                                                         IEnumerable<Tuple<Vector2, float>> housesAndDistances)
        {
            return
            from h in housesAndDistances
            select
            from s in specialBuildings
            where Vector2.Distance(h.Item1, s) <= h.Item2
            select s;
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
            List<List<Tuple<Vector2, Vector2>>> result = new List<List<Tuple<Vector2, Vector2>>>();
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
            return result;
        }

        //Assignment 1 Merge sort + euclidean distance
        private static void DoMerge(List<KeyValuePair<Vector2, double>> vectorList, int l, int m, int r)
        {
            int sizeLeft = m - l + 1;
            int sizeRight = r - m;

            var listLeft = new List<KeyValuePair<Vector2, double>>();
            var listRight = new List<KeyValuePair<Vector2, double>>();

            for (int i = 0; i < sizeLeft; i++)
            {
                listLeft.Add(vectorList[l + i]);
            }

            for (int i = 0; i < sizeRight; i++)
            {
                listRight.Add(vectorList[m + i + 1]);
            }

            listLeft.Add(new KeyValuePair<Vector2, double>(new Vector2(Single.MaxValue), Double.MaxValue));
            listRight.Add(new KeyValuePair<Vector2, double>(new Vector2(Single.MaxValue), Double.MaxValue));

            int indexLeft = 0;
            int indexRight = 0;

            for (int x = l; x <= r; x++)
            {
                if (listLeft[indexLeft].Value <= listRight[indexRight].Value)
                {
                    vectorList[x] = listLeft[indexLeft];
                    indexLeft++;
                }
                else
                {
                    vectorList[x] = listRight[indexRight];
                    indexRight++;
                }
            }

        }

        //Merge sort algorithm (recursive), as described "Use the merge sort as sorting algorithm"
        private static void MergeSort(List<KeyValuePair<Vector2, double>> input, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;

                MergeSort(input, l, m);
                MergeSort(input, m + 1, r);

                DoMerge(input, l, m, r);
            }
        }

        //euclidean distance between vector house and buidling, square root( square( x.house - x.building ) + square( y.house - y.building ) )
        private static double EuclideanDistance(Vector2 house, Vector2 building)
        {
            var x = Math.Pow(house.X - building.X, 2);
            var y = Math.Pow(house.Y - building.Y, 2);
            var distance = Math.Sqrt(x + y);
            return distance;
        }
        //END: Assignment 1

    }
}
