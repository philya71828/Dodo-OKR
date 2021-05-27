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
        public List<User> Users { get; set; }
        public ObservableCollection<ObjectiveMask> Objectives { get; set; }
    }
}
