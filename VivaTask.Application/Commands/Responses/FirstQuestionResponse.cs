using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VivaTask.Application.Commands.Responses
{
    public class FirstQuestionResponse : Response
    {
        public FirstQuestionResponse(string? status,int secondLargestNumber) : base(status)
        {
            SecondLargestNumber = secondLargestNumber;
        }
        public FirstQuestionResponse(string? status, string? message)
            : base(status, message)
        {
        }

        public int? SecondLargestNumber { get; set; }
        public static FirstQuestionResponse CreateSuccessResponese(int secondLargestNumber) 
        {
            return new FirstQuestionResponse(nameof(Enums.ResponseStatus.Success),secondLargestNumber);
        }

        public static FirstQuestionResponse CreateErrorResponese(string message)
        {
            return new FirstQuestionResponse(nameof(Enums.ResponseStatus.Failed), message);
        }
    }
}
