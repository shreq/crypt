using zad3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;

namespace zad3.Tests
{
    [TestClass()]
    public class RSATests
    {
        [TestMethod()]
        public void GetSingatureTest()
        {
            var rsa = new RSA();
            var message = "ala ma kota";
            BigInteger messageHash = RSA.GetHashSha256(message);
            var signature = rsa.GetSingature(messageHash);
            var expected = BigInteger.ModPow(messageHash, rsa.Generator.D, rsa.Generator.N);
            Assert.AreEqual(signature, expected);
        }

        [TestMethod()]
        public void VerifySignatureTest()
        {
            var rsa = new RSA();
            var message = "ala ma kota";
            BigInteger messageHash = RSA.GetHashSha256(message);
            var signature = rsa.GetSingature(messageHash);
            Assert.IsTrue(rsa.VerifySignature(signature.ToString(), messageHash));
        }
    }
}