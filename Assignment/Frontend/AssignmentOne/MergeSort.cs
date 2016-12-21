using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Frontend.AssignmentOne
{
    public static class MergeSort
    {
        //Merge sort algorithm (recursive), as described "Use the merge sort as sorting algorithm"
        public static void Sort(List<KeyValuePair<Vector2, double>> input, int l, int r)
        {
            if (l < r)
            {
                int m = (l + r) / 2;

                Sort(input, l, m);
                Sort(input, m + 1, r);

                DoMerge(input, l, m, r);
            }
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

        
    }
}
