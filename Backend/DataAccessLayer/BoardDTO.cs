using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardDTO
    {
        private string boardName;
        private int boardID;
        private string email;
        private string owner;

        /// <summary>
        /// c# getters and setters
        /// </summary>
        public string BoardName { get => boardName; set => boardName = value; }
        public int BoardID { get => boardID; set => boardID = value; }
        public string Email { get => email; set => email = value; }
        public string Owner { get => owner; set => owner = value; }

        /// <summary>
        /// board DTO constructor
        /// </summary>
        /// <param name="boardName">boards name</param>
        /// <param name="boardID">boards id</param>
        /// <param name="email">the owner or joiner of the board</param>
        /// <param name="owner">if owner or not</param>
        public BoardDTO(string boardName, int boardID, string email,string owner)
        {
            this.boardName = boardName;
            this.boardID = boardID;
            this.email = email;
            this.owner = owner;
        }

      
    }
}
