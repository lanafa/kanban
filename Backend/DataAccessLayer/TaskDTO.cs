using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDTO
    {
        private int taskID;
        private DateTime creationTime;
        private DateTime dueDate;
        private string title;
        private string description;
        private string email;
        private int columnID;

        /// <summary>
        /// c# getters and setters
        /// </summary>
        public int TaskID { get => taskID; set { taskID = value; } }
        public DateTime CreationTime { get => creationTime; set { creationTime = value; } }
        public DateTime DueDate { get => dueDate; set { dueDate = value; } }
        public string Title { get => title; set { title = value; } }
        public string Description { get => description; set { description = value; } }
        public string Email { get => email; set { email = value; } }
        public int ColumnID { get => columnID; set { columnID = value; } }

        /// <summary>
        /// constructor to the task DTO
        /// </summary>
        /// <param name="taskID">task id</param>
        /// <param name="creationTime"> creation time of the task</param>
        /// <param name="dueDate">task due date</param>
        /// <param name="title">task title</param>
        /// <param name="description">task description</param>
        /// <param name="email">the assignee</param>
        /// <param name="columnID">task column</param>
        public TaskDTO(int taskID, DateTime creationTime, DateTime dueDate, string title, string description, string email, int columnID)
        {
            this.taskID = taskID;
            this.creationTime = creationTime;
            this.dueDate = dueDate;
            this.title = title;
            this.description = description;
            this.email = email;
            this.columnID = columnID;
        }

    }
}
