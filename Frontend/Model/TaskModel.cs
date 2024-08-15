using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class TaskModel:NotifiableObject
    {
        private int taskID;
        private string taskTitle;
        private string taskDescription;
        private DateTime creationDate;
        private DateTime dueDate;
        private int columnID;
        public int ColumnID
        {
            get => columnID;
            set
            {
                columnID = value;
                RaisePropertyChanged("ColumnID");
            }
        }
        public int TaskID
        {
            get => taskID;
            set
            {
                taskID = value;
                RaisePropertyChanged("TaskID");
            }
        }
        public string TaskTitle
        {
            get => taskTitle;
            set
            {
                taskTitle = value;
                RaisePropertyChanged("TaskTitle");
            }
        }
        public string TaskDescription
        {
            get => taskDescription;
            set
            {
                taskDescription = value;
                RaisePropertyChanged("TaskDescription");
            }
        }
        public DateTime CreationDate
        {
            get => creationDate;
            set
            {
                creationDate = value;
                RaisePropertyChanged("CeationDate");
            }
        }
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                dueDate = value;
                RaisePropertyChanged("DueDate");
            }
        }
        public TaskModel(BackendController controller, int taskID, string taskTitle, string taskDescription, DateTime creationDate, DateTime dueDate, int columnID)
        {
            this.taskID = taskID;
            this.TaskTitle = taskTitle;
            this.taskDescription = taskDescription;
            this.creationDate = creationDate;
            this.dueDate = dueDate;
            this.columnID = columnID;
        }
    }
}
