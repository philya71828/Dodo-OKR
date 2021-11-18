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
    public class ObjectiveAdditionVM : ControlViewModel
    {
        private string name;
        private string comment;
        private DateTime startDate;
        private DateTime finishDate;

        private DbEntity currentTable;

        public Objective Objective { get; set; }        
        private ObservableCollection<ObjectiveMask> objectives;

        public ObjectiveAdditionVM(ObservableCollection<ObjectiveMask> objectives, DbEntity entity)
        {
            name = "Цель";
            startDate = DateTime.Now;
            finishDate = DateTime.Now.AddMonths(1);
            this.objectives = objectives;
            currentTable = entity;
        }

        protected override void Create()
        {
            if (!DataChanger.CheckObjectiveField(name, comment,startDate, finishDate))
                return;
            Objective = new Objective(name, comment, startDate, finishDate, objectives.Count);
            if (currentTable is User)
                Objective.UserId = currentTable.Id;
            else
                Objective.TeamId = currentTable.Id;
            using (ApplicationContext db=new ApplicationContext(Connector.Options))
            {
                db.Objectives.Add(Objective);
                db.SaveChanges();
            }
            objectives.Add(new ObjectiveMask() { Obj = new[] { Objective }});
            objectives.Last().Tasks = new ObservableCollection<Task>();
            Visibility = Visibility.Hidden;
        }
    }
}
