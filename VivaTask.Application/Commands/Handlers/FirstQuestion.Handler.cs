using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivaTask.Application.Commands.Requests;
using VivaTask.Application.Commands.Responses;
using VivaTask.Application.Extensions;
using VivaTask.Application.Services;

namespace VivaTask.Application.Commands.Handlers
{
    public class FirstQuestionHandler : IRequestHandler<FirstQuestionRequest, Response>
    {
        private readonly IFirstQuestionService _firstQuestionService;
        public FirstQuestionHandler(IFirstQuestionService firstQuestionService)
        {
            _firstQuestionService = firstQuestionService;
        }
        public async Task<Response> Handle(FirstQuestionRequest request, CancellationToken cancellationToken)
        {
            var validationStatus = request.RequestArray.ValidateList();
            if (validationStatus == Enums.ListValidationStatus.None) 
            {
                var secondLargestNumber=_firstQuestionService.GetSecondLargestNumber(request.RequestArray);
                return FirstQuestionResponse.CreateSuccessResponese(secondLargestNumber);
            }

            return FirstQuestionResponse.CreateErrorResponese(validationStatus.GetDescription());
        }
    }
}
