using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public class Project : DbEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public Company Company { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        [Required]
        public User Head { get; set; }
        public int Progress
        {
            get
            {
                var result = 0;
                foreach (var e in Teams)
                    result += e.Progress;
                return result / Teams.Count;
            }
        }
        public int Number { get; set; }
    }
}
