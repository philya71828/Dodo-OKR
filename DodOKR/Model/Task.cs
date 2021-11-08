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
    public class Task : Problem
    {

        private int current;
        private int target;

        public int Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged("Current");
            }
        }
        public int Target
        {
            get => target;
            set
            {
                target = value;
                OnPropertyChanged("Target");
            }
        }
        [Required]
        public Objective Objective { get; set; }
        public int? ObjectiveId { get; set; }
    }
}
