using Frontend.Model;
using Frontend.View;
using Frontend.ViewModel;
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

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            this.viewModel = (MainViewModel)DataContext;
        }

        private void LogInbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserModel user = viewModel.LogIn(EmailTextBox.Text, PasswordTextBox.Password);
                if (user != null)
                {
                    BoardView boardView = new BoardView();
                    boardView.ViewModel.Model = user;
                    boardView.ViewModel.UserEmail_content = user.Email;
                    boardView.ViewModel.Boards_Content = boardView.ViewModel.userBoards();
                    boardView.Show();
                    this.Close();
                }
                else
                {
                    notifications.Content= viewModel.Message;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Registerbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Register(EmailTextBox.Text, PasswordTextBox.Password);
                UserModel user = viewModel.LogIn(EmailTextBox.Text, PasswordTextBox.Password);
                if (user != null)
                {
                    BoardView boardView = new BoardView();
                    boardView.ViewModel.Model = user;
                    boardView.ViewModel.UserEmail_content = user.Email;
                    boardView.Show();
                    this.Close();
                }
                else
                {
                    notifications.Content = viewModel.Message;
                }
            }
            catch(Exception ex)
            {
                notifications.Content = ex.Message;
            }
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
