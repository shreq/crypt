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
        public readonly int[] PC1 = {
            57, 49, 41, 33, 25, 17,  9,
             1, 58, 50, 42, 34, 24, 18,
            10,  2, 59, 51, 43, 35, 27,
            19, 11,  3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
             7, 62, 54, 46, 38, 30, 22,
            14,  6, 61, 53, 45, 37, 29,
            21, 13,  5, 28, 20, 12,  4 };

        public readonly int[] PC2 = {
            14, 17, 11, 24,  1,  5,
             3, 28, 15,  6, 21, 10,
            23, 19, 12,  4, 26,  8,
            16,  7, 27, 20, 13,  2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32 };

        public string key_s;
        public string filepath;
        public byte[] file_b;

        public List<int> key = new List<int>();
        public List<byte> file = new List<byte>();

        public Desx() {}

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

        public void ChopperInt(string s, List<int> l) // chops string to ints and packs them into list
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            for (int i = 0; i < s.Length; i++)
                l.Add(Convert.ToInt32(s[i]) - '0');
        }

        public void ChopperByte(string s, List<byte> l) // chops string to bytes and packs them into list
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            for (int i = 0; i < s.Length;)
            {
                string sb = "";

                for (int j = 0; j < 8; i++, j++)
                    sb += s[i];

                l.Add(Convert.ToByte(sb, 2));
            }
        }

        public void FillUp64(List<byte> l) // in order to encrypt the message it has to be a multiple of 64 bits, i.e. 8 bytes
        {
            while (l.Count() % 8 != 0)
                l.Add(0);
        }

        public void Encrypt() //WIP
        {
            FillUp64(file);

            if (file.Count % 8 != 0)
                throw new Exception();

            List<byte> bl = new List<byte>();
            List<byte> br = new List<byte>();

            for (int i = 0; i < file.Count; i += 4)
            {
                bl.Clear();
                br.Clear();

                for (int j = 0; j < 4; i++, j++)
                {
                    bl.Add(file[i]);
                    br.Add(file[i + 4]);
                }
            }
        }

        public List<List<int>> CreateSubkeys() // WIP
        {
            if (key.Count % 8 != 0)
                throw new Exception();

            List<int> key_pc1l = new List<int>();
            List<int> key_pc1r = new List<int>();

            for (int i = 0; i < PC1.Length / 2; i++)
                key_pc1l.Add(key[PC1[i] - 1]);
            for (int i = PC1.Length / 2; i < PC1.Length; i++)
                key_pc1r.Add(key[PC1[i] - 1]);

            List<List<int>> left_halfs = new List<List<int>>();
            List<List<int>> right_halfs = new List<List<int>>();
            left_halfs.Add(key_pc1l); //add original halfs at the begining for the first step
            right_halfs.Add(key_pc1r);

            for (int i = 1; i <= 16; i++)
            {
                if (i == 1 || i == 2 || i == 9 || i == 16) //shift these halfs once, rest is shifted twice
                {
                    left_halfs.Add(ShiftLeft(left_halfs[i - 1], 1));
                    right_halfs.Add(ShiftLeft(right_halfs[i - 1], 1));
                }
                else
                {
                    left_halfs.Add(ShiftLeft(left_halfs[i - 2], 2));
                    right_halfs.Add(ShiftLeft(right_halfs[i - 2], 2));
                }
            }

            List<List<int>> pre_subkeys = new List<List<int>>(); //list of merged, not permutated subkeys
            List<int> temp = new List<int>();

            for (int i = 1; i <= 16; i++)
            {
                temp = left_halfs[i];
                temp.AddRange(right_halfs[i]);
                pre_subkeys.Add(temp);
            }

            List<List<int>> subkeys = new List<List<int>>(pre_subkeys.Count); //permutated subkeys

            for (int i = 0; i < pre_subkeys.Count; i++)
            {
                List<int> subkey = new List<int>();
                for (int j = 0; j < PC2.Count(); j++)
                {
                    subkey.Add(pre_subkeys[i][PC2[j]-1]);
                }
                subkeys.Add(subkey);
            }
            return subkeys;
        }

        private List<int> ShiftLeft(List<int> key, int shiftAmount = 1)
        {
            List<int> ret = new List<int>();

            for (int i = shiftAmount; i < key.Count; i++)
                ret.Add(key[i]);
            for (int i = 0; i < shiftAmount; i++)
                ret.Add(key[i]);

            return ret;
        }
    }
}
