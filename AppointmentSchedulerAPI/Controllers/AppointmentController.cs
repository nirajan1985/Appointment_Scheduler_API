using AppointmentSchedulerAPI.Data;
using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Models.Dto;
using AppointmentSchedulerAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AppointmentSchedulerAPI.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepository _dbAppointment;
        private readonly IMapper _mapper;
        protected APIResponse _response;
        public AppointmentController(IAppointmentRepository dbAppointment,IMapper mapper)
        {
            _dbAppointment= dbAppointment;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAppointments()
        {
            
            try
            {
                IEnumerable<Appointment> appointmentList = await _dbAppointment.GetAllAsync();
                _response.Result = _mapper.Map<List<AppointmentDTO>>(appointmentList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages=new List<string>() { ex.ToString()};
            }
            return _response;
            
        }

        [HttpGet("{id:int}", Name = "GetAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task< ActionResult<APIResponse>> GetAppointment(int id)
        {
            try 
            { 
            if (id == 0)
            {
                return BadRequest();
            }
            var appointment =await _dbAppointment.GetAsync(u => u.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }
            _response.Result = _mapper.Map<AppointmentDTO>(appointment);
            _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task< ActionResult<APIResponse>> CreateAppointment([FromBody] AppointmentCreateDTO createDTO)
        {

            try
            {
                if (await _dbAppointment.GetAsync(u => u.Title.ToLower() == createDTO.Title.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Appointment already exists !");
                    return BadRequest(ModelState);
                }


                if (createDTO == null)
                {
                    return BadRequest();
                }


                Appointment appointment = _mapper.Map<Appointment>(createDTO);


                await _dbAppointment.CreateAsync(appointment);
                _response.Result = _mapper.Map<AppointmentCreateDTO>(appointment);
                _response.StatusCode = HttpStatusCode.Created;
                _response.isSuccess = true;

                return CreatedAtRoute("GetAppointment", new { id = appointment.Id }, _response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpDelete("{id:int}", Name = "DeleteAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteAppointment(int id)
        {
            try
            { 
            if (id == 0)
            {
                return BadRequest();
            }
            var appointment = await _dbAppointment.GetAsync(u => u.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            await _dbAppointment.RemoveAsync(appointment);
            _response.StatusCode= HttpStatusCode.NoContent;
            _response.isSuccess=true;
            
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut("{id:int}", Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<APIResponse>> UpdateAppointment(int id,[FromBody]AppointmentUpdateDTO updateDTO)
        {
            try
            { 
            if(updateDTO==null || id!=updateDTO.Id)
            {
                return BadRequest();
            }

            Appointment appointment = _mapper.Map<Appointment>(updateDTO);
            
            await _dbAppointment.UpdateAsync(appointment);
            _response.StatusCode= HttpStatusCode.NoContent;
            _response.isSuccess=true;
            
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPatch("{id:int}",Name="UpdatePartialAppointment")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]


        public async Task<ActionResult<APIResponse>> UpdatePartialAppointment(int id,JsonPatchDocument<AppointmentUpdateDTO> patchDTO)
        {
            try
            { 
            if(patchDTO==null || id==0)
            {
                return BadRequest();
            }
            var appointment=await _dbAppointment.GetAsync(u=>u.Id==id,tracked:false);

            AppointmentUpdateDTO appointmentDTO=_mapper.Map<AppointmentUpdateDTO>(appointment);
           
            
            patchDTO.ApplyTo(appointmentDTO, ModelState);

            Appointment model = _mapper.Map<Appointment>(appointmentDTO);
            
            await _dbAppointment.UpdateAsync(model);
            _response.StatusCode= HttpStatusCode.NoContent;
            _response.isSuccess=true;
            

            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
