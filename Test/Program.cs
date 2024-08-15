using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace IntroSE.Kanban.BackendTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            /*
             
             
             * check if we close the connection after using it
             * test the code
             */
            //GradingService gradingService = new GradingService();
           // gradingService.DeleteData();
           /* gradingService.Register("mail@mail.com", "Password1");
            gradingService.AddBoard("mail@mail.com", "board1");
            gradingService.AddTask("mail@mail.com", "board1", "task1", "description1", new DateTime(2025, 10, 20));
            gradingService.AssignTask("mail@mail.com", "board1", 0, 0, "mail@mail.com");
            gradingService.AddTask("mail@mail.com", "board1", "task2", "description2", new DateTime(2025, 10, 21));
            gradingService.AssignTask("mail@mail.com", "board1", 0, 1, "mail@mail.com");
            gradingService.AdvanceTask("mail@mail.com", "board1", 0, 1);
            gradingService.AddTask("mail@mail.com", "board1", "task3", "description3", new DateTime(2025, 10, 22));
            gradingService.AssignTask("mail@mail.com", "board1", 0, 2, "mail@mail.com");
            gradingService.AdvanceTask("mail@mail.com", "board1", 0, 2);
            gradingService.AdvanceTask("mail@mail.com", "board1", 1, 2);
            gradingService.AddBoard("mail@mail.com", "board2");
            gradingService.AddBoard("mail@mail.com", "board6");
            gradingService.AddBoard("mail@mail.com", "board24");
            gradingService.AddBoard("mail@mail.com", "board3");*/
            /* gradingService.DeleteData();
             Console.WriteLine(gradingService.DeleteData());
             string email = "lana@gmail.com", board = "bbb";
             string invalid = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
             Console.WriteLine(gradingService.AddBoard(email, "one"));
            gradingService.Register("ahmad@gmail.com", "Aka123k123");
             gradingService.Register(email, "Aka123k123");
             gradingService.Login(email, "Aka123k123");
             Console.WriteLine(gradingService.Login("ahmad@gmail.com", "Aka123k123"));
             Console.WriteLine(gradingService.AddBoard("ahmad@gmail.com", "one"));
             Console.WriteLine(gradingService.AddTask(email, "one", "aaa", null, new DateTime(2025, 10, 12)));
             Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", new DateTime(2025, 10, 12)));
             Console.WriteLine(gradingService.AddTask(email, "one", null, "HELLOW WORLD", new DateTime(2025, 10, 12)));
             Console.WriteLine(gradingService.AddTask(email, "one", "new66", "HELLOW WORLD", new DateTime(2025, 10, 12)));
             Console.WriteLine(gradingService.AddTask("ahmad@gmail.com", "one", "bRAND22222", "HELLOW WORLD", new DateTime(2025, 10, 12)));
             Console.WriteLine(gradingService.AddTask("ahmad@gmail.com", "one", "new22222222", "HELLOW WORLD", new DateTime(2025, 10, 12)));
             Console.WriteLine(BoardService.boardController.GetTask(0).Person);
             gradingService.AssignTask("ahmad@gmail.com", "one", 0, 0, "ahmad@gmail.com");
             gradingService.AssignTask("ahmad@gmail.com", "one", 0, 1, "ahmad@gmail.com");
             Console.WriteLine(gradingService.AddBoard("ahmad@gmail.com", "one"));
             Console.WriteLine(gradingService.AddBoard("ahmad@gmail.com", "one"));
             Console.WriteLine(gradingService.AddBoard("ahmad@gmail.com", "on"));
             Console.WriteLine(gradingService.AddTask("ahmad@gmail.com", "on", "new22222222", "HELLOW WORLD", new DateTime(2025, 10, 12)));
             Console.WriteLine(gradingService.AdvanceTask(email, "on", 0, 0));
             Console.WriteLine(gradingService.AdvanceTask("ahmad@gmail.com", "one", 0, 1));
             Console.WriteLine(gradingService.GetColumn(email, "one", 0));
             Console.WriteLine(gradingService.GetColumn("ahmad@gmail.com", "one", 0));
             Console.WriteLine(gradingService.GetColumn("ahmad@gmail.com", "on", 0));
             Console.WriteLine(gradingService.AdvanceTask("ahmad@gmail.com", "on", 0, 0));
             Console.WriteLine(gradingService.AdvanceTask("ahmad@gmail.com", "on", 0, 5));
             Console.WriteLine(gradingService.InProgressTasks(email));
             Console.WriteLine(gradingService.InProgressTasks("ahmad@gmail.com"));
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0));
             Console.WriteLine("..........................................................*********************************************************");
             Console.WriteLine(gradingService.GetColumnName("ahmad@gmail.com", "one", 0));
             Console.WriteLine(gradingService.GetColumnName("ahmad@gmail.com", "one", 1));
             Console.WriteLine(gradingService.GetColumnName("ahmad@gmail.com", "one", 2));
             gradingService.Login(email, "Aka123k1");
             gradingService.Login(email, "Aka123k123");
             gradingService.Login(email + "ah", "Aka123k123");
             gradingService.Login(email, null);
             gradingService.Login(null, "Aka123k123");
             Console.WriteLine(gradingService.Logout(null));
             gradingService.Logout(email);
             gradingService.Logout(email);
             gradingService.Logout(email + "aa");
             Console.WriteLine(gradingService.Login(email, "Aka123k123"));
             Console.WriteLine(gradingService.AddBoard(email, "twoooooo"));
             Console.WriteLine(gradingService.AddTask("ahmad:", "twoooooo", "ahmad", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.AddTask(email, "twoooooo", "ahmad2", "HELLOW WORLD", DateTime.Now));
             gradingService.UpdateTaskDueDate(email, "twoooooo", 0, 0, new DateTime(2000, 10, 10));
             gradingService.UpdateTaskTitle(email, "twoooooo", 0, 0, "jkh");
             gradingService.UpdateTaskDescription(email, "twoooooo", 0, 0, null);
             Console.WriteLine(gradingService.LimitColumn(email, "twoooooo", 0, -5));
             Console.WriteLine(gradingService.LimitColumn(null, "twoooooo", 0, -5));
             Console.WriteLine(gradingService.LimitColumn(email, "twoooooo", 1, 0));
             Console.WriteLine(gradingService.AddTask(email, "twoooooo", "ahmad20", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.AdvanceTask(email, "twoooooo", 0, 0));
             Console.WriteLine(gradingService.AdvanceTask(email, "twoooooo", 5, 6));
             Console.WriteLine(gradingService.AdvanceTask(email, "twoooooo", 0, 1));
             Console.WriteLine(gradingService.AdvanceTask(email, "twoooooo", 5, 6));
             Console.WriteLine(gradingService.AddTask(email, "twoooooo", "ahmad", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.GetColumn(email, "twoooooo", 0));
             Console.WriteLine(gradingService.GetColumnName(email, null, 1));
             Console.WriteLine(gradingService.AddTask(email, "twoooooo", "ahmad3", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.GetColumn(email, "twoooooo", 2));
             Console.WriteLine("..........................................................");
             Console.WriteLine(gradingService.InProgressTasks(email));
             Console.WriteLine(gradingService.GetColumn(email, "one", 0));
             Console.WriteLine(gradingService.AdvanceTask(email, null, 0, 0));
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 1));
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 0));
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 1, 1));
             Console.WriteLine(gradingService.GetColumn("aaaa", "one", 2));
             Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 2, 0));
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 0)); // no such task in column 0
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));
             Console.WriteLine(gradingService.InProgressTasks(email));
             Console.WriteLine(gradingService.LimitColumn(email, board, 1, 5));
             Console.WriteLine(gradingService.LimitColumn(email, board, 1, 4));
             Console.WriteLine(gradingService.LimitColumn(email, board, 1, 10));
             Console.WriteLine(gradingService.GetColumnLimit(email, board, 1));
             Console.WriteLine(gradingService.GetColumnName(email, board, 5)); // INVALID NUMBER
             Console.WriteLine(gradingService.AddTask(email, "three", "new", "HELLOW WORLD", DateTime.Now)); // no such board three
             Console.WriteLine(gradingService.UpdateTaskDueDate(null, "one", 1, 0, DateTime.Now));// not good , changes to task that not in true coloumn number
             Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 9, 2, DateTime.Now));// not good , changes to invalid coloumn number
             Console.WriteLine(gradingService.RemoveBoard(email, "one"));
             Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.AddBoard(email, "two"));
             Console.WriteLine(gradingService.AddTask(email, "two", "new", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.UpdateTaskDueDate(email, "two", 0, 0, DateTime.Now));//
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 0, "new title"));//
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 1, "new title"));//no such task
             Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 0));
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, "new title"));//
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, invalid));//invalid title
             Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, "new descp"));
             Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, invalid));//
             Console.WriteLine(gradingService.LimitColumn(email, "two", 1, 1));
             Console.WriteLine(gradingService.AddTask(email, "two", "new task", "HELLOW WORLD", DateTime.Now));
             Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 1)); // the column in full
             Console.WriteLine(gradingService.RemoveBoard(email, "twoooooo"));

             //..................................................................................................................................
             //..................................................................................................................................
             //..................................................................................................................................
             //..................................................................................................................................
             //..................................................................................................................................

             var date = "5/1/2028 8:30:00 AM";
             var dateN = "5/1/2020 8:30:00 AM";
             DateTime DueDate = DateTime.Parse(date, System.Globalization.CultureInfo.InvariantCulture);
             DateTime DueDateN = DateTime.Parse(dateN, System.Globalization.CultureInfo.InvariantCulture);
             Console.WriteLine("++" + gradingService.Register(email, "Aka123k123"));                                        //create user Aka123k123
             Console.WriteLine("++" + gradingService.Login(email, "Aka123k123"));                                           //log in user Aka123k123
             Console.WriteLine("++" + gradingService.Logout(email));                                           //log in user Aka123k123
             Console.WriteLine("++" + gradingService.Login(email, "Aka1256563k123"));                                           //log in user Aka123k123
             Console.WriteLine("++" + gradingService.Login(email, "Aka123k123"));                                           //log in user Aka123k123

             // string invalid = "jgiosejiooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooojjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
             Console.WriteLine("++" + gradingService.AddBoard(email, "one"));                                               //add board one
             Console.WriteLine("++" + gradingService.AddBoard(email, ""));                                               //add board one
             Console.WriteLine("++" + gradingService.AddBoard(email, "     "));                                               //add board one

             Console.WriteLine("++" + gradingService.AddBoard(email, null));                                               //add board one

             Console.WriteLine("++" + gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DueDate));         // task number 0
             Console.WriteLine("++" + gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DueDateN));         //false - early
             Console.WriteLine("++" + gradingService.AddTask(email, "one", "bRAND", "HELLOW WORLD", DateTime.Now));         //false - early
             Console.WriteLine("++" + gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DueDate));           //add taskID 1 new
             Console.WriteLine("++" + gradingService.AddTask(email, "one", null, "HELLOW WORLD", DueDate));         //null - invalid
             Console.WriteLine("+------+" + gradingService.AddTask(email, "one", "", "HELLOW WORLD", DueDate));         //only spaces - invalid
             Console.WriteLine("......................" + gradingService.AddTask(email, "one", "liran", "   ", DueDate));         // task number 1
             Console.WriteLine("++" + gradingService.AddTask(email, "one", "liran", null, DueDate));         //task number 2
             Console.WriteLine(gradingService.GetColumn(email, "one", 0));
             Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 0, 0));                                      //advance taskID 0 to column 1
             Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 0, 1));                                      //advance taskID 1 to column 1
             Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 1, 0));                                      //advance taskID 0 to column 2
             Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 1, 1));                                      //advance taskID 1 to column 2
             Console.WriteLine("++" + gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DueDate));           //add taskID 3 new
             Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 2, 0));                                      //advance taskID 0 to column 3 - Error
             Console.WriteLine("++" + gradingService.AdvanceTask(email, "one", 0, 0));                                      // no such task in column 0
             Console.WriteLine("++" + gradingService.InProgressTasks(email));                                               //error

             Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 2));                                      //advance taskID 2 to column 1
             Console.WriteLine(gradingService.AdvanceTask(email, "one", 0, 3));                                      //advance taskID 2 to column 1
             Console.WriteLine("==....>>>>>>>>>" + gradingService.InProgressTasks(email));                                               //return taskID 2
             Console.WriteLine(gradingService.LimitColumn(email, board, 1, 5));                                      //limit column 1 to 5
             Console.WriteLine(gradingService.LimitColumn(email, board, 1, 4));                                      //limit column 1 to 4
             Console.WriteLine(gradingService.LimitColumn(email, board, 1, 10));                                     //limit column 1 to 10
             Console.WriteLine(gradingService.GetColumnLimit(email, board, 1));                                      //receive limit - 10
             Console.WriteLine(gradingService.GetColumnName(email, board, 5));                                       // INVALID NUMBER - Error
             Console.WriteLine(gradingService.AddTask(email, "three", "new", "HELLOW WORLD", DueDate));         // no such board three
             Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 1, 0, DateTime.Now));                  // not good , changes to task that not in true coloumn number
             Console.WriteLine(gradingService.UpdateTaskDueDate(email, "one", 9, 2, DueDate));
             Console.WriteLine("_" + gradingService.AddBoard(email, "two"));                                               //add Board 'two'
                                                                                                                           // not good , changes to invalid coloumn number
             Console.WriteLine(gradingService.RemoveBoard(email, "one"));                                            //delete Board one
             Console.WriteLine(gradingService.AddTask(email, "one", "new", "HELLOW WORLD", DueDate));           //Error - there is no Board with this name

             Console.WriteLine("_" + gradingService.AddBoard(email, "one"));                                               //add Board 'two'
             Console.WriteLine(gradingService.AddTask(email, "two", "new", "HELLOW WORLD", DueDate));           //add taskID 0 new
             Console.WriteLine(gradingService.UpdateTaskDueDate(email, "two", 0, 0, DueDate));                  //update dueDate
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 0, "new title"));                     //update taskID 0 title to 'new title'
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 0, 1, "new title"));                     //Error - no such task
             Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 0));                                      //advance taskID 0 to column 1
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, "new title"));                     //update taskID 0 title to 'new title'
             Console.WriteLine(gradingService.UpdateTaskTitle(email, "two", 1, 0, invalid));                         //Error - invalid title
             Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, "new descp"));               //update taskID 0 description
             Console.WriteLine(gradingService.UpdateTaskDescription(email, "two", 1, 0, invalid));                   //Error - invalid description
             Console.WriteLine(gradingService.LimitColumn(email, "two", 1, 1));                                      //limit column 1 to 1
             Console.WriteLine(gradingService.AddTask(email, "two", "new task", "HELLOW WORLD", DateTime.Now));      //add taskID 2 to Board 'two' with title 'new task'
             Console.WriteLine(gradingService.AdvanceTask(email, "two", 0, 1));
             Console.WriteLine(gradingService.GetUserBoards(email));
             Console.WriteLine(gradingService.GetUserBoards("ahmad@gmail.com"));
             gradingService.JoinBoard(email, 1);
             Console.WriteLine(gradingService.GetUserBoards(email));
             gradingService.DeleteData();*/
        }

    }
}