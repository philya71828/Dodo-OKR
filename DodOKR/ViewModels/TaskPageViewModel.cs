using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DodOKR.ViewModels
{
    public class TaskPageViewModel : ViewModel
    {
        private Data.User currentUser;
        private Data.Team currentTeam;
        private Data.Company currentCompany;
        private Data.PageType type;
        private int progress;
        private ObservableCollection<Data.ObjectiveMask> objectives;

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

        public TaskPageViewModel()
        {
            currentUser = new Data.User();
            currentTeam = new Data.Team();
            currentCompany = new Data.Company();
            Create();
            ChangeProgress();
        }

        private void ChangeProgress()
        {
            var sum = 0;
            foreach (var obj in Objectives)
                sum += obj.Objective.Progress;
            Progress = sum / Objectives.Count;
        }

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
