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
    public class TaskInBoardDAO
    {

        private SQLiteConnection connection;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// constructor to the taskinboard table inthe database
        /// </summary>
        /// <param name="connection">the connection with the database</param>
        public TaskInBoardDAO(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// this task removes from the table
        /// </summary>
        /// <param name="taskID">the task id that should be removed</param>
        public void removeTask(int taskID)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM taskInBoard WHERE taskID =@taskIDVal", connection);
            SQLiteParameter taskidparameter = new SQLiteParameter(@"taskIDVal", taskID);
            cmd.Parameters.Add(taskidparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task removed from the board in the table taskInBoard ");
        }

        /// <summary>
        /// this method add new taskinboard DTO
        /// </summary>
        /// <param name="taskInBoardDTO">the new DTO</param>
        public void insert(TaskInBoardDTO taskInBoardDTO)
        {

            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO taskInBoard (taskID,boardID) VALUES (@TaskIDVal,@BoardIDVal);", connection);
            SQLiteParameter TaslIDParameter = new SQLiteParameter(@"TaskIDVal",taskInBoardDTO.TaskID);
            SQLiteParameter boardIDParameter = new SQLiteParameter(@"BoardIDVal", taskInBoardDTO.BoardID);
            cmd.Parameters.Add(TaslIDParameter);
            cmd.Parameters.Add(boardIDParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("task added to the board in the table taskInBoard ");
        }

        /// <summary>
        /// this method remove board from the table
        /// </summary>
        /// <param name="boardID">the id of the board thta sould be removed</param>
        public void removeBoard(int boardID)
        {
            connection.Open();
            SQLiteCommand cmd1 = new SQLiteCommand("DELETE FROM task WHERE taskID IN (SELECT taskID FROM taskInBoard WHERE taskInBoard.boardID=@boardIDVal)", connection);
            SQLiteParameter boardIDParameter1 = new SQLiteParameter(@"boardIDVal", boardID);
            cmd1.Parameters.Add(boardIDParameter1);
            cmd1.Prepare();
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            connection.Close();
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM taskInBoard WHERE taskInBoard.boardID=@boardIDVal", connection);
            SQLiteParameter boardIDParameter = new SQLiteParameter(@"boardIDVal", boardID);
            cmd.Parameters.Add (boardIDParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("board removed from  the table taskInBoard ");
        }

    }
}
