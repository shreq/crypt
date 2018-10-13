using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1
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

        public static void XorWithList(List<bool> collection, List<bool> xor)
        {
            if (!collection.Count().Equals(xor.Count()))
                throw new Exception();

            for (int i = 0; i < collection.Count(); i++)
                collection[i] ^= xor[i];
        }
    }
}
