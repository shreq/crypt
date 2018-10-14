using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zad1
{
    public partial class Form1 : Form
    {
        Desx dx = new Desx();

        public Form1()
        {
            InitializeComponent();
        }

        private void HelpB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Key should be written in binary system with 64 bits\n",
                            "Help");
        }

        private void KeyTB_TextChanged(object sender, EventArgs e)
        {
            dx.keyString = keyTB.Text.Replace(" ", "");
            label1.Text = dx.keyString; /**/
        }

        private void FilepathB_Click(object sender, EventArgs e)
        {
            dx.filepath = filepathTB.Text;

            try
            {
                dx.LoadFile();
                dx.ConvertIntoBoolList(dx.BytesToString(dx.file_b), dx.file);
                label2.Text = dx.file_b.Length.ToString() + "B / " + (8 * dx.file_b.Length).ToString() + "b";
                label1.Text = Convert.ToString(dx.file.Last());
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void EncryptB_Click(object sender, EventArgs e)
        {
            try
            {
                dx.ConvertIntoBoolList(dx.keyString, dx.key);
                dx.ConvertIntoBoolList(dx.xKey1String, dx.xKey1);
                dx.ConvertIntoBoolList(dx.xKey2String, dx.xKey2);
                //dx.xKey1 = dx.xKey2 = dx.key;
                dx.Encrypt();

                if (textoutTB.TextLength == 0)
                    dx.Save(dx.filepath + "xxx", dx.result_bytes.ToArray());
                else
                    textoutTB.Text = dx.BytesToString(dx.result_bytes.ToArray());
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void DecryptB_Click(object sender, EventArgs e)
        {
            try
            {
                dx.ConvertIntoBoolList(dx.keyString, dx.key);
                dx.ConvertIntoBoolList(dx.xKey1String, dx.xKey1);
                dx.ConvertIntoBoolList(dx.xKey2String, dx.xKey2);
                //dx.xKey1 = dx.xKey2 = dx.key;
                dx.Decrypt();

                if (textoutTB.TextLength == 0)
                    dx.Save(dx.filepath + "x", dx.result_bytes.ToArray());
                else
                    textoutTB.Text = dx.BytesToString(dx.result_bytes.ToArray());
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void TextB_Click(object sender, EventArgs e)
        {
            dx.filepath = textinTB.Text;

            try
            {
                dx.LoadText();
                dx.ConvertIntoBoolList(dx.BytesToString(dx.file_b), dx.file);
                label2.Text = dx.file_b.Length.ToString() + "B / " + (8 * dx.file_b.Length).ToString() + "b";
                label1.Text = Convert.ToString(dx.file.Last());
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void Keyx1TB_TextChanged(object sender, EventArgs e)
        {
            dx.xKey1String = keyx1TB.Text.Replace(" ", "");
        }

        private void Keyx2TB_TextChanged(object sender, EventArgs e)
        {
            dx.xKey2String = keyx2TB.Text.Replace(" ", "");
        }
    }
}
