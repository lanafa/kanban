using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class Kanban
    {
        private static readonly Kanban instance = new Kanban();
        private TaskDAO taskDAO;
        private TaskInBoardDAO taskInBoardDAO;
        private BoardDAO boardDAO;
        private UserDAO userDAO;
        private SQLiteConnection connection;


        /// <summary>
        /// c# getters and setters
        /// </summary>
        public TaskDAO TaskDAO { get => taskDAO; set { taskDAO = value; } }
        public TaskInBoardDAO TaskInBoardDAO { get => taskInBoardDAO; set { taskInBoardDAO = value; } }
        public BoardDAO BoardDAO { get => boardDAO; set { boardDAO = value; } }
        public UserDAO UserDAO { get => userDAO; set { userDAO = value; } }
        public SQLiteConnection Connection { get => connection; set { connection = value; } }

        /// <summary>
        /// constructor for the database
        /// </summary>
        private Kanban()
        {
            string path= Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "kanban.db"));
            //path=Path.Combine(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(path)+"")+"")+"")+"")+"","Backend"),"kanban.db");
            //Console.WriteLine(path);
            string _connectionString = $"Data Source={path}; Version=3;";
            this.connection = new SQLiteConnection(_connectionString);
            this.taskDAO = new TaskDAO(connection);
            this.taskInBoardDAO = new TaskInBoardDAO(connection);
            this.boardDAO = new BoardDAO(connection);
            this.userDAO = new UserDAO(connection);
        }

        /// <summary>
        /// the singleton instance of the database
        /// </summary>
        /// <returns>the singleton instance of the database</returns>
        public static Kanban getInstance()
        {
            return instance;
        }

        /// <summary>
        /// this method closes the connection with the database
        /// </summary>
        public void close()
        {
            connection.Close();
        }

        /// <summary>
        /// this methodopen the connection with the database
        /// </summary>
        public void open()
        {
            connection.Open();
        }

        /// <summary>
        /// this method loads the boards from the data base
        /// </summary>
        /// <returns>linkedlist of boards DTOs</returns>
        public LinkedList<BoardDTO> loadBoardsDTO()
        {
            return boardDAO.loadBoards();
        }

        /// <summary>
        /// this method loads the tasks in the data base
        /// </summary>
        /// <returns>reader to read the data from the database</returns>
        public SQLiteDataReader loadTasksInBoards()
        {
            connection.Open();
            LinkedList<UserDTO> users = new LinkedList<UserDTO>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT board.email,board.boardName,task.title,task.description,task.duedate,task.columnID,task.taskID FROM board,task,taskInBoard WHERE taskInBoard.boardID=board.boardID AND taskInBoard.taskID=task.taskID AND board.email=task.email;", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// this method delete all the data from the database
        /// </summary>
        public void DeleteData()
        {
            connection.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM board", connection);
            adapter.DeleteCommand = cmd;
            adapter.DeleteCommand.ExecuteNonQuery();
            cmd.Dispose();
            
            SQLiteDataAdapter adapter1 = new SQLiteDataAdapter();
            SQLiteCommand cmd1 = new SQLiteCommand("DELETE FROM user", connection);
            adapter1.DeleteCommand = cmd1;
            adapter1.DeleteCommand.ExecuteNonQuery();
            cmd1.Dispose();
            
            SQLiteDataAdapter adapter2 = new SQLiteDataAdapter();
            SQLiteCommand cmd2 = new SQLiteCommand("DELETE FROM taskInBoard", connection);
            adapter2.DeleteCommand = cmd2;
            adapter2.DeleteCommand.ExecuteNonQuery();
            cmd2.Dispose();
            
            SQLiteDataAdapter adapter3 = new SQLiteDataAdapter();
            SQLiteCommand cmd3 = new SQLiteCommand("DELETE FROM task", connection);
            adapter3.DeleteCommand = cmd3;
            adapter3.DeleteCommand.ExecuteNonQuery();
            cmd3.Dispose();
            connection.Close();
        }
     
    } 
}
