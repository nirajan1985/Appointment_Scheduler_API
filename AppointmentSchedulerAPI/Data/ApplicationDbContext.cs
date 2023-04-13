using AppointmentSchedulerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSchedulerAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {

        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentCategory> AppointmentCategeroies { get; set; }

        
    }
}
