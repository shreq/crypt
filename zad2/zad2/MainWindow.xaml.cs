using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
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
            if (FileTextCHB.IsChecked == true)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                ks.encryptedFile = new List<BigInteger>();

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
            else
            {
                ks.encryptedFile = new List<BigInteger>();

                ks.filepath = FileTB.Text;
                DebugL.Content = ks.filepath;

                ks.LoadText();
                ks.StringToIntList(ks.BytesToString(ks.fileBytes), ks.file);
                DebugL.Content = "text loaded";

                EncryptB.Visibility = Visibility.Visible;
                DecryptB.Visibility = Visibility.Visible;
            }
        }

        private void EncryptB_Click(object sender, RoutedEventArgs e)
        {
            if (FileTextCHB.IsChecked == true)
            {
                DebugL.Content = "encrypting";
                ks.Encrypt();
                DebugL.Content = "encrypting done";
                //SaveFileDialog dlg = new SaveFileDialog();
                SaveFileDialog dlg = new SaveFileDialog() { Filter = "Encrypted file (*.enc)|*.enc" };
                List<string> encrypted = new List<string>();
                ks.encryptedFile.ForEach(item => encrypted.Add(item.ToString()));
                if (dlg.ShowDialog() == true)
                {
                    //File.WriteAllLines(dlg.FileName+".enc", encrypted);
                    File.WriteAllLines(dlg.FileName, encrypted);
                    Clear();
                    DebugL.Content = "file saved";
                }
            }
            else
            {
                DebugL.Content = "encrypting";
                ks.Encrypt();
                DebugL.Content = "encrypting done";

                List<string> encrypted = new List<string>();
                ks.encryptedFile.ForEach(item => encrypted.Add(item.ToString()));

                List<byte> encryptedBytes = new List<byte>();
                //encrypted.ForEach(item => encryptedBytes.Add(Convert.ToByte(item)));  // to wywali wyjatek
                encryptedBytes = StringToBytesList(encrypted);

                FileTB.Text = System.Text.Encoding.Default.GetString(encryptedBytes.ToArray());
                DebugL.Content = "text written";
            }
        }

        private List<byte> StringToBytesList(List<string> ls)
        {
            StringBuilder sb = new StringBuilder();
            ls.ForEach(x => sb.Append(x));

            List<byte> lb = new List<byte>();
            string s = sb.ToString();

            if (s.Length % 2 != 0)          // nie wiem co tu powinno sie zrobic
            {
                //throw new Exception();
                s += "0";
            }

            for (int i = 0; i < s.Length; i += 2)
                lb.Add(Convert.ToByte(s[i] + s[i + 1]));

            return lb;
        }

        private void DecryptB_Click(object sender, RoutedEventArgs e)
        {
            DebugL.Content = "decrypting";
            string decrypted = ks.Decrypt();
            decrypted = decrypted.TrimEnd('0');
            while(decrypted.Length%8 != 0)
            {
                decrypted += "0";
            }
            DebugL.Content = "decrypting done";
            byte[] arr = ks.StringToBytesArray(decrypted);
            if (FileTextCHB.IsChecked == true)
            {
                SaveFileDialog dlg = new SaveFileDialog();
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
            else
            {
                FileTB.Text = System.Text.Encoding.Default.GetString(arr);
                //Clear(true);
                DebugL.Content = "text written";
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
                DebugL.Content = "empty block size";
            }
        }

        private void FileTextCHB_Click(object sender, RoutedEventArgs e)
        {
            if (FileTextCHB.IsChecked == true)
            {
                FileB.Content = "Browse";
                FileTextCHB.Content = "File";
                DebugL.Content = "checked";
            }
            else
            {
                FileB.Content = "Load Text";
                FileTextCHB.Content = "Text";
                DebugL.Content = "unchecked";
            }
        }
    }
}
