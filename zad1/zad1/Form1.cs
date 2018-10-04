using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

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

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aye!");
        }

        private void KeyTB_TextChanged(object sender, EventArgs e)
        {
            dx.key_s = keyTB.Text;
            label1.Text = dx.key_s; /**/
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dx.filepath_s = filepathTB.Text;

            try
            {
                dx.LoadFile();
                label2.Text = dx.BytesToString(dx.file_b);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
