using System;
using System.Collections.Generic;
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

namespace DodOKR.Views.Controls
{
    /// <summary>
    /// Interaction logic for GlobalTree.xaml
    /// </summary>
    public partial class GlobalTree : UserControl
    {
        private Point scrollMousePoint = new Point();
        private double hOff = 1;
        private double vOff = 1;

        public GlobalTree()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Prevent the TreeView from responding to the keyboard.
            // Since there's no sane way to set a TreeView's SelectedItem,
            // we can't customize the keyboard navigation logic. :(
            this.tree.PreviewKeyDown += delegate (object obj, KeyEventArgs args) { args.Handled = true; };

            // Put some data into the TreeView.
            this.PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            var root = new Node("Цель Команды");
            root.ChildNodes.Add(new Node("Проект 1"));
            root.ChildNodes.Add(new Node("Проект 2"));
            root.ChildNodes.Add(new Node("Проект 3"));
            root.ChildNodes[0].ChildNodes.Add(new Node("Команда 1"));
            root.ChildNodes[1].ChildNodes.Add(new Node("Команда 2"));
            root.ChildNodes[1].ChildNodes.Add(new Node("Команда 3"));
            root.ChildNodes[1].ChildNodes.Add(new Node("Команда 4"));
            root.ChildNodes[2].ChildNodes.Add(new Node("Команда 5"));
            root.ChildNodes[2].ChildNodes.Add(new Node("Команда 6"));

            Node dummy = new Node();
            dummy.ChildNodes.Add(root);

            this.tree.ItemsSource = dummy.ChildNodes;
        }

        private void MoveScroll(object sender, MouseEventArgs e)
        {
            if (scroll.IsMouseCaptured)
            {
                scroll.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scroll).X));
                scroll.ScrollToVerticalOffset(vOff + (scrollMousePoint.Y - e.GetPosition(scroll).Y));
            }
        }

        private void PressRightButton(object sender, MouseEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scroll);
            hOff = scroll.HorizontalOffset;
            vOff = scroll.VerticalOffset;
            scroll.CaptureMouse();
        }

        private void ButtonUp(object sender, MouseButtonEventArgs e)
        {
            scroll.ReleaseMouseCapture();
        }
    }
}
