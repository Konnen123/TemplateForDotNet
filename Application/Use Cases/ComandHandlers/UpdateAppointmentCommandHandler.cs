using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Use_Cases.ComandHandlers
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, Result<Unit>>
    {
        private readonly IAppointmentRepository repository;
        private readonly IMapper mapper;

        public UpdateAppointmentCommandHandler(IAppointmentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = mapper.Map<Appointment>(request);
            var result = await repository.UpdateAsync(appointment);

            if (!result.IsSuccess)
            {
                return Result<Unit>.Failure(result.Error);
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
