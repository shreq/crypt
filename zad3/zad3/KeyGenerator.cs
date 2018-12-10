using System.Numerics;
using System.Security.Cryptography;

namespace zad3
{
    public class KeyGenerator
    {
        public BigInteger E { get; set; } // public key
        public BigInteger N { get; set; } // public key
        public BigInteger D { get; set; } // private key
        public void GenerateKey()
        {
            BigInteger p, q, n, e, d, product;
            using (var rng = RandomNumberGenerator.Create())
            {
                var smallPrimes = Utils.GetPrimes(65000);
                do
                {
                    p = Utils.RandomInRange(rng, BigInteger.Pow(10, 100), BigInteger.Pow(10, 200));
                } while (!p.IsProbablePrime(40, smallPrimes));
                do
                {
                    q = Utils.RandomInRange(rng, BigInteger.Pow(10, 100), BigInteger.Pow(10, 200));
                } while (!q.IsProbablePrime(40, smallPrimes));
                n = p * q;
                product = (p - 1) * (q - 1);
                do
                {
                    e = Utils.RandomInRange(rng, 1, BigInteger.Pow(10, 100));
                } while (BigInteger.GreatestCommonDivisor(e, product) != 1);
            }
            d = Utils.Inverse(e, product);

            E = e; N = n; D = d;
        }
    }
}
