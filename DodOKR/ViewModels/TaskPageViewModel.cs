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

        private List<ObjectiveMask> uMasks;
        private List<ObjectiveMask> tMasks;
        private DbEntity currentTable;

        private User currentUser;
        private Team currentTeam;
        private PageType type;
        private int progress;
        private ObservableCollection<ObjectiveMask> objectives;

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
                var mask = type == PageType.Personal ? uMasks : tMasks;
                currentTable = type == PageType.Personal ? currentUser : currentTeam;
                Objectives = new ObservableCollection<ObjectiveMask>(mask);
                OnPropertyChanged("Type");
            }
        }
        #endregion

        public TaskPageViewModel(TaskPage taskPage,Grid grid)
        {
            this.taskPage = taskPage;
            this.grid = grid;

            using (ApplicationContext db = new ApplicationContext(Connector.Options))
            {
                currentUser = db.GetUserInfo(4);
                currentTeam = currentUser.Team;
            }
            uMasks = FillMask(currentUser.Objectives);
            tMasks = FillMask(currentTeam.Objectives);
            Type = PageType.Personal;
            Progress = DataChanger.ChangeProgress(Objectives.Select(o => o.Obj[0]).ToList());
        }

        private List<ObjectiveMask> FillMask(List<Objective> objectives)
        {
            var mask = new List<ObjectiveMask>();
            foreach (var obj in objectives)
            {
                obj.Status = DataChanger.SetStatus(obj.StartDate, obj.FinishDate, obj.Progress);
                foreach (var task in obj.Tasks)
                    task.Status = DataChanger.SetStatus(task.StartDate, task.FinishDate, task.Progress);
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
        public ICommand TurnPersonal => new RelayCommand(obj => Turn(PageType.Personal));
        public ICommand TurnTeam => new RelayCommand(obj => Turn(PageType.Team));

        public ICommand AddNewObjective => new RelayCommand(obj =>
        {
            var element = new ObjectiveAdditionControl(this.Objectives, currentTable);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        });

        public void AddNewTask(Objective obj)
        {
            var objective = this.Objectives[obj.Index];
            var element = new TaskAdditionControl(objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        }

        public void EditTask(Task task)
        {
            var objectives = Objectives;
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
            var mask = currentTable is User ? uMasks : tMasks;
            var element = new ObjectiveEditorControl(obj, Objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
            element.IsVisibleChanged += UpdatePageMask;
        }

        private void UpdatePageMask(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (currentTable is User)
                uMasks = new List<ObjectiveMask>(Objectives);
            else
                tMasks = new List<ObjectiveMask>(Objectives);
        }

        private void Turn(PageType type)
        {
            if (Type != type)
                Type = type;
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
    }
}
