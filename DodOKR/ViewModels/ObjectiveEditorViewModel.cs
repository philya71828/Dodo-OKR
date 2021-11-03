﻿using System;
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
    public class ObjectiveEditorViewModel :ViewModel
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

        public ObjectiveMask Mask { get; private set; }
        private ObservableCollection<ObjectiveMask> objectives;
        

        public ObjectiveEditorViewModel(Objective objective, ObservableCollection<ObjectiveMask> objectives)
        {
            this.objectives = objectives;
            Mask = new ObjectiveMask { Obj=new[] { objective} };
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
            Mask.Objective.Status = DataChanger.SetStatus(Mask.Objective.StartDate, Mask.Objective.FinishDate, Mask.Objective.Progress);
            Mask.Tasks = objectives[Mask.Objective.Index].Tasks;
            objectives[Mask.Objective.Index] = Mask;
            Visibility = Visibility.Hidden;
        });
    }
}
