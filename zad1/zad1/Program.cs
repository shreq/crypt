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
 * - key length = 56 bits, expressed with 64 bits with every 8th one being parity check
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
            Application.Run(new Form1());

            Desx dx = new Desx("./../../../file.txt", "./../../../key.txt");

            //Application.label1
        }
    }
}
