using System.ComponentModel.DataAnnotations;

namespace AppointmentSchedulerAPI.Models.Dto
{
    public class AppointmentCategoryDTO
    {
        [Required]
        public int AppointmentCategoryNo { get; set; }
        public string AppointmentCategoryName { get;set; }

    }
}
