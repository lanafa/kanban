using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using log4net;
using System.Reflection;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskDAO
    {

        private SQLiteConnection connection;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// constructor for the task table in  the database
        /// </summary>
        /// <param name="connection">the connection with the data base</param>
        public TaskDAO(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// this method return the max task id
        /// </summary>
        /// <returns>max task id</returns>
        public int taskIDCounter()
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT max(taskID) as maximum FROM task", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            connection.Close();
            return int.Parse(reader["maximum"].ToString());
        }
       
        /// <summary>
        /// this method insert new task to the table
        /// </summary>
        /// <param name="taskDTO">new task DTO</param>
        public void insert(TaskDTO taskDTO)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO task (taskID,creationTime,dueDate,title,description,email,columnID) VALUES (@TaskIDVal,@CreationTimeVal,@DueDateVal,@TitleVal,@DescriptionVal,@EmailVal,@ColumnIDVal)", connection);
            SQLiteParameter taskidparameter=new SQLiteParameter(@"taskIDVal",taskDTO.TaskID);
            SQLiteParameter creationTimeparameter = new SQLiteParameter(@"CreationTimeVal", taskDTO.CreationTime);
            SQLiteParameter dueDateparameter = new SQLiteParameter(@"DueDateVal", taskDTO.DueDate);
            SQLiteParameter titleparameter = new SQLiteParameter(@"TitleVal", taskDTO.Title);
            SQLiteParameter descriptionparameter = new SQLiteParameter(@"DescriptionVal", taskDTO.Description);
            SQLiteParameter emailparameter = new SQLiteParameter(@"EmailVal", taskDTO.Email);
            SQLiteParameter columnIDparameter = new SQLiteParameter(@"ColumnIDVal", taskDTO.ColumnID);
            cmd.Parameters.Add(taskidparameter);
            cmd.Parameters.Add(creationTimeparameter);
            cmd.Parameters.Add(dueDateparameter);
            cmd.Parameters.Add(titleparameter);
            cmd.Parameters.Add(descriptionparameter);
            cmd.Parameters.Add(emailparameter);
            cmd.Parameters.Add(columnIDparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task added to database ");
        }

        /// <summary>
        /// this method advances the task column
        /// </summary>
        /// <param name="taskID">the id of the task that should be advaced</param>
        public void advanceTask(int taskID)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE task SET columnID=columnID+1 WHERE taskID=@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            cmd.Parameters.Add(taskidparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task advanced in the database");
        }

        /// <summary>
        /// this method update the task due date in the table
        /// </summary>
        /// <param name="taskID">the task id that should be updated</param>
        /// <param name="dueDate">new due date</param>
        public void updateTaskDueDate(int taskID,string dueDate)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE task SET dueDate=@dueDateVal WHERE taskID=@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            SQLiteParameter duedateparameter = new SQLiteParameter(@"dueDateVal", dueDate);
            cmd.Parameters.Add(taskidparameter);
            cmd.Parameters.Add(duedateparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task duedate updated in database");
        }

        /// <summary>
        /// this method update the task description in the table
        /// </summary>
        /// <param name="taskID">the task id that should be updated</param>
        /// <param name="description">new description</param>
        public void updateTaskDescription(int taskID, string description)
        {
            connection.Open();
          
            SQLiteCommand cmd = new SQLiteCommand("UPDATE task SET description=@descriptionVal  WHERE taskID=@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            SQLiteParameter descriptionparameter = new SQLiteParameter(@"descriptionVal", description);
            cmd.Parameters.Add(taskidparameter);
            cmd.Parameters.Add(descriptionparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task description updated in database");

        }

        /// <summary>
        /// this method update the task title in the table
        /// </summary>
        /// <param name="taskID">the task id that should be updated</param>
        /// <param name="title">new title</param>
        public void updateTaskTitle(int taskID, string title)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE task SET title=@titleVal  WHERE taskID=@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            SQLiteParameter titleparameter = new SQLiteParameter(@"titleVal", title);
            cmd.Parameters.Add(taskidparameter);
            cmd.Parameters.Add(titleparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task title updated in database");
        }

        /// <summary>
        /// this method delete task from the table
        /// </summary>
        /// <param name="taskID">the task id that should be deleted</param>
        public void removeTask(int taskID)
        {
            connection.Open();
           
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM task WHERE taskID =@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            cmd.Parameters.Add(taskidparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task removed in database");
        }

        /// <summary>
        /// this method assign user to task
        /// </summary>
        /// <param name="taskID">the task id that should be assigned</param>
        /// <param name="email">the assignee</param>
        public void assignTask(int taskID,string email)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE task SET email=@emailVal WHERE taskID=@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            SQLiteParameter emailparameter = new SQLiteParameter(@"emailVal", email);
            cmd.Parameters.Add(taskidparameter);    
            cmd.Parameters.Add(emailparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("user assigned to the task in database");
        }

    }
}
