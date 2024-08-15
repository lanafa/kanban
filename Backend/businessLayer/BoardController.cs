using IntroSE.Kanban.Backend.businessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.business_layer
{
    public class BoardController
    {
        private static readonly BoardController instance = new BoardController();
        Dictionary<string, LinkedList<Board>> boards = new Dictionary<string, LinkedList<Board>>();
        private int taskIDCounter;
        private int boardIDCounter;

        public int BoardIDCounter { get => boardIDCounter; set { boardIDCounter = value; } }
        public int TaskIDCounter { get => taskIDCounter; set { taskIDCounter = value; } }
        public Dictionary<string, LinkedList<Board>> Boards { get => boards; set { boards = value; } }
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// init the bordcontroller
        /// </summary>
        private BoardController()
        {
            boards = new Dictionary<string, LinkedList<Board>>();
            taskIDCounter = 0;
            boardIDCounter = 0;

        }

        /// <summary>
        /// returns the instance of the singletone board controller 
        /// </summary>
        /// <returns>returns the instance</returns>
        public static BoardController getInstance()
        {
            return instance;
        }

        /// <summary>
        /// this method adds a new user with empty list of boards to the controller 
        /// </summary>
        /// <param name="email">the eamil adress of the new user</param>
        public void addUser(string email)
        {
            boards.Add(email, new LinkedList<Board>());
        }

        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            if (email == null || boardName == null || emailAssignee == null || email == "" || boardName == "" || emailAssignee == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            emailAssignee = emailAssignee.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (UserController.getInstance().getUser(emailAssignee) == null)
            {
                log.Debug("user to assigne is not exist");
                throw new Exception("user to assigne is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            if (columnOrdinal == 2)
            {
                log.Debug("cant assign done task");
                throw new Exception("cant assign done task");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName) || boards[email].ElementAt(i).isJoined(emailAssignee))
                {
                    string str = boards[email].ElementAt(i).AssignTask(columnOrdinal, taskID, emailAssignee);
                    return str;
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).Name == boardName)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email) || boards.ElementAt(i).Value.ElementAt(j).isJoined(emailAssignee))
                        {
                            string str = boards.ElementAt(i).Value.ElementAt(j).AssignTask(columnOrdinal, taskID, emailAssignee);
                            return str;
                        }
                    }
                }
            }
            log.Debug("board is not exist");
            throw new Exception("board is not exist");
        }

        /// <summary>
        /// this method adds a new board to the user that identified by email
        /// </summary>
        /// <param name="email">the eamil address of the user</param>
        /// <param name="name">the name of the new board</param>
        /// <returns>if adding a new board to the user ended successfully</returns>
        /// <exception cref="Exception">if adding a new board to the user failed</exception>
        public string AddBoard(string email, string name)
        {
            if (email == null || name == null || name == "" || email == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            bool checkBoardName = false;
            for (int i = 0; i < name.Length && !checkBoardName; i++)
            {
                if (name[i] != ' ') checkBoardName = true;
            }
            if (!checkBoardName)
            {
                log.Debug("board name is spaces");
                throw new Exception("board name is spaces");
            }
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(name))
                {
                    log.Debug("board already exist");
                    throw new Exception("board already exist");
                }
            }
            boards[email].AddLast(new Board(name, boardIDCounter, email));
            boardIDCounter++;
            return "board added successfully";
        }

        /// <summary>
        /// this method removes an existing board from a user that identified by email
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="name">the board to be deleted</param>
        /// <returns>if deleting thee board ended successfully</returns>
        /// <exception cref="Exception">if deleting thee board failed</exception>
        public string RemoveBoard(string email, string name)
        {
            if (email == null || name == null || email == "" || name == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(name))
                {

                    boards[email].Remove(boards[email].ElementAt(i));
                    return "board removed successfully";
                }
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this method returns if the column limited successfully
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="boardName">the name of the board that contains the column</param>
        /// <param name="columnOrdinal">the column id</param>
        /// <param name="limit">the new limit of the number of the tasks in the column</param>
        /// <returns>if the column limited successfully</returns>
        /// <exception cref="Exception">if limitting the column failed</exception>
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            if (email == null || boardName == null || email == "" || boardName == "")
            {
                log.Debug("email or boardName are null");
                throw new Exception("email or boardName are null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    return boards[email].ElementAt(i).limitColumns(limit, columnOrdinal);
                }
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this mehod adds a task to the  column in the board
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="boardName">the name of the board that contains the column</param>
        /// <param name="title"> the task title</param>
        /// <param name="description"> the task description</param>
        /// <param name="dueDate"> the task due date</param>
        /// <returns>  if the task added succefully to the column</returns>
        /// <exception cref="Exception">if adding the task failed</exception>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            if (email == null || boardName == null || title == null || email == "" || boardName == "" || title == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            bool checkTitle = false;
            for (int i = 0; i < title.Length && !checkTitle; i++)
            {
                if (title[i] != ' ') checkTitle = true;
            }
            if (!checkTitle)
            {
                log.Debug("title is spaces");
                throw new Exception("title is spaces");
            }
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logging in");
                throw new Exception("user is not logging in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    string str = boards[email].ElementAt(i).AddTask(taskIDCounter, title, description, dueDate);

                    taskIDCounter++;
                    return str;
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).Name == boardName)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                        {
                            string str = boards.ElementAt(i).Value.ElementAt(j).AddTask(taskIDCounter, title, description, dueDate);
                            taskIDCounter++;
                            return str;
                        }
                    }
                }
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this method updates the  task description
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="boardName">the name of the board that contains the column</param>
        /// <param name="columnOrdinal"> The column to be updated identified column  </param>
        /// <param name="taskId"> The task to be updated identified task ID</param>
        /// <param name="description"> the new description of the task </param>
        /// <returns> if the task updated succefully in the column</returns>
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            if (email == null || boardName == null || email == "" || boardName == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    string str = boards[email].ElementAt(i).UpdateTaskDescription(columnOrdinal, taskId, description);

                    return str;
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).Name == boardName)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                        {
                            if (GetTask(taskId) != null && GetTask(taskId).Person == email)
                            {
                                string str = GetTask(taskId).UpdateTaskDescription(description);

                                return str;
                            }
                        }
                    }
                }
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this method updates the  task due date in the column
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="boardName">the name of the board that contains the column</param>
        /// <param name="columnOrdinal"> The column to be updated identified column  </param>
        /// <param name="taskId"> The task to be updated identified task ID</param>
        /// <param name="dueDate"> the new description of the task </param>
        /// <returns> if the task updated succefully in the column</returns>
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            if (email == null || boardName == null || boardName == "" || email == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    string str = boards[email].ElementAt(i).UpdateTaskDueDate(columnOrdinal, taskId, dueDate);

                    return str;
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).Name == boardName)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                        {
                            if (GetTask(taskId) != null && GetTask(taskId).Person == email)
                            {
                                string str = GetTask(taskId).UpdateTaskDueDate(dueDate);

                                return str;
                            }
                        }
                    }
                }
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this method updates the  task title in the column
        /// </summary>
        /// <param name="email">the user's email</param>
        /// <param name="boardName">the name of the board that contains the column</param>
        /// <param name="columnOrdinal"> The column to be updated identified column  </param>
        /// <param name="taskId"> The task to be updated identified task ID</param>
        /// <param name="title"> the new description of the task </param>
        /// <returns> if the task updated succefully in the column</returns>
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            if (email == null || boardName == null || title == null || email == "" || boardName == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            bool checkTitle = false;
            for (int i = 0; i < title.Length && !checkTitle; i++)
            {
                if (title[i] != ' ') checkTitle = true;
            }
            if (!checkTitle)
            {
                log.Debug("title is spaces");
                throw new Exception("title is spaces");
            }
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    string str = boards[email].ElementAt(i).UpdateTaskTitle(columnOrdinal, taskId, title);

                    return str;
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).Name == boardName)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                        {
                            if (GetTask(taskId) != null && GetTask(taskId).Person == email)
                            {
                                string str = GetTask(taskId).UpdateTaskTitle(title);

                                return str;
                            }
                        }
                    }
                }
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this method moves the task to the column that identefind by columnOrdinal+1
        /// </summary>
        /// <param name="columnOrdinal">The column by identified column ordinal </param>
        /// <param name="taskId">The task by identified taskid</param>
        /// <returns>return string if advancing the task ended successfully</returns>
        /// <exception cref="Exception"> if advancing task faild</exception>
        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            if (email == null || boardName == null || email == "" || boardName == "")
            {
                log.Debug("one of the inputs is null");
                throw new Exception("one of the inputs is null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    if (GetTask(taskId) != null && GetTask(taskId).Person == email)
                    {
                        string str = boards[email].ElementAt(i).AdvanceTask(columnOrdinal, taskId);
                        return str;
                    }
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).Name == boardName)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                        {
                            if (GetTask(taskId) != null && GetTask(taskId).Person == email)
                            {
                                string str = boards.ElementAt(i).Value.ElementAt(j).AdvanceTask(columnOrdinal, taskId);
                                return str;
                            }
                        }
                    }
                }
            }
            if (GetTask(taskId) == null)
            {
                log.Debug("task is not exist");
                throw new Exception("task is not exist");
            }
            log.Debug("did not find the board");
            throw new Exception("did not find the board");
        }

        /// <summary>
        /// this mehod returns all the tasks in progress
        /// </summary>
        /// <param name="email">the email address of the user</param>
        /// <returns>all the tasks in progress</returns>
        public LinkedList<Task> GetInProgressTasks(string email)
        {
            if (email == null || email == "")
            {
                log.Debug("email is null");
                throw new Exception("email is null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logged in");
                throw new Exception("user is not logged in");
            }
            LinkedList<Task> InProgressTasks = new LinkedList<Task>();
            for (int i = 0; i < boards[email].Count; i++)
            {
                foreach (Task task in boards[email].ElementAt(i).GetInProgressTasks())
                {
                    InProgressTasks.AddLast(task);
                }
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                    {
                        foreach (Task task in boards.ElementAt(i).Value.ElementAt(j).GetInProgressTasks())
                        {
                            if (task.Person == email)
                            {
                                InProgressTasks.AddLast(task);
                            }
                        }
                    }
                }
            }
            return InProgressTasks;
        }

        public Task GetTask(int taskID)
        {
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    foreach (Task task in boards.ElementAt(i).Value.ElementAt(j).getTasks())
                    {
                        if (task.ID == taskID)
                        {
                            return task;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// this method returns the board idenified by boardname 
        /// </summary>
        /// <param name="email">user's email address</param>
        /// <param name="boardName">the name of the board to be returned</param>
        /// <returns>returns the board idenified by boardname </returns>
        public Board GetBoard(string email, string boardName)
        {
            if (email == null || boardName == null || email == "" || boardName == "")
            {
                log.Debug("email or boardName are null");
                throw new Exception("email or boardName are null");
            }
            email = email.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logging in");
                throw new Exception("user is not logging in");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    return boards[email].ElementAt(i);
                }
            }
            log.Debug("board is not exist");
            throw new Exception("board is not exist");
        }

        public Board GetBoard(int boardID)
        {
            if (boardID < 0)
            {
                log.Debug("invaled ID");
                throw new Exception("invaled ID");
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).ID == boardID)
                    {
                        return boards.ElementAt(i).Value.ElementAt(j);
                    }
                }
            }
            log.Debug("board is not exist");
            throw new Exception("board is not exist");
        }

        public LinkedList<Board> getUserBoards(string email)
        {
            LinkedList<Board> userBoards = new LinkedList<Board>();
            if (email != null)
            {
                email = email.ToLower();
                for (int i = 0; i < boards[email].Count; i++)
                {
                    userBoards.AddLast(boards[email].ElementAt(i));
                }
                for (int i = 0; i < boards.Count; i++)
                {
                    for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                    {
                        if (boards.ElementAt(i).Value.ElementAt(j).isJoined(email))
                        {
                            userBoards.AddLast(boards.ElementAt(i).Value.ElementAt(j));
                        }
                    }
                }
            }
            return userBoards;
        }


        /// <summary>
        /// this method get user to join board for another user
        /// </summary>
        /// <param name="email">the user who want to join</param>
        /// <param name="boardName">the board that wnated to be joined</param>
        /// <returns>the string "{}" , unless an error occurs</returns>
        public string JoinBoard(string email, int boardID)
        {
            if (email == null || email == "")
            {
                log.Debug("some inputs are null");
                throw new Exception("some inputs are null");
            }
            email = email.ToLower();
            if (boardID < 0)
            {
                log.Debug("baordID invaled");
                throw new Exception("boardID invaled");
            }
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logging in");
                throw new Exception("user is not logging in");
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).ID.Equals(boardID))
                    {

                        string str = boards.ElementAt(i).Value.ElementAt(j).JoinBoard(email);
                        return str;
                    }
                }
            }
            log.Debug("board is not exist");
            throw new Exception("board is not exist");
        }

        /// <summary>
        /// this method get user to leave board for another user
        /// </summary>
        /// <param name="email">the user who want to leave</param>
        /// <param name="emailToLeave">the user that own the board</param>
        /// <param name="boardName">the board that wnated to be left</param>
        /// <returns>the string "{}" , unless an error occurs</returns>
        public string LeaveBoard(string emailToLeave, int boardID)
        {
            if (emailToLeave == null || emailToLeave == "")
            {
                log.Debug("some inputs are null");
                throw new Exception("some inputs are null");
            }
            emailToLeave = emailToLeave.ToLower();
            if (boardID < 0)
            {
                log.Debug("baordID invaled");
                throw new Exception("boardID invaled");
            }
            if (UserController.getInstance().getUser(emailToLeave) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(emailToLeave).LoggedIn)
            {
                log.Debug("user is not logging in");
                throw new Exception("user is not logging in");
            }
            for (int i = 0; i < boards.Count; i++)
            {
                for (int j = 0; j < boards.ElementAt(i).Value.Count; j++)
                {
                    if (boards.ElementAt(i).Value.ElementAt(j).ID.Equals(boardID))
                    {
                        if (boards.ElementAt(i).Key == emailToLeave)
                        {
                            log.Debug("user cant leave his own board");
                            throw new Exception("user cant leave his own board");
                        }
                        string str = boards.ElementAt(i).Value.ElementAt(j).LeaveBoard(emailToLeave);
                        return str;
                    }
                }
            }
            log.Debug("board is not exist");
            throw new Exception("board is not exist");
        }

        /// <summary>
        /// the owner of a board transfer the board ownership to another user
        /// </summary>
        /// <param name="email">the owner of the board</param>
        /// <param name="transferTo">the user who will own the board</param>
        /// <param name="boardName">the board wich will be transfered</param>
        /// <returns>the string "{}" , unless an error occurs</returns>
        public string TransferBoard(string email, string transferTo, string boardName)
        {
            if (email == null || transferTo == null || boardName == null || email == "" || transferTo == "" || boardName == "")
            {
                log.Debug("some inputs are null");
                throw new Exception("some inputs are null");
            }
            email = email.ToLower();
            transferTo = transferTo.ToLower();
            if (UserController.getInstance().getUser(email) == null)
            {
                log.Debug("user is not exist");
                throw new Exception("user is not exist");
            }
            if (!UserController.getInstance().getUser(email).LoggedIn)
            {
                log.Debug("user is not logging in");
                throw new Exception("user is not logging in");
            }
            if (UserController.getInstance().getUser(transferTo) == null)
            {
                log.Debug("user to transfer to is not exist");
                throw new Exception("user to transfer to is not exist");
            }
            for (int i = 0; i < boards[email].Count; i++)
            {
                if (boards[email].ElementAt(i).Name.Equals(boardName))
                {
                    Board board = boards[email].ElementAt(i);
                    if (board.isJoined(transferTo))
                    {
                        board.LeaveBoard(transferTo);
                        board.JoinBoard(email);
                        boards[email].Remove(board);
                        boards[transferTo].AddLast(board);
                        DataBaseController.getInstance().TransferBoard(board.ID, email.ToLower(), transferTo);
                        return "ownership transferred sucessfully";
                    }
                    else
                    {
                        log.Debug("user to transfer doesnt join the board");
                        throw new Exception("user to transfer doesnt join the board");
                    }
                }
            }
            log.Debug("board is not exist");
            throw new Exception("board is not exist");
        }

        public void loadData()
        {
            DataBaseController.getInstance().loadBoards();
        }

    }
}

