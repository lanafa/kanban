using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    /// <summary>
    /// build the json to be returned in service layer
    /// </summary>
    public class Response
    {
        public string ErrorMessage { get; set; }
        public object ReturnValue { get; set; }
    }
}
