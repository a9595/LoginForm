using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void WindowMouseLeftButtonDown1(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonCloseClick(object sender, RoutedEventArgs e)
        {
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            buttonClose.Background = b;
            Close();
        }

        private void ButtonAcceptClick1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                string.IsNullOrWhiteSpace(passwordBoxPass.Password) ||
                string.IsNullOrWhiteSpace(passwordBoxPassAgain.Password))
                MessageBox.Show("Заповніть будь-ласка УСІ поля");
            else
            {
                if (passwordBoxPass.Password != passwordBoxPassAgain.Password)
                {
                    MessageBox.Show("Повторний пароль не вірний =(");
                    passwordBoxPassAgain.Focus();
                    passwordBoxPassAgain.SelectAll();
                }
                else
                {
                    Users tmp = new Users(textBoxLogin.Text, passwordBoxPass.Password);
                    if (MainWindow.UsersList.Contains(tmp))
                    {
                        MessageBox.Show(string.Format("профіль  " + textBoxLogin.Text + " уже існує." +
                                                  "\nСтворіть будь-ласка профіль з іншою назвою"));
                        textBoxLogin.Clear();
                        passwordBoxPass.Clear();
                        passwordBoxPassAgain.Clear();
                    }
                    else
                    {
                        MainWindow.UsersList.Add(tmp);
                        MainWindow.WriteAllLoginsToBase();
                        MessageBox.Show(string.Format("Логін: {0} ", textBoxLogin.Text),
                                    "Профіль створено успішно");
                        MainWindow window = new MainWindow();
                        Close();
                        window.ShowDialog();
                    }
                }
            }

        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            MainWindow tmp = new MainWindow();
            Close();
            tmp.ShowDialog();
        }
    }
}
