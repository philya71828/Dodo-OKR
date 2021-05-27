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
        private TaskPageViewModel vm;
        public TaskMenuControl(TaskPageViewModel vm)
        {
            this.vm = vm;
            InitializeComponent();
        }

        private void HideTaskMenu(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void OpenJoinPage(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
        }

        private void TurnPersonal(object sender, RoutedEventArgs e) => Turn(Data.PageType.Personal);

        private void TurnTeam(object sender, RoutedEventArgs e) => Turn(Data.PageType.Team);

        private void TurnCompany(object sender, RoutedEventArgs e) => Turn(Data.PageType.Company);

        private void Turn(Data.PageType type)
        {
            vm.Type = type;
            this.Visibility = Visibility.Hidden;
        }
    }
}
