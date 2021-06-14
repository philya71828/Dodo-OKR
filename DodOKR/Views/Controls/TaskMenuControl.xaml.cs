using DodOKR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DodOKR
{
    /// <summary>
    /// Interaction logic for TaskMenuControl.xaml
    /// </summary>
    public partial class TaskMenuControl : UserControl
    {
        public bool IsJoinPage { get => (DataContext as TaskMenuVM).IsJoinPage; }
        public bool IsTreeOpened { get => (DataContext as TaskMenuVM).IsTreeOpened; }
        public TaskMenuControl(TaskPageViewModel tvm)
        {
            InitializeComponent();
            DataContext = new TaskMenuVM(tvm);
        }
    }
}
