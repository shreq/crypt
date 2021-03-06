﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace zad2
{
    [TestClass]
    public class KeyGeneratorTests
    {
        KeyGenerator generator;
        
        [TestInitialize]
        public void Init()
        {
            generator = new KeyGenerator(new List<BigInteger>() { 2, 7, 11, 21, 42, 89, 180, 354 }, 881, 588);
        }

        [TestMethod]
        public void PublicKeyGenerationTest()
        {
            List<BigInteger> expectedList = new List<BigInteger>() { 295, 592, 301, 14, 28, 353, 120, 236 };
            CollectionAssert.AreEqual(expectedList, generator.PublicKey);
        }

        [TestMethod]
        public void GetPrivateKeyTest()
        {
            var privateKey = generator.GetPrivateKey();
            List<BigInteger> w = privateKey.Item1;
            List<BigInteger> expectedW = new List<BigInteger>() { 2, 7, 11, 21, 42, 89, 180, 354 };
            BigInteger q = privateKey.Item2;
            BigInteger r = privateKey.Item3;
            CollectionAssert.AreEqual(expectedW, w);
            Assert.AreEqual(new BigInteger(881), q);
            Assert.AreEqual(new BigInteger(588), r);
        }

    }
}