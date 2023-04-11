using AppointmentSchedulerAPI.Data;
using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Repository.IRepository;

namespace AppointmentSchedulerAPI.Repository
{
    public class AppointmentCategoryRepository : Repository<AppointmentCategory>, IAppointmentCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentCategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<AppointmentCategory> UpdateAsync(AppointmentCategory entity)
        {
            _db.AppointmentCategeroies.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
