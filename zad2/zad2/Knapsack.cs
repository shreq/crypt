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
        public List<int> file = new List<int>();
        public KeyGenerator generator;

        public Knapsack()
        {
            generator = new KeyGenerator();
        }

        /// <summary>
        /// Reads bytes from file which path had been specified in [this.filepath] and saves them in [this.fileBytes]
        /// </summary>
        public void LoadFile()
        {
            fileBytes = System.IO.File.ReadAllBytes(filepath) ?? throw new Exception();
        }

        /// <summary>
        /// Reads bytes from text which had been saved to [this.filepath] and saves them in [this.fileBytes]
        /// </summary>
        public void LoadText()
        {
            fileBytes = System.Text.Encoding.Default.GetBytes(filepath) ?? throw new Exception();
        }

        /// <summary>
        /// Converts array of bytes to single string.
        /// Represented in binary system
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        public string BytesToString(byte[] ar)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in ar)
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            return sb.ToString();
        }

        /// <summary>
        /// Converts string (representing binary value) to List of ints.
        /// Each int equals either 0 or 1
        /// </summary>
        /// <param name="s"></param>
        /// <param name="l"></param>
        /// <returns></returns>
        public List<int> StringToIntList(string s, List<int> l)
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            for(int i=0; i<s.Length; i++)
                l.Add(Convert.ToInt16(Convert.ToInt16(s[i]) - '0'));

            return l;
        }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! elaborate!
        /// Takes [this.file] and encrypts each chunk of it with public key
        /// Result is saved in [this.encryptedFile]
        /// </summary>
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

        /// <summary>
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! elaborate!
        /// Takes [this.encryptedFile] and decrypts it with private key
        /// Result is returned
        /// </summary>
        /// <returns></returns>
        public string Decrypt()
        {
            string decryptedMessage = string.Empty;
            var privateKey = generator.GetPrivateKey();
            List<BigInteger> w = privateKey.Item1;
            StringBuilder decryptedChunk;
            foreach (var item in encryptedFile)
            {
                decryptedChunk = new StringBuilder(new string('0', generator.PublicKey.Count));
                BigInteger i = (item * generator.Inverse(privateKey.Item3, privateKey.Item2)) % privateKey.Item2;
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

        /// <summary>
        /// Finds the biggest number in [w] that is smaller than [i] and returns it's index
        /// </summary>
        /// <param name="w"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private int FindIndexOfSmallest(List<BigInteger> w, BigInteger i)
        {
            return w.Where(x => x <= i).Count() - 1;
        }

        /// <summary>
        /// Converts string (representing binary value) to array of bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
