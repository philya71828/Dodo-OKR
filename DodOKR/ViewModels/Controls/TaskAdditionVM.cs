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
        private ObjectiveMask mask;

        public TaskAdditionVM(ObjectiveMask objective)
        {
            name = "Задача";
            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddMonths(1);
            this.mask = objective;
            tasks = objective.Tasks;
        }

        protected override void Create()
        {
            if (!DataChanger.CheckTaskField(name, comment, current, target, startDate, finishDate))
                return;
            Task = new Task(name, comment,
                startDate, finishDate,
                int.Parse(current), int.Parse(current),
                mask.Tasks.Count, mask.Objective.Id);
            Task.Progress = (int)(double.Parse(current) / double.Parse(target) * 100);
            Task.Status = DataChanger.SetStatus(startDate, finishDate, Task.Progress);
            tasks.Add(Task);
            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                db.Tasks.Add(Task);
                db.SaveChanges();
            }
            var objective = mask.Objective;
            SetObjectiveProgress(mask, objective);
            objective.Status = DataChanger.SetStatus(objective.StartDate, objective.FinishDate, objective.Progress);            
            Visibility = Visibility.Hidden;
        }
    }
}
