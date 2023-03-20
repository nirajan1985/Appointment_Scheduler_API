using AppointmentSchedulerAPI.Data;
using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSchedulerAPI.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController:ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public AppointmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAppointments()
        {
            return Ok(_db.Appointments.ToList());
        }
        
        [HttpGet("{id:int}",Name ="GetAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AppointmentDTO> GetAppointment(int id)
        {
            if(id== 0) 
            {
                return BadRequest();
            }
            var appointment= _db.Appointments.FirstOrDefault(u=>u.Id==id);

            if(appointment== null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)] 
        public ActionResult<AppointmentDTO> CreateAppointment([FromBody]AppointmentDTO appointmentDTO)
        {
            if (_db.Appointments.FirstOrDefault(u => u.Title.ToLower() == appointmentDTO.Title.ToLower())!=null) 
            {
                ModelState.AddModelError("CustomError", "Appointment already exists !");
                return BadRequest(ModelState);
            }
           
            
            if(appointmentDTO==null)
            {
                return BadRequest();
            }
            if(appointmentDTO.Id>0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Appointment model = new()
            {
                Id = appointmentDTO.Id,
                Title = appointmentDTO.Title,
                AppointmentDate= appointmentDTO.AppointmentDate,
                Reminder=appointmentDTO.Reminder,

            };

            _db.Appointments.Add(model);
            _db.SaveChanges();
            
            //return Ok(appointmentDTO);
            return CreatedAtRoute("GetAppointment",new{id=appointmentDTO.Id },appointmentDTO);
        }
        [HttpDelete ("{id:int}",Name ="DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAppointment(int id)
        {
            if(id==0)
            {
                return BadRequest();
            }
            var appointment= _db.Appointments.FirstOrDefault(u=>u.Id==id);
            if (appointment == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appointment);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
