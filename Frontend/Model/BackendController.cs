using IntroSE.Kanban.Backend.business_layer;
using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Model
{
    public class BackendController
    {
        private GradingService Service { get; set; }

        public BackendController(GradingService service)
        {
            this.Service = service;
        }

        public BackendController()
        {
            this.Service = new GradingService();
            Service.LoadData();
        }

        public UserModel Login(string username, string password)
        {
            Response user = System.Text.Json.JsonSerializer.Deserialize<Response>(Service.Login(username, password));
            if (user.ErrorMessage != null)
            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(this, username);
        }

        public void Register(string username, string password)
        {
            Response res = System.Text.Json.JsonSerializer.Deserialize<Response>(Service.Register(username, password));
            if (res.ErrorMessage != null)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        public LinkedList<BoardModel> UserBoards(string email)
        {
            LinkedList<BoardModel> boardsModel = new LinkedList<BoardModel>();
            LinkedList<Board> boards = Service.serviceFactory.BoardService.GetBoardController().getUserBoards(email);
            foreach(Board board in boards)
            {
                boardsModel.AddLast(new BoardModel(this, board.ID, board.Name));
            }
            return boardsModel;
        }
        public LinkedList<TaskModel> setAllTasks(string email, int boardID)
        {
            LinkedList<TaskModel> taskModel = new LinkedList<TaskModel>();
            int backlog1 = 0;
            int Inprogress1 = 1;
            int done1 = 2;
            LinkedList<IntroSE.Kanban.Backend.business_layer.Task> backlog = Service.serviceFactory.BoardService.GetBoardController().GetBoard(boardID).GetColumn(backlog1);
            LinkedList<IntroSE.Kanban.Backend.business_layer.Task> inprogress = Service.serviceFactory.BoardService.GetBoardController().GetBoard(boardID).GetColumn(Inprogress1);
            LinkedList<IntroSE.Kanban.Backend.business_layer.Task> done = Service.serviceFactory.BoardService.GetBoardController().GetBoard(boardID).GetColumn(done1);
            foreach (IntroSE.Kanban.Backend.business_layer.Task task in backlog)
            {
                taskModel.AddLast(new TaskModel(this, task.ID, task.Title, task.Description, task.CreationTime, task.DueDate, 0));
            }
            foreach (IntroSE.Kanban.Backend.business_layer.Task task in inprogress)
            {
                taskModel.AddLast(new TaskModel(this, task.ID, task.Title, task.Description, task.CreationTime, task.DueDate, 1));
            }
            foreach (IntroSE.Kanban.Backend.business_layer.Task task in done)
            {
                taskModel.AddLast(new TaskModel(this, task.ID, task.Title, task.Description, task.CreationTime, task.DueDate, 2));
            }
            return taskModel;
        }
    }
}
