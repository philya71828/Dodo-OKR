using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DodOKR
{
    public class TaskMenuVM: ViewModel
    {
        public bool IsJoinPage { get => isJoinPage; }
        public bool IsTreeOpened { get => isTreeOpened; }
        public bool Turned { get => turned; }

        private bool isJoinPage = false;
        private bool isTreeOpened = false;
        private bool turned = false;

        private Visibility visibility;
        private TaskPageViewModel tvm;

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public TaskMenuVM(TaskPageViewModel tvm)
        {
            this.tvm = tvm;
            if (tvm.Type == PageType.Tree) isTreeOpened = true;
        }

        public ICommand TurnPersonal => new RelayCommand(obj => Turn(PageType.Personal));
        public ICommand TurnTeam => new RelayCommand(obj => Turn(PageType.Team));
        public ICommand TurnCompany => new RelayCommand(obj => Turn(PageType.Company));
        public ICommand OpenTree=>new RelayCommand(obj =>
        {
            isTreeOpened = true;
            Turn(PageType.Tree);
        });

        public ICommand HideTaskMenu => new RelayCommand(obj => 
        {
            turned = true;
            CloseTaskMenu();
        });

        public ICommand OpenJoinPage=>new RelayCommand(obj =>
        {
            isJoinPage = true;
            CloseTaskMenu();
        });

        private void Turn(PageType type)
        {
            if (tvm.Type != type)
            {
                tvm.Type = type;
                CloseTaskMenu();
            }
        }

        private void CloseTaskMenu() => Visibility = Visibility.Hidden;
    }
}
