using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Livrable_3.View;
using Livrable_3.ViewModel;

namespace Livrable_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataContext = new FullBackupViewModel();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataContext = new DeferentialBackupViewModel();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataContext = new EncryptionViewModel();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DataContext = new AboutViewModel();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string path = @"C:\000\log\language.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.Write("true");
                }

            }
            else if (File.Exists(path))
            {
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.Write("true");
                }
            }
            Full_Backup.Content = "Sauvegarde complète";
            Differential_Backup.Content = "Sauvegarde différentielle";

            Encryption.Content = "cryptage/décryptage";
            Settings.Content = "à propos";

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string path = @"C:\000\log\language.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();

                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.Write("false");
                }

            }
            else if (File.Exists(path))
            {
                using (TextWriter tw = new StreamWriter(path))
                {
                    tw.Write("false");
                }
            }
            Full_Backup.Content = "Full Backup";
            Differential_Backup.Content = "Differential Backup";

            Encryption.Content = "Encryption/Decryption";
            Settings.Content = "About";
        }
    }
}
