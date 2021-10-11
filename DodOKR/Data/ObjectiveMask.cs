using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DodOKR
{
    public class ObjectiveMask
    {        
        public Task Objective { get => Obj[0]; }
        public Task[] Obj { get; set; }
        public ObservableCollection<Task> Tasks { get; set; }        
    }
}
