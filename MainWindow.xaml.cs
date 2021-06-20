using System;
using System.Linq;
using System.Windows;
using ToDoApp.model;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            UpdateListTasks();
        }

        public void UpdateListTasks()
        {
            using var context = new DataContext();
            var tasks = context.Tasks.ToList();

            ItemsPanel.Children.Clear();
            foreach (var task in tasks)
            {
                var item = new TaskItem(task.Text, task.Finish.ToString(), task.TaskId, this);
                ItemsPanel.Children.Add(item);
            }
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();

            var idUser = context.Configs.Find(1).UserAuthId;
            var user = context.Users.Find(idUser);

            var task = new Task()
            {
                Text = TextTask.Text,
                Finish = Date.Value ?? DateTime.Now,
                User = user
            };

            context.Tasks.Add(task);
            context.SaveChanges();

            UpdateListTasks();
        }

        private void OutButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();
            var config = context.Configs.Find(1);
            config.UserAuthId = -1;
            context.Configs.Update(config);
            context.SaveChanges();

            var window = new AuthorizeWindow();
            window.Show();
            this.Close();
        }
    }
}
