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
    [Table("Companies")]
    public class Company : DbEntity
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<ObjectiveMask> Objectives { get; set; } = new List<ObjectiveMask>();
    }
}
