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
    public class ExtensionsTests
    {
        [TestMethod()]
        public void FillToSizeTest()
        {
            List<int> list = new List<int>();
            list.FillToSize(5, 0);
            Assert.AreEqual(5, list.Count);
            list.FillToSize(20, 0);
            Assert.AreEqual(20, list.Count);
        }
    }
}