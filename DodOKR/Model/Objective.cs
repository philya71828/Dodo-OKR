using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DodOKR
{
    //У цели не должно быть current и target
    public class Objective : Problem
    {
        public List<Task> Tasks { get; set; } = new List<Task>();
        public Project Project { get; set; }
        public User User { get; set; }
        public Team Team { get; set; }
    }
}
