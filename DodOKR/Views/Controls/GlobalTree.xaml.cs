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
            var root = new Node("1");
            root.ChildNodes.Add(new Node("1.1"));
            root.ChildNodes.Add(new Node("1.2"));
            root.ChildNodes.Add(new Node("1.3"));
            root.ChildNodes[0].ChildNodes.Add(new Node("1.1.1"));
            root.ChildNodes[1].ChildNodes.Add(new Node("1.2.1"));
            root.ChildNodes[1].ChildNodes.Add(new Node("1.2.2"));
            root.ChildNodes[1].ChildNodes.Add(new Node("1.2.3"));
            root.ChildNodes[2].ChildNodes.Add(new Node("1.3.1"));
            root.ChildNodes[2].ChildNodes.Add(new Node("1.3.2"));
            for (var i = 0; i < 20; i++)
                root.ChildNodes.Add(new Node("1."+(i+3)));

            var node = root.ChildNodes[0].ChildNodes;
            for (var i = 0; i < 20; i++)
            {
                node.Add(new Node("test"));
                node = node[0].ChildNodes;
            }

            node = root.ChildNodes[15].ChildNodes;
            for (var i = 0; i < 20; i++)
            {
                node.Add(new Node("test"));
                node = node[0].ChildNodes;
            }

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

        private void PressButton(object sender, MouseEventArgs e)
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
