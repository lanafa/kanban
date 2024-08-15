using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.business_layer;
using IntroSE.Kanban.Backend.businessLayer;
using log4net;
using log4net.Config;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class TaskService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static BoardController boardController = BoardController.getInstance();

        private DataBaseController dataBaseController = DataBaseController.getInstance();

        /// <summary>
        /// init task service
        /// </summary>
        public TaskService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// This method advances a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns> The string "{}", unless an error occurs</returns>
        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                string result = boardController.AdvanceTask(email, boardName, columnOrdinal, taskId);
                dataBaseController.advanceTask(taskId);
                log.Info("task advanced successfully");
                return "{}";
            }
            catch(Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
        }

        /// <summary>
        /// This method assigns a task to a user
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column number. The first column is 0, the number increases by 1 for each column</param>
        /// <param name="taskID">The task to be updated identified a task ID</param>        
        /// <param name="emailAssignee">Email of the asignee user</param>
        /// <returns>An empty response, unless an error occurs</returns>
        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            try
            {
                string result = boardController.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
                dataBaseController.assignTask(taskID, emailAssignee.ToLower());
                log.Info(result);
                return "{}";

            }
            catch (Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
                   
        }

        /// <summary>
        /// This method updates the due date of a task
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns> The string "{}", unless an error occurs</returns>
        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            try
            {
                string result = boardController.UpdateTaskDueDate(email, boardName, columnOrdinal, taskId, dueDate);
                dataBaseController.updateTaskDueDate(taskId, dueDate.ToString());
                log.Info(result);
                return "{}";
                
            }
            catch (Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
        }

        /// <summary>
        /// This method updates task title.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns> The string "{}", unless an error occurs</returns>
        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                string result = boardController.UpdateTaskTitle(email, boardName, columnOrdinal, taskId, title);
               dataBaseController.updateTaskTitle(taskId, title);   
                log.Info(result);
                return "{}";
                
            }
            catch (Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
        }

        /// <summary>
        /// This method updates the description of a task.
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>The string "{}", unless an error occurs</returns>
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                string result = boardController.UpdateTaskDescription(email, boardName, columnOrdinal, taskId, description);
                dataBaseController.updateTaskDescription(taskId, description);
                log.Info(result);
                return "{}";
            }
            catch (Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
        }


        /// <summary>
        /// This method adds a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns> An empty response, unless an error occurs</returns>
        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            try
            {
                string result = boardController.AddTask(email, boardName, title, description, dueDate);
                dataBaseController.addTask(boardController.GetTask(boardController.TaskIDCounter-1).TaskDTO, boardController.GetBoard(email.ToLower(), boardName).ID);
                log.Info(result);
                /*Response response=  new Response { ErrorMessage = null, ReturnValue = email };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);*/
                return "{}";

            }
            catch (Exception ex)
            {
                Response response = new Response {ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
           
        }
    }
}
