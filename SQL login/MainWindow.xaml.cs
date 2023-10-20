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

namespace SQL_login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            String Password = Password_box.Password;
        }

        private void Log_in_btn_Click(object sender, RoutedEventArgs e)
        {
            Login_screen.Visibility = Visibility.Visible;
            Register_screen.Visibility = Visibility.Collapsed;
        }

        private void Sign_up_btn_Click(object sender, RoutedEventArgs e)
        {
            Register_screen.Visibility = Visibility.Visible;
            Login_screen.Visibility = Visibility.Collapsed;
        }

        private void Show_pass_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            Show_password.Text = Password_box.Password;
            Show_password.Visibility = Visibility.Visible;
            Password_box.Visibility = Visibility.Collapsed;

            Show_Confirm_password.Text = Password_box.Password;
            Show_Confirm_password.Visibility = Visibility.Visible;
            Password_box.Visibility = Visibility.Collapsed;
        }

        private void Show_pass_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Password_box.Password = Show_password.Text;
            Password_box.Visibility = Visibility.Visible;
            Show_password.Visibility = Visibility.Collapsed;

            Password_box.Password = Show_Confirm_password.Text;
            Password_box.Visibility = Visibility.Visible;
            Show_Confirm_password.Visibility = Visibility.Collapsed;

        }

        private void Show_pass2_checkbox_Checked(object sender, RoutedEventArgs e)
        {
            Show_Password2.Text = Password2_box.Password;
            Show_Password2.Visibility = Visibility.Visible;
            Password2_box.Visibility = Visibility.Collapsed;
        }

        private void Show_pass2_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Password2_box.Password = Show_Password2.Text;
            Password2_box.Visibility = Visibility.Visible;
            Show_Password2.Visibility = Visibility.Collapsed;
        }

        private void Clear_btn_Click(object sender, RoutedEventArgs e)
        {
            Username_box.Text = String.Empty;
            Password_box.Password = String.Empty;
            Show_password.Text = String.Empty;
            Confirm_password_box.Password = String.Empty;
            Show_Confirm_password.Text = String.Empty;

        }

        private void Clear2_btn_Click(object sender, RoutedEventArgs e)
        {
            Username2_box.Text = String.Empty;
            Password2_box.Password = String.Empty;
            Show_Password2.Text = String.Empty;
            
        }

        
    }
}
