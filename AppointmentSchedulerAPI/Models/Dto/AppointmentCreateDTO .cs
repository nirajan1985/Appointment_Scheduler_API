using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerAPI.Models.Dto
{
    public class AppointmentCreateDTO
    {
        
       
        [Required]
        public string Title { get; set; }
        public DateTime AppointmentDate { get; set; }

        public bool Reminder { get; set; }
    }
}
