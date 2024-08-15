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
    public class Board
    {
        private LinkedList<Column> columns;
        private int Id;
        private string name;
        private LinkedList<string> joinedUsers;
        private BoardDTO boardDTO;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private enum column { backlog,inprogress,done}

        /// <summary>
        /// c# getters and setters
        /// </summary>
        public int ID { get => Id; set => Id = value; }
        public string Name { get => name; set => name = value; }
        public LinkedList<string> JoinedUsers { get => joinedUsers; set => joinedUsers = value; }
        public BoardDTO BoardDTO { get => boardDTO; set => boardDTO = value; }
     
        /// <summary>
        /// constructor for board
        /// </summary>
        /// <param name="name">board name</param>
        /// <param name="ID">board id</param>
        /// <param name="email">who own the board</param>
        public Board(string name,int ID,string email)
        {
            joinedUsers = new LinkedList<string>();
            columns = new LinkedList<Column>();
            columns.AddLast(new Column("backlog"));
            columns.AddLast(new Column("in progress"));
            columns.AddLast(new Column("done"));
            this.name = name;
            this.Id = ID;
            boardDTO = new BoardDTO(name, ID, email,"TRUE");
        }

     
        /// <summary>
        /// this method get user to join board
        /// </summary>
        /// <param name="emailToJion">the user that own the board</param>
        /// <returns>if the user joining the board</returns>
        public string JoinBoard(string emailToJion)
        {
            if (isJoined(emailToJion))
            {
                log.Debug("user already joined");
                throw new Exception("user already joined");
            }
            joinedUsers.AddLast(emailToJion);
            return "joined succesfully";
        }

        /// <summary>
        /// this method get user to leave the board
        /// </summary>
        /// <param name="emailToLeave">the user that own the board</param>
        /// <returns>the user left the board, unless an error occurs</returns>
        public string LeaveBoard(string emailToLeave)
        {
            if (!isJoined(emailToLeave))
            {
                log.Debug("user is not joining");
                throw new Exception("user is not joining");
            }
            joinedUsers.Remove(emailToLeave);
            return "left succesfully";
        }

        /// <summary>
        /// this method assign person to task in the board
        /// </summary>
        /// <param name="columnOrdinal">the coumn id for the task</param>
        /// <param name="taskID">the task id</param>
        /// <param name="emailAssignee">the user that will be assigned</param>
        /// <returns>if the method completed well or not</returns>
        /// <exception cref="Exception">if the column id is not valid</exception>
        public string AssignTask(int columnOrdinal, int taskID, string emailAssignee)
        {
            
            if(columnOrdinal<(int)column.backlog || columnOrdinal > (int)column.done)
            {
                log.Debug("invaled columnOrdinal");
                throw new Exception("invaled columnOrdinal");
            }
            return columns.ElementAt(columnOrdinal).AssignTask(taskID, emailAssignee);
        }

        public bool isJoined(string email)
        {
            return joinedUsers.Contains(email);
        }


        /// <summary>
        /// this mehod adds a task to the  column in the board
        /// </summary>
        /// /// <param name="ID">task id</param>
        /// <param name="title"> the task title</param>
        /// <param name="description"> the task description</param>
        /// <param name="dueDate"> the task due date</param>
        /// <returns>  if the task added succefully to the column</returns>
        public string AddTask(int ID,string title, string description, DateTime dueDate)
        {
            return columns.ElementAt((int)column.backlog).AddTask(ID,title, description, dueDate, "not assigned");
        }

        /// <summary>
        /// this method updates the  task description in the column
        /// </summary>
        /// <param name="columnOrdinal"> The column to be updated identified column  </param>
        /// <param name="taskId"> The task to be updated identified task ID</param>
        /// <param name="description"> the new description of the task </param>
        /// <returns> if the task updated succefully in the column</returns>
        public string UpdateTaskDescription(int columnOrdinal, int taskId, string description)
        {
            if (columnOrdinal > (int)column.done || columnOrdinal < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");

            }
            return columns.ElementAt(columnOrdinal).UpdateTaskDescription(taskId, description);
        }

        /// <summary>
        /// this method updates the  task due date in the column
        /// </summary>
        /// <param name="columnOrdinal"> The column to be updated identified column  </param>
        /// <param name="taskId"> The task to be updated identified task ID</param>
        /// <param name="dueDate"> the new description of the task </param>
        /// <returns> if the task updated succefully in the column</returns>
        public string UpdateTaskDueDate(int columnOrdinal, int taskId, DateTime dueDate)
        {
            if (columnOrdinal > (int)column.done || columnOrdinal < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");

            }
            return columns.ElementAt(columnOrdinal).UpdateTaskDueDate(taskId, dueDate);
        }

        /// <summary>
        /// this method updates the  task title in the column
        /// </summary>
        /// <param name="columnOrdinal"> The column to be updated identified column  </param>
        /// <param name="taskId"> The task to be updated identified task ID</param>
        /// <param name="title"> the new description of the task </param>
        /// <returns> if the task updated succefully in the column</returns>
        public string UpdateTaskTitle(int columnOrdinal, int taskId, string title)
        {
            if (columnOrdinal > (int)column.done || columnOrdinal < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");

            }
            return columns.ElementAt(columnOrdinal).UpdateTaskTitle( taskId, title);
        }

        /// <summary>
        /// this method returns if the column limited successfully
        /// </summary>
        /// <param name="limit"> the new limit of the number of the tasks in the column</param>
        /// <param name="id"> the column id</param>
        /// <returns>if the column limited successfully</returns>
        /// <exception cref="Exception"> if there is no column with this id</exception>
        public string limitColumns(int limit, int id)
        {
            if (id > (int)column.done || id < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");
            
            }
            columns.ElementAt(id).limitColumns(limit);
            return "limited the column successfully";
        }

        /// <summary>
        /// returns the column limit
        /// </summary>
        /// <param name="columnID"> the id of the column</param>
        /// <returns>the column limit</returns>
        public int getColumnLimit(int columnID)
        {
            if (columnID > (int)column.done || columnID < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");
            }
            return columns.ElementAt(columnID).Limit;
        }

        /// <summary>
        /// this method returns the column name 
        /// </summary>
        /// <param name="columnID"> the column id</param>
        /// <returns>returns the column name </returns>
        public string getColumnName(int columnID)
        {
            if(columnID > (int)column.done || columnID < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");
            }
            return columns.ElementAt(columnID).Name;
            
        }

        /// <summary>
        /// this method moves the task to the column that identefind by columnOrdinal+1
        /// </summary>
        /// <param name="columnOrdinal">The column by identified column ordinal </param>
        /// <param name="taskId">The task by identified taskid</param>
        /// <returns>return string if advancing the task ended successfully</returns>
        /// <exception cref="Exception"> if advancing task faild</exception>
        public string AdvanceTask(int columnOrdinal, int taskId)
        {
            if (columnOrdinal > (int)column.done || columnOrdinal < (int)column.backlog)
            {
                log.Debug("column id is not valid");
                throw new Exception("column id is not valid");
            }
            if (columnOrdinal == (int)column.done)
            {
                log.Debug("cant advance tasks that are done");
              throw new Exception("cant advance tasks that are done");
            }
            if (columns.ElementAt(columnOrdinal).GetTask(taskId) == null)
            {
                log.Debug("task is not exist");
                throw new Exception("task is not exist");
            }
            Task t = columns.ElementAt(columnOrdinal).GetTask(taskId);
            columns.ElementAt(columnOrdinal).removeTask(taskId);
            return columns.ElementAt(columnOrdinal + 1).AddTask(t.ID, t.Title, t.Description, t.DueDate,t.Person);
        }

        /// <summary>
        /// this method returns the column identified by columnOrdinal
        /// </summary>
        /// <param name="columnOrdinal">The column by identified column ordinal</param>
        /// <returns>return the column as a string</returns>
        public LinkedList<Task> GetColumn(int columnOrdinal)
        {
            if (columnOrdinal > (int)column.done || columnOrdinal < (int)column.backlog)
            {
                log.Debug("id dont match any column");
                throw new Exception("id dont match any column");
            }
            return columns.ElementAt(columnOrdinal).Tasks;
        }

        /// <summary>
        /// this mehod returns all the tasks in progress
        /// </summary>
        /// <returns>all the tasks in progress</returns>
        public LinkedList<Task> GetInProgressTasks()
        {
            return GetColumn((int)column.inprogress);
        }

        public LinkedList<Task> getTasks()
        {
            LinkedList<Task> tasks = new LinkedList<Task>();
            foreach(Task task in GetColumn((int)column.backlog))
            {
                tasks.AddLast(task);
            }
            foreach (Task task in GetColumn((int)column.inprogress))
            {
                tasks.AddLast(task);
            }
            foreach (Task task in GetColumn((int)column.done))
            {
                tasks.AddLast(task);
            }
            return tasks;
        }

    }
}
