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
using MySql.Data;
using MySql.Data.MySqlClient;

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

            Show_Confirm_password.Text = Confirm_password_box.Password;
            Show_Confirm_password.Visibility = Visibility.Visible;
            Password_box.Visibility = Visibility.Collapsed;
        }

        private void Show_pass_checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            Password_box.Password = Show_password.Text;
            Password_box.Visibility = Visibility.Visible;
            Show_password.Visibility = Visibility.Collapsed;

            Confirm_password_box.Password = Show_Confirm_password.Text;
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

        private void Register_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Username = Username_box.Text;
                string InitialPassword = Password_box.Password;
                string ConfirmPassword = Confirm_password_box.Password;

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(InitialPassword) || string.IsNullOrEmpty(ConfirmPassword))
                {
                    MessageBox.Show("Please make sure you enter both a username and passwords.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (InitialPassword != ConfirmPassword)
                {
                    MessageBox.Show("Passwords do not match. Please re enter.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MySqlConnection connection = new MySqlConnection(@"Server=localhost;Database=nea;UID=root;Password=Root");
                connection.Open();

                string adding_data = "INSERT INTO users (username, password) VALUES (@username, @password)";
                MySqlCommand command = new MySqlCommand(adding_data, connection);

                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", InitialPassword);
                command.ExecuteNonQuery();

                connection.Close();

                MessageBox.Show("Registration Successful: Welcome!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration failed: " + ex.Message);
            }
        }

        private void Sign_in_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Username = Username2_box.Text;
                string Password = Password2_box.Password;

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Please make sure you enter both a username and passwords.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                string connectionString = "Server=localhost;Database=nea;UID=root;Password=Root";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT username FROM users WHERE username = @username AND password = @password";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@username", Username2_box.Text);
                    command.Parameters.AddWithValue("@password", Password2_box.Password);

                    // I used a nullable string becuase of a potential null result
                    string? username = command.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(username))
                    {
                        MessageBox.Show("Welcome back, " + username);
                        string loggedInUsername = username;
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log in failed: " + ex.Message);
            }
        }
    }
}
