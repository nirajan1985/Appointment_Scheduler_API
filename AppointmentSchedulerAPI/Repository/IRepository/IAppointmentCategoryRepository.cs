using AppointmentSchedulerAPI.Models;

namespace AppointmentSchedulerAPI.Repository.IRepository
{
    public interface IAppointmentCategoryRepository:IRepository<AppointmentCategory>
    {
        Task <AppointmentCategory> UpdateAsync (AppointmentCategory entity);
    }
}
