using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.business_layer;
using IntroSE.Kanban.Backend.DataAccessLayer;

namespace IntroSE.Kanban.Backend.businessLayer
{
    public class DataBaseController
    {
        private static readonly DataBaseController instance=new DataBaseController();
        private IntroSE.Kanban.Backend.DataAccessLayer.Kanban kanban;

        /// <summary>
        /// constructor for data base controller
        /// </summary>
        private DataBaseController() 
        {
            kanban = IntroSE.Kanban.Backend.DataAccessLayer.Kanban.getInstance();
        }

        /// <summary>
        /// this methof returns the singleton instance
        /// </summary>
        /// <returns>the singleton instance</returns>
        public static DataBaseController getInstance()
        {
            return instance;
        }

        /// <summary>
        /// this method adds user to database in user table
        /// </summary>
        /// <param name="userDTO">the user DTO</param>
        public void addUser(UserDTO userDTO)
        {
            kanban.UserDAO.insert(userDTO);
        }

        /// <summary>
        /// this method adds board to the data base in board table
        /// </summary>
        /// <param name="boardDTO">boards DTO</param>
        public void addBoard(BoardDTO boardDTO)
        {
            kanban.BoardDAO.insert(boardDTO);
        }

        /// <summary>
        /// this method adds task to the data base in task and taskinboard tables
        /// </summary>
        /// <param name="taskDTO">task DTO</param>
        /// <param name="boardID">to add DTO in taskinboard table</param>
        public void addTask(TaskDTO taskDTO,int boardID)
        {
            kanban.TaskDAO.insert(taskDTO);
            kanban.TaskInBoardDAO.insert(new TaskInBoardDTO(boardID, taskDTO.TaskID));
        }

        /// <summary>
        /// this method removes board from the data base
        /// </summary>
        /// <param name="boardID">the id of the board that should be deleted</param>
        public void removeBoard(int boardID)
        {
            kanban.BoardDAO.removeBoard(boardID);
            kanban.TaskInBoardDAO.removeBoard(boardID);
        }

        /// <summary>
        /// this method removes task from the data base
        /// </summary>
        /// <param name="taskID">the id of the task that should be deleted</param>
        public void removeTask(int taskID)
        {
            kanban.TaskInBoardDAO.removeTask(taskID);
            kanban.TaskDAO.removeTask(taskID);
        }

        /// <summary>
        /// this method advances the task column in the database
        /// </summary>
        /// <param name="taskID">the id of the task that should be advanced</param>
        public void advanceTask(int taskID)
        {
            kanban.TaskDAO.advanceTask(taskID);
        }

        /// <summary>
        /// this method updates tasks duedate in the data base
        /// </summary>
        /// <param name="taskID">the id of the task that should be updated</param>
        /// <param name="dueDate">new due date</param>
        public void updateTaskDueDate(int taskID,string dueDate)
        {
            kanban.TaskDAO.updateTaskDueDate(taskID,dueDate);
        }

        /// <summary>
        /// this method updates tasks description in the data base
        /// </summary>
        /// <param name="taskID">the id of the task that should be updated</param>
        /// <param name="description">the new description</param>
        public void updateTaskDescription(int taskID, string description)
        {
            kanban.TaskDAO.updateTaskDescription(taskID,description);
        }

        /// <summary>
        /// this method updates tasks title in the data base
        /// </summary>
        /// <param name="taskID">the id of the task that should be updated</param>
        /// <param name="title">the new title</param>
        public void updateTaskTitle(int taskID, string title)
        {
            kanban.TaskDAO.updateTaskTitle(taskID,title);
        }

        /// <summary>
        /// this method returns the max task id
        /// </summary>
        /// <returns>max task id</returns>
        public int getTaskIDCounter()
        {
            return kanban.TaskDAO.taskIDCounter();
        }

        /// <summary>
        /// this method returns the max board id
        /// </summary>
        /// <returns>max board id</returns>
        public int getBoardIDCounter()
        {
            return kanban.BoardDAO.boardIDCounter();
        }

        /// <summary>
        /// this method loads boards and tasks from the database
        /// </summary>
        public void loadBoards()
        {
            LinkedList<UserDTO> usersDTO = kanban.UserDAO.loadUsers();
            LinkedList<BoardDTO> boardsDTO = kanban.loadBoardsDTO();
            for (int i = 0; i < usersDTO.Count; i++)
            {
                
                BoardController.getInstance().addUser(usersDTO.ElementAt(i).Email);
                UserController.getInstance().login(usersDTO.ElementAt(i).Email, usersDTO.ElementAt(i).Password);
            }
            for(int i = 0; i < boardsDTO.Count; i++)
                if (boardsDTO.ElementAt(i).Owner == "TRUE")
                {
                    BoardController.getInstance().AddBoard(boardsDTO.ElementAt(i).Email, boardsDTO.ElementAt(i).BoardName);
                }
                         
            for(int i=0;i<boardsDTO.Count;i++)
                if (boardsDTO.ElementAt(i).Owner == "FALSE")
                {
                    BoardController.getInstance().JoinBoard(boardsDTO.ElementAt(i).Email, boardsDTO.ElementAt(i).BoardID);
                }
                    
                
                    
            SQLiteDataReader reader = kanban.loadTasksInBoards();
            while (reader.Read())
            {
                //Console.WriteLine(reader["taskID"]);
                BoardController.getInstance().AddTask(reader["email"].ToString(), reader["boardName"].ToString(), reader["title"].ToString(),
                    reader["description"].ToString(), 
                    new DateTime(int.Parse(reader["dueDate"].ToString().Split('-')[0])
                    ,int.Parse(reader["dueDate"].ToString().Split('-')[1])
                    ,int.Parse(reader["dueDate"].ToString().Split('-')[2].Split(' ')[0])));
                BoardController.getInstance().AssignTask(reader["email"].ToString(), reader["boardName"].ToString(),0,int.Parse(reader["taskID"].ToString()), reader["email"].ToString());
                if (int.Parse(reader["columnID"].ToString()) != 0)
                {
                    for(int i=1;i<= int.Parse(reader["columnID"].ToString()); i++)
                    {
                        BoardController.getInstance().AdvanceTask(reader["email"].ToString(), reader["boardName"].ToString(), i-1, int.Parse(reader["taskID"].ToString()));
                    }
                }

                //Console.WriteLine("done2");
            }
            kanban.Connection.Close();
            for (int i = 0; i < usersDTO.Count; i++)
            {
                UserController.getInstance().logout(usersDTO.ElementAt(i).Email);
            }
        }

        /// <summary>
        /// this method loads users from the databse
        /// </summary>
        /// <returns>linked list of users</returns>
        public LinkedList<User> loadUsers()
        {
            LinkedList<UserDTO> usersDTO = kanban.UserDAO.loadUsers();
            LinkedList<User> users=new LinkedList<User>();
            for(int i = 0; i < usersDTO.Count(); i++)
            {
                users.AddLast(new User(usersDTO.ElementAt(i).Email, usersDTO.ElementAt(i).Password));
            }
            return users;
        }
        public void JoinBoard(string boardname,string email,int boardId)
        {
            addBoard(new BoardDTO(boardname, boardId, email, "FALSE"));
        }
        public void leaveBoard(int id,string emailToLeave)
        {
            kanban.BoardDAO.leaveBoard(id,emailToLeave);
        }
        public void TransferBoard(int boardId,string email,string transferto)
        {
            kanban.BoardDAO.TransferBoard(boardId, transferto,email);
        }
        public void DeleteData()
        {
            kanban.DeleteData();
        }

        public void assignTask(int taskID,string email)
        {
            kanban.TaskDAO.assignTask(taskID, email);
        }
    }
}
