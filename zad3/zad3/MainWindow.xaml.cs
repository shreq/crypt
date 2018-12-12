using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace zad3
{
    public partial class MainWindow : Window
    {
        public RSA Rsa { get; set; }
        public BigInteger Message { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Rsa = new RSA();
        }

        private void LoadFileB_Click(object sender, RoutedEventArgs e)
        {
            var filePath = string.Empty;
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = dialog.FileName;

                    var fileStream = dialog.OpenFile();

                    using (var stream = File.OpenRead(filePath))
                    {
                        Message = RSA.GetHashSha256(stream);
                    }
                }
                LoadFileTB.Text = filePath;
            }
        }

        private void LoadTextB_Click(object sender, RoutedEventArgs e)
        {
            var message = LoadTextTB.Text;
            Message = RSA.GetHashSha256(message);
        }

        private void SaveKeyB_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.Create);
                    var formatter = new BinaryFormatter();
                    try
                    {
                        formatter.Serialize(fs, Rsa.Generator);
                    }
                    catch (SerializationException exception)
                    {
                        Console.WriteLine("Failed to serialize. Reason: " + exception.Message);
                        throw;
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
        }

        private void LoadKeyB_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.Open);
                    try
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        Rsa.Generator = (KeyGenerator)formatter.Deserialize(fs);
                    }
                    catch (SerializationException exception)
                    {
                        Console.WriteLine("Failed to deserialize. Reason: " + exception.Message);
                        throw;
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
        }

        private void SignB_Click(object sender, RoutedEventArgs e)
        {
            SignatureTB.Text = Rsa.GetSingature(Message).ToString();
        }

        private void VerifyB_Click(object sender, RoutedEventArgs e)
        {
            if (Rsa.VerifySignature(SignatureTB.Text, Message))
            {
                System.Windows.Forms.MessageBox.Show("Signature is correct.");
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Signature is incorrect.");
            }
        }
    }
}
