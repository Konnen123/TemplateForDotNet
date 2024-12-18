﻿using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetAppointmentByIdQuery : IRequest<Result<AppointmentDto>>
    {
        public Guid Id { get; set; }
    }
}
