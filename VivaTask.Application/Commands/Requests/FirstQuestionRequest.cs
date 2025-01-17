using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Application.Commands.Requests
{
    public class FirstQuestionRequest : Request
    {
       public IEnumerable<int> RequestArray { get; set; }
    }
}
