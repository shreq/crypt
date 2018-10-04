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
        public string filepath_s;
        public byte[] file_b;
        public List<uint> key = new List<uint>();
        public List<uint> file = new List<uint>();

        public Desx()
        {}

        public void LoadFile()
        {
            file_b = File.ReadAllBytes(filepath_s) ?? throw new Exception();
        }

        public string BytesToString(byte[] ar)
        {
            StringBuilder sb = new StringBuilder();

            foreach(byte b in ar)
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            return sb.ToString();
        }

        public void Chopper(string s)
        {

        }
    }
}
