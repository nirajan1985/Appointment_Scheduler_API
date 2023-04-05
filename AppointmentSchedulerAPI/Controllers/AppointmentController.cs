using AppointmentSchedulerAPI.Data;
using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSchedulerAPI.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
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

        [HttpGet("{id:int}", Name = "GetAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AppointmentDTO> GetAppointment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var appointment = _db.Appointments.FirstOrDefault(u => u.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(appointment);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<AppointmentDTO> CreateAppointment([FromBody] AppointmentCreateDTO appointmentDTO)
        {
            if (_db.Appointments.FirstOrDefault(u => u.Title.ToLower() == appointmentDTO.Title.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Appointment already exists !");
                return BadRequest(ModelState);
            }


            if (appointmentDTO == null)
            {
                return BadRequest();
            }
            

            Appointment model = new()
            {
                
                Title = appointmentDTO.Title,
                AppointmentDate = appointmentDTO.AppointmentDate,
                Reminder = appointmentDTO.Reminder,

            };

            _db.Appointments.Add(model);
            _db.SaveChanges();

            //return Ok(appointmentDTO);
            return CreatedAtRoute("GetAppointment", new { id = model.Id }, model);
        }
        [HttpDelete("{id:int}", Name = "DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAppointment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var appointment = _db.Appointments.FirstOrDefault(u => u.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appointment);
            _db.SaveChanges();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public IActionResult UpdateAppointment(int id,[FromBody]AppointmentUpdateDTO appointmentDTO)
        {
            if(appointmentDTO==null || id!=appointmentDTO.Id)
            {
                return BadRequest();
            }
           

            Appointment model= new Appointment()
            {
                Id = appointmentDTO.Id,
                Title= appointmentDTO.Title,
                AppointmentDate= appointmentDTO.AppointmentDate,
                Reminder= appointmentDTO.Reminder,
            };
            _db.Appointments.Update(model);
            _db.SaveChanges();
            
            return NoContent();
        }
        [HttpPatch("{id:int}",Name="UpdatePartialAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public IActionResult UpdatePartialAppointment(int id,JsonPatchDocument<AppointmentUpdateDTO> patchDTO)
        {
            if(patchDTO==null || id==0)
            {
                return BadRequest();
            }
            var appointment=_db.Appointments.AsNoTracking().FirstOrDefault(u=>u.Id==id);
           
            AppointmentUpdateDTO appointmentDTO = new()
            {
               
                Id=appointment.Id,
                Title= appointment.Title,
                AppointmentDate= appointment.AppointmentDate,
                Reminder= appointment.Reminder,
            };
            patchDTO.ApplyTo(appointmentDTO, ModelState);

            Appointment model = new()
            {
                Id=appointmentDTO.Id,
                Title= appointmentDTO.Title,
                AppointmentDate= appointmentDTO.AppointmentDate,
                Reminder= appointmentDTO.Reminder,
            };
            _db.Appointments.Update(model);
            _db.SaveChanges();

            return NoContent();
        }
    }
}
