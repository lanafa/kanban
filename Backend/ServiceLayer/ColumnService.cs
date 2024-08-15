using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.business_layer;
using log4net;
using log4net.Config;
using Task = IntroSE.Kanban.Backend.business_layer.Task;

namespace IntroSE.Kanban.Backend.ServiceLayer

{
    public class ColumnSercive
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static BoardController boardController = BoardController.getInstance();

        /// <summary>
        /// init column service
        /// </summary>
        public ColumnSercive()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        /// <summary>
        /// This method limits the number of tasks in a specific column.
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>The string "{}", unless an error occurs</returns>
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                string result = boardController.LimitColumn(email, boardName, columnOrdinal, limit);
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

       
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns> Response with  a list of the in progress tasks, unless an error occurs</returns>
        public string InProgressTasks(string email)
        {
            try
            {
                LinkedList<Task> tasks = boardController.GetInProgressTasks(email);
                TaskJson[] taskJsons = new TaskJson[tasks.Count];
                for (int i = 0; i < tasks.Count; i++)
                {
                    TaskJson taskJson = new TaskJson();
                    taskJson.Id = tasks.ElementAt(i).ID;
                    taskJson.CreationTime = tasks.ElementAt(i).CreationTime;
                    taskJson.Title = tasks.ElementAt(i).Title;
                    taskJson.Description = tasks.ElementAt(i).Description;
                    taskJson.DueDate = tasks.ElementAt(i).DueDate;
                    taskJsons[i] = taskJson;
                }
                Response r = new Response { ErrorMessage = null, ReturnValue = taskJsons };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                string answer = System.Text.Json.JsonSerializer.Serialize(r, options);
                return answer;
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
        /// Json that present list of tasks
        /// </summary>
        private class TaskJson
        {
            public int Id { get; set; }
            public DateTime CreationTime { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DueDate { get; set; }
        }


    }
}
