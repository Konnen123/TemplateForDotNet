using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public record DeleteAppointmentCommand(Guid Id) : IRequest<Result<Unit>>;
}
