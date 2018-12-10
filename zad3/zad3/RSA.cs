using System.Security.Cryptography;
using System.Text;

namespace zad3
{
    public class RSA
    {
        public KeyGenerator Generator { get; set; }

        public RSA()
        {
            Generator = new KeyGenerator();
        }

        public RSA(KeyGenerator generator)
        {
            Generator = generator;
        }

        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            return hashString;
        }

    }
}
