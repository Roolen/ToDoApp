using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Security.Cryptography;
using ToDoApp.model;

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();

            var hash = PasswordText.Password;
            var user = new User()
            {
                Login = LoginText.Text,
                Password = hash
            };

            context.Users.Add(user);
            context.SaveChanges();

            this.Close();
        }
    }
}
