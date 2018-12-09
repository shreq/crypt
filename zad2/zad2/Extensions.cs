using System.Collections.Generic;
using System.Linq;

namespace zad2
{
    public static class Extensions
    {
        /// <summary>
        /// Split container into chunks of desired size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static List<List<T>> Split<T>(this IEnumerable<T> collection, int size)
        {
            var chunks = new List<List<T>>();
            var count = 0;
            var temp = new List<T>();

            foreach (var element in collection)
            {
                if (count++ == size)
                {
                    chunks.Add(temp);
                    temp = new List<T>();
                    count = 1;
                }

                temp.Add(element);
            }

            chunks.Add(temp);
            return chunks;
        }

        /// <summary>
        /// Fills List up to desired size with given value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        public static void FillToSize<T>(this List<T> collection, int size, T value)
        {
            int count = collection.Count == 0 ? size : size - collection.Count % size;
            collection.AddRange(Enumerable.Repeat(value, count));
        }

        /// <summary>
        /// Sum of all items in List
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static BigInteger Sum(this List<BigInteger> collection)
        {
            BigInteger sum = 0;
            collection.ForEach(item => sum += item);
            return sum;
        }
    }
}
