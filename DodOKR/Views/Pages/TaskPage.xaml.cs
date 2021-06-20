using DodOKR.Data;
using System;
using System.Collections.Generic;
using Caliburn.Micro;
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
using System.Data;
using DodOKR.ViewModels;
using DodOKR.Views.Controls;

namespace DodOKR
{
    /// <summary>
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        public TaskPage()
        {
            InitializeComponent();            
            DataContext = new TaskPageViewModel(this, grid);
        }

        private void EditObjective(object sender, MouseButtonEventArgs e) 
            => (DataContext as TaskPageViewModel).EditObjective((Data.Task)((DataGridRow)e.Source).DataContext);

        private void AddNewTask(object sender, RoutedEventArgs e) 
            => (DataContext as TaskPageViewModel).AddNewTask((Data.Task)((Button)e.Source).DataContext);

        private void EditTask(object sender, MouseButtonEventArgs e) 
            => (DataContext as TaskPageViewModel).EditTask((Data.Task)((DataGridRow)e.Source).DataContext);
    }
}
