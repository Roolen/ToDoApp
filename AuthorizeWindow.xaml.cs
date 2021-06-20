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
using System.Windows.Shapes;
using ToDoApp.model;

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для AuthorizeWindow.xaml
    /// </summary>
    public partial class AuthorizeWindow : Window
    {
        public AuthorizeWindow()
        {
            InitializeComponent();

            using var context = new DataContext();
            var config = context.Configs.Find(1);
            if (config is null)
            {
                config = new Config()
                {
                    ConfigId = 1,
                    UserAuthId = -1
                };

                context.Configs.Add(config);
                context.SaveChanges();
            }
            else
            {
                if (config.UserAuthId != -1)
                {
                    var window = new MainWindow();
                    window.Show();
                    this.Close();
                }
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateWindow();
            createWindow.Owner = this;
            createWindow.ShowDialog();
        }

        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();
            var login = LoginText.Text;
            var password = PasswordText.Password;

            var user = context.Users.Where(u => u.Login == login && u.Password == password)
                .FirstOrDefault();

            if (user is not null)
            {
                context.Configs.Find(1).UserAuthId = user.UserId;
                context.SaveChanges();

                var window = new MainWindow();
                window.Show();
                this.Close();
            }
            else
            {
                ErrorText.Content = "Неверный логин/пароль.";
                LoginText.Clear();
                PasswordText.Clear();
            }
        }
    }
}
