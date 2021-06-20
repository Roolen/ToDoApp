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

namespace ToDoApp
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public int idTask;
        private MainWindow ma;
        public EditWindow(string text, string date, int id, MainWindow ma)
        {
            InitializeComponent();

            TextTask.Text = text;
            DateTask.Text = date;
            idTask = id;
            this.ma = ma;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            using var context = new DataContext();
            var task = context.Tasks.Find(idTask);
            task.Text = TextTask.Text;
            task.Finish = DateTask.Value ?? DateTime.Now;

            context.Tasks.Update(task);
            context.SaveChanges();

            ma.UpdateListTasks();
            this.Close();
        }
    }
}
