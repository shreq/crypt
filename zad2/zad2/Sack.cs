using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// chapter 19.2, p.613

namespace zad2
{
    public class Sack
    {
        public string filepath;
        public string keyString;
        public byte[] fileBytes;

        public List<bool> key = new List<bool>();
        public List<bool> file = new List<bool>();

        public List<byte> resultBytes = new List<byte>();

        public Sack() {}

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
    }
}
