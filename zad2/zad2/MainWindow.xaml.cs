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
            DebugL.Visibility = Visibility.Visible;     // [Visible | Hidden] - to change visibility of 'Debug Label'
        }

        private void FileB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog() { Filter = "All Files (*.*)|*.*|Encrypted file (*.enc)|*.enc" };

            if (dlg.ShowDialog() == true)
            {
                ks.filepath = FileTB.Text = dlg.FileName;
                DebugL.Content = ks.filepath;
                if (dlg.FileName.Contains(".enc"))
                {
                    List<string> encryptedLines = new List<string>(File.ReadAllLines(dlg.FileName));
                    encryptedLines.ForEach(item => ks.encryptedFile.Add(new BigInteger(item)));
                    DebugL.Content = ".enc file loaded";
                }
                else
                {
                    ks.LoadFile();
                    ks.StringToIntList(ks.BytesToString(ks.fileBytes), ks.file);
                    DebugL.Content = "regular file loaded";
                }
                EncryptB.Visibility = Visibility.Visible;
                DecryptB.Visibility = Visibility.Visible;
            }
        }

        private void EncryptB_Click(object sender, RoutedEventArgs e)
        {
            DebugL.Content = "encrypting";
            ks.Encrypt();
            DebugL.Content = "encrypting done";
            SaveFileDialog dlg = new SaveFileDialog() { Filter = "Encrypted file (*.enc)|*.enc" };
            List<string> encrypted = new List<string>();
            ks.encryptedFile.ForEach(item => encrypted.Add(item.ToString()));
            if(dlg.ShowDialog() == true)
            {
                File.WriteAllLines(dlg.FileName/*+".enc"*/, encrypted);
                Clear();
                DebugL.Content = "file saved";
            }
        }

        private void DecryptB_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            DebugL.Content = "decrypting";
            string decrypted = ks.Decrypt();
            DebugL.Content = "decrypting done";
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
                DebugL.Content = "file saved";
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
            DebugL.Content = "Debug Label";
        }

        private void LoadKeyB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                keysave = KeyTB.Text = dlg.FileName;
                DebugL.Content = keysave;

                BinaryFormatter bin = new BinaryFormatter();
                ks.generator = (KeyGenerator)bin.Deserialize(new FileStream(keysave, FileMode.Open));
                DebugL.Content = "key loaded";
            }
        }

        private void SaveKeyB_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog() == true)
            {
                keysave = KeyTB.Text = dlg.FileName;
                DebugL.Content = keysave;

                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(new FileStream(keysave, FileMode.Create), ks.generator);
                DebugL.Content = "key saved";
            }
        }

        private void GenerateKeyB_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BlockSizeTB.Text))
            {
                DebugL.Content = "generating key";
                ks.generator = new KeyGenerator(Int32.Parse(BlockSizeTB.Text));
                DebugL.Content = "generating key done";
            }
            else
            {
                MessageBox.Show("Specify block size first!", "ah!");
            }
        }
    }
}
