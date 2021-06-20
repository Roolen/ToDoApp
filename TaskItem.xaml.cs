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
        public TaskItem(string text, string date, int id, MainWindow ma)
        {
            InitializeComponent();

            this.ma = ma;
            this.id = id;
            TextTask.Text = text;
            TextDate.Text = date;
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
            var window = new EditWindow(TextTask.Text, TextDate.Text, id, ma);
            window.ShowDialog();
        }
    }
}
