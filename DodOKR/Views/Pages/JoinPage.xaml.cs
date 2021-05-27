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

namespace DodOKR
{
    /// <summary>
    /// Interaction logic for JoinPage.xaml
    /// </summary>
    public partial class JoinPage : Page
    {
        public JoinPage()
        {
            InitializeComponent();
        }
        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            panel.Visibility = Visibility.Visible;
        }

        private void Join(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TaskPage());
        }
    }
}
