using AppointmentSchedulerAPI.Data;
using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Models.Dto;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AppointmentController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            IEnumerable<Appointment> appointmentList=await _db.Appointments.ToListAsync();
            return Ok(_mapper.Map<List<AppointmentDTO>>(appointmentList));
        }

        [HttpGet("{id:int}", Name = "GetAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult<AppointmentDTO>> GetAppointment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var appointment =await _db.Appointments.FirstOrDefaultAsync(u => u.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AppointmentDTO>(appointment));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task< ActionResult<AppointmentDTO>> CreateAppointment([FromBody] AppointmentCreateDTO createDTO)
        {
            if (await _db.Appointments.FirstOrDefaultAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Appointment already exists !");
                return BadRequest(ModelState);
            }


            if (createDTO == null)
            {
                return BadRequest();
            }
            

            Appointment model=_mapper.Map<Appointment>(createDTO);
           

            await _db.Appointments.AddAsync(model);
            await _db.SaveChangesAsync();

            
            return CreatedAtRoute("GetAppointment", new { id = model.Id }, model);
        }
        [HttpDelete("{id:int}", Name = "DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var appointment = await _db.Appointments.FirstOrDefaultAsync(u => u.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            _db.Appointments.Remove(appointment);
            await _db.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}", Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdateAppointment(int id,[FromBody]AppointmentUpdateDTO updateDTO)
        {
            if(updateDTO==null || id!=updateDTO.Id)
            {
                return BadRequest();
            }

            Appointment model = _mapper.Map<Appointment>(updateDTO);
            
            _db.Appointments.Update(model);
            await _db.SaveChangesAsync();
            
            return NoContent();
        }
        [HttpPatch("{id:int}",Name="UpdatePartialAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public async Task<IActionResult> UpdatePartialAppointment(int id,JsonPatchDocument<AppointmentUpdateDTO> patchDTO)
        {
            if(patchDTO==null || id==0)
            {
                return BadRequest();
            }
            var appointment=await _db.Appointments.AsNoTracking().FirstOrDefaultAsync(u=>u.Id==id);

            AppointmentUpdateDTO appointmentDTO=_mapper.Map<AppointmentUpdateDTO>(appointment);
           
            
            patchDTO.ApplyTo(appointmentDTO, ModelState);

            Appointment model = _mapper.Map<Appointment>(appointmentDTO);
            
            _db.Appointments.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
