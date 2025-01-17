﻿using DodOKR.Data;
using DodOKR.ViewModels;
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

namespace DodOKR.Views.Controls
{
    /// <summary>
    /// Interaction logic for ObjectiveEditorControl.xaml
    /// </summary>
    public partial class ObjectiveEditorControl : UserControl
    {
        public ObjectiveEditorControl(Data.Task objective,ObservableCollection<ObjectiveMask>objectives)
        {
            InitializeComponent();
            DataContext = new ObjectiveEditorViewModel(objective, objectives);
        }
    }
}
