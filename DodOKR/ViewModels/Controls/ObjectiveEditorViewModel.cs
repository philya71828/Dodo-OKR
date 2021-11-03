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
    public class ObjectiveEditorViewModel : ControlViewModel
    {
        public ObjectiveMask Mask { get; private set; }
        private ObservableCollection<ObjectiveMask> objectives;
        

        public ObjectiveEditorViewModel(Objective objective, ObservableCollection<ObjectiveMask> objectives)
        {
            this.objectives = objectives;
            Mask = new ObjectiveMask { Obj=new[] { objective} };
        }

        protected override void DeleteProblem()
        {
            objectives.RemoveAt(Mask.Objective.Index);
            for (var i = Mask.Objective.Index; i < objectives.Count; i++)
                objectives[i].Objective.Index--;
            Visibility = Visibility.Hidden;
        }

        protected override void EditProblem()
        {
            Mask.Objective.Status = DataChanger.SetStatus(Mask.Objective.StartDate, Mask.Objective.FinishDate, Mask.Objective.Progress);
            Mask.Tasks = objectives[Mask.Objective.Index].Tasks;
            objectives[Mask.Objective.Index] = Mask;
            Visibility = Visibility.Hidden;
        }
    }
}
