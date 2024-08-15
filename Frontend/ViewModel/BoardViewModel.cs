using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    public class BoardViewModel : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        private string userEmail;
        private UserModel model;
        private LinkedList<BoardModel> boards;

        public string UserEmail_content
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                RaisePropertyChanged("UserEmail_content");
            }
        }
        public UserModel Model
        {
            get { return model; }
            set
            {
                model = value;
                RaisePropertyChanged("Model");
            }
        }
        public LinkedList<BoardModel> Boards_Content
        {
            get { return boards; }
            set
            {
                boards = value;
                RaisePropertyChanged("Boards_Content");
            }
        }
        public BoardViewModel(UserModel user)
        {
            model=user;
            Controller = MainViewModel.Controller;
        }

        public LinkedList<BoardModel> userBoards()
        {
            return Controller.UserBoards(userEmail);
        }
    }
}
