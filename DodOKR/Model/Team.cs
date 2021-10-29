using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public class Team : DbEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public List<User> Users { get; set; } = new List<User>();
        [Required]
        public ObservableCollection<ObjectiveMask> Objectives { get; set; } = new ObservableCollection<ObjectiveMask>();
        [Required]
        public Company Company { get; set; }
        public Project Project { get; set; }
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
