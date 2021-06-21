using System;
using System.Linq;
using System.Windows;
using ToDoApp.model;

namespace ToDoApp
{
    public enum SortType
    {
        Start,
        Finish,
        Checked
    }

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

        public void UpdateListTasks(SortType sortType = SortType.Finish)
        {
            using var context = new DataContext();
            var tasks = context.Tasks.ToList();
            tasks = sortType switch
            {
                SortType.Finish => tasks.OrderBy(t => t.Finish).ToList(),
                SortType.Start  => tasks.OrderBy(t => t.Start).ToList(),
                _               => tasks
            };

            ItemsPanel.Children.Clear();
            foreach (var task in tasks)
            {
                var item = new TaskItem(task.Text, task.Start.ToString(),
                    task.Finish.ToString(), task.TaskId, task.isComplete, this);
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
                isComplete = false,
                Start = DateStart.Value ?? DateTime.Now,
                Finish = DateFinish.Value ?? DateTime.Now,
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

        private void ByStartButton_Click(object sender, RoutedEventArgs e) => UpdateListTasks(SortType.Start);

        private void ByFinishButton_Click(object sender, RoutedEventArgs e) => UpdateListTasks();

        private void GraphButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();
            var tasks = context.Tasks.ToList();
            var window = new GraphicWindow();
            window.Show();
        }
    }
}
