using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DodOKR
{
    public class TaskPageViewModel : ViewModel
    {
        private TaskPage taskPage;
        private Grid grid;

        public Task SelectedObjective
        {
            get => selectedObjective;
            set
            {
                selectedObjective = value;
                OnPropertyChanged("SelectedObjective");
            }
        }

        public Task SelectedTask
        {
            get => selectedTask;
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        private List<ObjectiveMask> uMasks;
        private List<ObjectiveMask> objMasks;

        private User currentUser;
        private Team currentTeam;
        private PageType type;
        private int progress;
        private ObservableCollection<ObjectiveMask> objectives;
        private Task selectedObjective;
        private Task selectedTask;

        #region
        public ObservableCollection<ObjectiveMask> Objectives 
        {
            get => objectives;
            set
            {
                objectives = value;
                OnPropertyChanged("Objectives");
            }
        }

        public int Progress
        {
            get=>progress;
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public PageType Type
        {
            get => type;
            set
            {
                type = value;
                var mask = type == PageType.Personal ? uMasks : objMasks;
                Objectives = new ObservableCollection<ObjectiveMask>(mask);
                OnPropertyChanged("Type");
            }
        }
        #endregion

        public TaskPageViewModel(TaskPage taskPage,Grid grid)
        {
            this.taskPage = taskPage;
            this.grid = grid;

            currentUser = DefaultDb.User;
            currentTeam = DefaultDb.User.Team;
            uMasks = FillMask(currentUser.Objectives);
            objMasks = FillMask(currentTeam.Objectives);
            Type = PageType.Personal;
            ChangeProgress();
        }

        private List<ObjectiveMask> FillMask(List<Objective> objectives)
        {
            var mask = new List<ObjectiveMask>();
            foreach (var obj in objectives)
            {
                obj.Status = SetStatus(obj.StartDate, obj.FinishDate, obj.Progress);
                foreach (var task in obj.Tasks)
                    task.Status = SetStatus(task.StartDate, task.FinishDate, task.Progress);
                mask.Add(new ObjectiveMask { Obj = new[] { obj }, Tasks = new ObservableCollection<Task>(obj.Tasks) });
            }
            return mask;
        }

        public ICommand ShowTaskMenu => new RelayCommand(obj =>
        {
            var element = new TaskMenuControl(this);
            grid.Children.Add(element);
            element.IsVisibleChanged += CloseTaskMenu;
        });

        public ICommand OpenTreeCommand => new RelayCommand(obj => OpenTree());

        public ICommand AddNewObjective => new RelayCommand(obj =>
        {
            var element = new ObjectiveAdditionControl(this.Objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        });        

        public ICommand TurnPersonal => new RelayCommand(obj => Turn(PageType.Personal));
        public ICommand TurnTeam => new RelayCommand(obj => Turn(PageType.Team));      

        public void AddNewTask(Task task)
        {
            var objective = this.Objectives[task.Index];
            var element = new TaskAdditionControl(objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }

        public void EditTask(Task task)
        {
            var objectives = this.Objectives;
            var objective = new ObjectiveMask();
            var i = task.Index;
            foreach (var e in objectives)
            {
                if (e.Tasks.Count >= i && e.Tasks.Count > 0)
                    if (e.Tasks[i] == task)
                    {
                        objective = e;
                        break;
                    }

            }
            var element = new TaskEditorControl(task, objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }

        public void EditObjective(Objective obj)
        {
            var objectives = this.Objectives;
            var element = new ObjectiveEditorControl(obj, objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }

        //Инкапсулировать
        private void ChangeProgress()
        {
            var sum = 0;
            foreach (var obj in Objectives)
                sum += obj.Objective.Progress;
            Progress = sum / Objectives.Count;
        }
            
        private void Turn(PageType type)
        {
            if (this.Type != type)
                this.Type = type;
        }

        private void CloseTaskMenu(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = grid.Children[grid.Children.Count - 1] as TaskMenuControl;
            grid.Children.RemoveAt(grid.Children.Count - 1);
            if (element.IsJoinPage) OpenJoinPage();
            if (element.IsTreeOpened) OpenTree();
        }

        private void Destroy(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = this;
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

        private void OpenJoinPage() => taskPage.NavigationService.GoBack();
        private void OpenTree()
        {
            var tree = new GlobalTree(this);
            grid.Children.Add(tree);
            this.Type = PageType.Tree;
            tree.IsVisibleChanged += CloseTree;
        }

        private void CloseTree(object sender, DependencyPropertyChangedEventArgs e)
        {
            grid.Children.RemoveAt(grid.Children.Count - 1);
        }

        //Убрать
        private Status SetStatus(DateTime start, DateTime finish, int progress)
        {
            double a = DateTime.Now.Subtract(start).Days;
            double b = finish.Subtract(start).Days;
            var res = progress - (a / b * 100);
            if (res < -20)
                return Status.Bad;
            if (res < 20)
                return Status.Good;
            return Status.Great;
        }
    }
}
