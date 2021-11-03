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
    public class TaskAdditionVM : ControlViewModel
    {
        private string name;
        private string comment;
        private string current;
        private string target;
        private DateTime startDate;
        private DateTime finishDate;

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

        protected override void Create()
        {
            if (!DataChanger.CheckTaskField(name, comment, current, target, startDate, finishDate))
                return;
            Task = new Task();
            Task.Name = name;
            Task.Comment = comment;
            Task.StartDate = startDate;
            Task.FinishDate = finishDate;
            Task.Current = int.Parse(current);
            Task.Target = int.Parse(target);
            Task.Progress = (int)(double.Parse(current) / double.Parse(target) * 100);
            Task.Status = DataChanger.SetStatus(startDate, finishDate, Task.Progress);
            Task.Index = objective.Tasks.Count;
            tasks.Add(Task);
            var obj = objective.Objective;
            obj.Progress = DataChanger.ChangeProgress(objective.Tasks.ToList());
            obj.Status = DataChanger.SetStatus(obj.StartDate, obj.FinishDate, obj.Progress);            
            Visibility = Visibility.Hidden;
        }
    }
}
