using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Caliburn.Micro;

namespace DodOKR
{
    /// <summary>
    /// Interaction logic for ObjectiveAdditionControl.xaml
    /// </summary>
    public partial class ObjectiveAdditionControl : UserControl
    {
        public ObjectiveAdditionControl(ObservableCollection<ObjectiveMask> objectives, DbEntity entity)
        {
            InitializeComponent();
            DataContext = new ObjectiveAdditionVM(objectives, entity);
        }
    }
}
