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
            MessageBox.Show("1. Key in binary\n" +
                            "2. Yeet!");
        }

        private void KeyTB_TextChanged(object sender, EventArgs e)
        {
            dx.key_s = keyTB.Text.Replace(" ", "");
            label1.Text = dx.key_s; /**/
        }

        private void FilepathB_Click(object sender, EventArgs e)
        {
            dx.filepath = filepathTB.Text;

            try
            {
                dx.LoadFile();
                dx.Chopper(dx.BytesToString(dx.file_b), dx.file);
                label2.Text = dx.BytesToString(dx.file_b); /**/
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
                dx.Chopper(dx.key_s, dx.key);
            }
            catch(Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
