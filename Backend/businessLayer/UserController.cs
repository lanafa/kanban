using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using log4net;
using System.Reflection;
using IntroSE.Kanban.Backend.businessLayer;

namespace IntroSE.Kanban.Backend.business_layer
{
    public class UserController
    {
        private static readonly UserController instance = new UserController();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private LinkedList<User> users;

        public LinkedList<User> Users { get => users; set { users = value; } }

        /// <summary>
        /// initation the user controller
        /// </summary>
        private UserController()
        {
            users = new LinkedList<User>();
        }

        /// <summary>
        /// returns the instance of the singletone user controller 
        /// </summary>
        /// <returns>returns the instance</returns>
        public static UserController getInstance()
        {
            return instance;
        }

        /// <summary>
        /// This method add a new user to the usercontroller.
        /// </summary>
        /// <param name="email">The user email address, used as the username for logging the system.</param>
        /// <param name="password">The user password.</param>
        /// <returns> string about adding user situation</returns>
        /// <exception cref="Exception">if failed to add user</exception>
        public string addUser(string email, string password)
        {
            int minpasswordlength = 6;
            int maxpasswordlength = 20;
            if (email == null || email == "")
            {
                log.Debug("email is null");
                throw new Exception("email is null");
            }
            email = email.ToLower();
            if (password == null || password == "")
            {
                log.Debug("password is null");
                throw new Exception("password is null");
            }
            bool big = false;
            bool small = false;
            bool num = false;
            if (password.Length < minpasswordlength || password.Length > maxpasswordlength)
            {
                log.Debug("password length should be between 6 to 20 character");
                throw new Exception("password length should be between 6 to 20 character");
            }
            int fromasciibig = 65;
            int toasciibig = 90;
            int fromasciismall = 97;
            int toasciismall = 122;
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] >= fromasciibig && password[i] <= toasciibig) big = true;
                if (password[i] >= fromasciismall && password[i] <= toasciismall) small = true;
                string ifnum = "" + password[i]; int number;
                if (int.TryParse(ifnum, out number)) num = true;
            }
            if (!big || !small || !num)
            {
                log.Debug("password should contains numbers, big and small litters");
                throw new Exception("password should contains numbers, big and small litters");
            }

            if(!Regex.IsMatch(email,@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$") || !Regex.IsMatch(email, @"^\w+([.-]?\w+)@\w+([.-]?\w+)(.\w{2,3})+$")
                || !Regex.IsMatch(email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$") || !Regex.IsMatch(email, @"^[a-zA-Z0-9_!#$%&'*+/=?`{|}~^.-]+@[a-zA-Z0-9.-]+$"))
            {
                log.Debug("illegal email format");
                throw new Exception("illegal email format");
            }
            for (int i = 0; i < users.Count; i++)
            {
                if (users.ElementAt(i).Email == email)
                {
                    log.Debug("email exists");
                    throw new Exception("email exists");
                }
            }
            
            User user1 = new User(email, password);
            users.AddLast(user1);
            BoardController.getInstance().addUser(email);
            return "registred successfully";
        }

        /// <summary>
        /// this method returns a user
        /// </summary>
        /// <param name="email">the of the purchased user</param>
        /// <returns>returns User that is identified by this email</returns>
        public User getUser(string email)
        {
            if(email!=null) email = email.ToLower();
            for (int i = 0; i < users.Count; i++)
                if (users.ElementAt(i).Email == email) return users.ElementAt(i);
            return null;
        }


        /// <summary>
        /// This method logs out a logged in user. 
        /// </summary>
        /// <param name="email">The email of the user to log out</param>
        /// <returns> string if logged out successfuly</returns>
        /// <exception cref="Exception">if user is not exist</exception>
        public string logout(string email)
        {
            if(email == null || email=="")
            {
                log.Debug("email is null");
                throw new Exception("email is null");
            }
            email = email.ToLower();
            for (int i = 0; i < users.Count; i++)
                if (users.ElementAt(i).Email == email) return users.ElementAt(i).logout();
            log.Debug("user is not exist");
            throw new Exception("user is not exist");
        }

        /// <summary>
        ///  This method logs in an existing user.
        /// </summary>
        /// <param name="email">The email address of the user to login</param>
        /// <param name="password">The password of the user to login</param>
        /// <returns> string about logging in situation</returns>
        /// <exception cref="Exception">if failed to logg in the user</exception>
        public string login(string email, string password)
        {
            if(email==null || password == null)
            {
                log.Debug("email or password are null");
                throw new Exception("email or password are null");
            }
            email = email.ToLower();
            for (int i = 0; i < users.Count; i++)
            {
                if (users.ElementAt(i).Email == email && users.ElementAt(i).Password == password)
                    return getUser(email).logIn();
            }
            log.Debug("email or password are incorrect");
            throw new Exception("email or password are incorrect");
        }

        public void loadData()
        {
            users=DataBaseController.getInstance().loadUsers();
        }
    }
}
