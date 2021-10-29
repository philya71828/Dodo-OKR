using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public class User : DbEntity
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string SurName { get; set; }
        [MaxLength(20)]
        public string Patronymic { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public Team Team { get; set; }
        [Column("Objectives")]
        public List<Objective> Objectives { get; set; } = new List<Objective>();
        //[NotMapped]
        //public ObservableCollection<ObjectiveMask> Objectives { get; set; }
    }
}
