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
        public string keyString;
        public string xKey1String;
        public string xKey2String;
        public string filepath;
        public byte[] file_b;

        public List<bool> file = new List<bool>();
        public List<bool> key = new List<bool>();
        public List<bool> xKey1 = new List<bool>();
        public List<bool> xKey2 = new List<bool>();
        private List<List<bool>> subKeys = new List<List<bool>>();
        public List<byte> result_bytes = new List<byte>();

        public Desx() {}

        public void LoadFile()
        {
            file_b = File.ReadAllBytes(filepath) ?? throw new Exception();
        }

        public void LoadText()
        {
            file_b = System.Text.Encoding.Default.GetBytes(filepath) ?? throw new Exception();
        }

        public string BytesToString(byte[] ar)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in ar)
                sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));

            return sb.ToString();
        }

        public void ConvertIntoBoolList(string s, List<bool> l) // chops string to bools and packs them into list
        {
            l.Clear();

            if (s.Length % 8 != 0)
                throw new Exception();

            for (int i = 0; i < s.Length; i++)
                l.Add(Convert.ToBoolean(Convert.ToInt32(Convert.ToInt32(s[i]) - '0')));
        }

        private void ChopperByte(string s, List<byte> l) // chops string to bytes and packs them into list
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

        private void FillUp64(List<bool> l) // in order to encrypt the message it has to be a multiple of 64 bits, i.e. 8 bytes
        {
            while (l.Count() % 64 != 0)
                l.Add(false);
        }

        public void Encrypt() //
        {
            FillUp64(file);
            this.subKeys = CreateSubkeys();

            if (file.Count % 8 != 0)
                throw new Exception();

            ChopperByte(Crypt(file), result_bytes);
        }

        public void Decrypt() //
        {
            this.subKeys = CreateSubkeys();
            this.subKeys.Reverse();
            SwapLists(this.xKey1, this.xKey2);

            ChopperByte(Crypt(file), result_bytes);
        }

        public void Save(string fp, byte[] ar)
        {
            System.IO.File.WriteAllBytes(fp, ar);
        }

        private string Crypt(List<bool> data) // crypts whole file
        {
            string result = "";

            foreach (var chunk in data.Split(64))
                foreach (bool bit in CryptChunk(chunk))
                    result += Convert.ToInt16(bit).ToString();

            return result;
        }

        private List<bool> CryptChunk(List<bool> chunk) // crypts 64 bit chunk of message
        {
            Extensions.XorWithList(chunk, this.xKey1);      // extra XOR
            chunk = InitialPermutation(chunk);
            List<List<bool>> halves = chunk.Split(32);

            for (int i = 0; i < 16; i++)                    // encryption
            {
                Extensions.XorWithList(halves[0], Feistel(halves[1], this.subKeys[i]));
                SwapLists(halves[0], halves[1]);
            }

            chunk = halves[1].Concat(halves[0]).ToList();   // extra XOR
            chunk = FinalPermutation(chunk);
            Extensions.XorWithList(chunk, this.xKey2);

            return chunk;
        }

        private List<bool> FinalPermutation(List<bool> chunk) // does FP of the message
        {
            List<bool> permutedChunk = new List<bool>();

            for (int i = 0; i < 64; i++)
                permutedChunk.Add(chunk[FP[i] - 1]);

            return permutedChunk;
        }

        private void SwapLists(List<bool> list1, List<bool> list2) // swaps two lists
        {
            List<bool> temp = new List<bool>(list1);
            list1.Clear();
            list1.AddRange(list2);
            list2.Clear();
            list2.AddRange(temp);
        }

        private List<bool> Feistel(List<bool> halfBlock, List<bool> subKey) // function used for right half of M before xoring with left one
        {
            List<bool> result = Expand(halfBlock);
            Extensions.XorWithList(result, subKey);
            result = SboxSubstitution(result);
            return PboxPermutation(result);
        }

        private List<bool> PboxPermutation(List<bool> halfBlock) // used in the end of Feistel to permutate merged halfblock
        {
            List<bool> permutedChunk = new List<bool>();

            for (int i = 0; i < 32; i++)
                permutedChunk.Add(halfBlock[P[i] - 1]);

            return permutedChunk;
        }

        private List<bool> SboxSubstitution(List<bool> expandedHalfBlock) // converts 6 bits to 4 bits from Sbox
        {
            int boxCounter = 0;
            List<bool> mixedBlock = new List<bool>();

            foreach (var sixBitBlock in expandedHalfBlock.Split(6))
            {
                int index = CalculateSboxIndex(sixBitBlock);
                int substitutedValue = S[boxCounter++][index];
                FillWithValueBits(mixedBlock, substitutedValue);
            }

            return mixedBlock;
        }

        private void FillWithValueBits(List<bool> mixedBlock, int substitutedValue) //
        {
            string substitutedValueString = Convert.ToString(substitutedValue, 2).PadLeft(4, '0');

            foreach (char bit in substitutedValueString)
                mixedBlock.Add(Convert.ToBoolean(Convert.ToInt32(bit) - '0'));
        }

        private int CalculateSboxIndex(List<bool> sixBitBlock) // calculates index in Sbox
        {
            string rowString = "";
            string columnString = "";

            for (int i = 0; i < 6; i++)
            {
                if (i == 0 || i == 5)
                    rowString += Convert.ToInt16(sixBitBlock[i]).ToString();
                else
                    columnString += Convert.ToInt16(sixBitBlock[i]).ToString();
            }

            int row = Convert.ToInt32(rowString, 2);
            int column = Convert.ToInt32(columnString, 2);

            return 16 * row + column;
        }

        private List<bool> Expand(List<bool> halfBlock) // used in Feistel to expand half of chunk from 32 to 48 bits
        {
            List<bool> expandedBlock = new List<bool>();

            for (int i = 0; i < 48; i++)
                expandedBlock.Add(halfBlock[E[i] - 1]);

            return expandedBlock;
        }

        private List<bool> InitialPermutation(List<bool> chunk) // does IP of the message
        {
            List<bool> permutedChunk = new List<bool>();

            for (int i = 0; i < 64; i++)
                permutedChunk.Add(chunk[IP[i] - 1]);

            return permutedChunk;
        }

        private List<List<bool>> CreateSubkeys() // creates 16 subkeys with use of PC1 & PC2, each of which is 48 bits long
        {
            if (key.Count % 8 != 0)
                throw new Exception();

            // Permuted Choice 1, split into left and right
            List<bool> key_pc1l = new List<bool>();
            List<bool> key_pc1r = new List<bool>();

            for (int i = 0; i < PC1.Length / 2; i++)
                key_pc1l.Add(key[PC1[i] - 1]);

            for (int i = PC1.Length / 2; i < PC1.Length; i++)
                key_pc1r.Add(key[PC1[i] - 1]);

            // next, create 16 blocks based on previous ones with specific bit shifts
            List<List<bool>> left_halves = new List<List<bool>>();
            List<List<bool>> right_halves = new List<List<bool>>();

            left_halves.Add(key_pc1l); // add original halves at the beginning for the first step
            right_halves.Add(key_pc1r);

            for (int i = 1; i <= 16; i++)
            {
                if (i == 1 || i == 2 || i == 9 || i == 16) // shift these halves once, rest is shifted twice
                {
                    left_halves.Add(ShiftLeft(left_halves[i - 1], 1));
                    right_halves.Add(ShiftLeft(right_halves[i - 1], 1));
                }
                else
                {
                    left_halves.Add(ShiftLeft(left_halves[i - 1], 2));
                    right_halves.Add(ShiftLeft(right_halves[i - 1], 2));
                }
            }

            // now merge left and right halves
            List<List<bool>> pre_subkeys = new List<List<bool>>(); // list of merged, not permuted subkeys
            List<bool> temp = new List<bool>();

            for (int i = 1; i <= 16; i++)
            {
                temp = left_halves[i];
                temp.AddRange(right_halves[i]);
                pre_subkeys.Add(temp);
            }

            // Permuted Choice 2
            List<List<bool>> subkeys = new List<List<bool>>(pre_subkeys.Count); // permuted subkeys

            for (int i = 0; i < pre_subkeys.Count; i++)
            {
                List<bool> subkey = new List<bool>();

                for (int j = 0; j < PC2.Count(); j++)
                    subkey.Add(pre_subkeys[i][PC2[j] - 1]);

                subkeys.Add(subkey);
            }

            return subkeys;
        }

        private List<bool> ShiftLeft(List<bool> key, int shiftAmount) // shifts/rotates bits to the left
        {
            List<bool> rot = new List<bool>();

            for (int i = shiftAmount; i < key.Count; i++)
                rot.Add(key[i]);

            for (int i = 0; i < shiftAmount; i++)
                rot.Add(key[i]);

            return rot;
        }

        private readonly int[] PC1 = {
            57, 49, 41, 33, 25, 17,  9,
             1, 58, 50, 42, 34, 24, 18,
            10,  2, 59, 51, 43, 35, 27,
            19, 11,  3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
             7, 62, 54, 46, 38, 30, 22,
            14,  6, 61, 53, 45, 37, 29,
            21, 13,  5, 28, 20, 12,  4 };

        private readonly int[] PC2 = {
            14, 17, 11, 24,  1,  5,
             3, 28, 15,  6, 21, 10,
            23, 19, 12,  4, 26,  8,
            16,  7, 27, 20, 13,  2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32 };

        private readonly int[] IP = {
            58, 50, 42, 34, 26, 18, 10,  2,
            60, 52, 44, 36, 28, 20, 12,  4,
            62, 54, 46, 38, 30, 22, 14,  6,
            64, 56, 48, 40, 32, 24, 16,  8,
            57, 49, 41, 33, 25, 17,  9,  1,
            59, 51, 43, 35, 27, 19, 11,  3,
            61, 53, 45, 37, 29, 21, 13,  5,
            63, 55, 47, 39, 31, 23, 15,  7 };

        private readonly int[] E = {
            32,  1,  2,  3,  4,  5,
            4,  5,  6,  7,  8,  9,
            8,  9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32,  1
        };

        private readonly int[][] S = {
            new int[]{
                14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7,
                0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8,
                4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0,
                15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13
            },
            new int[]{
                15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10,
                 3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5,
                 0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15,
                13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9
            },
            new int[]{
                10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8,
                13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1,
                13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7,
                 1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12
            },
            new int[]{
                7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15,
                13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9,
                10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4,
                 3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14
            },
            new int[]{
                2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9,
                14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6,
                 4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14,
                11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3
            },
            new int[]{
                12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11,
                10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8,
                 9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6,
                 4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13
            },
            new int[]{
                4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1,
                13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6,
                 1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2,
                 6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12
            },
            new int[]{
                13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7,
                 1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2,
                 7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8,
                 2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11
            }
        };

        private readonly int[] P = {
            16,  7, 20, 21,
            29, 12, 28, 17,
            1, 15, 23, 26,
            5, 18, 31, 10,
            2,  8, 24, 14,
            32, 27,  3,  9,
            19, 13, 30,  6,
            22, 11,  4, 25
        };

        private readonly int[] FP = {
            40,  8, 48, 16, 56, 24, 64, 32,
            39,  7, 47, 15, 55, 23, 63, 31,
            38,  6, 46, 14, 54, 22, 62, 30,
            37,  5, 45, 13, 53, 21, 61, 29,
            36,  4, 44, 12, 52, 20, 60, 28,
            35,  3, 43, 11, 51, 19, 59, 27,
            34,  2, 42, 10, 50, 18, 58, 26,
            33,  1, 41,  9, 49, 17, 57, 25
        };
    }
}
