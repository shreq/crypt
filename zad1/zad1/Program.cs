using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * app for en/de-crypting data inserted by user or loaded from file with DESX algorithm
 *
 * chapter 12, p.370
 *
 * algorithm explanation:
 * - DES is symetric, ie. same algorithm and key are used to both encrypt/decrypt
 * - encrypts data in 64 bit blocks
 * - key length = 56 bits, expressed with 64 bits with every 8th one being parity check; 64 bits = 8 bytes
 */

namespace zad1
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Desx dx = new Desx("./../../../file.txt", "./../../../key.txt");

            Form1 f = new Form1();
            //f.label1.Text = dx.BytesArrayToString(dx.file);
            //f.label2.Text = dx.BytesArrayToString(dx.key);

            Application.Run(f);
        }
    }
}
