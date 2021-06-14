using DodOKR.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DodOKR.ViewModels
{
    public class TaskAdditionVM:ViewModel
    {
        private string name;
        private string comment;
        private string current;
        private string target;
        private DateTime startDate;
        private DateTime finishDate;
        private Priority priority;

        private Visibility visibility;

        #region
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public string Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        }
        public string Target
        {
            get => target;
            set
            {
                target = value;
                OnPropertyChanged("Target");
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime FinishDate
        {
            get => finishDate;
            set
            {
                finishDate = value;
                OnPropertyChanged("FinishDate");
            }
        }

        public Priority Priority
        {
            get { return priority; }
            set
            {
                if (priority == value)
                    return;

                priority = value;
                OnPropertyChanged("Priority");
                OnPropertyChanged("IsLowPriority");
                OnPropertyChanged("IsMiddlePriority");
                OnPropertyChanged("IsHighPriority");
            }
        }

        public bool IsLowPriority
        {
            get { return Priority == Priority.Low; }
            set { Priority = value ? Priority.Low : Priority; }
        }

        public bool IsMiddlePriority
        {
            get { return Priority == Priority.Middle; }
            set { Priority = value ? Priority.Middle : Priority; }
        }

        public bool IsHighPriority
        {
            get { return Priority == Priority.High; }
            set { Priority = value ? Priority.High : Priority; }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
        #endregion

        public Data.Task Task { get; set; }
        private ObservableCollection<Data.Task> tasks;
        private ObjectiveMask objective;

        public TaskAdditionVM(ObjectiveMask objective)
        {
            name = "Задача";
            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddMonths(1);
            priority = Priority.Middle;
            this.objective = objective;
            tasks = objective.Tasks;
        }

        public ICommand AddTask => new RelayCommand(obj => CreateNewTask());

        public ICommand CloseControl => new RelayCommand(obj => Visibility = Visibility.Hidden);

        private void CreateNewTask()
        {
            if (!CheckAllFields(name, comment, current, target, startDate, finishDate))
                return;
            Task = new Data.Task();
            Task.Name = name;
            Task.Comment = comment;
            Task.StartDate = startDate;
            Task.FinishDate = finishDate;
            Task.Priority = priority;
            Task.Current = int.Parse(current);
            Task.Target = int.Parse(target);
            Task.Progress = (int)(double.Parse(current) / double.Parse(target) * 100);
            Task.Status = SetStatus(startDate, finishDate, Task.Progress);
            Task.Index = objective.Tasks.Count;
            tasks.Add(Task);
            var obj = objective.Objective;
            obj.Progress = ChangeProgress();
            obj.Status = SetStatus(obj.StartDate, obj.FinishDate, obj.Progress);            
            Visibility = Visibility.Hidden;
        }

        private bool CheckAllFields(string name, string comment, string target, string current,
                                    DateTime? start, DateTime? finish)
        {
            int num;
            var a = name != "";
            var b = comment != "";
            var c = start != null;
            var d = finish != null;
            var g = target != "" && int.TryParse(target, out num);
            var h = current != "" && int.TryParse(current, out num);

            return a && b && c && d && g && h;
        }

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
            foreach (var task in objective.Tasks)
                sum += task.Progress;
            return sum / objective.Tasks.Count;
        }
    }
}
