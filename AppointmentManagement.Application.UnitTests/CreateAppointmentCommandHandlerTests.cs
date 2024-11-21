using Application.Use_Cases.Commands;
using Application.Use_Cases.ComandHandlers;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace AppointmentManagement.Application.UnitTests
{
    public class CreateAppointmentCommandHandlerTests
    {
        private readonly IAppointmentRepository repository;
        private readonly IMapper mapper;
        private readonly CreateAppointmentCommandHandler handler;

        public CreateAppointmentCommandHandlerTests()
        {
            repository = Substitute.For<IAppointmentRepository>();
            mapper = Substitute.For<IMapper>();
            handler = new CreateAppointmentCommandHandler(repository, mapper);
        }

        [Fact]
        public async Task Given_ValidCreateAppointmentCommand_When_HandleIsCalled_Then_ShouldBeCreated()
        {
            // Arrange
            var command = new CreateAppointmentCommand
            {
                Type = "Author",
                Description = "1234567891234",
                StartTime = new TimeOnly(10, 30),
                EndTime = new TimeOnly(11, 30)
            };
            var appointment = new Appointment
            {
                Id = new Guid("0550c1dc-df3f-4dc2-9e29-4388582d2889"),
                Type = command.Type,
                Description = command.Description,
                StartTime = command.StartTime,
                EndTime = command.EndTime
            };
            mapper.Map<Appointment>(command).Returns(appointment);
            repository.AddAsync(appointment).Returns(Result<Guid>.Success(appointment.Id));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(appointment);
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(appointment.Id);
        }

        [Fact]
        public async Task Given_InvalidCreateAppointmentCommand_When_HandleIsCalled_Then_FailureResultShouldBeReturned()
        {
            // Arrange
            var command = new CreateAppointmentCommand
            {
                Type = "Author",
                Description = "1234567891234",
                StartTime = new TimeOnly(10, 30),
                EndTime = new TimeOnly(9, 30)
            };
            var appointment = new Appointment
            {
                Id = new Guid("0550c1dc-df3f-4dc2-9e29-4388582d2889"),
                Type = command.Type,
                Description = command.Description,
                StartTime = command.StartTime,
                EndTime = command.EndTime
            };
            mapper.Map<Appointment>(command).Returns(appointment);
            repository.AddAsync(appointment).Returns(Result<Guid>.Failure("Error adding appointment"));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            await repository.Received(1).AddAsync(appointment);
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Error adding appointment");
        }

    }
}
