using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace zad2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string keysave;
        Knapsack ks = new Knapsack();

        public MainWindow()
        {
            InitializeComponent();
            Clear(true);
        }

        private void FileB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                ks.filepath = FileTB.Text = dlg.FileName;
                if (dlg.FileName.Contains(".enc"))
                {
                    List<string> encryptedLines = new List<string>(File.ReadAllLines(dlg.FileName));
                    ks.encryptedFile = new List<BigInteger>();
                    encryptedLines.ForEach(item => ks.encryptedFile.Add(new BigInteger(item)));
                }
                else
                {
                    ks.LoadFile();
                    ks.StringToIntList(ks.BytesToString(ks.fileBytes), ks.file);

                }
                EncryptB.Visibility = Visibility.Visible;
                DecryptB.Visibility = Visibility.Visible;

            }
        }

        private void EncryptB_Click(object sender, RoutedEventArgs e)
        { 
            ks.Encrypt();
            SaveFileDialog dlg = new SaveFileDialog();
            List<string> encrypted = new List<string>();
            ks.encryptedFile.ForEach(item => {
                if(item != 0)
                {
                    encrypted.Add(item.ToString());
                }
            });
            if(dlg.ShowDialog() == true)
            {
                File.WriteAllLines(dlg.FileName+".enc", encrypted);
            }
        }

        private void DecryptB_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            string decrypted = ks.Decrypt();
            byte[] arr = ks.StringToBytesArray(decrypted);
            if (dlg.ShowDialog() == true)
            {
                Stream stream = new FileStream(dlg.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(stream);
                foreach (var b in arr)
                {
                    bw.Write(b);
                }

                bw.Flush();
                bw.Close();
                Clear();
            }
        }

        private void Clear(bool light = false)
        {
            if (!light)
            {
                ks.filepath = FileTB.Text = "";
                ks.fileBytes = null;
                ks.file = new List<int>();
            }

            EncryptB.Visibility = Visibility.Hidden;
            DecryptB.Visibility = Visibility.Hidden;
        }

        private void LoadKeyB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                keysave = KeyTB.Text = dlg.FileName;

                BinaryFormatter bin = new BinaryFormatter();
                ks.generator = (KeyGenerator)bin.Deserialize(new FileStream(keysave, FileMode.Open));
            }
        }

        private void SaveKeyB_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog() == true)
            {
                keysave = KeyTB.Text = dlg.FileName;

                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(new FileStream(keysave, FileMode.Create), ks.generator);
            }
        }

        private void GenerateKeyB_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BlockSizeTB.Text))
            {
                ks.generator = new KeyGenerator(Int32.Parse(BlockSizeTB.Text));
            }
        }
    }
}
