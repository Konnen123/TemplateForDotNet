using Application.DTOs;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace AppointmentManagement.Application.UnitTests
{
    public class GetAppointmentsQueryHandlerTests
    {
        private readonly IAppointmentRepository repository;
        private readonly IMapper mapper;
        public GetAppointmentsQueryHandlerTests()
        {
            repository = Substitute.For<IAppointmentRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_GetAppointmentsQueryHandler_When_HandleIsCalled_Then_AListOfAppointmentsShouldBeReturned()
        {
            // Arrange
            List<Appointment> appointments = GenerateAppointments();
            repository.GetAllAsync().Returns(Result<IEnumerable<Appointment>>.Success(appointments));

            var query = new GetAppointmentQuery();
            GenerateAppointmentsDto(appointments);
            // Act
           
            var handler = new GetAppointmentsQueryHandler(repository, mapper);
            var result = await handler.Handle(query, CancellationToken.None);
            // Assert
            
            result.Should().NotBeNull();
            Assert.Equal(2, result.Value.Count);
            Assert.Equal(appointments[0].Id, result.Value[0].Id);
        }

        private void GenerateAppointmentsDto(List<Appointment> appointments)
        {
            mapper.Map<List<AppointmentDto>>(appointments).Returns(new List<AppointmentDto>
            {
                new AppointmentDto
                {
                    Id = appointments[0].Id,
                    Type = appointments[0].Type,
                    Description = appointments[0].Description,
                    StartTime = appointments[0].StartTime,
                    EndTime = appointments[0].EndTime
                },
                new AppointmentDto
                {
                    Id = appointments[1].Id,
                    Type = appointments[1].Type,
                    Description = appointments[1].Description,
                    StartTime = appointments[1].StartTime,
                    EndTime = appointments[1].EndTime
                }
            });
        }

        private List<Appointment> GenerateAppointments()
        {
            return new List<Appointment>
            {
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    Type = "Appointment 1",
                    Description = "Author 1",
                    StartTime = new TimeOnly(10, 30),
                    EndTime = new TimeOnly(11, 30)
                },
                new Appointment
                {
                    Id = Guid.NewGuid(),
                    Type = "Appointment 2",
                    Description = "Author 2",
                    StartTime = new TimeOnly(10, 30),
                    EndTime = new TimeOnly(11, 30)
                }
            };
        }
    }
}