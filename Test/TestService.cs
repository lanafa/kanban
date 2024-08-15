using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;


namespace IntroSE.Kanban.BackendTest
{
    public class TestService
    {
        static BoardService BoardService = new BoardService();
        static UserService UserService = new UserService();
        static ColumnSercive columnSercive = new ColumnSercive();
        static TaskService TaskService = new TaskService();  

        /// <summary>
        /// runs all the tests method for the system 
        /// </summary>
        public static void Test()
        {
            TestRegister();
            testLogIn();
            testAddBoard();
            testLimitColumn();
            testAddTask();
            testUpdateTaskDescription();
            testUpdateTaskDueDate();
            testUpdateTaskTitle();
            testAdvanceTask();
            testInProgressTasks();
            testRemoveBoard();
            testLogout();
        }

        /// <summary>
        /// test register a new user
        /// </summary>
        public static void TestRegister()
        {
           UserService.Register("lana@gmail.com", "Lana12345");
        }

        /// <summary>
        /// test log in an existing user
        /// </summary>
        public static void testLogIn()
        {
            UserService.Login("lana@gmail.com", "Lana12345");
        }

        /// <summary>
        /// test log out to logging in user
        /// </summary>
        public static void testLogout()
        {
            UserService.Logout("lana@gmail.com");
        }

        /// <summary>
        /// test add board to existing user
        /// </summary>
        public static void testAddBoard()
        {
            BoardService.AddBoard("lana@gmail.com", "boardname");
        }

        /// <summary>
        /// test remove board to existing board in existing user
        /// </summary>
        public static void testRemoveBoard()
        {
            BoardService.RemoveBoard("lana@gmail.com", "boardname");
        }

        /// <summary>
        /// test limitting a column
        /// </summary>
        public static void testLimitColumn()
        {
            columnSercive.LimitColumn("lana@gmail.com", "boardname", 0, 1);
        }

        /// <summary>
        /// test adding new task
        /// </summary>
        public static void testAddTask()
        {
            TaskService.AddTask("lana@gmail.com", "boardname", "title", "description", new DateTime(2025, 10, 10));
        }

        /// <summary>
        /// test updating duedate in a task
        /// </summary>
        public static void testUpdateTaskDueDate()
        {
            TaskService.UpdateTaskDueDate("lana@gmail.com", "boardname", 0, 0, new DateTime(2026, 12, 1));
        }

        /// <summary>
        /// test updating title in a task
        /// </summary>
        public static void testUpdateTaskTitle()
        {
            TaskService.UpdateTaskTitle("lana@gmail.com", "boardname", 0, 0, "title");
        }

        /// <summary>
        /// test updating description in a task
        /// </summary>
        public static void testUpdateTaskDescription()
        {
            TaskService.UpdateTaskDescription("lana@gmail.com", "boardname", 0, 0, "description");
        }

        /// <summary>
        /// test advancing a task
        /// </summary>
        public static void testAdvanceTask()
        {
            TaskService.AdvanceTask("lana@gmail.com", "boardname", 0, 0);
        }

        /// <summary>
        /// test gitting all in progress tasks to a user 
        /// </summary>
        public static void testInProgressTasks()
        {
            columnSercive.InProgressTasks("lana@gmail.com");
        }


    }
}
