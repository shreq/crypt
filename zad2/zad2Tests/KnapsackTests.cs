using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace zad2.Tests
{
    [TestClass()]
    public class KnapsackTests
    {
        Knapsack sack;
        KeyGenerator generator;

        [TestInitialize]
        public void Init()
        {
            sack = new Knapsack();
            generator = new KeyGenerator(new List<int>() { 2, 7, 11, 21, 42, 89, 180, 354 }, 881, 588);
            sack.generator = generator;
        }

        [TestMethod()]
        public void EncryptTest()
        {
            sack.file = new List<int>() { 0, 1, 1, 0, 0, 0, 0, 1 };
            sack.Encrypt();
            Assert.AreEqual(1129, sack.encryptedFile[0]);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            sack.encryptedFile = new List<int>() { 1129 };
            List<int> decrypted = sack.Decrypt();
            Assert.AreEqual('a', (char)(decrypted[0]));
        }
    }
}