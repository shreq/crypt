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
        public byte[] key;
        public byte[] file;

        public Desx(String filepath, String keypath)
        {
            this.file = File.ReadAllBytes(filepath);
            string txt = System.IO.File.ReadAllText(keypath);
            this.key = Encoding.Default.GetBytes(txt);
        }

        public Desx(String filepath, ulong key)
        {
            this.file = File.ReadAllBytes(filepath);
            this.key = BitConverter.GetBytes(key);
            Array.Reverse(this.key);    // cuz lil endian
        }

        public String BytesArrayToString(byte[] ar)
        {
            String s = "";

            foreach(byte f in ar)
                s = f.ToString();

            return s;
        }
    }
}
