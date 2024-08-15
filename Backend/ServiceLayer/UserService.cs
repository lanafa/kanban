using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.business_layer;
using log4net;
using System.Text.Json;
using System.Reflection;
using log4net.Config;
using System.IO;
using IntroSE.Kanban.Backend.businessLayer;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private UserController userController = UserController.getInstance();
        private DataBaseController dataBaseController = DataBaseController.getInstance(); 

        /// <summary>
        /// init user service
        /// </summary>
        public UserService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
        }

        /// <summary>
        /// This method registers a new user to the system.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns> The string "{}", unless an error occurs</returns>
        public string Register(string email, string password)
        {
            try
            {
                string result = userController.addUser(email, password);
                dataBaseController.addUser(userController.getUser(email.ToLower()).UserDTO);
                log.Info(result);
                Login(email, password);
                return "{}";
            }
            catch (Exception ex)
            {
                Response response= new Response {  ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);
            }
        }

        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns> Response with user email, unless an error occurs <returns>
        public string Login(string email, string password)
        {
            try
            {
                string result = userController.login(email, password);
                log.Info(result);
                Response response = new Response { ErrorMessage =null, ReturnValue = email };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response, options);

            }
            catch (Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response,options);
            }
        }

        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns> The string "{}", unless an error occurs </returns>
        public string Logout(string email)
        {
            try
            {
                string result = userController.logout(email);
                log.Info(result);
                return "{}";

            }
            catch (Exception ex)
            {
                Response response = new Response { ErrorMessage = ex.Message, ReturnValue = null };
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented=true;
                options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                return System.Text.Json.JsonSerializer.Serialize(response,options);
            }
        }

        public UserController GetUserController()
        {
            return userController;
        }
    }
}
