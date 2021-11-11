﻿using System;
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

namespace DodOKR
{
    public class GlobalTreeViewModel : ViewModel
    {
        private TreeView tree;
        private int progress;
        private Visibility visibility;
        private TaskPageViewModel tvm;
        private TaskMenuControl taskMenu;
        private Grid grid;

        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public GlobalTreeViewModel(TreeView tree, TaskPageViewModel tvm, Grid grid)
        {
            this.tvm = tvm;
            this.grid = grid;
            this.tree = tree;
            this.tree.PreviewKeyDown += delegate (object obj, KeyEventArgs args) { args.Handled = true; };
            this.PopulateTreeView();
        }

        public ICommand ShowTaskMenu => new RelayCommand(obj =>
        {
            taskMenu = new TaskMenuControl(tvm);
            grid.Children.Add(taskMenu);
            taskMenu.IsVisibleChanged += CloseTaskMenu;
        });

        //Сделать общий переключатель
        public ICommand TurnPersonal => new RelayCommand(obj => Turn(PageType.Personal));
        public ICommand TurnTeam => new RelayCommand(obj => Turn(PageType.Team));

        private void Turn(PageType type)
        {
            Visibility = Visibility.Hidden;
            if (tvm.Type != type)
            {
                tvm.Type = type;                
            }                
        }
        //

        private void CloseTaskMenu(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!(taskMenu.DataContext as TaskMenuVM).Turned)
                Visibility = Visibility.Hidden;
        }

        private void PopulateTreeView()
        {
            var companyTarget = new Node("Цель Компании");
            List<Project> projects;
            using(ApplicationContext db=new ApplicationContext(Connector.Options))
            {
                var company = db.Companies.Where(c => c.Id == ApplicationContext.User.Team.CompanyId).FirstOrDefault();
                projects = db.Projects.Where(p => p.CompanyId == company.Id).ToList();
                foreach(var project in projects)
                {
                    var head = db.Users.Where(u => u.Id == project.HeadId).FirstOrDefault();
                    var teams = db.Teams.Where(t => t.ProjectId == project.Id).ToList();
                    foreach(var team in teams)
                    {
                        var objective=db.Objectives.Where(obj => obj.TeamId == team.Id).ToList();
                    }
                }
            }


            //var rand = new Random();
            //var projectCount = rand.Next(5)+1;
            //var projects = new List<Project>();
            //for(var k = 0; k < projectCount; k++)
            //{
            //    var project = new Project() { Name = "Project "+(k+1), Head = new User() { FirstName = "Name", SurName = "SurName" } };
            //    var teamCount = rand.Next(5)+1;
            //    var teams = new List<Team>();
            //    for (var j = 0; j < teamCount; j++)
            //    {
            //        var team = new Team() { Name = "Team " + (k + 1)+"."+(j+1) };
            //        var size = rand.Next(5)+1;
            //        var objs = new ObservableCollection<ObjectiveMask>();
            //        for (var i = 0; i < size; i++)
            //        {
            //            var obj = new Objective() { Progress = rand.Next(100) };
            //            objs.Add(new ObjectiveMask() { Obj = new[] { obj } });
            //        }
            //        team.Objectives = objs.Select(o=>o.Obj[0]).ToList();
            //        teams.Add(team);
            //    }
            //    project.Teams = teams;
            //    projects.Add(project);
            //}

            foreach (var proj in projects)
            {
                companyTarget.ChildNodes.Add(new Node(proj));
                var r = companyTarget.ChildNodes[companyTarget.ChildNodes.Count - 1];
                foreach (var t in proj.Teams)
                {
                    r.ChildNodes.Add(new Node(t));
                }
            }

            Node dummy = new Node();
            dummy.ChildNodes.Add(companyTarget);
            Progress = 0;
            foreach (var e in projects)
                Progress += e.Progress;
            Progress /= projects.Count;

            this.tree.ItemsSource = dummy.ChildNodes;
        }
    }
}
