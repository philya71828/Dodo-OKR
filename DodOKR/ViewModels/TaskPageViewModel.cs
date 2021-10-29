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

        private User currentUser;
        private Team currentTeam;
        private Company currentCompany;
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
                switch (type)
                {
                    case PageType.Personal: Objectives = currentUser.Objectives; break;
                    case PageType.Team: Objectives = currentTeam.Objectives; break;
                    default:break;
                    //Словарь!!!!
                }
                OnPropertyChanged("Type");
            }
        }
        #endregion

        public TaskPageViewModel(TaskPage taskPage,Grid grid)
        {
            this.taskPage = taskPage;
            this.grid = grid;

            currentUser = new User();
            currentTeam = new Team();
            currentCompany = new Company();
            Create();
            ChangeProgress();
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
        public ICommand TurnCompany => new RelayCommand(obj => Turn(PageType.Company));        

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

        private void Create()
        {
            currentUser.Objectives = new ObservableCollection<ObjectiveMask>();
            currentUser.Objectives.Add(new ObjectiveMask()
            {
                Obj = new[]
                {
                    new Objective()
                    {
                        Name = "Objective 1",
                        Comment = "Description 1",
                        Status=Status.Good,
                        Progress=80,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index = currentUser.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Task>
                {
                    new Task()
                    {
                        Name = "Task 1.1",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index=0
                    },
                    new Task()
                    {
                        Name = "Task 1.2",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index=1
                    }
                },

            });
            currentUser.Objectives.Add(new ObjectiveMask()
            {
                Obj = new[]
                {
                    new Objective()
                    {
                        Name = "Objective 2",
                        Comment = "Description 2",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index = currentUser.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Task>
                {
                    new Task()
                    {
                        Name = "Task 2.1",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index=0
                    }
                }
                
            });
            currentTeam.Objectives = new ObservableCollection<ObjectiveMask>();
            currentTeam.Objectives.Add(new ObjectiveMask()
            {
                Obj = new[]
                {
                    new Objective()
                    {
                        Name = "Team Objective 1",
                        Comment = "Description 1",
                        Status=Status.Good,
                        Progress=80,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index = currentTeam.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Task>
                {
                    new Task()
                    {
                        Name = "Task 1.1",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index=0
                    },
                    new Task()
                    {
                        Name = "Task 1.2",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index = 1
                    }
                }                
            });
            currentTeam.Objectives.Add(new ObjectiveMask()
            {
                Obj = new[]
                {
                    new Objective()
                    {
                        Name = "Team Objective 2",
                        Comment = "Description 2",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index=currentTeam.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Task>
                {
                    new Task()
                    {
                        Name = "Task 2.1",
                        Status = Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Index=0
                    }
                }                
            });
            Type = PageType.Personal;
        }
    }
}
