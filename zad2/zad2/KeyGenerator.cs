using System;
using System.Collections.Generic;
using System.Linq;

namespace zad2
{
    [Serializable]
    public class KeyGenerator
    {
        private List<int> w = new List<int>();
        private int q, r;   // w, q and r constitute the private key

        public List<int> PublicKey { get; set; } = new List<int>();

        public KeyGenerator(int keySize = 8)
        {
            GeneratePrivateKey(keySize);
            GeneratePublicKey();
        }

        public KeyGenerator(List<int> w, int q, int r)
        {
            this.w = w;
            this.q = q;
            this.r = r;
            GeneratePublicKey();
        }

        public KeyGenerator(List<int> publicKey)
        {
            PublicKey = publicKey;
        }

        private List<int> GetPrimes(int r)
        {
            List<int> primes = Enumerable.Range(0, r + 1).ToList();
            for (int i = 2; i <= Math.Sqrt(r); i++)
            {
                if (primes[i] != i)
                {
                    continue;
                }
                for (int j = i * i; j <= r; j += i)
                {
                    if (primes[j] < i)
                    {
                        continue;
                    }
                    primes[j] = i;
                }
            }
            return primes;
        }

        public int GetRandomCoprime(int n)
        {
            int copy = n;
            List<int> primes = GetPrimes(n);
            List<int> factors = new List<int>();
            while (primes[n] != n)
            {
                factors.Add(primes[n]);
                n /= primes[n];
            }
            factors.Add(n);
            List<int> ret = Enumerable.Range(2, copy - 2).ToList();
            foreach (int item in factors)
            {
                for (int i = item; i < copy; i += item)
                {
                    ret[i - 2] = 0;
                }
            }
            ret.RemoveAll(x => x == 0);
            return ret[new Random().Next(ret.Count())];
        }

        private void GeneratePublicKey()
        {
            // calculate the public key, where publicKey[i] = w[i] * r % q
            w.ForEach(w => PublicKey.Add((w * r) % q));
        }

        private void GeneratePrivateKey(int keySize)
        {
            Random rnd = new Random();
            // generate superincreasing sequence
            w.Add(rnd.Next(2));
            for (int i = 1; i < keySize; i++)
            {
                w.Add(w.Last() + rnd.Next(2));
            }
            // pick a number bigger than the sum of w
            q = rnd.Next(w.Sum(), w.Sum() + (int)Math.Sqrt(w.Sum()));
            // from range [1, q), pick a number that is coprime to q
            r = GetRandomCoprime(q);
        }

        public Tuple<List<int>,int,int> GetPrivateKey()
        {
            return new Tuple<List<int>, int, int>(w, q, r);
        }

        public int Inverse(int a, int n)
        {
            int t, r, newt, newr;
            t = 0; newt = 1;
            r = n; newr = a;
            while (newr != 0)
            {
                int quotient = r / newr;
                int temp = t;
                t = newt;
                newt = temp - quotient * newt;
                temp = r;
                r = newr;
                newr = temp - quotient * newr;
            }
            if (t < 0)
            {
                t += n;
            }
            return t;
        }
    }
}
