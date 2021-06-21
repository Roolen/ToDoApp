using DevExpress.Mvvm.Gantt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ToDoApp
{
    public class GraphViewModel
    {
        public ObservableCollection<GanttTask> Tasks { get; set; }

        public GraphViewModel()
        {
            using var context = new DataContext();
            var tasks = context.Tasks.ToList();
            Tasks = new ObservableCollection<GanttTask>();

            foreach (var task in tasks)
            {
                Tasks.Add(new GanttTask()
                {
                    Id = task.TaskId,
                    Name = task.Text,
                    StartDate = task.Start,
                    FinishDate = task.Finish
                });
            }
        }
    }
}
