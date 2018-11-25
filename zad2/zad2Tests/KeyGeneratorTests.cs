using zad2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace zad2
{
    [TestClass]
    public class KeyGeneratorTests
    {
        KeyGenerator generator;
        
        [TestInitialize]
        public void Init()
        {
            generator = new KeyGenerator(new List<int>() { 2, 7, 11, 21, 42, 89, 180, 354 }, 881, 588);
        }

        [TestMethod]
        public void PublicKeyGenerationTest()
        {
            List<int> expectedList = new List<int>() { 295, 592, 301, 14, 28, 353, 120, 236 };
            CollectionAssert.AreEqual(expectedList, generator.PublicKey);
        }

        [TestMethod]
        public void GetPrivateKeyTest()
        {
            var privateKey = generator.GetPrivateKey();
            List<int> w = privateKey.Item1;
            List<int> expectedW = new List<int>() { 2, 7, 11, 21, 42, 89, 180, 354 };
            int q = privateKey.Item2;
            int r = privateKey.Item3;
            CollectionAssert.AreEqual(expectedW, w);
            Assert.AreEqual(881, q);
            Assert.AreEqual(588, r);
        }

        [TestMethod]
        public void GetRandomCoprimeTest()
        {
            int coprime = generator.GetRandomCoprime(66);
            List<int> expected = new List<int> { 5, 7, 13, 17, 19, 23, 25, 29, 31, 35, 37, 41, 43, 47, 49, 53, 59, 61, 65 };
            CollectionAssert.Contains(expected, coprime);
        }

    }
}