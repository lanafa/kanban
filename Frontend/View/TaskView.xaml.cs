using Frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Frontend.View
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : Window
    {
        private TaskViewModel viewModel;
        public TaskViewModel ViewModel
        {
            get => viewModel;
            set
            {
                viewModel = value;
            }
        }

        public TaskView()
        {
            this.DataContext = new TaskViewModel();
            this.viewModel = (TaskViewModel)DataContext;
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }


}
