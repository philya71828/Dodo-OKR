using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public class Company
    {
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public ObservableCollection<ObjectiveMask> Objectives { get; set; }
    }
}
