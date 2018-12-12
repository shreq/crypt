using System.Numerics;
using System.Security.Cryptography;

namespace zad3
{
    public class KeyGenerator
    {
        public BigInteger N { get; set; } // public key
        public BigInteger D { get; set; } // private key
        public int E { get; set; } // public key
        public BigInteger R { get; set; } // blinding factor
        public void GenerateKey()
        {
            int e;
            BigInteger p, q, n, r, d, product;
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
                    e = new System.Random().Next();
                } while (BigInteger.GreatestCommonDivisor(e, product) != 1 && e > 0);
                do
                {
                    r = Utils.RandomInRange(rng, 1, BigInteger.Pow(2, 64));
                } while (BigInteger.GreatestCommonDivisor(r, n) != 1);
            }
            d = Utils.Inverse(e, product);

            E = e; N = n; D = d; R = r;
        }
    }
}
