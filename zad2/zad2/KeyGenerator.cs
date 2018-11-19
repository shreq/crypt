using System;
using System.Collections.Generic;
using System.Linq;

namespace zad2
{
    public class KeyGenerator
    {
        private List<int> publicKey = new List<int>();
        private List<int> w = new List<int>();
        private int q, r; //w, q and r constitute the private key
        private readonly int keySize;

        public KeyGenerator(int keySize = 8)
        {
            this.keySize = keySize;
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

        public List<int> GetCoprimes(int n)
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
            return ret;
        }


        public List<int> GetPublicKey()
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
            r = GetCoprimes(q)[rnd.Next(GetCoprimes(q).Count())];
            // calculate the public key, where publicKey[i] = w[i] * r % q
            w.ForEach(w => publicKey.Add(w * r % q));
            return publicKey;
        }

        public void GetPrivateKey(out List<int> w, out int q, out int r)
        {
            w = this.w;
            q = this.q;
            r = this.r;
        }

        public int inverse(int a, int n)
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
