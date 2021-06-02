using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR.Data
{
    public class Project
    {
        public string Name { get; set; }
        public Company Company { get; set; }
        public List<Team> Teams { get; set; }
        public User Head { get; set; }
        public int Progress { get; set; }
        public int Number { get; set; }
    }
}
