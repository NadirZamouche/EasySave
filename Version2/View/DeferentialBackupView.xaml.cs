using Livrable_3.ViewModel;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
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

namespace Livrable_3.View
{
    /// <summary>
    /// Interaction logic for DeferentialBackupView.xaml
    /// </summary>
    public partial class DeferentialBackupView : UserControl
    {
        public bool metier;
        public string source;
        public string destination;
        public double size;
        DeferentialBackupViewModel objet = new DeferentialBackupViewModel();
        public string path = System.IO.File.ReadAllText(@"C:\000\log\language.txt");
        public DeferentialBackupView()
        {
            InitializeComponent();

            if (path == "true")
            {
                differential.Text = "Sauvegarde différentielle";
                Cancel.Content = "Annuler";
                Pause.Content = "Pause";
                Resume.Content = "Reprendre";
                Progress_Bar.Text = "Barre de progression";
                Destination_Path.Text = "Chemin de destination";
                Source_Path.Text = "Chemin source";
                exe.Content = "Ne copiez pas les logiciels métier";
                Max_size_label.Content = "Taille maximale du fichier";
            }
            else if (path == "false")
            {
                differential.Text = "Differential Backup";
                Cancel.Content = "Cancel";
                Pause.Content = "Pause";
                Resume.Content = "Resume";
                Progress_Bar.Text = "Progress Bar";
                Destination_Path.Text = "Destination Path";
                Source_Path.Text = "Source Path";
                exe.Content = "Do not copy business software";
                Max_size_label.Content = "Max file size";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            objet.Pause();

        }
        public void RemoteCall()
        {
            objet.CopyDirectoryDiff(source, destination, size, metier);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {


                destination_textbox.Text = dialog.FileName;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {


                source_textbox.Text = dialog.FileName;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            if (exe.IsChecked == true)
            {
                metier = true;
            }
            else
            {
                metier = false;
            }
            source = source_textbox.Text;
            destination = destination_textbox.Text;
            size = (long)Convert.ToDouble(size_textbox.Text);
            Task.Run(RemoteCall);

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            objet.Cancel();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            objet.Resume();
        }
    }
}
