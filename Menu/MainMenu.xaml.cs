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

namespace Menu
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Play_btn_Click(object sender, RoutedEventArgs e)
        {
            
            Welcome.Visibility = Visibility.Collapsed;
            PlayMenu.Visibility = Visibility.Visible;
            RulesMenuPage1.Visibility = Visibility.Collapsed;
            SettingsMenu.Visibility = Visibility.Collapsed;
            RuleMenuButtons.Visibility = Visibility.Collapsed;
            RulesMenuPage2.Visibility = Visibility.Collapsed;
            Toggle_button.IsChecked = false;

        }

        private void Rules_btn_Click(object sender, RoutedEventArgs e)
        {
            Welcome.Visibility = Visibility.Collapsed;
            RulesMenuPage1.Visibility = Visibility.Visible;
            PlayMenu.Visibility = Visibility.Collapsed;
            SettingsMenu.Visibility = Visibility.Collapsed;
            RuleMenuButtons.Visibility = Visibility.Visible;
            Toggle_button.IsChecked = false;

        }

        private void Settings_btn_Click(object sender, RoutedEventArgs e)
        {
            Welcome.Visibility = Visibility.Collapsed;
            SettingsMenu.Visibility = Visibility.Visible;
            PlayMenu.Visibility = Visibility.Collapsed;
            RulesMenuPage1.Visibility = Visibility.Collapsed;
            RuleMenuButtons.Visibility = Visibility.Collapsed;
            RulesMenuPage2.Visibility = Visibility.Collapsed;
            Toggle_button.IsChecked = false;

        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void play_btn_Click_1(object sender, RoutedEventArgs e)
        {
            this.Content = new UI.Board();
        }

        private void LastPageBtn_Click(object sender, RoutedEventArgs e)
        {
            RulesMenuPage1.Visibility = Visibility.Visible;
            RulesMenuPage2.Visibility = Visibility.Collapsed;
            RuleMenuButtons.Visibility = Visibility.Visible;
        }

        private void NextPageBtn_Click(object sender, RoutedEventArgs e)
        {
            RulesMenuPage1.Visibility = Visibility.Collapsed;
            RulesMenuPage2.Visibility = Visibility.Visible;
            RuleMenuButtons.Visibility = Visibility.Visible;
        }

        private void Page1Btn_Click(object sender, RoutedEventArgs e)
        {
            RulesMenuPage1.Visibility = Visibility.Visible;
            RulesMenuPage2.Visibility = Visibility.Collapsed;
            RuleMenuButtons.Visibility = Visibility.Visible;
        }

        private void Page2Btn_Click(object sender, RoutedEventArgs e)
        {
            RulesMenuPage1.Visibility = Visibility.Collapsed;
            RulesMenuPage2.Visibility = Visibility.Visible;
            RuleMenuButtons.Visibility = Visibility.Visible;
        }
    }
}
