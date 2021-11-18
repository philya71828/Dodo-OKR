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
            this.CreateTreeView();
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

        private void CreateTreeView()
        {
            var companyTarget = new Node("Цель Компании");
            List<Project> projects;
            using (ApplicationContext db=new ApplicationContext(Connector.Options))
                projects = db.LoadTreeData();
            PopulateTree(companyTarget, projects);

            Node dummy = FillProgress(companyTarget, projects);

            this.tree.ItemsSource = dummy.ChildNodes;
        }

        private static void PopulateTree(Node companyTarget, List<Project> projects)
        {
            foreach (var project in projects)
            {
                companyTarget.ChildNodes.Add(new Node(project));
                var root = companyTarget.ChildNodes[companyTarget.ChildNodes.Count - 1];
                foreach (var task in project.Teams)
                {
                    root.ChildNodes.Add(new Node(task));
                }
            }
        }

        private Node FillProgress(Node companyTarget, List<Project> projects)
        {
            Node dummy = new Node();
            dummy.ChildNodes.Add(companyTarget);
            Progress = 0;
            foreach (var e in projects)
                Progress += e.Progress;
            Progress /= projects.Count;
            return dummy;
        }        
    }
}
