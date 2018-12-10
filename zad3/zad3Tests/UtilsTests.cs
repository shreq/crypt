using zad3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Numerics;

namespace zad3.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void GetPrimesTest()
        {
            var primes = Utils.GetPrimes(100000);
            Assert.AreEqual(9592, primes.Count);
        }

        [TestMethod()]
        public void RandomInRangeTest()
        {
            var bigInt = Utils.RandomInRange(RandomNumberGenerator.Create(), BigInteger.Pow(10, 100), BigInteger.Pow(10, 200));
            var bigIntString = bigInt.ToString();
            Assert.IsTrue(bigIntString.Length >= 100);
            Assert.IsTrue(bigIntString.Length <= 200);
        }
    }
}