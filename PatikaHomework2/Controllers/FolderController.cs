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
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;
 

        public FolderController(IFolderService folderService,IMapper mapper)
        {
            _folderService = folderService;
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
            var folder = await Task.Run(() => _folderService.GetAll());
            GenericResponse<IEnumerable<Folder>> response = new GenericResponse<IEnumerable<Folder>>();
            response.Success = true;
            response.Message = null;
            response.Data = folder;

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
            var folder = await Task.Run(() => _folderService.GetById(id));
            GenericResponse<Folder> response = new GenericResponse<Folder>();

            if (folder == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = null;
            response.Data = folder;
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
        public async Task<IActionResult> Post(FolderDto model)
        {
            GenericResponse<Folder> response = new GenericResponse<Folder>();
            var entity = _mapper.Map<FolderDto, Folder>(model);
            var result = await Task.Run(() => _folderService.Add(entity));

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

        public async Task<IActionResult> Patch(int id,FolderDto model)
        {
            GenericResponse<Folder> response = new GenericResponse<Folder>();
            var folder = await Task.Run(() => _folderService.GetById(id));
            if (folder == null)
            {
                response.Success = false;
                response.Message = "Does not exist. Please check id.";
                response.Data = null;
                return NotFound(response);
            }

            var entity = _mapper.Map<FolderDto, Folder>(model);

            folder.AccessType = !String.IsNullOrEmpty(entity.AccessType) ? entity.AccessType : folder.AccessType;
            folder.EmployeeId = entity.EmployeeId != 0 ? entity.EmployeeId : folder.EmployeeId;


            var result = await Task.Run(() => _folderService.Update(entity));
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

        public async Task<IActionResult> Put(FolderDto model)
        {
            GenericResponse<Folder> response = new GenericResponse<Folder>();
            var entity = _mapper.Map<FolderDto, Folder>(model);
            var result = await Task.Run(() => _folderService.Add(entity));

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
            var folder = await Task.Run(() => _folderService.Delete(id));
           
            GenericResponse<String> response = new GenericResponse<String>();
            if (folder == null)
            {
                response.Success = false;
                response.Message = "Does not exist.";
                response.Data = null; ;
                return NotFound(response);
            }
            response.Success = true;
            response.Message = folder;
            response.Data = null;
            return Ok(response);

        }
    }
}
