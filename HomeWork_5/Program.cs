using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework
{
    static class ListExtensions
    {
        private static readonly Random rng = new Random();

        public static void Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            List<string> firstList = new List<string>()
            {
                "image_1.png", "image_1.png",
                "image_2.png", "image_2.png",
                "image_3.png", "image_3.png",
                "image_4.png", "image_4.png",
                "image_5.png", "image_5.png",
                "image_6.png", "image_6.png",
                "image_7.png", "image_7.png",
                "image_8.png", "image_8.png",
                "image_9.png", "image_9.png",
                "image_10.png", "image_10.png",
                "image_11.png", "image_11.png",
                "image_12.png", "image_12.png",
                "image_13.png", "image_13.png",
                "image_14.png", "image_14.png",
                "image_15.png", "image_15.png",
                "image_16.png", "image_16.png",
                "image_17.png", "image_17.png",
                "image_18.png", "image_18.png",
                "image_19.png", "image_19.png",
                "image_20.png", "image_20.png",
                "image_21.png", "image_21.png",
                "image_22.png", "image_22.png",
                "image_23.png", "image_23.png",
                "image_24.png", "image_24.png",
                "image_25.png", "image_25.png",
                "image_26.png", "image_26.png",
                "image_27.png", "image_27.png",
                "image_28.png", "image_28.png",
                "image_29.png", "image_29.png",
                "image_30.png", "image_30.png",
                "image_31.png", "image_31.png",
                "image_32.png", "image_32.png"
            };

            List<string> secondList = new List<string>(firstList);

            var indexed = firstList
                .Select((value, index) => (value, index))
                .ToList();

            indexed.Shuffle();

            List<string> shuffled = indexed.Select(p => p.value).ToList();
            int[] originalIndices = indexed.Select(p => p.index).ToArray();

            Console.WriteLine("[" + string.Join(", ", originalIndices) + "]");
        }
    }
}