using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using System.Security.Cryptography;

namespace zad3.Tests
{
    [TestClass()]
    public class BigIntegerExtensionsTests
    {
        [TestMethod()]
        public void IsProbablePrimeTest()
        {
            var smallPrimes = Utils.GetPrimes(65000);
            var bigInt = BigInteger.Parse("76921421106760125285550929240903354966370431827792714920086011488103952094969175731459908117375995349245839343");
            Assert.IsTrue(bigInt.IsProbablePrime(40, smallPrimes));
        }
    }
}