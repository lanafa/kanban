using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using IntroSE.Kanban.Backend.business_layer;
using log4net;

namespace IntroSE.Kanban.Backend.business_layer
{
    internal class Column
    {
        private string name;
        private LinkedList<Task> tasks;
        private int limit;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        


        public string Name { get => name; set => name = value; }
      public LinkedList<Task> Tasks { get => tasks; set => tasks = value; }
        public int Limit { get => limit; set => limit = value; }
        /// <summary>
        /// init a new column
        /// </summary>
        /// <param name="name">the column name</param>
        public Column(string name)
        {
            this.name = name;
            tasks = new LinkedList<Task>();
            int defultlimit = -1;
            limit =defultlimit;
        }

        /// <summary>
        /// this method assign user to task
        /// </summary>
        /// <param name="taskID">task id</param>
        /// <param name="email">the assignee</param>
        /// <returns>if the assigning completed</returns>
        /// <exception cref="Exception">the assigning failed</exception>
        public string AssignTask(int taskID,string emailAssignee)
        {
            if(emailAssignee == null || emailAssignee == "")
            {
                log.Debug("email is null");
                throw new Exception("email is null");
            }
            if (taskID < 0)
            {
                log.Debug("invaled taskID");
                throw new Exception("invaled taskID");
            }
            foreach(Task task in tasks)
            {
                if (task.ID == taskID)
                {
                    return task.AssignTask(emailAssignee);
                }
            }
            log.Debug("task is not exist");
            throw new Exception("task is not exist");
        }

        /// <summary>
        /// this method adds a task to the column
        /// </summary>
        /// <param name="ID">task id</param>
        /// <param name="title">the title of the new task</param>
        /// <param name="description">the description of the new task</param>
        /// <param name="dueDate">the duedate of the new task</param>
        /// <returns>returns string about if adding the task ended successfully</returns>
        /// <exception cref="Exception">if adding the task failed</exception>
        public string AddTask(int ID,string title, string description, DateTime dueDate,string person)
        {
            int maxdescrptionlength = 300;
            int maxtitlelength = 50;
            int nolimit = -1;
            if (title == null)
            {
                log.Debug("title is null");
                throw new Exception("title is null");
            }
            if (description != null)
            {
                if (description.Length > maxdescrptionlength)
                {
                    log.Debug("description should not contain more than 300 character");
                    throw new Exception("description should not contain more than 300 character");
                }
            }
            if (title.Length > maxtitlelength) {
                log.Debug("title should not contain more than 50 character");
                throw new Exception ("error title should not contain more than 50 character"); }
            if (title == null) { 
                log.Debug("title should not be null");
                throw new Exception("error title should not be null"); }
            if (DateTime.Compare(DateTime.Now, dueDate) > 0)
            {
                log.Debug("due date is earlier than today");
                throw new Exception( "due date is earlier than today");
            }
            if (limit == nolimit || tasks.Count <= limit - 1)
            {
                Task t = new Task(ID, dueDate, title, description);
                t.Person= person;
                tasks.AddLast(t);
                return "Task added successfully";
            }
            log.Debug("the column is full of tasks");

            throw new Exception( "he column is full of tasks");
        }

        /// <summary>
        /// this method removes a task from the column
        /// </summary>
        /// <param name="id">The task to be removed identified task ID</param>
        /// <returns>return true if removed the task successfuly else returns false</returns>
        public bool removeTask(int id)
        {
            foreach (Task T in tasks)
            {
                if (T.ID == id)
                {
                    tasks.Remove(T);
                    return true;
                }

            }
            return false;
        }

        /// <summary> 
        /// this method updates the description of the task.
        /// </summary>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">the new description in the task</param>
        /// <returns> if the description updated succefully</returns>
        /// <exception cref="Exception"> if updating the description failed</exception>
        public string UpdateTaskDescription(int taskId, string description)
        {
            foreach (Task T in this.tasks)
            {
                if (T.ID == taskId)
                {
                    return T.UpdateTaskDescription(description);
                }
            }
            log.Debug("task is not exist");
            throw new Exception("task is not exist");
        }

        /// <summary> 
        /// this method updates the dueDate of the task.
        /// </summary>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">the new duedate in the task</param>
        /// <returns> if the dueDate updated succefully</returns>
        /// <exception cref="Exception"> if updating the dueDate failed</exception>
        public string UpdateTaskDueDate(int taskId, DateTime dueDate)
        {
            foreach (Task T in this.tasks)
            {
                if (T.ID == taskId)
                {
                    return T.UpdateTaskDueDate(dueDate);
                }

            }
            log.Debug("task is not exist");
            throw new Exception("task is not exist");
        }

        /// <summary> 
        /// this method updates the title of the task.
        /// </summary>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">The new title in the task</param>
        /// <returns> if the title updated succefully</returns>
        /// <exception cref="Exception"> if updating the title failed</exception>
        public string UpdateTaskTitle(int taskId, string title)
        {
            foreach (Task T in this.tasks)
            {
                if (T.ID == taskId)
                {
                    return T.UpdateTaskTitle(title);
                }
            }
            log.Debug("task is not exist");
            throw new Exception("task is not exist");
        }

        /// <summary>
        /// this method limits the number of tasks in the column
        /// </summary>
        /// <param name="limit">the max limit of the tasks that can be in the column</param>
        public void limitColumns(int limit)
        {
            int nolimit = -1;
            if (limit < nolimit)
            {
                log.Debug("limit is illegal");
                throw new Exception("limit is illegal");
            }
            if (limit < tasks.Count && limit != nolimit)
            {
                log.Debug("there is already tasks more than the new limit");
                throw new Exception("there is already tasks more than the new limit");
            }
            this.limit = limit;
        }

       
        /// <summary>
        /// this method returns a task in the column
        /// </summary>
        /// <param name="taskID">The task to be returned identified task ID</param>
        /// <returns>returns a task in the column</returns>
        public Task GetTask(int taskID)
        { 
            foreach (Task T in this.tasks)
            {
                if (T.ID == taskID)
                {
                    return T;
                }
            }
            return null;
        }
    }
}
