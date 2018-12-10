using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;

namespace zad3
{
    public static class BigIntegerExtensions
    {
        public static bool IsProbablePrime(this BigInteger source, int certainty, List<int> smallPrimes)
        {
            if (source == 2 || source == 3)
            {
                return true;
            }

            if (source < 2 || source % 2 == 0)
            {
                return false;
            }

            foreach (var item in smallPrimes)
            {
                if(source%item == 0)
                {
                    return false;
                }
            }

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                a = Utils.RandomInRange(RandomNumberGenerator.Create(), 2, source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                {
                    continue;
                }

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                    {
                        return false;
                    }

                    if (x == source - 1)
                    {
                        break;
                    }
                }

                if (x != source - 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
