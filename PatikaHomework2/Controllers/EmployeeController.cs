using Microsoft.AspNetCore.Mvc;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Dto.Response;
using PatikaHomework2.Dto.Dto;
using PatikaHomework2.Service.IServices;
using AutoMapper;

namespace PatikaHomework2.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeService,IMapper mapper)
        {
            _employeService = employeService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Country>>), StatusCodes.Status200OK)]
 
        public async Task<IActionResult> GetAll()
        {
            var employee = await Task.Run(() => _employeService.GetAll());
            GenericResponse<IEnumerable<Employee>> response = new GenericResponse<IEnumerable<Employee>>();
            response.Success = true;
            response.Message = null;
            response.Data = employee;

            return Ok(response);

        }


        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
  
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await Task.Run(() => _employeService.GetById(id));
            GenericResponse<Employee> response = new GenericResponse<Employee>();

            if (employee == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = employee;
            return Ok(response);

        }


        /// <summary>
        /// post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        [HttpPost]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post(EmployeeDto model)
        {
            GenericResponse<Employee> response = new GenericResponse<Employee>();
            var entity = _mapper.Map<EmployeeDto, Employee>(model);
            var result = await Task.Run(() => _employeService.Add(entity));

            if (result == null)
            {
                response.Success = false;
                response.Message = "An error ocurred.";
                response.Data = null;
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = result;

            return Created("", response);

          
        }



        /// <summary>
        /// update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
 
        public async Task<IActionResult> Patch(int id,EmployeeDto model)
        {
            GenericResponse<Employee> response = new GenericResponse<Employee>();
            var employee = await Task.Run(() => _employeService.GetById(id));
            if (employee == null)
            {
                response.Success = false;
                response.Message = "Does not exist. Please check id.";
                response.Data = null;
                return NotFound(response);
            }

            var entity = _mapper.Map<EmployeeDto, Employee>(model);

            employee.Name = !String.IsNullOrEmpty(entity.Name) ? entity.Name : employee.Name;
            employee.DepartmentId = entity.DepartmentId != 0 ? entity.DepartmentId : employee.DepartmentId;


            var result = await Task.Run(() => _employeService.Update(entity));
            if (result == null)
            {
                response.Success = false;
                response.Message = "An error occured.";
                response.Data = null;
                return BadRequest(response);
            }

            response.Success = true;
            response.Message = null;
            response.Data = result;
            return Ok(response);
      
        }


        /// <summary>
        /// post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="201">Retuns data </response>
        /// <response code="404">Returns error</response>
        /// <response code="400">Returns error</response>

        [HttpPut]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Put(EmployeeDto model)
        {
            GenericResponse<Employee> response = new GenericResponse<Employee>();
            var entity = _mapper.Map<EmployeeDto, Employee>(model);
            var result = await Task.Run(() => _employeService.Add(entity));

            if (result == null)
            {
                response.Success = false;
                response.Message = "An error ocurred.";
                response.Data = null;
                return BadRequest(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = result;

            return Created("", response);
           
        }


        /// <summary>
        /// delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        /// <response code="404">Returns error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
   
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await Task.Run(() => _employeService.Delete(id));
            GenericResponse<String> response = new GenericResponse<String>();
            if (employee == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = employee;
            response.Data = null;
            return Ok(response);
       
          
        }
    }
}
