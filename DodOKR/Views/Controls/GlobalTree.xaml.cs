using DodOKR.ViewModels;
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
        private TaskPageViewModel tvm;

        public GlobalTree(TaskPageViewModel tvm)
        {
            InitializeComponent();
            this.tvm = tvm;
            DataContext = new GlobalTreeViewModel(tree, scroll);
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

        private void ShowTaskMenu(object sender, RoutedEventArgs e)
        {
            var element = new TaskMenuControl(tvm);
            grid.Children.Add(element);
            element.IsVisibleChanged += CloseTaskMenu;
        }

        private void CloseTaskMenu(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
