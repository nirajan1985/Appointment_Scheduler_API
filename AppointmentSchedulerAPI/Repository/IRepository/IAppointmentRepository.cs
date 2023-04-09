using AppointmentSchedulerAPI.Models;
using System.Linq.Expressions;

namespace AppointmentSchedulerAPI.Repository.IRepository
{
    public interface IAppointmentRepository:IRepository<Appointment>
    {
       
        Task <Appointment> UpdateAsync(Appointment entity);
        
    }
}
