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
        public List<int> encryptedFile = new List<int>();

        public List<int> publicKey = new List<int>();
        //TODO: dodac dopelnianie do rozmiaru klucza
        public List<int> file = new List<int>();

        public List<byte> resultBytes = new List<byte>();

        public KeyGenerator generator;

        public Knapsack() {}

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

        public void Encrypt()
        {
            int encryptedValue;
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
        
        public List<int> Decrypt()
        {
            List<int> decryptedMessage = new List<int>();
            var privateKey = generator.GetPrivateKey();
            List<int> w = privateKey.Item1;
            int decryptedChunk;
            foreach (var item in encryptedFile)
            {
                decryptedChunk = 0;
                int i = (item * generator.Inverse(privateKey.Item3, privateKey.Item2))%privateKey.Item2;
                while (i > 0)
                {
                    int index = FindIndexOfSmallest(w, i);
                    i -= w[index];
                    decryptedChunk |= (1 << index);
                }
                decryptedMessage.Add(decryptedChunk);
            }
            return decryptedMessage;
        }

        private int FindIndexOfSmallest(List<int> w, int i)
        {
            int value = w.Where(x => x <= i).Max();
            return w.IndexOf(value);

        }
    }
}
