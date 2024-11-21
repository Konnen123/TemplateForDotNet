using Application.Use_Cases.Commands;
using Application.Use_Cases.ComandHandlers;
using Domain.Repositories;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Domain.Common;
using Domain.Entities;

namespace BookManagement.Application.UnitTests
{
    public class DeleteAppointmentCommandHandlerTests
    {
        private readonly IAppointmentRepository repository;
        private readonly DeleteAppointmentCommandHandler handler;

        public DeleteAppointmentCommandHandlerTests()
        {
            repository = Substitute.For<IAppointmentRepository>();
            handler = new DeleteAppointmentCommandHandler(repository);
        }

        [Fact]
        public async Task Given_ValidDeleteBookCommand_When_HandleIsCalled_Then_BookShouldBeDeleted()
        {
            // Arrange
            var appointmentId = new Guid("0550c1dc-df3f-4dc2-9e29-4388582d2888");
            var command = new DeleteAppointmentCommand(appointmentId);

            repository.DeleteAsync(appointmentId).Returns(Result<Unit>.Success(Unit.Value));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue(); 
            result.Value.Should().Be(Unit.Value);
        }
    }
}
