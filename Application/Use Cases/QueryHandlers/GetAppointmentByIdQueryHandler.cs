using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, Result<AppointmentDto>>
    {
        private readonly IAppointmentRepository repository;
        private readonly IMapper mapper;
        
        public GetAppointmentByIdQueryHandler(IAppointmentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<AppointmentDto>> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointmentResult = await repository.GetByIdAsync(request.Id);
            if (!appointmentResult.IsSuccess)
                return Result<AppointmentDto>.Failure(appointmentResult.Error);

            var appointmentDto = mapper.Map<AppointmentDto>(appointmentResult.Value);
            return Result<AppointmentDto>.Success(appointmentDto);
        }
    }
}
