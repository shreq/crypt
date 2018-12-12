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
            var signature = rsa.GetSingature(message);
            BigInteger messageHash = RSA.GetHashSha256(message);
            var expected = BigInteger.ModPow(messageHash, rsa.Generator.D, rsa.Generator.N);
            Assert.AreEqual(signature, expected);
        }
    }
}