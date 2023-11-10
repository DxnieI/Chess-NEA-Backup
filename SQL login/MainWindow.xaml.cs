using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

                using (connection)
                {
                    connection.Open();

                    string slowHashSalt = PasswordHashing.CreateHash(Password_box.Password);
                    int Index = slowHashSalt.IndexOf(":");
                    string extractedString = slowHashSalt.Substring(0, Index);
                    Index = slowHashSalt.IndexOf(":");
                    extractedString = slowHashSalt.Substring(++Index);
                    Index = extractedString.IndexOf(":");
                    Index = extractedString.IndexOf(":");
                    extractedString = extractedString.Substring(++Index);

                    string saltQuery = "INSERT INTO users (username, slowHashSalt) VALUES (@username, @slowHashSalt)";
                    MySqlCommand cmd = new MySqlCommand(saltQuery, connection);

                    cmd.Parameters.AddWithValue("@username", Username);
                    cmd.Parameters.AddWithValue("@slowHashSalt", slowHashSalt);

                    cmd.ExecuteReader();

                    connection.Close();

                    MessageBox.Show("Registration Successful: Welcome!");
                }
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
                    MessageBox.Show("Please make sure you enter both a username and password.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string connectionString = "Server=localhost;Database=nea;UID=root;Password=Root";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT slowHashSalt FROM users WHERE username = @username";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    command.Parameters.AddWithValue("@username", Username2_box.Text);

                    // Retrieve the slow hash salt for the provided username
                    string? slowHashSalt = command.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(slowHashSalt))
                    {
                        // Use the VerifyPassword method to validate the provided password
                        if (PasswordHashing.PasswordVerification(Password, slowHashSalt))
                        {
                            MessageBox.Show("Welcome back, " + Username);
                            MenuScreen();

                        }
                        else
                        {
                            MessageBox.Show("Invalid password");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username");
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Log in failed: " + ex.Message);
            }
        }

        public void MenuScreen()
        {
            Content = new Menu.MainMenu();
            this.Width = 850;
            this.Height = 500;
            this.ResizeMode = ResizeMode.CanResize;

            this.Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
        }
    }
}