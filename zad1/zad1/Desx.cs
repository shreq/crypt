using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1
{
    public class Desx
    {
        public string key_s;
        public string filepath;
        public byte[] file_b;

        public List<byte> key = new List<byte>();
        public List<byte> file = new List<byte>();

        public Desx()
        {}

        public void LoadFile()
        {
            file_b = File.ReadAllBytes(filepath) ?? throw new Exception();
        }

        public string BytesToString(byte[] ar)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in ar)
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            return sb.ToString();
        }

        public void Chopper(string s)
        {
            if (s.Length % 8 != 0)
                throw new Exception();

            for (int i = 0; i < s.Length;)
            {
                string sb = "";

                for (int j = 0; j < 8; i++, j++)
                    sb += s[i];

                file.Add(Convert.ToByte(sb, 2));
            }
        }
    }
}
