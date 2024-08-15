using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.business_layer
{
    public class Task
    {
        private int Id;
        private DateTime creationTime = DateTime.Now;
        private DateTime dueDate;
        private string title;
        private string description;
        private string person;
        private TaskDTO taskDTO;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// c# getters and setters
        /// </summary>
        public int ID { get => Id; set { Id = value; } }
        public DateTime CreationTime { get => creationTime; set { creationTime = value; } }
        public DateTime DueDate { get => dueDate; set { dueDate = value; } }
        public string Title { get => title; set { title = value; } }
        public string Description { get => description; set { description = value; } }
        public TaskDTO TaskDTO { get => taskDTO; set { taskDTO = value; } }
        public String Person { get => person; set { person = value; } }

        /// <summary>
        /// init a new task
        /// </summary>
        /// <param name="ID">the task id</param>
        /// <param name="dueDate">due date of the task</param>
        /// <param name="title">title of the task</param>
        /// <param name="Description">description of the task</param>
        public Task(int ID,DateTime dueDate, string title, string Description)
        {
            this.Id = ID;
            this.dueDate = dueDate;
            this.title = title;
            this.description = Description;
            this.person = null;
            taskDTO = new TaskDTO(ID, creationTime, dueDate, title, Description, "not assigned",0);
        }

        /// <summary>
        /// assigning user to the task
        /// </summary>
        /// <param name="person">the usignee</param>
        /// <returns>user assigned successfully</returns>
        public string AssignTask(string person)
        {
            this.person=person;
            return "user assigned successfully";
        }

    

        /// <summary> 
        /// this method updates the description of the task.
        /// </summary>
        /// <param name="description">the new description in the task</param>
        /// <returns> if the description updated succefully</returns>
        /// <exception cref="Exception"> if the description contains more than 300 character </exception>
        public string UpdateTaskDescription(string description)
        {

            if (description.Length > 300)
            {
                log.Debug("description should not contain more than 300 character");
                throw new Exception("description should not contain more than 300 character");
            }
            this.description = description;
            return "description updated successfully";
        }

        /// <summary> 
        /// this method updates the due date of the task.
        /// </summary>
        /// <param name="dueDate">the new duedate in the task</param>
        /// <returns> if the due date updated succefully</returns>
        /// <exception cref="Exception"> if the due date is earlier than today </exception>
        public string UpdateTaskDueDate(DateTime dueDate)
        {
            if (DateTime.Compare(DateTime.Now, dueDate) > 0)
            {
                log.Debug("due date is earlier than today");
                throw new Exception("due date is earlier than today");
            }
            this.dueDate = dueDate;
            return "due date updated successfully";
        }

        /// <summary> 
        /// this method updates the title of the task.
        /// </summary>
        /// <param name="title">the new title in the task</param>
        /// <returns> if the title updated succefully</returns>
        /// <exception cref="Exception"> if the title have more than 50 character or the title is null </exception>
        public string UpdateTaskTitle(string title)
        {
            if (title.Length > 50)
            {
                log.Debug("title should not be more than 50 character");

                throw new Exception("title should not be more than 50 character");
            }
            if (title == null)
            {
                log.Debug("title should not be null");
                throw new Exception("title should not be null");
            }
            this.title = title;
            return "title updated successfully";
        }

    }
}
