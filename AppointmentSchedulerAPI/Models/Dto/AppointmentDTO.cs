using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerAPI.Models.Dto
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
       
        [Required]
        public int CategoryNo { get; set; }
        public string Title { get; set; }
        public DateTime AppointmentDate { get; set; }

        public bool Reminder { get; set; }
    }
}
