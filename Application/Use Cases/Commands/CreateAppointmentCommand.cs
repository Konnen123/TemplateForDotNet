using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateAppointmentCommand : BaseAppointmentCommand, IRequest<Result<Guid>>
    {

    }
}
