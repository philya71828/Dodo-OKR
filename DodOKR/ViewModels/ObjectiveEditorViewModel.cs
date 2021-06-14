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
    public class ObjectiveEditorViewModel :ViewModel
    {
        private Visibility visibility;
        private Priority priority;

        public Priority Priority
        {
            get => priority;
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

        public ObjectiveMask Mask { get; private set; }
        private ObservableCollection<ObjectiveMask> objectives;
        

        public ObjectiveEditorViewModel(Data.Task objective, ObservableCollection<ObjectiveMask> objectives)
        {
            this.objectives = objectives;
            Mask = new ObjectiveMask { Obj=new[] { objective} };
            Priority = objective.Priority;
        }

        public ICommand CloseControl => new RelayCommand(obj => Visibility = Visibility.Hidden);
        public ICommand DeleteObjective => new RelayCommand(obj =>
        {
            objectives.RemoveAt(Mask.Objective.Index);
            for (var i = Mask.Objective.Index; i < objectives.Count; i++)
                objectives[i].Objective.Index--;
            Visibility = Visibility.Hidden;
        });
        public ICommand EditObjective => new RelayCommand(obj =>
        {
            Mask.Objective.Status = SetStatus(Mask.Objective.StartDate, Mask.Objective.FinishDate, Mask.Objective.Progress);
            Mask.Objective.Priority = Priority;
            Mask.Tasks = objectives[Mask.Objective.Index].Tasks;
            objectives[Mask.Objective.Index] = Mask;
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
    }
}
