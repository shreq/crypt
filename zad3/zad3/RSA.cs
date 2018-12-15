using System.IO;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace zad3
{
    public class RSA
    {
        public KeyGenerator Generator { get; set; }

        public RSA()
        {
            Generator = new KeyGenerator();
            Generator.GenerateKey();
        }

        public RSA(KeyGenerator generator)
        {
            Generator = generator;
        }

        public BigInteger GetSingature(BigInteger message)
        {
            var blindedHash = (message % Generator.N) * (BigInteger.ModPow(Generator.R, Generator.E, Generator.N));
            var blindedSignature = BigInteger.ModPow(blindedHash, Generator.D, Generator.N);
            var inverse_r = Utils.Inverse(Generator.R, Generator.N);
            var signature = blindedSignature * inverse_r % Generator.N;
            return signature;

        }

        public bool VerifySignature(string signatureString, BigInteger message)
        {
            BigInteger signature;
            if(BigInteger.TryParse(signatureString, out signature) == false)
            {
                return false;
            }
            var expected = BigInteger.ModPow(signature, Generator.E, Generator.N);
            return message.Equals(expected);
        }

        public static BigInteger GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            var hash = SHA256.Create().ComputeHash(bytes);
            return new BigInteger(hash);
        }

        public static BigInteger GetHashSha256(FileStream stream)
        {
            var hash = SHA256.Create().ComputeHash(stream);
            return new BigInteger(hash);
        }

    }
}
