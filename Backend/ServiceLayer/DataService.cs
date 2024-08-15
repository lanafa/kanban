using IntroSE.Kanban.Backend.business_layer;
using IntroSE.Kanban.Backend.businessLayer;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class DataService
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static BoardController boardController = BoardController.getInstance();
        private static UserController userController = UserController.getInstance();
        private static DataBaseController dataBaseController = DataBaseController.getInstance();

        /// <summary>
        /// init task service
        /// </summary>
        public DataService()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        ///<summary>This method loads all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method. 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LoadData()
        {
            try
            {
                userController.loadData();
                boardController.loadData();
                log.Info("data has loaded");
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

        ///<summary>This method deletes all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        ///<returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string DeleteData()
        {
            try
            {
                dataBaseController.DeleteData();
                log.Info("data has deleted");
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
    }
}
