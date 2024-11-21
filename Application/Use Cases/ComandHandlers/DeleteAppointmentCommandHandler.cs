using Application.Use_Cases.Commands;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.ComandHandlers
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, Result<Unit>>
    {
        private readonly IAppointmentRepository repository;

        public DeleteAppointmentCommandHandler(IAppointmentRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Unit>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.Id);
            if (!result.IsSuccess)
            {
                return Result<Unit>.Failure(result.Error);
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
