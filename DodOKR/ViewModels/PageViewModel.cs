using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR
{
    public abstract class PageViewModel : ViewModel
    {
        public PageType Type { get; set; }
    }
}
