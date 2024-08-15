using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    public class TaskViewModel:NotifiableObject 
    {
        public BackendController Controller { get; private set; }
        private LinkedList<TaskModel> backlog;
        private LinkedList<TaskModel> inProgress;
        private LinkedList<TaskModel> done;
        private LinkedList<TaskModel> allTasks;
        private string userEmail;
        private string boardName;

        public string BardName_content
        {
            get { return boardName; }
            set
            {
                boardName = value;
                RaisePropertyChanged("BardName_content");
            }
        }
        public string UserEmail_content
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                RaisePropertyChanged("UserEmail_content");
            }
        }
        public LinkedList<TaskModel> Tasks_Content
        {
            get { return allTasks; }
            set
            {
                allTasks=value;
                RaisePropertyChanged("Tasks_Content");
            }
        }

        public TaskViewModel()
        {
            Controller = MainViewModel.Controller;
        }
    }
}
