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
    public class BoardDAO
    {
        private SQLiteConnection connection;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// constructor for the bord table
        /// </summary>
        /// <param name="connection">the connection with the data base</param>
        public BoardDAO(SQLiteConnection connection)
        {
            this.connection = connection;
        }
        /// <summary>
        /// this method takes the max board id 
        /// </summary>
        /// <returns>max board id</returns>
        public int boardIDCounter()
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT max(boardID) as maximum FROM board", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            connection.Close();
            return int.Parse(reader["maximum"].ToString());

        }

        /// <summary>
        /// insert  new bord DTO to the table
        /// </summary>
        /// <param name="boardDTO">the new board DTO</param>
        public void insert(BoardDTO boardDTO)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO board (boardID,boardName,email,owner) VALUES (@BoardIDVal,@BoardNameVal,@EmailVal,@OwnerVal)", connection);
            SQLiteParameter boardidparameter = new SQLiteParameter(@"BoardIDVal", boardDTO.BoardID);
            SQLiteParameter boardnameparameter = new SQLiteParameter(@"BoardNameVal", boardDTO.BoardName);
            SQLiteParameter emailparameter = new SQLiteParameter(@"EmailVal", boardDTO.Email);
            SQLiteParameter ownerparameter = new SQLiteParameter(@"OwnerVal", boardDTO.Owner);
            cmd.Parameters.Add(boardidparameter);
            cmd.Parameters.Add(boardnameparameter);
            cmd.Parameters.Add(emailparameter);
            cmd.Parameters.Add(ownerparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("board added to database");
        }

        /// <summary>
        /// this method removes board from the table
        /// </summary>
        /// <param name="boardID">the id of the board that should be removed</param>
        public void removeBoard(int boardID)
        {
            connection.Open();
           
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM board WHERE boardID=@boardIDVal", connection);
            SQLiteParameter boardidparameter = new SQLiteParameter(@"boardIDVal", boardID);
            cmd.Parameters.Add(boardidparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("board removed from database");
        }

        /// <summary>
        /// this method loads the boards from the table
        /// </summary>
        /// <returns>linked list of board DTOs</returns>
        public LinkedList<BoardDTO> loadBoards()
        {
            connection.Open();
            LinkedList<BoardDTO> boards = new LinkedList<BoardDTO>();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM board ORDER BY boardID", connection);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                boards.AddLast(new BoardDTO(reader["boardName"].ToString(), int.Parse(reader["boardID"].ToString()), reader["email"].ToString(),reader["owner"].ToString()));
            }
            connection.Close();
            return boards;
        }

        /// <summary>
        /// this method makethe user leave the board
        /// </summary>
        /// <param name="id">the id of the board</param>
        /// <param name="emailToLeave">the user that will leave the board</param>
        public void leaveBoard(int id,string emailToLeave)
        {
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM board WHERE boardID=@boardIDVal AND email = @emailToLeaveVal", connection);
            SQLiteParameter boardidparameter = new SQLiteParameter(@"boardIDVal", id);
            SQLiteParameter emailToLeaveParameter=new SQLiteParameter(@"emailToLeaveVal", emailToLeave);
            cmd.Parameters.Add(emailToLeaveParameter);
            cmd.Parameters.Add(boardidparameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            log.Info("user left the board in the database");
        }

        /// <summary>
        /// this method transfer ownership of the board from userto another
        /// </summary>
        /// <param name="boardId">the board id</param>
        /// <param name="transferto">the new owner</param>
        /// <param name="email">the old owner</param>
        public void TransferBoard(int boardId,string transferto,string email)
        {
            Console.WriteLine(boardId);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("UPDATE board SET owner=@falseVal WHERE boardID=@boardIDVal AND email=@emailVal", connection);
            SQLiteParameter boardidparameter = new SQLiteParameter(@"boardIDVal", boardId);
            SQLiteParameter emailParameter = new SQLiteParameter(@"emailVal", email);
            SQLiteParameter falseParameter = new SQLiteParameter(@"falseVal", "FALSE");
            cmd.Parameters.Add(emailParameter);
            cmd.Parameters.Add(boardidparameter);
            cmd.Parameters.Add(falseParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            SQLiteCommand cmd1 = new SQLiteCommand("UPDATE board SET owner=@trueVal WHERE boardID=@boardIDVal AND email=@transfertoVal", connection);
            SQLiteParameter transferparameter = new SQLiteParameter(@"transfertoVal", transferto);
            SQLiteParameter trueparameter = new SQLiteParameter(@"trueVal", "TRUE");
            cmd1.Parameters.Add(transferparameter);
            cmd1.Parameters.Add(boardidparameter);
            cmd1.Parameters.Add(trueparameter);
            cmd1.Prepare();
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            connection.Close();
            log.Info("ownership trasfered in the database");
        }

      
    }
}
