using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DodOKR
{
    [NotMapped]
    public class ObjectiveMask
    {        
        public Objective Objective { get => Obj[0]; }
        public Objective[] Obj { get; set; }
        public ObservableCollection<Task> Tasks { get; set; }        
    }
}
