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
    public class TaskEditorViewModel : ControlViewModel
    {
        public Task Task { get; private set; }
        private ObjectiveMask mask;

        public TaskEditorViewModel(Task task, ObjectiveMask objective)
        {
            Task = task;
            this.mask = objective;
        }

        protected override void DeleteProblem()
        {            
            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                db.Tasks.Remove(Task);
                db.SaveChanges();
            }
            mask.Tasks.RemoveAt(Task.Index);
            for (var i = Task.Index; i < mask.Tasks.Count; i++)
                mask.Tasks[i].Index--;
            var objective = mask.Objective;
            SetObjectiveProgress(mask, objective);
            objective.Status = DataChanger.SetStatus(objective.StartDate, objective.FinishDate, objective.Progress);
            Visibility = Visibility.Hidden;
        }

        protected override void EditProblem()
        {
            Task.Progress = (int)((double)Task.Current / (double)Task.Target * 100);
            Task.Status = DataChanger.SetStatus(Task.StartDate, Task.FinishDate, Task.Progress);
            mask.Tasks[Task.Index] = Task;
            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                db.Tasks.Update(Task);
                db.SaveChanges();
            }
            var objective = mask.Objective;
            SetObjectiveProgress(mask, objective);
            objective.Status = DataChanger.SetStatus(objective.StartDate, objective.FinishDate, objective.Progress);
            Visibility = Visibility.Hidden;
        }
    }
}
