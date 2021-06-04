using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR.Data
{
    public class Team
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public ObservableCollection<ObjectiveMask> Objectives { get; set; }
        public int Progress
        {
            get
            {
                var result = 0;
                foreach (var e in Objectives)
                    result += e.Objective.Progress;
                return result / Objectives.Count;
            }
        }
    }
}
