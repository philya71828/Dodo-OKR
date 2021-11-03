using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DodOKR
{
    public class ControlViewModel : ViewModel
    {
        private Visibility visibility;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public ICommand CloseControl => new RelayCommand(obj => Visibility = Visibility.Hidden);

        public ICommand Add => new RelayCommand(obj => Create());        

        public ICommand Edit => new RelayCommand(obj => EditProblem());

        public ICommand Delete => new RelayCommand(obj => DeleteProblem());

        protected virtual void DeleteProblem() { }

        protected virtual void EditProblem() { }

        protected virtual void Create() { }
    }
}
