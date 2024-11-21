using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Domain.Repositories
{
    public interface IAppointmentRepository
    {
        Task<Result<IEnumerable<Appointment>>> GetAllAsync();
        Task<Result<Appointment>> GetByIdAsync(Guid id);
        Task<Result<Guid>> AddAsync(Appointment book);
        Task<Result<Unit>> UpdateAsync(Appointment book);
        Task<Result<Unit>> DeleteAsync(Guid id);
    }
}
