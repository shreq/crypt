using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

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

                ks.LoadFile();
                ks.StringToIntList(ks.BytesToString(ks.fileBytes), ks.file);

                EncryptB.Visibility = Visibility.Visible;
                DecryptB.Visibility = Visibility.Visible;
            }
        }

        private void EncryptB_Click(object sender, RoutedEventArgs e)
        {
            ks.Encrypt();
            ks.RewriteToType(ks.encryptedFile, ks.resultBytes);

            SaveFileDialog dlg = new SaveFileDialog();

            if(dlg.ShowDialog() == true)
            {
                ks.SaveFile(dlg.FileName, ks.resultBytes.ToArray());
                Clear();
            }
        }

        private void DecryptB_Click(object sender, RoutedEventArgs e)
        {
            ks.RewriteToType(ks.Decrypt(), ks.resultBytes);

            SaveFileDialog dlg = new SaveFileDialog();

            if (dlg.ShowDialog() == true)
            {
                ks.SaveFile(dlg.FileName, ks.resultBytes.ToArray());
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
            ks.generator = new KeyGenerator(Int32.Parse(BlockSizeTB.Text));
        }
    }
}
