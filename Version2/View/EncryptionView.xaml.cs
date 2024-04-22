using Livrable_3.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Forms;
namespace Livrable_3.View
{
    /// <summary>
    /// Interaction logic for EncryptionView.xaml
    /// </summary>
    public partial class EncryptionView : System.Windows.Controls.UserControl
    {
        public string path = System.IO.File.ReadAllText(@"C:\000\log\language.txt");
        public EncryptionView()
        {
            InitializeComponent();

            if (path == "true")
            {
                path_crypto.Text = "Entrez votre chemin";
                crypto_button_one_by_one.Content = "Chiffrer/déchiffrer";
                crypto_button_everything.Content = "Tout chiffrer/déchiffrer";
                key_label.Content = "Clé";
            }
            else if (path == "false")
            {
                path_crypto.Text = "Enter your path";
                crypto_button_one_by_one.Content = "Encrypt/Decrypt";
                crypto_button_everything.Content = "Encrypt/Decrypt everything";
                key_label.Content = "Key";
            }
        }

        private void path_crypto_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select your folder please!";

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                path_crypto.Text = fbd.SelectedPath;
                string[] destpath = Directory.GetFiles(path_crypto.Text);
                foreach (string file in destpath)
                {
                    if (System.IO.Path.GetExtension(file) == ".txt")
                    {
                        list_crypto.Items.Add(file);
                    }


                }

            }

        }

        private void crypto_button_everything_Click(object sender, RoutedEventArgs e)
        {
            if (list_crypto.Items.Count != 0 && key.Text.Length > 0)
            {
                EncryptionViewModel obj = new EncryptionViewModel();
                crypto_status.Content = "";
                foreach (string file in list_crypto.Items)
                {
                    obj.item = file;
                    obj.key = int.Parse(key.Text);
                    obj.destination = System.IO.Path.GetDirectoryName(file);
                    obj.Encryption();
                }
            }
            else
            {
                if (path == "true")
                {
                    System.Windows.Forms.MessageBox.Show("Veuillez sélectionner votre dossier ou votre clé !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    crypto_status.Content = "Veuillez sélectionner votre dossier ou votre clé !";
                }
                else if (path == "false")
                {
                    System.Windows.Forms.MessageBox.Show("Please select your folder or key !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    crypto_status.Content = "Please select your folder or key!";
                }
            }
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void crypto_button_one_by_one_Click(object sender, RoutedEventArgs e)
        {
            if (list_crypto.Items.Count != 0 && key.Text.Length > 0)
            {
                EncryptionViewModel pip = new EncryptionViewModel();
                string sus = list_crypto.SelectedItem.ToString();
                pip.item = sus;
                pip.key = int.Parse(key.Text);
                pip.destination = System.IO.Path.GetDirectoryName(sus);
                pip.Encryption();
            }
            else
            {
                if (path == "true")
                {
                    System.Windows.Forms.MessageBox.Show("Veuillez sélectionner votre dossier ou votre clé !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    crypto_status.Content = "Veuillez sélectionner votre dossier ou votre clé !";
                }
                else if (path == "false")
                {
                    System.Windows.Forms.MessageBox.Show("Please select your folder or key !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    crypto_status.Content = "Please select your folder or key!";
                }


            }
        }
    }
}
