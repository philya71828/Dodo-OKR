using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodOKR.ViewModels
{
    public abstract class PageViewModel : ViewModel
    {
        public Data.PageType Type { get; set; }
    }
}
