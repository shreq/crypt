using Microsoft.VisualStudio.TestTools.UnitTesting;
using zad2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2
{
    [TestClass()]
    public class EncryptionTests
    {
        [TestMethod()]
        public void GetCoprimesTest()
        {
            Encryption uh = new Encryption();
            var result = uh.GetCoprimes(66);
            List<int> expected = new List<int> { 5, 7, 13, 17, 19, 23, 25, 29, 31, 35, 37, 41, 43, 47, 49, 53, 59, 61, 65 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GeneratePublicKeyTest()
        {
            Encryption uh = new Encryption();
            int expected = 8;
            var result = uh.GeneratePublicKey().Count();
            Assert.AreEqual(expected, result);
        }
    }
}