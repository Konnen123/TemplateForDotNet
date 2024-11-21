using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentQuery, Result<List<AppointmentDto>>>
    {
        private readonly IAppointmentRepository repository;
        private readonly IMapper mapper;

        public GetAppointmentsQueryHandler(IAppointmentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<Result<List<AppointmentDto>>> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllAsync();
            if (!result.IsSuccess)
                return Result<List<AppointmentDto>>.Failure(result.Error);

            return Result<List<AppointmentDto>>.Success(mapper.Map<List<AppointmentDto>>(result.Value));
        }
    }
}
