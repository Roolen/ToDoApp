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

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для TaskItem.xaml
    /// </summary>
    public partial class TaskItem : UserControl
    {
        public int id;
        private MainWindow ma;
        private Brush backIsComplete = new SolidColorBrush(new Color() { R = 50, G = 50, B = 150, A = 1 });
        public TaskItem(string text, string dateStart, string dateFinish, int id, bool isComplete, MainWindow ma)
        {
            InitializeComponent();

            this.ma = ma;
            this.id = id;
            TextTask.Text = text;
            CompleteBox.IsChecked = isComplete;
            TextDateStart.Text = dateStart;
            TextDateFinish.Text = dateFinish;

            if (isComplete == true)
            {
                Background = backIsComplete;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();
            var task = context.Tasks.Find(id);
            context.Tasks.Remove(task);
            context.SaveChanges();
            ma.UpdateListTasks();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new EditWindow(TextTask.Text, TextDateStart.Text, TextDateFinish.Text, id, ma);
            window.ShowDialog();
        }

        private void CompleteBox_Checked(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();
            var task = context.Tasks.Find(id);
            task.isComplete = CompleteBox.IsChecked ?? false;
            context.SaveChanges();

            if (CompleteBox.IsChecked == true)
            {
                Background = backIsComplete;
            }
        }
    }
}
