using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BoardModel:NotifiableObject
    {
        private int boardID;
        private string boardName;
        public int ID
        {
            get => boardID;
            set
            {
                boardID = value;
                RaisePropertyChanged("ID");
            }
        }
        public string Name
        {
            get => boardName;
            set
            {
                boardName = value;
                RaisePropertyChanged("Name");
            }
        }

        public BoardModel(BackendController controller,int boardID, string boardName)
        {
            this.boardID = boardID;
            this.boardName = boardName;
        }
    }
}
