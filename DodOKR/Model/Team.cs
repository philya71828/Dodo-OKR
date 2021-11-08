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
        public List<Objective> Objectives { get; set; } = new List<Objective>();
        [Required]
        public Company Company { get; set; }
        public Project Project { get; set; }
        public int? CompanyId { get; set; }
        public int? ProjectId { get; set; }
        public int Progress
        {
            get
            {
                var result = 0;
                foreach (var e in Objectives)
                    result += e.Progress;
                return result / Objectives.Count;
            }
        }
    }
}
