using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VivaTask.Application.Commands.Requests;
using VivaTask.Application.Commands.Responses;

namespace VivaTask.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly IMediator _mediator;
        public Controller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("firstquestion")]
        public async Task<Response> GetSecondLargestInteger(FirstQuestionRequest firstQuestionRequest)
        {
            return await _mediator.Send(firstQuestionRequest);
        }

        [HttpGet("countries")]
        public async Task<Response> GetCountriesAsync()
        {
            return await _mediator.Send(new CountriesRequest());
        }

    }
}
