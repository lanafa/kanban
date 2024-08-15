using IntroSE.Kanban.Backend.DataAccessLayer;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.business_layer
{
    public class User
    {
        private string email;
        private string password;
        private bool loggedIn;
        private UserDTO userDTO;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// c# getters and setters
        /// </summary>
        public bool LoggedIn { get => loggedIn; set { loggedIn = value; } }
        public string Password { get => password; set { password = value; } }
        public string Email { get => email; set { email = value; } }
        public UserDTO UserDTO { get => userDTO; set { userDTO = value; } }
        
        /// <summary>
        /// init new user
        /// </summary>
        /// <param name="email">the enail address of the user</param>
        /// <param name="password">the password of the user</param>
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
            userDTO = new UserDTO(email, password);
            loggedIn = false;
        }

        /// <summary>
        /// this method make the user logged in
        /// </summary>
        /// <returns>if logged in successfully</returns>
        /// <exception cref="Exception">if logging in failed</exception>
        public string logIn()
        {
            if (loggedIn)
            {
                log.Debug ("already logging in");
              throw new Exception( "already logging in");
            }
            loggedIn = true;
            return "logged in successfully";

        }

        /// <summary>
        /// this method logging out the user
        /// </summary>
        /// <returns>if logged out successfully</returns>
        /// <exception cref="Exception">if logging out failed</exception>
        public string logout()
        {
            if (!loggedIn) 
            {
                log.Debug("user not logged in");
                throw new Exception("user not logged in");
            }
            loggedIn = false;
            return "logged out successfully";
        }

    }
}
