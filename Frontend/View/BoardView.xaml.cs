using Frontend.Model;
using Frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : Window 
    {

        private BoardViewModel viewModel;
        public BoardViewModel ViewModel 
        { 
            get => viewModel;
            set
            {
                viewModel = value;
            }
        }

       
        public BoardView()
        {
            this.DataContext = new BoardViewModel(null);
            this.viewModel = (BoardViewModel)DataContext;
  
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView dataGrid=(ListView)sender;
            BoardModel row_selected = dataGrid.SelectedItem as BoardModel;
            //string row_selected = dataGrid.SelectedItem.ToString(); 
            //if(row_selected == )
            if (row_selected != null)
            {
                TaskView taskView = new TaskView();
                taskView.ViewModel.UserEmail_content = this.viewModel.UserEmail_content;
                taskView.ViewModel.Tasks_Content = this.viewModel.Controller.setAllTasks(taskView.ViewModel.UserEmail_content, row_selected.ID);
                taskView.ViewModel.BardName_content = row_selected.Name;
                //string l = taskView.ViewModel.Tasks_Content.ElementAt(0).TaskTitle;
                taskView.Show();
            }
        }
    }
}
