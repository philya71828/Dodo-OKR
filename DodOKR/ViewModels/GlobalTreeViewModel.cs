using DodOKR.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DodOKR.ViewModels
{
    public class GlobalTreeViewModel : INotifyPropertyChanged
    {
        private TreeView tree;
        private ScrollViewer scroll;
        private int progress;

        public int Progress
        {
            get => progress;
            set
            {
                progress = value;
                OnPropertyChanged("Progress");
            }
        }

        public GlobalTreeViewModel(TreeView tree,ScrollViewer scroll)
        {
            this.tree = tree;
            this.scroll = scroll;
            this.tree.PreviewKeyDown += delegate (object obj, KeyEventArgs args) { args.Handled = true; };
            this.PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            var root = new Node("Цель Команды");


            var rand = new Random();
            var projectCount = rand.Next(5)+1;
            var projects = new List<Project>();
            for(var k = 0; k < projectCount; k++)
            {
                var project = new Project() { Name = "Project "+(k+1), Head = new User() { FirstName = "Name", SurName = "SurName" } };
                var teamCount = rand.Next(5)+1;
                var teams = new List<Team>();
                for (var j = 0; j < teamCount; j++)
                {
                    var team = new Team() { Name = "Team " + (k + 1)+"."+(j+1) };
                    var size = rand.Next(5)+1;
                    var objs = new ObservableCollection<ObjectiveMask>();
                    for (var i = 0; i < size; i++)
                    {
                        var obj = new Data.Task() { Progress = rand.Next(100) };
                        objs.Add(new ObjectiveMask() { Obj = new[] { obj } });
                    }
                    team.Objectives = objs;
                    teams.Add(team);
                }
                project.Teams = teams;
                projects.Add(project);
            }
            
            foreach(var proj in projects)
            {
                root.ChildNodes.Add(new Node(proj));
                var r = root.ChildNodes[root.ChildNodes.Count - 1];
                foreach(var t in proj.Teams)
                {
                    r.ChildNodes.Add(new Node(t));
                }
            }

            Node dummy = new Node();
            dummy.ChildNodes.Add(root);
            Progress = 0;
            foreach (var e in projects)
                Progress += e.Progress;
            Progress /= projects.Count;

            this.tree.ItemsSource = dummy.ChildNodes;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
