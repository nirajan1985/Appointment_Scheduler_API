using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSchedulerAPI.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryNo { get; set; } 
        public AppointmentCategory Category { get; set; }
        
        public string Title { get; set; }
        public DateTime AppointmentDate { get; set; }
       
        public bool Reminder { get; set; }  
    }
}
