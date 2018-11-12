using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace zad2
{
    [TestClass()]
    public class KeyGeneratorTests
    {
        [TestMethod()]
        public void GetCoprimesTest()
        {
            KeyGenerator uh = new KeyGenerator();
            var result = uh.GetCoprimes(66);
            List<int> expected = new List<int> { 5, 7, 13, 17, 19, 23, 25, 29, 31, 35, 37, 41, 43, 47, 49, 53, 59, 61, 65 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetPublicKeyTest()
        {
            KeyGenerator uh = new KeyGenerator();
            int expected = 8;
            var result = uh.GetPublicKey().Count();
            Assert.AreEqual(expected, result);
        }
    }
}