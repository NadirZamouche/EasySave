using Livrable_3.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text.RegularExpressions;
using System.Windows.Media.TextFormatting;

namespace Livrable_3.View
{
    /// <summary>
    /// Interaction logic for FullBackupView.xaml
    /// </summary>
    public partial class FullBackupView : UserControl
    {
        string Dpath;
        string Spath;
        bool SubDirectory = true;
        bool SubDirectoryno = false;
        //Thread FullCPTask;
        public bool metier;
        public long size;

        public string path = System.IO.File.ReadAllText(@"C:\000\log\language.txt");

        FullBackupViewModel objet = new FullBackupViewModel();
        public FullBackupView()
        {
            InitializeComponent();

            if (path == "true")
            {
                Full_BackUp.Text = "Sauvegarde complète";
                status.Text = "prêt";
                Source_Folder_label.Text = "Dossier source";
                Destination_Folder_label.Text = "Dossier destination";
                Source.Content = "Source ...";
                Destination.Content = "Destination ...";
                Copy.Content = "Copier !";
                pause.Content = "Pause";
                @continue.Content = "Continuer";
                cancel.Content = "Annuler";
                sub_folders_checkbox.Content = "Copier les sous-dossiers";
                Max_size_label.Content = "Taille maximale du fichier";
                exe_check.Content = "Ne copiez pas les logiciels métier";
            }
            else if (path == "false")
            {
                Full_BackUp.Text = "Full BackUp";
                status.Text = "Ready";
                Source_Folder_label.Text = "Source Folder";
                Destination_Folder_label.Text = "Destination Folder";
                Source.Content = "Source ...";
                Destination.Content = "Destination ...";
                Copy.Content = "Copy !";
                pause.Content = "Pause";
                @continue.Content = "Continue";
                cancel.Content = "Cancel";
                sub_folders_checkbox.Content = "Copy subfolders";
                Max_size_label.Content = "Max file size";
                exe_check.Content = "Do not copy business software";
            }


        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialogSource = new CommonOpenFileDialog();
            dialogSource.IsFolderPicker = true;
            // dialog.Filter = "All files (*.*)|*.*";
            if (dialogSource.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Spath = dialogSource.FileName;

                Source_textbox.Text = Spath;


            }
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Dpath = dialog.FileName;

                Destination_textbox.Text = Dpath;
            }
        }
        public void RemoteCalltrue()
        {

            objet.CopyConvert(Spath, Dpath, SubDirectory, metier, size);



        }
        public void RemoteCallfalse()
        {


            objet.CopyConvert(Spath, Dpath, SubDirectoryno, metier, size);



        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (exe_check.IsChecked == true)
            {
                metier = true;
            }
            else
            {
                metier = false;
            }
            size = (long)Convert.ToDouble(Max_size_textbox.Text);
            if (sub_folders_checkbox.IsChecked == true)
            {
                Task.Run(RemoteCalltrue);
            }
            else
            {

                Task.Run(RemoteCallfalse);

            }

            if (path == "true")
            {
                status.Text = "Opération terminée";
            }
            else if (path == "false")
            {
                status.Text = "Operation Completed";
            }

        }

        public void pasue_Click(object sender, RoutedEventArgs e)
        {

            // FullCPTask.Suspend();
            if (path == "true")
            {
                status.Text = "Tâche suspendue";
            }
            else if (path == "false")
            {
                status.Text = "Task Suspended";
            }
            objet.Pause();
        }




        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void continue_Click(object sender, RoutedEventArgs e)
        {
            if (path == "true")
            {
                status.Text = "Tâche a repris";
            }
            else if (path == "false")
            {
                status.Text = "Task resumed";
            }
            objet.Resume();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (path == "true")
            {
                status.Text = "Tâche annulé";
            }
            else if (path == "false")
            {
                status.Text = "Task canceled";
            }
            objet.Cancel();
        }
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
