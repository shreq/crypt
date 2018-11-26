using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// chapter 19.2, p.613

namespace zad2
{
    public class Knapsack
    {
        public string filepath;
        public string keyString;
        public byte[] fileBytes;
        public List<BigInteger> encryptedFile = new List<BigInteger>();

        public List<int> publicKey = new List<int>();
        //TODO: dodac dopelnianie do rozmiaru klucza
        public List<int> file = new List<int>();

        public List<byte> resultBytes = new List<byte>();

        public KeyGenerator generator;

        public Knapsack()
        {
            generator = new KeyGenerator();
        }

        public void LoadFile()
        {
            fileBytes = System.IO.File.ReadAllBytes(filepath) ?? throw new Exception();
        }

        public void SaveFile(string fp, byte[] ar)
        {
            System.IO.File.WriteAllBytes(fp, ar);
        }

        public void LoadText()
        {
            fileBytes = System.Text.Encoding.Default.GetBytes(filepath) ?? throw new Exception();
        }

        public string BytesToString(byte[] ar)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in ar)
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            return sb.ToString();
        }

        public List<bool> StringToBoolList(string s, List<bool> l)
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            for (int i = 0; i < s.Length; i++)
                l.Add(Convert.ToBoolean(Convert.ToInt16(Convert.ToInt16(s[i]) - '0')));

            return l;
        }

        public List<byte> StringToByteList(string s, List<byte> l)
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            string sb;
            for (int i = 0; i < s.Length;)
            {
                sb = "";

                for (int j = 0; j < 0; i++, j++)
                    sb += s[i];

                l.Add(Convert.ToByte(sb, 2));
            }

            return l;
        }

        public List<int> StringToIntList(string s, List<int> l)
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            for(int i=0; i<s.Length; i++)
                l.Add(Convert.ToInt16(Convert.ToInt16(s[i]) - '0'));

            return l;
        }

        public void FillUp64(List<bool> l)
        {
            while (l.Count() % 64 != 0)
                l.Add(false);
        }

        public void SwapLists<T>(List<T> l1, List<T> l2)
        {
            List<T> temp = new List<T>(l1);
            l1.Clear();
            l1.AddRange(l2);
            l2.Clear();
            l2.AddRange(temp);
        }

        public List<byte> RewriteToType(List<string> lt, List<byte> lk)
        {
            //lk.Clear();
           // foreach (var item in lt)
            //    lk.Add((byte)item);

            return lk;
        }

        public void Encrypt()
        {
            BigInteger encryptedValue;
            file.FillToSize(generator.PublicKey.Count, 0);
            foreach (var chunk in file.Split(generator.PublicKey.Count))
            {
                encryptedValue = 0;
                foreach (var item in chunk.Zip(generator.PublicKey, Tuple.Create))
                {
                    encryptedValue += (item.Item1 * item.Item2);
                }
                encryptedFile.Add(encryptedValue);
            }
        }

        public string Decrypt()
        {
            string decryptedMessage = string.Empty;
            var privateKey = generator.GetPrivateKey();
            List<BigInteger> w = privateKey.Item1;
            StringBuilder decryptedChunk;
            foreach (var item in encryptedFile)
            {
                decryptedChunk = new StringBuilder(new string('0', generator.PublicKey.Count));
                BigInteger i = (item * generator.Inverse(privateKey.Item3, privateKey.Item2))%privateKey.Item2;
                while (i != 0)
                {
                    int index = FindIndexOfSmallest(w, i);
                    i -= w[index];
                    decryptedChunk[index] = '1';
                }
                decryptedMessage += decryptedChunk;
            }
            return decryptedMessage;
        }

        private int FindIndexOfSmallest(List<BigInteger> w, BigInteger i)
        {
            return w.Where(x => x <= i).Count() - 1;
        }

        public byte[] StringToBytesArray(string str)
        {
            var bitsToPad = 8 - str.Length % 8;

            if (bitsToPad != 8)
            {
                var neededLength = bitsToPad + str.Length;
                str = str.PadLeft(neededLength, '0');
            }

            int size = str.Length / 8;
            byte[] arr = new byte[size];

            for (int a = 0; a < size; a++)
            {
                arr[a] = Convert.ToByte(str.Substring(a * 8, 8), 2);
            }

            return arr;
        }
    }
}
