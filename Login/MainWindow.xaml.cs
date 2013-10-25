using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public List<Users> UsersList = new List<Users>(); //список логінів-паролей
        private int LoginedIndex; //індекс залогіненого
        private bool Logined; //чі залогінився
        public MainWindow()
        {
            InitializeComponent();
            ReadAllLoginsFromBase();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush b = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            buttonClose.Background = b;
            Close();
        }

        private void ButtonRegClick1(object sender, RoutedEventArgs e)
        {
            Registration a = new Registration();
            Close();
            a.ShowDialog();

        }

        private void buttonEnter_Click_2(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxLogin.Text) || String.IsNullOrWhiteSpace(passwordBoxPass.Password))
            {
                MessageBox.Show("Заповніть будь-ласка усі поля");
                return;
            }
            for (int i = 0; i < UsersList.Count(); i++)
            {
                if(string.Compare(textBoxLogin.Text, UsersList[i].Login, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.Compare(passwordBoxPass.Password, UsersList[i].Password, StringComparison.CurrentCulture) ==0)
                {
                    MessageBox.Show("Ви увійшли успішно!");
                    Logined = true;
                    break;
                }
                
            }
            if(!Logined)
                MessageBox.Show("Невірний логін або пароль");
        }

        public static void WriteAllLoginsToBase()
        {
            if (!Directory.Exists(@"files"))//якшо не буде папкі чогось, то нову створить
                Directory.CreateDirectory(@"files");

            BinaryWriter writer = new BinaryWriter(File.Open(@"files\base.dat", FileMode.Create),
                                                       Encoding.BigEndianUnicode);
            foreach (Users login in UsersList)
            {
                writer.Write(login.Login);
                writer.Write(login.Password);
            }
            writer.Close();
        }
        public static void ReadAllLoginsFromBase()
        {
            UsersList.Clear(); //очищаємо список, для уникнення дублікатів
            try
            {
                BinaryReader reader = new BinaryReader(File.Open(@"files\base.dat", FileMode.Open), Encoding.BigEndianUnicode);
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    UsersList.Add(new Users(reader.ReadString(), reader.ReadString()));
                }
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Виникла помилка з файлом бази данних профілів");
            }
        }
    }
}
