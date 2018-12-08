using System;
using System.Collections.Generic;

namespace zad2
{
    [Serializable]
    public class KeyGenerator
    {
        private List<BigInteger> w = new List<BigInteger>();
        private BigInteger q, r;   // w, q and r constitute the private key

        public List<BigInteger> PublicKey { get; set; } = new List<BigInteger>();

        public KeyGenerator(int keySize = 8)
        {
            GeneratePrivateKey(keySize);
            GeneratePublicKey();
        }

        public KeyGenerator(List<BigInteger> w, BigInteger q, BigInteger r)
        {
            this.w = w;
            this.q = q;
            this.r = r;
            GeneratePublicKey();
        }

        public KeyGenerator(List<BigInteger> publicKey)
        {
            PublicKey = publicKey;
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
            w.Add(1);
            for (int i = 1; i < keySize; i++)
            {
                w.Add(w.Sum() + BigInteger.Random(1, 2));
            }
            // pick a number bigger than the sum of w
            q = BigInteger.Random(w.Sum(), 2 * w.Sum());
            // from range [1, q), pick a number that is coprime to q
            do
            {
                r = BigInteger.Random(1, q - 1);
            } while (BigInteger.Nwd(r, q)!=1);
        }

        public Tuple<List<BigInteger>,BigInteger,BigInteger> GetPrivateKey()
        {
            return new Tuple<List<BigInteger>, BigInteger, BigInteger>(w, q, r);
        }

        public BigInteger Inverse(BigInteger a, BigInteger n)
        {
            BigInteger t, r, newt, newr;
            t = 0; newt = 1;
            r = n; newr = a;
            while (newr != 0)
            {
                BigInteger quotient = r / newr;
                BigInteger temp = t;
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
