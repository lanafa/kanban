using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class TaskInBoardDTO
    {
        private int boardID;
        private int taskID;

        /// <summary>
        /// c# getters and setters
        /// </summary>
        public int BoardID { get => boardID; set => boardID = value; }
        public int TaskID { get => taskID; set => taskID = value; }

        /// <summary>
        /// constructor to task in board DTO
        /// </summary>
        /// <param name="boardID">the board id</param>
        /// <param name="taskID">the task id</param>
        public TaskInBoardDTO(int boardID, int taskID)
        {
            this.boardID = boardID;
            this.taskID = taskID;
        }

    }
}
