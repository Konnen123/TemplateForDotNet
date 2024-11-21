using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Commands
{
    public class UpdateAppointmentCommand : BaseAppointmentCommand, IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}
