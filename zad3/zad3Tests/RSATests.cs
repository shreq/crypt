using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace zad3.Tests
{
    [TestClass()]
    public class RSATests
    {
        [TestMethod()]
        public void GetHashSha256Test()
        {
            string text = "W Szczebrzeszynie chrząszcz brzmi w trzcinie.";
            string result = RSA.GetHashSha256(text);
            string expected = "60d5a481f6e872f46b3b1c957d42a07796105f05722ca4f950c2c4b9d025dfc4";
            Assert.AreEqual(expected, result);
        }
    }
}