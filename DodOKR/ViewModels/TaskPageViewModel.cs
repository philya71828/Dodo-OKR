using DodOKR.Views.Controls;
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

namespace DodOKR.ViewModels
{
    public class TaskPageViewModel : ViewModel
    {
        private TaskPage taskPage;
        private Grid grid;
        private GlobalTree tree;
        
        public Data.Task Objective { get; }
        public Data.Task Task { get; }

        private Data.User currentUser;
        private Data.Team currentTeam;
        private Data.Company currentCompany;
        private Data.PageType type;
        private int progress;
        private ObservableCollection<Data.ObjectiveMask> objectives;

        #region
        public ObservableCollection<Data.ObjectiveMask> Objectives 
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

        public Data.PageType Type
        {
            get => type;
            set
            {
                type = value;
                switch (type)
                {
                    case Data.PageType.Personal: Objectives = currentUser.Objectives; break;
                    case Data.PageType.Team: Objectives = currentTeam.Objectives; break;
                    case Data.PageType.Company: Objectives = currentCompany.Objectives; break;
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
            tree = new GlobalTree(this);
            tree.Visibility = Visibility.Hidden;
            grid.Children.Add(tree);

            currentUser = new Data.User();
            currentTeam = new Data.Team();
            currentCompany = new Data.Company();
            Create();
            ChangeProgress();
        }

        public ICommand ShowTaskMenu => new RelayCommand(() =>
        {
            var element = new TaskMenuControl(this);
            grid.Children.Add(element);
            element.IsVisibleChanged += CloseTaskMenu;
        });

        public ICommand OpenTreeCommand => new RelayCommand(() => OpenTree());

        public ICommand AddNewObjective => new RelayCommand(() =>
        {
            var element = new ObjectiveAdditionControl(this.Objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        });

        public ICommand AddNewTask => new RelayCommand(()=> 
        {
            var ind = Objective;
            var objective = this.Objectives[ind.Index];
            var element = new TaskAdditionControl(objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        });

        public ICommand EditObjective => new RelayCommand(() =>
        {
            var objectives = this.Objectives;
            var objective = new Data.ObjectiveMask();
            var task = Objective;
            var i = task.Index;
            foreach (var obj in objectives)
            {
                if (obj.Tasks.Count >= i && obj.Tasks.Count > 0)
                    if (obj.Tasks[i] == task)
                    {
                        objective = obj;
                        break;
                    }

            }

            var element = new TaskEditorControl(task, objective);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        });

        public ICommand EditTask => new RelayCommand(() =>
        {
            var objectives = this.Objectives;
            var objective = Task;
            var element = new ObjectiveEditorControl(objective, objectives);
            grid.Children.Add(element);
            element.IsVisibleChanged += Destroy;
        });

        public ICommand TurnPersonal => new RelayCommand(() => Turn(Data.PageType.Personal));
        public ICommand TurnTeam => new RelayCommand(() => Turn(Data.PageType.Team));
        public ICommand TurnCompany => new RelayCommand(() => Turn(Data.PageType.Company));

        private void ChangeProgress()
        {
            var sum = 0;
            foreach (var obj in Objectives)
                sum += obj.Objective.Progress;
            Progress = sum / Objectives.Count;
        }

        private void Turn(Data.PageType type)
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
        private void OpenTree() => tree.Visibility = Visibility.Visible;

        private void Create()
        {
            currentUser.Objectives = new ObservableCollection<Data.ObjectiveMask>();
            currentUser.Objectives.Add(new Data.ObjectiveMask()
            {
                Obj = new[]
                {
                    new Data.Task()
                    {
                        Name = "Objective 1",
                        Comment = "Description 1",
                        Status=Data.Status.Good,
                        Progress=80,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.Low,
                        Index = currentUser.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Data.Task>
                {
                    new Data.Task()
                    {
                        Name = "Task 1.1",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=0
                    },
                    new Data.Task()
                    {
                        Name = "Task 1.2",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=1
                    }
                },

            });
            currentUser.Objectives.Add(new Data.ObjectiveMask()
            {
                Obj = new[]
                {
                    new Data.Task()
                    {
                        Name = "Objective 2",
                        Comment = "Description 2",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index = currentUser.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Data.Task>
                {
                    new Data.Task()
                    {
                        Name = "Task 2.1",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=0
                    }
                }
                
            });
            currentTeam.Objectives = new ObservableCollection<Data.ObjectiveMask>();
            currentTeam.Objectives.Add(new Data.ObjectiveMask()
            {
                Obj = new[]
                {
                    new Data.Task()
                    {
                        Name = "Team Objective 1",
                        Comment = "Description 1",
                        Status=Data.Status.Good,
                        Progress=80,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.Low,
                        Index = currentTeam.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Data.Task>
                {
                    new Data.Task()
                    {
                        Name = "Task 1.1",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=0
                    },
                    new Data.Task()
                    {
                        Name = "Task 1.2",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index = 1
                    }
                }                
            });
            currentTeam.Objectives.Add(new Data.ObjectiveMask()
            {
                Obj = new[]
                {
                    new Data.Task()
                    {
                        Name = "Team Objective 2",
                        Comment = "Description 2",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=currentTeam.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Data.Task>
                {
                    new Data.Task()
                    {
                        Name = "Task 2.1",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=0
                    }
                }                
            });
            currentCompany.Objectives = new ObservableCollection<Data.ObjectiveMask>();
            currentCompany.Objectives.Add(new Data.ObjectiveMask()
            {
                Obj = new[]
                {
                    new Data.Task()
                    {
                        Name = "Company Objective 1",
                        Comment = "Description 1",
                        Status=Data.Status.Good,
                        Progress=80,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.Low,
                        Index = currentCompany.Objectives.Count
                    }
                },
                Tasks = new ObservableCollection<Data.Task>
                {
                    new Data.Task()
                    {
                        Name = "Task 1.1",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=0
                    },
                    new Data.Task()
                    {
                        Name = "Task 1.2",
                        Status = Data.Status.Bad,
                        Progress = 10,
                        StartDate = new DateTime(2021, 2, 1),
                        FinishDate = new DateTime(2021, 5, 10),
                        Priority = Data.Priority.High,
                        Index=1
                    }
                }                
            });
            Type = Data.PageType.Personal;
        }
    }
}
