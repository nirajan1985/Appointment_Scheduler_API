using AppointmentSchedulerAPI.Models;
using AppointmentSchedulerAPI.Models.Dto;
using AppointmentSchedulerAPI.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AppointmentSchedulerAPI.Controllers
{

    [ApiController]
    [Route("api/AppointmentCategory")]
    public class AppointmentCategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentCategoryRepository _dbAppointmentCategory;
        protected APIResponse _response;
        public AppointmentCategoryController(IMapper mapper, IAppointmentCategoryRepository dbAppointmentCategory)
        {
            _mapper = mapper;
            _dbAppointmentCategory = dbAppointmentCategory;
            this._response = new();   
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetAppointmentCategories()
        {
            try
            {
                IEnumerable<AppointmentCategory>appointmentCategoryList=await _dbAppointmentCategory.GetAllAsync();
                _response.Result=_mapper.Map<List <AppointmentCategoryDTO>>(appointmentCategoryList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpGet("{id:int}", Name = "GetAppointmentCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> GetAppointmentCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var appointmentCategory=await _dbAppointmentCategory.GetAsync(u=>u.AppointmentCategoryNo==id);
                _response.Result = _mapper.Map<AppointmentCategoryDTO>(appointmentCategory);
                _response.StatusCode = HttpStatusCode.OK;
                _response.isSuccess = true;
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

        public async Task<ActionResult<APIResponse>> CreateAppointmentCategory([FromBody] AppointmentCategoryCreateDTO createDTO)
        {
            try
            {
                if (await _dbAppointmentCategory.GetAsync(u => u.AppointmentCategoryName.ToLower() == createDTO.AppointmentCategoryName.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "AppointmentCategory already exists");
                    return BadRequest(ModelState);
                }

                AppointmentCategory appointmentCategory = _mapper.Map<AppointmentCategory>(createDTO);
                await _dbAppointmentCategory.CreateAsync(appointmentCategory);
                _response.Result = _mapper.Map<AppointmentCategoryCreateDTO>(appointmentCategory);
                _response.StatusCode = HttpStatusCode.Created;
                _response.isSuccess = true;

                return CreatedAtRoute("GetAppointmentCategory", new { id = appointmentCategory.AppointmentCategoryNo }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete("{id:int}",Name ="DeleteAppointmentCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIResponse>> DeleteAppointmentCategory(int id)
        {
            try
            {
                var appointmentCategory = await _dbAppointmentCategory.GetAsync(u => u.AppointmentCategoryNo == id);
                await _dbAppointmentCategory.RemoveAsync(appointmentCategory);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }
        [HttpPut ("{id:int}",Name ="UpdateAppointmentCategory")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>>UpdateAppointmentCategory(int id,[FromBody] AppointmentCategoryUpdateDTO updateDTO)
        {
            try
            {
                if (id != updateDTO.AppointmentCategoryNo)
                {
                    return BadRequest();
                }
                AppointmentCategory appointmentCategory = _mapper.Map<AppointmentCategory>(updateDTO);
                await _dbAppointmentCategory.UpdateAsync(appointmentCategory);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.isSuccess = true;
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
