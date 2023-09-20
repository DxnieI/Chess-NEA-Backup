using System;
using System.CodeDom;
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

namespace Menu
{
    /// <summary>
    /// Interaction logic for PlayMenu.xaml
    /// </summary>
    public partial class PlayMenu : UserControl
    {
        public PlayMenu()
        {
            InitializeComponent();
        }

        private void Play_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rules_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Settings_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void play_btn_Click_1(object sender, RoutedEventArgs e)
        {
            this.Content = new MainWindow();
        }
    }
}
