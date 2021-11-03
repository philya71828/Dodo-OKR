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

namespace DodOKR
{
    public class TaskAdditionVM:ViewModel
    {
        private string name;
        private string comment;
        private string current;
        private string target;
        private DateTime startDate;
        private DateTime finishDate;

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

        public Task Task { get; set; }
        private ObservableCollection<Task> tasks;
        private ObjectiveMask objective;

        public TaskAdditionVM(ObjectiveMask objective)
        {
            name = "Задача";
            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddMonths(1);
            this.objective = objective;
            tasks = objective.Tasks;
        }

        //Поправить иерархию (мб добавить делегирование)
        public ICommand AddTask => new RelayCommand(obj => CreateNewTask());

        public ICommand CloseControl => new RelayCommand(obj => Visibility = Visibility.Hidden);

        private void CreateNewTask()
        {
            if (!CheckAllFields(name, comment, current, target, startDate, finishDate))
                return;
            Task = new Task();
            Task.Name = name;
            Task.Comment = comment;
            Task.StartDate = startDate;
            Task.FinishDate = finishDate;
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

        //Инкапсулировать
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

        //Инкапсулировать
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

        //Инкапсулировать
        private int ChangeProgress()
        {
            var sum = 0;
            foreach (var task in objective.Tasks)
                sum += task.Progress;
            return sum / objective.Tasks.Count;
        }
    }
}
