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
    /// Interaction logic for EditorControl.xaml
    /// </summary>
    public partial class TaskEditorControl : UserControl
    {
        public TaskEditorControl(Task task, ObjectiveMask objective)
        {
            InitializeComponent();
            DataContext = new TaskEditorViewModel(task, objective);
        }
    }
}
