using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public class User
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public ObservableCollection<ObjectiveMask> Objectives { get; set; }
    }
}
