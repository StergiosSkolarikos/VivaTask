using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Application.Commands.Responses
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }

        public Response(string? status)=>Status = status;
        public Response(string? status, string? message)=>(Status,Message) = (status, message);
    }
}
