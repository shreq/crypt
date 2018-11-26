using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2
{
    public static class Extensions
    {
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

        public static void FillToSize<T>(this List<T> collection, int size, T value)
        {
            int count = collection.Count == 0 ? size : size - collection.Count % size;
            collection.AddRange(Enumerable.Repeat(value, count));
        }

        public static BigInteger Sum(this List<BigInteger> collection)
        {
            BigInteger sum = 0;
            collection.ForEach(item => sum += item);
            return sum;
        }
    }
}
