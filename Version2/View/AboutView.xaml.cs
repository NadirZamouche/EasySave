using System;
using System.Collections.Generic;
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

namespace Livrable_3.View
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl
    {
        public string path = System.IO.File.ReadAllText(@"C:\000\log\language.txt");
        public AboutView()
        {
            InitializeComponent();
            if (path == "true")
            {
                about.Content = "à propos";
                group.Content = "Application développée par";
            }
            else
            {
                about.Content = "about";
                group.Content = "App developed by";
            }

        }
    }
}
