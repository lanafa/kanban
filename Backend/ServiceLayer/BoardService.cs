
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
    public class BoardService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static BoardController boardController = BoardController.getInstance();
        private DataBaseController dataBaseController = DataBaseController.getInstance();

        /// <summary>
        /// init board service
        /// </summary>
        public BoardService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }


        /// <summary>
        /// This method adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns> The string "{}", unless an error occurs</returns>
        public string AddBoard(string email, string name)
        {
            try
            {
                string result = boardController.AddBoard(email, name);
                dataBaseController.addBoard(boardController.GetBoard(email.ToLower(), name).BoardDTO);

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
        /// this method get user to join board for another user
        /// </summary>
        /// <param name="emailToJion">the user that own the board</param>
        /// <param name="boardID">the board that wnated to be joined</param>
        /// <returns>the string "{}" , unless an error occurs</returns>
        public string JoinBoard(string emailToJion,int boardID)
        {
            try
            {
                string result = boardController.JoinBoard(emailToJion, boardID);
                dataBaseController.JoinBoard(boardController.GetBoard(boardID).Name, emailToJion.ToLower(), boardID);
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
        /// this method get user to leave board for another user
        /// </summary>
        /// <param name="emailToLeave">the user that own the board</param>
        /// <param name="boardID">the board that wnated to be left</param>
        /// <returns>the string "{}" , unless an error occurs</returns>
        public string LeaveBoard(string emailToLeave, int boardID)
        {
            try
            {
                string result = boardController.LeaveBoard(emailToLeave, boardID);
                 dataBaseController.leaveBoard(boardID, emailToLeave.ToLower());  
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
        /// the owner of a board transfer the board ownership to another user
        /// </summary>
        /// <param name="email">the owner of the board</param>
        /// <param name="transferTo">the user who will own the board</param>
        /// <param name="boardName">the board wich will be transfered</param>
        /// <returns>the string "{}" , unless an error occurs</returns>
        public string TransferBoard(string email,string transferTo,string boardName)
        {
            try
            {
                string result = boardController.TransferBoard(email.ToLower(), transferTo.ToLower(), boardName);
                
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
        /// This method removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns> The string "{}", unless an error occurs </returns>
        public string RemoveBoard(string email, string name)
        {
            try
            {
                int id = boardController.GetBoard(email, name).ID;
                string result = boardController.RemoveBoard(email.ToLower(), name);
                dataBaseController.removeBoard(id);
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
        /// the method returns BoardController
        /// </summary>
        /// <returns>BoardController  </returns>
        public BoardController GetBoardController()
        {
            return boardController;
        }
    }
}
