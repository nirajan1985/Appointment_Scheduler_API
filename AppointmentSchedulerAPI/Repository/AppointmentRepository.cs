using AppointmentSchedulerAPI.Data;
using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AppointmentSchedulerAPI.Repository
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;
        public AppointmentRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }

        
        public async Task<Appointment> UpdateAsync(Appointment entity)
        {
           
            _db.Appointments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
