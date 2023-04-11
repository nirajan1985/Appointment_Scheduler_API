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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Title = "Appointment with doctor",
                    AppointmentDate = new DateTime(2023, 03, 20, 10, 0, 0),

                    Reminder = true,
                },
                new Appointment
                {
                    Id = 2,
                    Title = "Appointment with professor",
                    AppointmentDate = new DateTime(2023, 03, 25, 11, 0, 0),

                    Reminder = false
                }

                ); ;


        }
    }
}
