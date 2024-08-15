using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class UserDTO
    {
        private string email;
        private string password;

        /// <summary>
        /// c# getters and setters
        /// </summary>
        public string Email { get => email; set { email = value; } }
        public string Password { get => password; set { password = value; } }
        
        /// <summary>
        /// the user DTO
        /// </summary>
        /// <param name="email">user email</param>
        /// <param name="password">user password</param>
        public UserDTO(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

    }
}
