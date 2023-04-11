using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSchedulerAPI.Models
{
    public class AppointmentCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AppointmentCategoryNo { get; set; }
        public string AppointmentCategoryName { get;set; }

    }
}
