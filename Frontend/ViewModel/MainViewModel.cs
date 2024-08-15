using Frontend.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    internal class MainViewModel : NotifiableObject
    {
        public static BackendController Controller { get; private set; }

        private string _username;
        private string _password;
        private string _message;
        public string Username
        {
            get => _username;
            set
            {
                this._username = value;
                RaisePropertyChanged("Username");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                this._password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                RaisePropertyChanged("Message");
            }
        }

        public MainViewModel()
        {
            Controller = new BackendController();
        }

        public UserModel LogIn(string email, string password)
        {
            this.Username = email;
            this.Password = password;
            Message = "";
            try
            {
                return Controller.Login(Username, Password);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }
        public void Register(string email, string password)
        {
            this.Username = email;
            this.Password = password;
            Message = "";
            try
            {
                Controller.Register(Username, Password);
                Message = "Registered successfully";
            }
            catch (Exception e)
            {
                Message = e.Message;
                throw new Exception(Message);
            }
        }

    }
}
