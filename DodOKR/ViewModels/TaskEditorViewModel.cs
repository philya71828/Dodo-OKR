using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DodOKR
{
    public class TaskEditorViewModel:ViewModel
    {
        private Visibility visibility;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public Task Task { get; private set; }
        private ObjectiveMask objective;

        public TaskEditorViewModel(Task task, ObjectiveMask objective)
        {
            Task = new Task();
            this.objective = objective;            
            Task.Name = task.Name;
            Task.Comment = task.Comment;
            Task.Current = task.Current;
            Task.Target = task.Target;
            Task.StartDate = task.StartDate;
            Task.FinishDate = task.FinishDate;
            Task.Status = task.Status;
            Task.Progress = task.Progress;
            Task.Index = task.Index;
        }

        public ICommand CloseControl => new RelayCommand(obj => Visibility = Visibility.Hidden);
        public ICommand DeleteTask => new RelayCommand(obj => 
        {
            objective.Tasks.RemoveAt(Task.Index);
            for (var i = Task.Index; i < objective.Tasks.Count; i++)
                objective.Tasks[i].Index--;
            var _obj = objective.Objective;
            _obj.Progress = ChangeProgress();
            _obj.Status = SetStatus(_obj.StartDate, _obj.FinishDate, _obj.Progress);            
            Visibility = Visibility.Hidden;
        });
        public ICommand EditTask => new RelayCommand(obj =>
        {
            Task.Progress = (int)((double)Task.Current / (double)Task.Target * 100);
            Task.Status = SetStatus(Task.StartDate, Task.FinishDate, Task.Progress);
            objective.Tasks[Task.Index] = Task;
            var _obj = objective.Objective;
            _obj.Progress = ChangeProgress();
            _obj.Status = SetStatus(_obj.StartDate, _obj.FinishDate, _obj.Progress);            
            Visibility = Visibility.Hidden;
        });

        private Status SetStatus(DateTime start, DateTime finish, int progress)
        {
            double a = DateTime.Now.Subtract(start).Days;
            double b = finish.Subtract(start).Days;
            var res = progress - (a / b * 100);
            if (res < -20)
                return Status.Bad;
            if (res < 20)
                return Status.Good;
            return Status.Great;
        }

        private int ChangeProgress()
        {
            var sum = 0;
            var tasks = objective.Tasks;
            foreach (var task in tasks)
                sum += task.Progress;
            return tasks.Count == 0 ? 0 : sum / tasks.Count;
        }
    }
}
