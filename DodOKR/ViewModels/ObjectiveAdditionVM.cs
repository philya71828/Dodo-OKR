using Caliburn.Micro;
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
    public class ObjectiveAdditionVM : ViewModel
    {
        private string name;
        private string comment;
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

        public Objective Objective { get; set; }        
        private ObservableCollection<ObjectiveMask> objectives;

        public ObjectiveAdditionVM(ObservableCollection<ObjectiveMask> objectives)
        {
            name = "Цель";
            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddMonths(1);
            this.objectives = objectives;
        }

        public ICommand AddObjective => new RelayCommand(obj => CreateNewObjective());

        public ICommand CloseControl => new RelayCommand(obj => Visibility=Visibility.Hidden);

        private void CreateNewObjective()
        {
            if (!CheckAllFields(name, comment,startDate, finishDate))
                return;
            Objective = new Objective();
            Objective.Name = name;
            Objective.Comment = comment;
            Objective.StartDate = startDate;
            Objective.FinishDate = finishDate;
            Objective.Progress = 0;
            Objective.Index = objectives.Count;
            Objective.Status = Status.Good;
            objectives.Add(new ObjectiveMask() { Obj = new[] { Objective }});
            objectives.Last().Tasks = new ObservableCollection<Task>();
            Visibility = Visibility.Hidden;
        }

        private bool CheckAllFields(string name, string comment, DateTime? start, DateTime? finish)
        {
            var a = name != "";
            var b = comment != "";
            var c = start != null;
            var d = finish != null;

            return a & b & c & d;
        }
    }
}
