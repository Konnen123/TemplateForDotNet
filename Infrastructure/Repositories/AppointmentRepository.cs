using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result<Guid>> AddAsync(Appointment appointment)
        {
            try
            {
                await context.Appointments.AddAsync(appointment);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(appointment.Id);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task<Result<Unit>> DeleteAsync(Guid id)
        {
            try
            {

                var appointment = await context.Appointments.FindAsync(id);
                if (appointment == null)
                {
                    return Result<Unit>.Failure("Appointment not found.");
                }

                context.Appointments.Remove(appointment);
                await context.SaveChangesAsync();
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task<Result<IEnumerable<Appointment>>> GetAllAsync()
        {
            try
            {
                return Result<IEnumerable<Appointment>>.Success(await context.Appointments.ToListAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.ToString());
            }
        }

        public async Task<Result<Appointment>> GetByIdAsync(Guid id)
        {
            try
            {
                Appointment? appointment = await context.Appointments.FindAsync(id);

                return appointment == null
                    ? Result<Appointment>.Failure("Appointment not found.")
                    : Result<Appointment>.Success(appointment);
            }
            catch(Exception ex)
            {
                return Result<Appointment>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task<Result<Unit>> UpdateAsync(Appointment appointment)
        {
            try
            {
                context.Entry(appointment).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
            catch(Exception ex)
            {
                return Result<Unit>.Failure(ex.InnerException!.ToString());
            }

        }
    }
}
