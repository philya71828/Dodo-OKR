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
            var index = Mask.Objective.Index;
            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                db.Objectives.Remove(objectives[index].Objective);
                db.SaveChanges();
            }
            objectives.RemoveAt(index);
            for (var i = Mask.Objective.Index; i < objectives.Count; i++)
                objectives[i].Objective.Index--;
            Visibility = Visibility.Hidden;
        }

        protected override void EditProblem()
        {
            Mask.Objective.Status = DataChanger.SetStatus(Mask.Objective.StartDate, Mask.Objective.FinishDate, Mask.Objective.Progress);
            Mask.Tasks = objectives[Mask.Objective.Index].Tasks;
            var index = Mask.Objective.Index;
            objectives[index] = Mask;
            var objective = Mask.Objective;
            using(ApplicationContext db=new ApplicationContext(Connector.Options))
            {
                db.Objectives.Update(objective);
                db.SaveChanges();
            }
            Visibility = Visibility.Hidden;
        }
    }
}
