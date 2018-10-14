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

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Click on the link below to continue learning how to build a desktop app using WinForms!
            System.Diagnostics.Process.Start("http://aka.ms/dotnet-get-started-desktop");
        }

        private void HelpB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1. Key should be written in binary system with 64 bits\n" +
                            "2. Yeet!",
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
                dx.xKey1 = dx.xKey2 = dx.key;
                dx.Encrypt();
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
                dx.xKey1 = dx.xKey2 = dx.key;
                dx.Decrypt();
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
                dx.LoadFile();
                dx.ConvertIntoBoolList(dx.BytesToString(dx.file_b), dx.file);
                label2.Text = dx.file_b.Length.ToString() + "B / " + (8 * dx.file_b.Length).ToString() + "b";
                label1.Text = Convert.ToString(dx.file.Last());
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
