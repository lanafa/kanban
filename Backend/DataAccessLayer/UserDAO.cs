using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using log4net;
using System.Reflection;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDAO
    {

        private SQLiteConnection connection;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// constructor to the user table
        /// </summary>
        /// <param name="connection">the connection with the databse</param>
        public UserDAO(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        /// <summary> 
        /// this method adds new userDTO to the data
        /// </summary>
        /// <param name="userDTO"> new UserDTO to add</param>
        public void insert(UserDTO userDTO)
        {
            connection.Open();
            SQLiteCommand cmd= new SQLiteCommand($"INSERT INTO user (email,password) VALUES (@emailVal,@passwordVal);",connection);
            SQLiteParameter emailParameter=new SQLiteParameter(@"emailVal",userDTO.Email);
            SQLiteParameter passwprdParameter = new SQLiteParameter(@"passwordVal", userDTO.Password);
            cmd.Parameters.Add(emailParameter);
            cmd.Parameters.Add(passwprdParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("user added to the database");
        }

        /// <summary>
        /// This method loads all persisted data.
        /// </summary>
        /// <returns>  LinkedList with UsersDTO  </returns>
        public LinkedList<UserDTO> loadUsers()
        {
            connection.Open();
            LinkedList<UserDTO> users = new LinkedList<UserDTO>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM user", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //Console.WriteLine(reader["email"] + "  " + reader["password"]);
                users.AddLast(new UserDTO(reader["email"].ToString(), reader["password"].ToString()));
            }
            connection.Close();
            return users;
        }

    }
}
