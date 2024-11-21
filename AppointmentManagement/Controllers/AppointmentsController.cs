using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppointmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateAppointmentCommand command)
        {
            var result = await mediator.Send(command);
            
            return result.IsSuccess
                ? CreatedAtAction(nameof(GetAppointmentById), new { Id = result.Value }, result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var result = await mediator.Send(new GetAppointmentByIdQuery { Id = id });

            return result.IsSuccess
                ? Ok(result.Value)
                : NotFound(result.Error);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, UpdateAppointmentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The id should be identical with command.Id");
            }

            var result = await mediator.Send(command);

            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
            
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await mediator.Send(new DeleteAppointmentCommand(id));
            return result.IsSuccess
                ? NoContent()
                : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAppointmentQuery());
            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}
