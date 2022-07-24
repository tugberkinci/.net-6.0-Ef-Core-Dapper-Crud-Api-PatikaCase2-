using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatikaHomework2.Data.Model;
using PatikaHomework2.Dto.Dto;
using PatikaHomework2.Dto.Response;
using PatikaHomework2.Service.IServices;

namespace PatikaHomework2.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService,IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retuns data </response>
        [HttpGet]
        [ProducesResponseType(typeof(GenericResponse<IEnumerable<Country>>), StatusCodes.Status200OK)]
 
        public async Task<IActionResult> Get()
        {
            var department = await Task.Run(()=> _departmentService.GetAll().Result);
            GenericResponse<IEnumerable<Department>> response = new GenericResponse<IEnumerable<Department>>();
            response.Success = true;
            response.Message = null;
            response.Data = department;

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
            var department = await Task.Run(() => _departmentService.GetById(id));
            GenericResponse<Department> response = new GenericResponse<Department>();

            if (department == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = department;
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

        public async Task<IActionResult> Post(DepartmentDto model)
        {
            GenericResponse<Department> response = new GenericResponse<Department>();
            var entity = _mapper.Map<DepartmentDto, Department>(model);
            var result = await Task.Run(() => _departmentService.Add(entity));

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
       
        public async Task<IActionResult> Patch(int id,DepartmentDto model)
        {
            GenericResponse<Department> response = new GenericResponse<Department>();
            var department = await Task.Run(() => _departmentService.GetById(id));
            if (department == null)
            {
                response.Success = false;
                response.Message = "Does not exist. Please check id.";
                response.Data = null;
                return NotFound(response);
            }

            var entity = _mapper.Map<DepartmentDto, Department>(model);

            department.DeptName = !String.IsNullOrEmpty(entity.DeptName) ? entity.DeptName : department.DeptName;
            department.CountryId = entity.CountryId != 0 ? entity.CountryId : department.CountryId;
           

            var result = await Task.Run(() => _departmentService.Update(entity));
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
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(GenericResponse<Country>), StatusCodes.Status404NotFound)]
        [HttpPut]
   
        public async Task<IActionResult> Put(DepartmentDto model)
        {
            GenericResponse<Department> response = new GenericResponse<Department>();
            var entity = _mapper.Map<DepartmentDto, Department>(model);
            var result = await Task.Run(() => _departmentService.Add(entity));

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
            var department = await Task.Run(() => _departmentService.Delete(id));
            GenericResponse<String> response = new GenericResponse<String>();
            if (department == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = department;
            response.Data = null;
            return Ok(response);

         
        }

    }
}
