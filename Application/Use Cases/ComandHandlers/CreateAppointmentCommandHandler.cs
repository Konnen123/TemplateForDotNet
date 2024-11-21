using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.ComandHandlers
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Result<Guid>>
    {
        private readonly IAppointmentRepository repository;
        private readonly IMapper mapper;

        public CreateAppointmentCommandHandler(IAppointmentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<Guid>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var book = mapper.Map<Appointment>(request);
           
            var result = await repository.AddAsync(book);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Value);
            }
            return Result<Guid>.Failure(result.Error);
        }
    }
}
