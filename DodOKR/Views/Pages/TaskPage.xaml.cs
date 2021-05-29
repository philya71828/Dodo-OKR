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
            DataContext = new TaskPageViewModel();
        }

        private void ShowTaskMenu(object sender, RoutedEventArgs e)
        {
            var element = new TaskMenuControl((DataContext as TaskPageViewModel));
            grid.Children.Add(element);
            element.IsVisibleChanged += CloseTaskMenu;
        }

        private void CloseTaskMenu(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = grid.Children[grid.Children.Count - 1] as TaskMenuControl;
            grid.Children.RemoveAt(grid.Children.Count - 1);
            if (element.IsJoinPage) OpenJoinPage();
            if (element.IsTreeOpened) OpenTree();

        }

        private void OpenJoinPage()
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void OpenTree()
        {
            var element = new GlobalTree();
            grid.Children.Add(element);
            element.IsVisibleChanged += CloseTree;
        }

        private void CloseTree(object sender, DependencyPropertyChangedEventArgs e)
        {
            grid.Children.RemoveAt(grid.Children.Count - 1);
        }

        private void AddNewObjective(object sender, RoutedEventArgs e)
        {
            var element = new ObjectiveAdditionControl((DataContext as TaskPageViewModel).Objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }        

        private void AddNewTask(object sender, RoutedEventArgs e)
        {
            var ind = (Data.Task)((Button)e.Source).DataContext;
            var objective = (DataContext as TaskPageViewModel).Objectives[ind.Index];
            var element = new TaskAdditionControl(objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }        

        private void EditTask(object sender, MouseButtonEventArgs e)
        {
            var objectives = (DataContext as TaskPageViewModel).Objectives;
            var objective = new ObjectiveMask();
            var task = (Data.Task)((DataGridRow)e.Source).DataContext;
            var i = task.Index;
            foreach (var obj in objectives)
            {                
                if (obj.Tasks.Count>=i && obj.Tasks.Count>0)
                    if (obj.Tasks[i] == task)
                    {
                        objective = obj;
                        break;
                    }

            }

            var element = new TaskEditorControl(task, objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }



        private void Destroy(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (DataContext as TaskPageViewModel);
            var objSum = 0;
            foreach (var obj in element.Objectives)
            {
                var taskSum = 0;
                foreach (var task in obj.Tasks)
                {
                    taskSum += task.Progress;
                }
                objSum += obj.Tasks.Count == 0 ? 0 : taskSum / obj.Tasks.Count;
            }

            var count = element.Objectives.Count;
            element.Progress = count == 0 ? 0 : objSum / count;
            grid.Children.RemoveAt(grid.Children.Count - 1);
        }

        private void EditObjective(object sender, MouseButtonEventArgs e)
        {
            var objectives = (DataContext as TaskPageViewModel).Objectives;
            var objective = (Data.Task)((DataGridRow)e.Source).DataContext;
            var element = new ObjectiveEditorControl(objective, objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }
    }
}
